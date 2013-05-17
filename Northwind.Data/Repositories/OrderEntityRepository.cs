using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.OrmLite;
using Northwind.Data.Model;

namespace Northwind.Data.Repositories
{
	/// <summary>
	/// Clase que representa un repositorio de entidades <see cref="OrderEntity"/>
	/// </summary>
	public class OrderEntityRepository : Repository<OrderEntity>, IOrderEntityRepository
	{
		public OrderEntityRepository( IDbConnectionFactory dbFactory )
			: base(dbFactory)
		{
		}

		#region Miembros de IOrderEntityRepository

		public IOrderDetailEntityRepository OrderDetailRepository { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="orderId"></param>
		/// <returns></returns>
		public List<OrderDetailEntity> GetDetails( long orderId )
		{
			using ( var db = dbFactory.OpenDbConnection() )
			{
				return OrderDetailRepository.GetFiltered(d => d.OrderId == orderId).ToList();
			}	
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="order"></param>
		/// <returns></returns>
		public List<OrderDetailEntity> GetDetails( OrderEntity order )
		{
			return GetDetails(order.Id);
		}

		#endregion
	}
}
