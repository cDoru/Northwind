using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using Northwind.Data.Repositories;
using Northwind.ServiceBase;
using Northwind.ServiceModel.Dto;

namespace Northwind.ServiceInterface.Services
{
	/// <summary>
	/// Servicio de clientes
	/// </summary>
	public class CustomersService : ServiceBase<CustomerEntityRepository, Customers>
	{
	}
}
