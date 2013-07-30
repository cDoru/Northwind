#region Licencia
/*
   Copyright 2013 Juan Diego Martinez

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

     http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.

*/        
#endregion
          
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.Service;
using ServiceStack.ServiceClient.Web;

namespace Northwind.Test
{
	/// <summary>
	/// Clase de configuración de test
	/// </summary>
	internal static class TestConfig
	{
		/// <summary>
		/// 
		/// </summary>
		public static int ProductionPort = 2828;

		/// <summary>
		/// 
		/// </summary>
		public static int TestPort = 2829;

		/// <summary>
		/// <see cref="Uri"/> base de los test
		/// </summary>
		public static Uri AbsoluteBaseUri = new Uri("http://localhost:" + ProductionPort.ToString() + "/");

		/// <summary>
		/// <see cref="Uri"/> para el servicio <see cref="Customers"/>
		/// </summary>
		public static Uri CustomerServiceUri = new Uri(AbsoluteBaseUri, "customers");

		/// <summary>
		/// <see cref="Uri"/> para el servicio obtener <see cref="Order"/> de un <see cref="Customer"/>
		/// </summary>
		public static Uri CustomerOrdersUri = new Uri(CustomerServiceUri, "{1}/orders");

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
