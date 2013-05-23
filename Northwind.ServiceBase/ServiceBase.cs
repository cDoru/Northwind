﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.Text;
using Northwind.Data.Model;
using Northwind.Data.Repositories;
using Northwind.ServiceBase.Common;
using Northwind.ServiceBase.Caching;

namespace Northwind.ServiceBase
{
	/// <summary>
	/// Clase que representa un servicio web
	/// </summary>
	public class ServiceBase<TEntity, TDto> : Service
		where TEntity : IEntity, new()
		where TDto : IDto, new()
	{
		#region Propiedades

		/// <summary>
		/// Repositorio
		/// </summary>
		public IRepository<TEntity> Repository { get; set; }			// Se establecerá mediante IoC			

		#endregion

		#region Métodos

		#region GET
		/// <summary>
		/// Recuperación de un elemento a partir de su clave primaria
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public virtual object Get( SingleRequest<TDto> request )
		{
			var cacheKey = UrnId.Create<TDto>(request.Id.ToString());			

			return RequestContext.ToOptimizedResultUsingCache(base.Cache, cacheKey, () =>
				{
					var result = Repository.Get(request.Id);					

					if ( result == null )
					{
						throw HttpError.NotFound("Not found");
					}

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
			var cacheKey = new CacheKey(this.Request.AbsoluteUri, this.Request.Headers).ToString();			

			return RequestContext.ToOptimizedResultUsingCache(base.Cache, cacheKey, () =>
				{
					var list = new List<TDto>();

					var result = Repository.GetAll(request.Offset, request.Limit).All(
						r =>
						{
							var dto = r.TranslateTo<TDto>();
							dto.Link = new Uri(String.Format("{0}/{1}", Request.GetPathUrl(), ServiceUtils.GetIdValue(dto)));
							list.Add(dto);
							return true;
						});

					return new CollectionResponse<TDto> { Result = list };
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

				return null;
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
		/// Devuelve Status 200 si la creación ha sido correcta
		/// </para>		
		/// </summary>	
		/// <param name="request">Petición</param>
		/// <returns>Respuesta <seealso cref="CustomerResponse"/></returns>
		public object Delete( SingleResponse<TDto> request )
		{
			try
			{
				Repository.Delete(request.TranslateTo<TEntity>());

				return null;
			}
			catch
			{
				throw;
			}

		}

		#endregion		

		#endregion

	}
}
