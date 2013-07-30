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
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Common.Utils;
using ServiceStack.Logging;
using ServiceStack.Logging.Support.Logging;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Sqlite;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.Cors;
using ServiceStack.ServiceInterface.Validation;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints;
using Northwind.Data.Model;
using Northwind.Data.Repositories;
using Northwind.ServiceBase;
using Northwind.ServiceBase.Formats;
using Northwind.ServiceBase.Meta;
using Northwind.ServiceBase.Query;
using Northwind.ServiceBase.Relations;
using Northwind.ServiceInterface.Services;
using Northwind.ServiceInterface.Validators;
using Northwind.ServiceModel.Contracts;
using Northwind.ServiceModel.Dto;

namespace Northwind.Test
{
	/// <summary>
	/// Clase que representa la aplicación Web
	/// </summary>
	public class TestAppHost : AppHostHttpListenerBase
	{
		/// <summary>
		/// Constructor de la clase
		/// </summary>
		public TestAppHost()
			: base("Northwind test web services", typeof(CustomersService).Assembly)
		{
		}

		#region Miembros de AppHostBase

		#region Configure
		/// <summary>
		/// Configuración de los servicios web
		/// </summary>
		/// <param name="container">Contenedor IoC</param>
		public override void Configure( Funq.Container container )
		{
			// JSON
			JsConfig.EmitCamelCaseNames = true;
			JsConfig.IncludeNullValues = true;
			JsConfig.DateHandler = JsonDateHandler.ISO8601;
			JsConfig.EscapeUnicode = true;

			// ServiceStack
			SetConfig(new EndpointHostConfig
			{
				DebugMode = true,
			});

			// Rutas			
			Routes
				.Add<CollectionRequest<Customer>>("/customers", "GET, PUT")
				.Add<SingleRequest<Customer>>("/customers/{Id}", "GET, DELETE, POST")
				.Add<CollectionRequest<Order>>("/orders", "GET, PUT")
				.Add<CustomerOrders>("/customers/{Id}/orders", "GET, PUT")
				.Add<SingleRequest<Order>>("/orders/{Id}", "GET, DELETE, POST")
				.Add<OrderDetails>("/orders/{Id}/details", "GET, DELETE, POST")
				.Add<CollectionRequest<Supplier>>("/suppliers", "GET, PUT")
				.Add<SingleRequest<Supplier>>("/suppliers/{Id}", "GET, DELETE, POST");

			// Plugins
			//Plugins.Add(new ValidationFeature());
			Plugins.Add(new CorsFeature(allowedMethods: "GET, POST"));

			// Validaciones
			container.RegisterValidators(typeof(CustomerValidator).Assembly);

			// Caché
			container.Register<ICacheClient>(new MemoryCacheClient());

			// Dependencias
			container.RegisterAs<CategoryEntityRepository, ICategoryEntityRepository>();
			container.RegisterAs<CustomerEntityRepository, ICustomerEntityRepository>();
			container.RegisterAs<EmployeeEntityRepository, IEmployeeEntityRepository>();
			container.RegisterAs<OrderEntityRepository, IOrderEntityRepository>();
			container.RegisterAs<OrderDetailEntityRepository, IOrderDetailEntityRepository>();
			container.RegisterAs<ProductEntityRepository, IProductEntityRepository>();
			container.RegisterAs<ShipperEntityRepository, IShipperEntityRepository>();
			container.RegisterAs<SupplierEntityRepository, ISupplierEntityRepository>();
			container.RegisterAs<RegionEntityRepository, IRegionEntityRepository>();
			container.RegisterAs<TerritoryEntityRepository, ITerritoryEntityRepository>();
			container.RegisterAs<EmployeeTerritoryEntityRepository, IEmployeeTerritoryEntityRepository>();			

			// Acceso a datos			
			var dbFactory = new OrmLiteConnectionFactory(
				"Northwind.sqlite".MapHostAbsolutePath(), 
				//ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString,
				true, 
				SqliteDialect.Provider);
			container.Register<IDbConnectionFactory>(dbFactory);			
		}
		#endregion

		#endregion
		
	}
}