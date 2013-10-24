#region Licencia
/*
   Copyright 2013 Juan Diego Martinez

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

     http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.

*/        
#endregion
          
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
	/// Servicio de <see cref="Order"/>
	/// </summary>	
	public class OrdersService : ServiceBase<OrderEntity, Order>
	{
		public OrdersService( IOrderEntityRepository repository )
			: base(repository)
		{ }

		/// <summary>
		/// Recuperación de un <see cref="Order"/>
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public object Get( GetOrder request )
		{
			return base.ToOptimizedResultUsingCache(
				() =>
				{
					return GetSingle<OrderResponse>(request);
				});
		}

		/// <summary>
		/// Recuperación de la lista de <see cref="Orders"/>
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public object Get( GetOrders request )
		{
			return base.ToOptimizedResultUsingCache(
				() =>
				{
					return GetCollection<OrdersCollectionResponse>(request);
				});			
		}

		/// <summary>
		/// Recuperación de <see cref="OrderDetail"/> para un <see cref="Order"/>
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public object Get( GetOrderDetails request )
		{
			return base.ToOptimizedResultUsingCache(
				() =>
				{
					var details = ((IOrderEntityRepository)Repository)
						.GetDetails(request.Id)
						.Select(o => o.TranslateTo<OrderDetail>())
						.ToList();

					return new OrderDetailsCollectionResponse(details);
				});
		}

		/// <summary>
		/// Actualización de un <see cref="Order"/>
		/// </summary>
		/// <param name="request"><see cref="Order"/> a actualizar</param>
		/// <returns></returns>
		public object Put( Order request )
		{
			return base.Update(request);
		}

		/// <summary>
		/// Creación de un <see cref="Order"/>
		/// </summary>
		/// <param name="request"><see cref="Order"/> a crear</param>
		/// <returns></returns>
		public object Post( Order request )
		{
			return base.Insert<OrderResponse>(request);
		}

		/// <summary>
		/// Eliminación de un <see cref="Order"/>
		/// </summary>
		/// <param name="request"><see cref="Order"/> a eliminar</param>
		/// <returns></returns>
		public object Delete( Order request )
		{
			return base.Remove(request);
		}
	}
}
