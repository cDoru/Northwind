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
		#region Constructor

		public CustomerEntityRepository( IDbConnectionFactory dbFactory )
			: base(dbFactory)
		{
		}

		#endregion

		#region Miembros de ICustomerEntityRepository

		public IOrderEntityRepository OrderRepository { get; set; }

		/// <summary>
		/// Recupera todos los <see cref="Order"/> para el <paramref name="id"/> indicado
		/// </summary>
		/// <param name="id">Identificador de <see cref="Customer"/></param>
		/// <returns>Lista con los <see cref="Order"/></returns>
		public List<OrderEntity> GetOrders( string id )
		{
			using ( var db = dbFactory.OpenDbConnection() )
			{
				return OrderRepository.GetFiltered(o => o.CustomerId == id).ToList();
			}			
		}

		/// <summary>
		/// Recupera todos los <see cref="Order"/> para el <paramref name="customer"/> indicado
		/// </summary>
		/// <param name="customer"><see cref="Customer"/> del que se recuperarán</param>
		/// <returns>Lista con los <see cref="Order"/></returns>
		public List<OrderEntity> GetOrders( CustomerEntity customer )
		{
			return GetOrders(customer.Id);
		}

		#endregion
	}
}
