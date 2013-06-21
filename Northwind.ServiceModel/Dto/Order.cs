﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Northwind.ServiceBase;
using Northwind.ServiceBase.Relations;

namespace Northwind.ServiceModel.Dto
{
	/// <summary>
	/// Clase que representa una entidad <see cref="Order"/>
	/// </summary>	
	public class Order : CommonDto
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
