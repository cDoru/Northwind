﻿#region Licencia
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
using System.Text;
using ServiceStack.OrmLite;
using Northwind.Data.Model;

namespace Northwind.Data.Repositories
{
	/// <summary>
	/// Clase que representa un repositorio de entidades <see cref="OrderEntity"/>
	/// </summary>
	public class OrderEntityRepository : Repository<OrderEntity>, IOrderEntityRepository
	{
		public OrderEntityRepository( IDbConnectionFactory dbFactory )
			: base(dbFactory)
		{
		}

		#region Miembros de IOrderEntityRepository

		public IOrderDetailEntityRepository OrderDetailRepository { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="orderId"></param>
		/// <returns></returns>
		public List<OrderDetailEntity> GetDetails( long orderId )
		{
			using ( var db = dbFactory.OpenDbConnection() )
			{
				return OrderDetailRepository.GetFiltered(d => d.OrderId == orderId).ToList();
			}	
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="order"></param>
		/// <returns></returns>
		public List<OrderDetailEntity> GetDetails( OrderEntity order )
		{
			return GetDetails(order.Id);
		}

		#endregion
	}
}
