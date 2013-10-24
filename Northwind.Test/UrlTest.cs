using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using ServiceStack.Common.Web;
using ServiceStack.Text;
using Northwind.ServiceBase;
using Northwind.ServiceBase.Meta;
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
	public class UrlTest
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
		public static void CustomersServiceTestInitialize( TestContext testContext )
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

		[TestMethod]
		public void Can_get_all_customers_using_url()
		{
			try
			{
				var client = TestConfig.CreateJsonServiceClient();
				var responseJson = (TestConfig.CustomerServiceUri.ToString()).GetJsonFromUrl();

				var responseObj = JsonSerializer.DeserializeFromString<CustomersCollectionResponse>(responseJson);

				Assert.IsNotNull(responseObj);
				Assert.IsFalse(responseObj.IsErrorResponse());
				Assert.IsInstanceOfType(responseObj, typeof(CustomersCollectionResponse));
				Assert.IsNotNull(responseObj.Customers);
				Assert.IsTrue(responseObj.Customers.Count > 0);

			}
			catch ( Exception ex )
			{
				Assert.Fail(ex.Message);
			}
		}

		[TestMethod]
		public void Can_select_fields_for_customers_using_url()
		{
			try
			{
				var client = TestConfig.CreateJsonServiceClient();
				var selectUri = TestConfig.CustomerServiceUri.ToString() + "?select={0}";
				var responseJson = (selectUri.Fmt("id,companyName,contactName")).GetJsonFromUrl();

				var responseObj = JsonSerializer.DeserializeFromString<CustomersCollectionResponse>(responseJson);

				Assert.IsNotNull(responseObj);
				Assert.IsFalse(responseObj.IsErrorResponse());
				Assert.IsInstanceOfType(responseObj, typeof(CustomersCollectionResponse));
				Assert.IsTrue(responseObj.Customers.All(c => String.IsNullOrEmpty(c.Address)));
				Assert.IsTrue(responseObj.Customers.All(c => !String.IsNullOrEmpty(c.ContactName)));

			}
			catch ( Exception ex )
			{
				Assert.Fail(ex.Message);
			}
		}
		
	}
}
