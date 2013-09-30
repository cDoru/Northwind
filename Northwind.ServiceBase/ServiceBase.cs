#region Licencia
/*
   Copyright 2013 Juan Diego Martinez

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

     http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.

*/        
#endregion
          
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using ServiceStack.Common;
using ServiceStack.Common.Utils;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints;
using Northwind.Common;
using Northwind.Data;
using Northwind.Data.Model;
using Northwind.Data.Repositories;
using Northwind.ServiceBase.Common;
using Northwind.ServiceBase.Caching;
using Northwind.ServiceBase.Meta;
using Northwind.ServiceBase.Query;

namespace Northwind.ServiceBase
{
	/// <summary>
	/// Clase que representa un servicio web
	/// </summary>
	public abstract class ServiceBase<TEntity, TDto> : Service
		where TEntity : IEntity, new()
		where TDto : CommonDto, new()
	{
		#region Propiedades

		/// <summary>
		/// Repositorio
		/// </summary>
		public IRepository<TEntity> Repository { get; set; }			// Se establecerá mediante IoC

		#endregion		

		#region Métodos CRUD

		#region GET
		/// <summary>
		/// Recuperación de un elemento a partir de su clave primaria
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public virtual object Get( SingleRequest<TDto> request )
		{
			var cacheKey = IdUtils.CreateUrn<TDto>(request.Id);

			return RequestContext.ToOptimizedResultUsingCache(base.Cache, cacheKey, () =>
			{
				var result = Repository.Get(request.Id);

				if ( result == null )
				{
					throw HttpError.NotFound("Not found");
				}

				Response.AddHeaderLastModified(result.LastUpdated);
				Response.AddHeader(HttpHeaders.ETag, result.GetETagValue());				

				return new SingleResponse<TDto> { Result = result.TranslateTo<TDto>() };
			});
		}

		/// <summary>
		/// Recuperación de todos los elementos
		/// </summary>
		/// <param name="request">Petición</param>
		/// <returns>Collección de elementos</returns>
		public virtual object Get( CollectionRequest<TDto> request )
		{
			var cacheKey = new CacheKey(Request.AbsoluteUri, Request.Headers).ToString();

			return RequestContext.ToOptimizedResultUsingCache(base.Cache, cacheKey, 
				() =>
				{
					var query = (QueryExpression<TEntity>)request.Query;
					var queryExpr = (query != null ? query.Select : null);
					var requestUrl = Request.GetPathUrl();

					FixOffsetAndLimit(request);

					var result = Repository
						.GetAll(queryExpr, request.Offset, request.Limit)
						.Select(e => 
							{								
								var dto = e.TranslateTo<TDto>();
								//var restPath = EndpointHostConfig.Instance.Metadata.Routes.RestPaths.Single(r => r.RequestType == request.GetType());
								dto.Link = new Uri(String.Format("{0}/{1}", requestUrl, dto.GetId<TDto>().ToString()));
								return dto;
							})
						.ToList();

					// Creación de la respuesta					
					return new CollectionResponse<TDto>(result, request.Offset, request.Limit, Repository.Count());
				});			
		}

		#endregion

		#region POST

		/// <summary>
		/// Actualización
		/// <para>
		/// Devuelve Status 201 si la creación ha sido correcta
		/// </para>
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public virtual object Post( SingleRequest<TDto> request )
		{
			try
			{
				var newEntity = Repository.Add(request.TranslateTo<TEntity>());

				var result = new SingleResponse<TDto> { Result = newEntity.TranslateTo<TDto>() };

				return new HttpResult(result, HttpStatusCode.Created);
			}
			catch
			{
				throw;
			}
		}

		#endregion

		#region PUT

		/// <summary>
		/// Actualización
		/// <para>
		/// Devuelve Status 200 si la actualización ha sido correcta
		/// </para>
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public virtual object Put( SingleRequest<TDto> request )
		{
			try
			{
				Repository.Update(request.TranslateTo<TEntity>());

				return new HttpResult(HttpStatusCode.OK);
			}
			catch
			{
				throw;
			}
		}

		#endregion

		#region DELETE

		/// <summary>		
		/// Elimina una entidad
		/// <para>
		/// Devuelve Status 204 (sin contenido) si la eliminación ha sido correcta
		/// </para>		
		/// </summary>	
		/// <param name="request">Petición</param>
		/// <returns>Respuesta <seealso cref="CustomerResponse"/></returns>
		public object Delete( SingleResponse<TDto> request )
		{
			try
			{
				Repository.Delete(request.TranslateTo<TEntity>());

				return new HttpResult(HttpStatusCode.NoContent);
			}
			catch
			{
				throw;
			}

		}

		#endregion		

		#endregion		

		#region Métodos protegidos

		/// <summary>
		/// Establece los límites de recuperación de datos
		/// </summary>
		/// <param name="request"></param>
		protected void FixOffsetAndLimit( CollectionRequest<TDto> request )
		{
			if ( request.Offset < 1 ) request.Offset = 1;
			if ( request.Limit < 1 ) request.Limit = 10;
		}		

		#endregion

	}
}
