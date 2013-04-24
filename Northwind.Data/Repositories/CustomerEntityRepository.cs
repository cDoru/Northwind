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
		
	}
}
