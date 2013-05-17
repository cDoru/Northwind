using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Northwind.Data.Model;

namespace Northwind.Data.Repositories
{
	/// <summary>
	/// Interfaz que representa un repositorio de entidades <see cref="OrderEntity"/>
	/// </summary>
	public interface IOrderEntityRepository : IRepository<OrderEntity>
	{
		IOrderDetailEntityRepository OrderDetailRepository { get; set; }

		List<OrderDetailEntity> GetDetails( long orderId );
		List<OrderDetailEntity> GetDetails( OrderEntity order );
	}
}
