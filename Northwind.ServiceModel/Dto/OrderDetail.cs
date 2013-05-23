using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Northwind.ServiceBase;

namespace Northwind.ServiceModel.Dto
{
	/// <summary>
	/// Clase que representa una entidad <see cref="Customer"/>
	/// </summary>
	public class OrderDetail : CommonDto
	{
		public string Id { get; set; }
		public long OrderId { get; set; }
		public long ProductId { get; set; }
		public decimal UnitPrice { get; set; }
		public long Quantity { get; set; }
		public double Discount { get; set; }
	}
}
