using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.Common.Web;
using ServiceStack.Text;
using NUnit.Framework;
using Northwind.ServiceModel.Contracts;
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
		private void AssertCollectionResponseIsValid( CustomersCollectionResponse response )
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
			var response = client.Get(new Customers());

			AssertCollectionResponseIsValid(response);
		}

		[Test(Description = "GET Customer por Id")]
		public void GetCustomerById()
		{
			var client = TestConfig.CreateJsonServiceClient();
			var response = client.Get(new Customers());

			var itemIndex = new Random().Next(1, response.Count);
			var source = response.Result.ElementAt(itemIndex);

			var responseById = client.Get(new CustomerDetail { Id = source.Id });
			var target = responseById.Result;

			Assert.That(target.Id, Is.EqualTo(source.Id));
			Assert.That(target.ToString(), Is.EqualTo(source.ToString()));
		}
	}
}
