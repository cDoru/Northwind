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
using Northwind.Common.Collections;
using Northwind.Data;
using Northwind.Data.Model;
using Northwind.Data.Repositories;
using Northwind.ServiceBase.Common;
using Northwind.ServiceBase.Caching;
using Northwind.ServiceBase.Meta;
using Northwind.ServiceBase.Query;

namespace Northwind.ServiceBase
{
	public abstract class ServiceBase<TEntity, TDto> : Service
		where TEntity : IEntity, new()
		where TDto : CommonDto, new()
	{		

		/// <summary>
		/// 
		/// </summary>
		protected readonly IRepository<TEntity> Repository;

		/// <summary>
		/// Constructor de la clase
		/// </summary>
		/// <param name="repository"></param>
		public ServiceBase( IRepository<TEntity> repository )
		{
			Verify.ArgumentNotNull(repository, "repository");

			Repository = repository;			
		}

		#region Métodos públicos

		#region GetCurrentRequestCacheKey
		/// <summary>
		/// Devuelve una clave de caché para la petición actual
		/// </summary>
		/// <returns>Cadena con la clave de caché para la petición actual</returns>
		public virtual string GetCurrentRequestCacheKey()
		{
			return new CacheKey(Request.AbsoluteUri, Request.Headers).ToString();
		}
		#endregion		

		#endregion

		#region Métodos protegidos

		#region GetSingle
		/// <summary>
		/// Obtención de un elemento a partir de su clave
		/// </summary>
		/// <typeparam name="TResponse">Tipo de la respuesta</typeparam>
		/// <param name="request">Petición</param>
		/// <returns>Elemento</returns>
		protected TResponse GetSingle<TResponse>( SingleRequest request ) where TResponse : SingleResponse<TDto>, new()
		{
			var result = Repository.Get(request.Id);

			if ( result == null )
			{
				throw HttpError.NotFound("Not found");
			}

			Response.AddHeaderLastModified(result.LastUpdated);
			Response.AddHeader(HttpHeaders.ETag, result.GetETagValue());

			return TypeExtensionHelper.CreateInstance<TResponse>(result.TranslateTo<TDto>());

		}
		#endregion

		#region GetCollection
		/// <summary>
		/// Obtención de una lista de elementos
		/// </summary>
		/// <typeparam name="TResponse">Tipo de la respuesta</typeparam>
		/// <param name="request">Parámetros de la petición</param>
		/// <returns>Lista de elementos</returns>
		protected TResponse GetCollection<TResponse>( CollectionRequest request ) where TResponse : CollectionResponse<TDto>, new()
		{
			var query = (QueryExpression<TEntity>)request.Query;
			var queryExpr = (query != null ? query.Select : null);
			var requestUrl = base.Request.GetPathUrl();

			FixOffsetAndLimit(request);

			var result = Repository
				.GetAll(queryExpr, request.Offset, request.Limit)
				.Select(e =>
				{
					var dto = e.TranslateTo<TDto>();
					dto.Link = new Uri(String.Format("{0}/{1}", requestUrl, dto.GetId<TDto>().ToString()));
					return dto;
				})
				.ToList();

			// Creación de la respuesta			
			var response = TypeExtensionHelper.CreateInstance<TResponse>(result);			
			response.Metadata = new Metadata(
				new Uri(base.RequestContext.AbsoluteUri),
				new StaticList<TDto>(result, request.Offset, request.Limit, Repository.Count())
			);

			return response;
				
		}
		#endregion

		#region ToOptimizedResultUsingCache
		/// <summary>
		/// Obtiene los resultados de una petición con comprobación de caché
		/// </summary>
		/// <param name="factoryFn">Elementos que se guardarán en caché si no existen</param>
		/// <returns>La respuesa optimizada</returns>
		protected object ToOptimizedResultUsingCache<TResponse>( Func<TResponse> factoryFn ) where TResponse : class
		{
			Verify.ArgumentNotNull(factoryFn, "factoryFn");

			return base.RequestContext.ToOptimizedResultUsingCache<TResponse>(base.Cache, GetCurrentRequestCacheKey(), factoryFn);
		}
		#endregion

		#region Put
		/// <summary>
		/// Actualización completa de un elemento.
		/// <para>
		/// Devuelve el status 200 si la creación ha sido correcta
		/// </para>
		/// </summary>
		/// <typeparam name="TResponse">Tipo de la respuesta</typeparam>
		/// <param name="request">Elemento a añadir</param>
		/// <returns>El nuevo objeto creado</returns>
		protected object Update( TDto dto )
		{
			Repository.Update(dto.TranslateTo<TEntity>());

			return new HttpResult(HttpStatusCode.OK);
		}
		#endregion

		#region Post
		/// <summary>
		/// Creación de un elemento
		/// <para>
		/// Devuelve el status 201 si la creación ha sido correcta
		/// </para>
		/// </summary>
		/// <param name="request">Elemento a crear</param>
		/// <returns>{TResponse}</returns>
		protected object Insert<TResponse>( TDto dto ) where TResponse : SingleResponse<TDto>, new()
		{
			var newEntity = Repository.Add(dto.TranslateTo<TEntity>());
			var response = TypeExtensionHelper.CreateInstance<TResponse>(newEntity.TranslateTo<TDto>());

			return new HttpResult(response, HttpStatusCode.Created);			
		}
		#endregion

		#region Delete
		/// <summary>
		/// Eliminación de un elemento
		/// </summary>
		/// <param name="request">Elemento a eliminar</param>
		/// <returns>Status 204 (sin contenido)</returns>
		protected object Remove( TDto request )
		{
			Repository.Delete(request.TranslateTo<TEntity>());

			return new HttpResult(HttpStatusCode.NoContent);
		}
		#endregion

		#region Patch
		/// <summary>
		/// Actualización parcial de un elemento
		/// </summary>
		/// <param name="request">Elemento a actualizar</param>
		/// <returns></returns>
		protected object UpdatePartial( TDto request )
		{
			var current = Repository.Get(request.GetId<TDto>());
			current.PopulateWithNonDefaultValues(request);

			Repository.Update(current.TranslateTo<TEntity>());

			return new HttpResult(HttpStatusCode.OK);

		}
		#endregion

		#endregion

		#region Métodos privados

		/// <summary>
		/// Establece los límites de recuperación de datos
		/// </summary>
		/// <param name="request"></param>
		private void FixOffsetAndLimit( ICollectionRequest request )
		{
			if ( request.Offset < 1 ) request.Offset = 1;
			if ( request.Limit < 1 ) request.Limit = 10;
		}		

		#endregion
	}
}
