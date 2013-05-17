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
using Northwind.ServiceModel.Contracts;
using Northwind.ServiceModel.Dto;
using Northwind.ServiceModel.Operations;

namespace Northwind.ServiceInterface.Services
{
	/// <summary>
	/// Servicio de clientes 
	/// </summary>	
	public class OrdersService : ServiceBase<OrderEntity, Order>
	{
		/// <summary>
		/// Recuperación de <see cref="Order"/> para un <see cref="Customer"/>
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public object Get( OrderDetails request )
		{
			var cacheKey = UrnId.Create<OrderDetails>(request.Id.ToString());

			return RequestContext.ToOptimizedResultUsingCache(base.Cache, cacheKey, () =>
			{
				var details = ((OrderEntityRepository)Repository).GetDetails(request.Id);

				var list = new List<OrderDetail>();
				var result = details.All(
					o =>
					{
						list.Add(o.TranslateTo<OrderDetail>());
						return true;
					});

				return new CollectionResponse<OrderDetail>(list);

			});
		}
	}
}
