using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.ServiceHost;
using Northwind.ServiceBase;
using Northwind.ServiceModel.Dto;

namespace Northwind.ServiceModel.Operations
{
	/// <summary>
	/// Clase que representa una respuesta para el tipo Customer
	/// </summary>
	public class CustomersResponse : ResponseBase<Customer>
	{

	}
}
