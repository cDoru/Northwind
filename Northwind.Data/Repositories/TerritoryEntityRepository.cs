using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.OrmLite;
using Northwind.Data.Model;

namespace Northwind.Data.Repositories
{
	/// <summary>
	/// Clase que representa un repositorio de entidades <see cref="TerritoryEntity"/>
	/// </summary>
	public class TerritoryEntityRepository : Repository<TerritoryEntity>, ITerritoryEntityRepository
	{
		public TerritoryEntityRepository( IDbConnectionFactory dbFactory )
			: base(dbFactory)
		{
		}
	}
}
