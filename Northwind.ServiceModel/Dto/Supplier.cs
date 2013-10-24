using System.Collections.Generic;
using Northwind.ServiceBase;
using Northwind.ServiceBase.Relations;
using ServiceStack.ServiceHost;

namespace Northwind.ServiceModel.Dto
{
	/// <summary>
	/// Clase que representa una entidad <see cref="Supplier"/>
	/// </summary>	
	[Api("Create a single Supplier.")]
	[Route("/suppliers", "POST")]
	[Route("/suppliers/{Id}", "PUT DELETE")]
	public class Supplier : CommonDto, IReturnVoid
	{
		public long Id { get; set; }
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
		public string HomePage { get; set; }		
	}
}
