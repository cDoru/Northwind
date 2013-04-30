using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.Service;
using ServiceStack.ServiceClient.Web;

namespace Northwind.Services.Test
{
	/// <summary>
	/// Clase de configuración de test
	/// </summary>
	internal static class TestConfig
	{
		/// <summary>
		/// <see cref="Uri"/> base de los test
		/// </summary>
		public static Uri AbsoluteBaseUri = new Uri("http://localhost:2828/");

		/// <summary>
		/// <see cref="Uri"/> para el servicio <see cref="Customers"/>
		/// </summary>
		public static Uri CustomerServiceUri = new Uri(AbsoluteBaseUri, "customers");

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		internal static IRestClient CreateJsonServiceClient()
		{
			return new JsonServiceClient(AbsoluteBaseUri.ToString());
		}
	}
}
