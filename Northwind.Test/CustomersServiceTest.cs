using Northwind.ServiceInterface.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ServiceStack.Common.Web;
using ServiceStack.Text;
using Northwind.ServiceBase;
using Northwind.ServiceModel.Contracts;
using Northwind.ServiceModel.Dto;
using Northwind.ServiceModel.Operations;

namespace Northwind.Test
{
        
    /// <summary>
    ///Se trata de una clase de prueba para CustomersServiceTest y se pretende que
    ///contenga todas las pruebas unitarias CustomersServiceTest.
    ///</summary>
	[TestClass()]
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
			//_appHost = new TestAppHost();
			//_appHost.Init();
			//_appHost.Start(TestConfig.AbsoluteBaseUri.ToString());
		}
		
		/// <summary>
		/// Fin de las pruebas
		/// </summary>
		[ClassCleanup]
		public static void CustomersServiceTestInitializeCleanUp()
		{
			//if ( _appHost != null )
			//{
			//    _appHost.Stop();
			//    _appHost.Dispose();
			//}
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

		public void AssertCollectionResponseIsValid( CollectionResponse<Customer> response )
		{
			Assert.IsNotNull(response);
			Assert.IsFalse(response.IsErrorResponse());
			Assert.IsNotNull(response.Result);
			Assert.IsTrue(response.Result.Count > 0);
		}		

		[TestMethod]
		public void GetAllCustomers()
		{
			try
			{
				var client = TestConfig.CreateJsonServiceClient();
				var response = client.Get(new CollectionRequest<Customer>());

				AssertCollectionResponseIsValid(response);
			}
			catch ( Exception ex )
			{
				Assert.Fail(ex.Message);
			}			
		}
	}
}
