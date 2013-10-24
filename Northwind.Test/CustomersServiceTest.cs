using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Net;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceClient.Web;
using ServiceStack.Text;
using Northwind.ServiceBase;
using Northwind.ServiceInterface.Services;
using Northwind.ServiceModel.Contracts;
using Northwind.ServiceModel.Dto;
using Northwind.ServiceModel.Operations;

namespace Northwind.Test
{
        
    /// <summary>
    /// Clase de prueba para CustomersService.
    ///</summary>
	[TestClass]
	public class CustomersServiceTest
	{
		private static TestAppHost _appHost = null;
		private TestContext testContextInstance;

		/// <summary>
		///Obtiene o establece el contexto de la prueba que proporciona
		///la información y funcionalidad para la ejecución de pruebas actual.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region Atributos de prueba adicionales
		
		/// <summary>
		/// Inicialización de las pruebas
		/// </summary>
		[ClassInitialize]
		public static void CustomersServiceTestInitialize(TestContext testContext)
		{
			_appHost = new TestAppHost();			
		}
		
		/// <summary>
		/// Fin de las pruebas
		/// </summary>
		[ClassCleanup]
		public static void CustomersServiceTestInitializeCleanUp()
		{
			if ( _appHost != null )
			{
			    _appHost.Stop();
			    _appHost.Dispose();
			}
		}
		
		//Use TestInitialize para ejecutar código antes de ejecutar cada prueba
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup para ejecutar código después de que se hayan ejecutado todas las pruebas
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion		

		/// <summary>
		/// 
		/// </summary>
		/// <param name="response"></param>
		public void AssertCollectionResponseIsValid( CustomersCollectionResponse response )
		{
			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsErrorResponse());
			Assert.IsNotNull(response.Customers);
			Assert.IsTrue(response.Customers.Count > 0);
		}

		[TestMethod]
		public void Can_get_all_customers()
		{
			try
			{
				var client = TestConfig.CreateJsonServiceClient();
				var response = client.Get(new GetCustomers());

				AssertCollectionResponseIsValid(response);
			}
			catch ( Exception ex )
			{
				Assert.Fail(ex.Message);
			}			
		}

		[TestMethod]
		public void Can_get_customer_by_id()
		{
			try
			{
				var client = TestConfig.CreateJsonServiceClient();
				var response = client.Get(new GetCustomers());

				var itemIndex = new Random().Next(1, response.Count);
				var source = response.Customers.ElementAt(itemIndex);

				var responseById = client.Get(new GetCustomer { Id = source.Id });
				var target = responseById.Customer;

				Assert.AreEqual(target.Id, source.Id);
				Assert.AreEqual(target.ToString(), source.ToString());
			}
			catch ( Exception ex )
			{
				Assert.Fail(ex.Message);
			}			
		}

		[TestMethod]
		public void Can_get_customer_orders()
		{
			try
			{
				var client = TestConfig.CreateJsonServiceClient();
				var response = client.Get(new GetCustomers());

				var itemIndex = new Random().Next(1, response.Count);
				var customer = response.Customers.ElementAt(itemIndex);

				// Recuperación de Order
				var orders = client.Get(new GetOrders());
				var sourceOrders = orders.Orders.Select(o => o.Customer == customer);

				var targetOrders = client.Get(new GetCustomerOrders { Id = customer.Id });

				Assert.IsNotNull(targetOrders);
				Assert.IsFalse(targetOrders.IsErrorResponse());
				Assert.IsNotNull(targetOrders.Orders);				
			}
			catch ( Exception ex )
			{
				Assert.Fail(ex.Message);
			}			
		}

		[TestMethod]
		public void Can_get_all_customers_has_metadata()
		{
			try
			{
				var client = TestConfig.CreateJsonServiceClient();
				var response = client.Get(new GetCustomers());

				AssertCollectionResponseIsValid(response);

				Assert.IsTrue(response.Metadata != null);
			}
			catch ( Exception ex )
			{
				Assert.Fail(ex.Message);
			}
		}

		[TestMethod]
		public void Can_create_customer()
		{
			try
			{
				var customer = new Customer
				{
					Id = "TEST",
					ContactName = "Test Name",
					CompanyName = "Test Company",
					ContactTitle = "Test Contact Title",
					Address = "Test Address",
					City = "Test City",
					Region = "Test Region",
					PostalCode = "12345",
					Country = "Test Country",
					Phone = "Test Phone",
					Fax = "Test Fax",
				};

				var client = TestConfig.CreateJsonServiceClient();
				client.Post(customer);

				var response = client.Get(new GetCustomer { Id = customer.Id });

				Assert.IsNotNull(response);
				Assert.AreEqual(customer.Id, response.Customer.Id);
			}
			catch ( Exception ex )
			{
				Assert.Fail(ex.Message);
			}
		}

		[TestMethod]
		public void Can_delete_customer()
		{
			try
			{
				var client = TestConfig.CreateJsonServiceClient();
				var customer = client.Get(new GetCustomers()).Customers.First();

				client.Delete(customer);

				var response = client.Get(new GetCustomer { Id = customer.Id });

				Assert.IsNotNull(response);
			}
			catch ( WebServiceException ex ) 
			{
				Assert.IsTrue(ex.StatusCode == (int)HttpStatusCode.NotFound);
			}
			catch ( Exception ex )
			{
				Assert.Fail(ex.Message);
			}
		}

		[TestMethod]
		public void Can_update_customer()
		{
			try
			{
				var client = TestConfig.CreateJsonServiceClient();
				var customer = client.Get(new GetCustomers()).Customers.First();
				customer.ContactName = "Updated";

				client.Put(customer);

				var updatedCustomer = client.Get(new GetCustomer { Id = customer.Id });

				Assert.IsNotNull(updatedCustomer);
				Assert.AreEqual(customer.ContactName, updatedCustomer.Customer.ContactName);
				
			}
			catch ( Exception ex )
			{
				Assert.Fail(ex.Message);
			}

		}
	}
}
