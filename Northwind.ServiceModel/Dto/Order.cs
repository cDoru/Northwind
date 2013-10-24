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
using System.Text;
using ServiceStack.ServiceHost;
using Northwind.ServiceBase;
using Northwind.ServiceBase.Relations;

namespace Northwind.ServiceModel.Dto
{
	/// <summary>
	/// Clase que representa una entidad <see cref="Order"/>
	/// </summary>	
	[Route("/orders", "POST")]
	[Route("/orders/{Id}", "PUT DELETE")]
	public class Order : CommonDto, IReturnVoid
	{
		public long Id { get; set; }

		[Relation(RelationType.BelongsTo, typeof(Customer))]	
		public Customer Customer { get; set; }

		public long EmployeeId { get; set; }

		public string OrderDate { get; set; }

		public string RequiredDate { get; set; }

		public string ShippedDate { get; set; }

		public long? ShipVia { get; set; }

		public decimal Freight { get; set; }

		public string ShipName { get; set; }

		public string ShipAddress { get; set; }

		public string ShipCity { get; set; }

		public string ShipRegion { get; set; }

		public string ShipPostalCode { get; set; }

		public string ShipCountry { get; set; }

		[Relation(RelationType.HasMany, typeof(OrderDetail))]
		public List<OrderDetail> Detail { get; set; }
	}
}
