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
		/// <summary>
		/// Obtención de un <seealso cref="Customer"/> a partir de su identificador
		/// </summary>
		/// <param name="request">Petición</param>
		/// <returns>Respuesta <seealso cref="CustomerResponse"/></returns>
		public CustomersResponse Get( CustomerDetail request )
		{
			var result = Repository.Get(request.Id);

			if ( result == null )
			{
				throw HttpError.NotFound("Customer {0} not found".Fmt(request.Id));
			}

			return new CustomersResponse { Result = result.TranslateTo<Customer>() };
		}

		/// <summary>
		/// Devuelve todos los elementos <see cref="Customers"/>
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public CustomersCollectionResponse Get( Customers request )
		{
			var result = Repository.GetAll();

			if ( result == null )
			{
				throw HttpError.NotFound("Customers not found");
			}

			var list = new List<Customer>();

			result.ToList().ForEach(
					r => list.Add(r.TranslateTo<Customer>())
				);			

			return new CustomersCollectionResponse { Result = list };
		}
	}
}
