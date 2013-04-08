using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.OrmLite;
using Northwind.Data.Model;

namespace Northwind.Data.Repositories
{
	/// <summary>
	/// Clase que representa un repositorio de entidades <see cref="SupplierEntity"/>
	/// </summary>
	public class SupplierEntityRepository : Repository<SupplierEntity>, ISupplierEntityRepository
	{
		public SupplierEntityRepository( IDbConnectionFactory dbFactory )
			: base(dbFactory)
		{
		}
	}
}
