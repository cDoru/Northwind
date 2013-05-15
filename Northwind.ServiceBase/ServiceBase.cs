using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.Text;
using Northwind.Data.Model;
using Northwind.Data.Repositories;
using Northwind.ServiceBase.Common;

namespace Northwind.ServiceBase
{
	/// <summary>
	/// Clase que representa un servicio web
	/// </summary>
	public abstract class ServiceBase<TEntity, TDto> : Service
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
			return RequestContext.ToOptimizedResultUsingCache(base.Cache, "urn:{0}".Fmt(typeof(TDto).Name), () =>
				{
					var list = new List<TDto>();

					var result = Repository.GetAll().All(
						r =>
						{
							list.Add(r.TranslateTo<TDto>());
							return true;
						});

					return new CollectionResponse<TDto> { Result = list };
				});			
		}

		#endregion

		#region POST
		#endregion

		#region PUT
		#endregion

		#region DELETE
		#endregion

		#endregion

	}
}
