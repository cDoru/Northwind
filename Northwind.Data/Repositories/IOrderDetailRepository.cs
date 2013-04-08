using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Northwind.Data.Model;

namespace Northwind.Data.Repositories
{
	/// <summary>
	/// Interfaz que representa un repositorio de entidades <see cref="OrderDetailEntity"/>
	/// </summary>
	public interface IOrderDetailEntityRepository : IRepository<OrderDetailEntity>
	{
	}
}
