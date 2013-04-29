using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.ServiceHost;
using Northwind.ServiceBase;
using Northwind.ServiceModel.Dto;
using Northwind.ServiceModel.Operations;

namespace Northwind.ServiceModel.Contracts
{
	/// <summary>
	/// Clase que representa una petición del tipo <see cref="Customer"/> por su clave
	/// </summary>
	[Api("Get a single Customer by Id.")]
	[Route("/customers/{Id}", "GET")]
	public class CustomerDetail : RequestBase<Customer, CustomersResponse>
	{
		/// <summary>
		/// Identificador
		/// </summary>
		public string Id { get; set; }
	}
}
