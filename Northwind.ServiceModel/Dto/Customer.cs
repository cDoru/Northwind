﻿using System.Collections.Generic;
using Northwind.ServiceBase;
using Northwind.ServiceBase.Relations;

namespace Northwind.ServiceModel.Dto
{
	/// <summary>
	/// Clase que representa una entidad <see cref="Customer"/>
	/// </summary>	
	public class Customer : CommonDto
	{		
		public string Id { get; set; }		
		public string CompanyName { get; set; }
		public string ContactName { get; set; }
		public string ContactTitle { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string Region { get; set; }
		public string PostalCode { get; set; }
		public string Country { get; set; }
		public string Phone { get; set; }
		public string Fax { get; set; }

		[Relation(RelationType.HasMany, typeof(Order))]
		public List<Order> Orders { get; set; }		
	}
}
