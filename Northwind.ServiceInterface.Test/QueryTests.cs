using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.Common.Web;
using ServiceStack.Text;
using NUnit.Framework;
using Northwind.ServiceBase;
using Northwind.ServiceModel.Contracts;
using Northwind.ServiceModel.Dto;
using Northwind.ServiceModel.Operations;

namespace Northwind.Services.Test
{
	[TestFixture]
	public class QueryTests
	{
		/// <summary>
		/// Comprobación de validez de un <see cref="CustomersCollectionResponse"/>
		/// </summary>
		/// <param name="response"></param>
		private void AssertCollectionResponseIsValid( CollectionResponse<Customer> response )
		{
			Assert.That(!response.IsErrorResponse());			
			Assert.That(response, Is.Not.Null);
			Assert.That(response.Result, Is.Not.Null);
			Assert.That(response.Count, Is.GreaterThanOrEqualTo(0));
		}

		[Test(Description = "GET Customers por Url")]
		public void GetAllCustomersUsingUrl()
		{
			var responseStr = (TestConfig.CustomerServiceUri.ToString()).GetJsonFromUrl();

			var response = JsonSerializer.DeserializeFromString<CustomersCollectionResponse>(responseStr);

			AssertCollectionResponseIsValid(response);
		}

		[Test(Description = "GET Customers por Dto")]
		public void GetAllCustomers()
		{
			var client = TestConfig.CreateJsonServiceClient();
			var response = client.Get(new CollectionRequest<Customer>());

			AssertCollectionResponseIsValid(response);
		}

		[Test(Description = "GET Customer por Id")]
		public void GetCustomerById()
		{
			var client = TestConfig.CreateJsonServiceClient();
			var response = client.Get(new CollectionRequest<Customer>());

			var itemIndex = new Random().Next(1, response.Count);
			var source = response.Result.ElementAt(itemIndex);

			var responseById = client.Get(new SingleRequest<Customer> { Id = source.Id });
			var target = responseById.Result;

			Assert.That(target.Id, Is.EqualTo(source.Id));
			Assert.That(target.ToString(), Is.EqualTo(source.ToString()));
		}

		[Test(Description = "GET lista de Order por Id de Customer")]
		public void GetCustomerOrdersById()
		{
			var client = TestConfig.CreateJsonServiceClient();
			var response = client.Get(new CollectionRequest<Customer>());

			var itemIndex = new Random().Next(1, response.Count);
			var customer = response.Result.ElementAt(itemIndex);			
			
			// Recuperación de Order
			var orders = client.Get(new CollectionRequest<Order>());
			var sourceOrders = orders.Result.Select(o => o.CustomerId == customer.Id);

			var targetOrders = client.Get(new CustomerOrders { Id = customer.Id });

			Assert.That(!targetOrders.IsErrorResponse());
			Assert.That(targetOrders, Is.Not.Null);
			Assert.That(targetOrders.Result, Is.Not.Null);
			Assert.That(targetOrders.Count, Is.GreaterThanOrEqualTo(0));			
		}

	}
}
