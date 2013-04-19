using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.Text;
using Northwind.Data.Model;
using Northwind.Data.Repositories;
using Northwind.ServiceBase;
using Northwind.ServiceModel.Contracts;
using Northwind.ServiceModel.Dto;
using Northwind.ServiceModel.Operations;

namespace Northwind.ServiceInterface.Services
{
	/// <summary>
	/// Servicio de clientes
	/// </summary>
	public class CustomersService : ServiceBase<ICustomerEntityRepository, CustomerEntity>
	{
		public CustomersResponse Get( CustomerDetail request )
		{
			var result = Repository.Get(request.Id);

			if ( result == null )
			{
				throw HttpError.NotFound("Customer {0} not found".Fmt(request.Id));
			}

			return new CustomersResponse { Result = result.ToDto<Customer>() };
		}
	}
}
