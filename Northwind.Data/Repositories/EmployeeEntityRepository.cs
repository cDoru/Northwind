using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.OrmLite;
using Northwind.Data.Model;

namespace Northwind.Data.Repositories
{
	/// <summary>
	/// Clase que representa un repositorio de entidades <see cref="EmployeeEntity"/>
	/// </summary>
	public class EmployeeEntityRepository : Repository<EmployeeEntity>, IEmployeeEntityRepository
	{
		public EmployeeEntityRepository( IDbConnectionFactory dbFactory )
			: base(dbFactory)
		{
		}
	}
}
