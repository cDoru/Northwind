using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.OrmLite;
using Northwind.Data.Model;

namespace Northwind.Data.Repositories
{
	/// <summary>
	/// Clase que representa un repositorio de entidades <see cref="ProductEntity"/>
	/// </summary>
	public class ProductEntityRepository : Repository<ProductEntity>, IProductEntityRepository
	{
		public ProductEntityRepository( IDbConnectionFactory dbFactory )
			: base(dbFactory)
		{
		}
	}
}
