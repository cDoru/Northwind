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
		public CustomersService( ICustomerEntityRepository repository )
			: base(repository)
		{ }

		/// <summary>
		/// Recuperación de un <see cref="Customer"/>
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public object Get( GetCustomer request )
		{
			return base.ToOptimizedResultUsingCache(
				() =>
				{
					return GetSingle<CustomerResponse>(request);
				});
		}

		/// <summary>
		/// Recuperación de la lista de <see cref="Customer"/>
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public object Get( GetCustomers request )
		{
			return base.ToOptimizedResultUsingCache(
				() => 
				{
					return GetCollection<CustomersCollectionResponse>(request);
				});
		}

		/// <summary>
		/// Recuperación de <see cref="Order"/> para un <see cref="Customer"/>
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public object Get( GetCustomerOrders request )
		{
			return base.ToOptimizedResultUsingCache(
				() =>
				{
					var orders = ((ICustomerEntityRepository)Repository)
						.GetOrders(request.Id)
						.Select(o => o.TranslateTo<Order>())
						.ToList();

					return new OrdersCollectionResponse(orders);
				});

		}

		/// <summary>
		/// Actualización de un <see cref="Customer"/>
		/// </summary>
		/// <param name="request"><see cref="Customer"/> a actualizar</param>
		/// <returns></returns>
		public object Put( Customer request )
		{
			return base.Update(request);
		}

		/// <summary>
		/// Creación de un <see cref="Customer"/>
		/// </summary>
		/// <param name="request"><see cref="Customer"/> a crear</param>
		/// <returns></returns>
		public object Post( Customer request )
		{
			return base.Insert<CustomerResponse>(request);
		}

		/// <summary>
		/// Eliminación de un <see cref="Customer"/>
		/// </summary>
		/// <param name="request"><see cref="Customer"/> a eliminar</param>
		/// <returns></returns>
		public object Delete( Customer request )
		{
			return base.Remove(request);
		}

		/// <summary>
		/// Actualización parcial de un <see cref="Customer"/>
		/// </summary>
		/// <param name="request"><see cref="Customer"/> a actualizar</param>
		/// <returns></returns>
		public object Patch( Customer request )
		{
			return base.UpdatePartial(request);
		}
	}
}
