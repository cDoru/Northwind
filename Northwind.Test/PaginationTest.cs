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
	public class PaginationTest
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
		public void Can_get_customers_paged()
		{
			try
			{
				var client = TestConfig.CreateJsonServiceClient();
				var response1 = client.Get(new GetCustomers { Offset = 1, Limit = 10 });
				var response2 = client.Get(new GetCustomers { Offset = 11, Limit = 10 });

				var c1 = response1.Customers.First();
				var c2 = response2.Customers.First();				
				
				// Comprobamos que obtenemos 10 elementos
				Assert.AreEqual(10, response1.Count);
				Assert.AreEqual(10, response2.Count);

				// Comprobamos que el primer elemento de cada respuesta sean diferentes
				Assert.AreNotEqual(c2, c1);
				Assert.AreNotEqual(c2.Id, c1.Id);

				// Comprobación de metadatos				
				var m1 = response1.Metadata;
				var m2 = response2.Metadata;

				Assert.IsNotNull(m1);
				Assert.IsNotNull(m2);
				Assert.AreEqual(1, m1.Offset);
				Assert.AreEqual(10, m1.Limit);
				Assert.AreEqual(11, m2.Offset);
				Assert.AreEqual(10, m2.Limit);
				Assert.AreEqual(m1.TotalCount, m2.TotalCount);

				// Comprobación de links
				var l1 = m1.Links;
				var l2 = m2.Links;

				Assert.IsNotNull(l1);
				Assert.IsNotNull(l2);
				Assert.IsTrue(l1.Count > 0);
				Assert.IsTrue(l2.Count > 0);
				Assert.IsTrue(l2.Count > l1.Count);
				Assert.AreNotEqual(l1[MetadataUriType.Self], l2[MetadataUriType.Self]);
				
				Assert.IsFalse(l1.ContainsKey(MetadataUriType.Previous));
				Assert.IsTrue(l1.ContainsKey(MetadataUriType.Next));
				Assert.IsTrue(l1.ContainsKey(MetadataUriType.Last));

				Assert.IsTrue(l2.ContainsKey(MetadataUriType.Previous));
				Assert.IsTrue(l2[MetadataUriType.Previous] == l1[MetadataUriType.Self]);
				Assert.IsTrue(l2[MetadataUriType.Self] == l1[MetadataUriType.Next]);
				Assert.IsTrue(l2[MetadataUriType.Last] == l1[MetadataUriType.Last]);
				Assert.IsTrue(l2[MetadataUriType.Next] != l1[MetadataUriType.Next]);
			}
			catch ( Exception ex )
			{
				Assert.Fail(ex.Message);
			}
		}
	}
}
