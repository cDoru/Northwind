using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.Text;
using Northwind.Data.Model;
using Northwind.Data.Repositories;
using Northwind.ServiceBase;
using Northwind.ServiceBase.Common;
using Northwind.ServiceBase.Caching;
using Northwind.ServiceModel.Contracts;
using Northwind.ServiceModel.Dto;
using Northwind.ServiceModel.Operations;

namespace Northwind.ServiceInterface.Services
{
	/// <summary>
	/// Servicio de <see cref="Customer"/> 
	/// </summary>	
	public class CustomersService : ServiceBase<CustomerEntity, Customer>
	{
		/// <summary>
		/// Recuperación de <see cref="Order"/> para un <see cref="Customer"/>
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public object Get( CustomerOrders request )
		{
			var cacheKey = new CacheKey(Request.AbsoluteUri, Request.Headers).ToString();

			return RequestContext.ToOptimizedResultUsingCache(base.Cache, cacheKey, () =>
			{
				var orders = ((CustomerEntityRepository)Repository)
					.GetOrders(request.Id)
					.Select(o => o.TranslateTo<Order>()).ToList();


				return new CollectionResponse<Order>(orders);
				
			});
		}		
	}
}
