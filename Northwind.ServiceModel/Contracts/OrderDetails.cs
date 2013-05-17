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
	[Api("Get details from a single Order by Id.")]
	//[Route("/customers/{Id}/orders", "GET, DELETE")]
	public class OrderDetails : CollectionRequest<OrderDetail>
	{
		public long Id { get; set; }
	}
}
