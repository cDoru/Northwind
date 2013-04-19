using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using Northwind.Data.Model;
using Northwind.Data.Repositories;

namespace Northwind.ServiceBase
{
	/// <summary>
	/// Clase que representa un servicio web
	/// </summary>
	public abstract class ServiceBase<TRepository, TEntity> : Service
		where TRepository : IRepository<TEntity>
		where TEntity : IEntity
	{
		#region Propiedades

		/// <summary>
		/// Repositorio
		/// </summary>
		public TRepository Repository { get; set; }			// Se establecerá mediante IoC	

		#endregion

	}
}
