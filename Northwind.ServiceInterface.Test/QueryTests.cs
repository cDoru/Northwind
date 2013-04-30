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

		[Test]
		public void GetAllCustomersUsingUrl()
		{
			var responseStr = (TestConfig.CustomerServiceUri.ToString()).GetJsonFromUrl();

			var response = JsonSerializer.DeserializeFromString<CustomersCollectionResponse>(responseStr);

			AssertCollectionResponseIsValid(response);
		}

		[Test]
		public void GetAllCustomers()
		{
			var client = TestConfig.CreateJsonServiceClient();
			var response = client.Get(new Customers());

			AssertCollectionResponseIsValid(response);
		}
	}
}
