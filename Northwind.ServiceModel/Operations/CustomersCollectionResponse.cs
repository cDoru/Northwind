using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Northwind.ServiceBase;
using Northwind.ServiceModel.Dto;

namespace Northwind.ServiceModel.Operations
{
	/// <summary>
	/// Clase que representa una respuesta para una colección de <seealso cref="Customer"/>
	/// </summary>
	public class CustomersCollectionResponse : ResponseCollectionBase<Customer>
	{
	}
}
