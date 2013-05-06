﻿using System;
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
	/// Clase que representa una petición de inserción de un <see cref="Customer"/>
	/// </summary>
	[Api("Insert a single Customer")]
	[Route("/customers", "POST")]
	public class CustomerCreation : Customer, IReturn<CustomersResponse>
	{
		
	}
}