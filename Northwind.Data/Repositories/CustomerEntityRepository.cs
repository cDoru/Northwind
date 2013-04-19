using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.OrmLite;
using Northwind.Data.Model;

namespace Northwind.Data.Repositories
{
	/// <summary>
	/// Clase que representa un repositorio de entidades <see cref="CustomerEntity"/>
	/// </summary>
	public class CustomerEntityRepository : Repository<CustomerEntity>, ICustomerEntityRepository
	{
		public CustomerEntityRepository( IDbConnectionFactory dbFactory )
			: base(dbFactory)
		{
		}

		#region Miembros de ICustomerEntityRepository

		/// <summary>
		/// Obtiene un <see cref="Customer"/> a partir de su clave
		/// </summary>
		/// <param name="id">Valor de la clave</param>
		/// <returns>CustomerEntity</returns>
		public CustomerEntity Get( string id )
		{
			using ( var db = dbFactory.OpenDbConnection() )
			{
				return db.GetById<CustomerEntity>(id);
			}
		}

		#endregion
	}
}
