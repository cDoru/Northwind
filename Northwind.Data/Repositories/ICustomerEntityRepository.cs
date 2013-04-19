using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Northwind.Data.Model;

namespace Northwind.Data.Repositories
{
	/// <summary>
	/// Interfaz que representa un repositorio de entidades <see cref="CustomerEntity"/>
	/// </summary>
	public interface ICustomerEntityRepository : IRepository<CustomerEntity>
	{
		/// <summary>
		/// Obtiene un <see cref="Customer"/> a partir de su clave
		/// </summary>
		/// <param name="id">Valor de la clave</param>
		/// <returns>TEntity</returns>
		CustomerEntity Get( string id );
	}
}
