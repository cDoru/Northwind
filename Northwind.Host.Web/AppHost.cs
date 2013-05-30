using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Configuration;
using Northwind.Data.Model;
using Northwind.Data.Repositories;
using Northwind.ServiceBase;
using Northwind.ServiceBase.Formats;
using Northwind.ServiceBase.Meta;
using Northwind.ServiceBase.Query;
using Northwind.ServiceInterface.Services;
using Northwind.ServiceInterface.Validators;
using Northwind.ServiceModel.Contracts;
using Northwind.ServiceModel.Dto;

namespace Northwind.Host.Web
{
	/// <summary>
	/// Clase que representa la aplicación Web
	/// </summary>
	public class AppHost : AppHostBase
	{
		/// <summary>
		/// Constructor de la clase
		/// </summary>
		public AppHost() : base("Northwind web services", typeof(CustomersService).Assembly)
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
			JsConfig.IncludeNullValues = false;
			JsConfig.DateHandler = JsonDateHandler.ISO8601;
			JsConfig.EscapeUnicode = true;
			JsConfig<MetadataUriType>.SerializeFn = text => text.ToString().ToCamelCase();

			// ServiceStack
			SetConfig(new EndpointHostConfig
			{
				DebugMode = true
			});

			// Rutas			
			Routes
				.Add<CollectionRequest<Customer>>("/customers", "GET, PUT")
				.Add<SingleRequest<Customer>>("/customers/{Id}", "GET, DELETE, POST")
				.Add<CollectionRequest<Order>>("/orders", "GET, PUT")
				.Add<CustomerOrders>("/customers/{Id}/orders", "GET, PUT")
				.Add<SingleRequest<Order>>("/orders/{Id}", "GET, DELETE, POST")
				.Add<OrderDetails>("/orders/{Id}/details", "GET, DELETE, POST");			

			// Formatos
			//AtomFeedFormat.Register(this);

			// Plugins
			Plugins.Add(new ValidationFeature());
			Plugins.Add(new QueryLanguageFeature());

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

			container.RegisterAs<CategoryEntityRepository, IRepository<CategoryEntity>>();
			container.RegisterAs<CustomerEntityRepository, IRepository<CustomerEntity>>();
			container.RegisterAs<EmployeeEntityRepository, IRepository<EmployeeEntity>>();
			container.RegisterAs<OrderEntityRepository, IRepository<OrderEntity>>();
			container.RegisterAs<OrderDetailEntityRepository, IRepository<OrderDetailEntity>>();
			container.RegisterAs<ProductEntityRepository, IRepository<ProductEntity>>();
			container.RegisterAs<ShipperEntityRepository, IRepository<ShipperEntity>>();
			container.RegisterAs<SupplierEntityRepository, IRepository<SupplierEntity>>();
			container.RegisterAs<RegionEntityRepository, IRepository<RegionEntity>>();
			container.RegisterAs<TerritoryEntityRepository, IRepository<TerritoryEntity>>();
			container.RegisterAs<EmployeeTerritoryEntityRepository, IRepository<EmployeeTerritoryEntity>>();

			// Acceso a datos
			var dbFactory = new OrmLiteConnectionFactory(
				"~/Northwind.sqlite".MapHostAbsolutePath(),
				SqliteDialect.Provider);

			//var connStr = ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString.Replace("{AppData}", AppDomain.CurrentDomain.BaseDirectory + @"..\Northwind.Data");
			//var dbFactory = new OrmLiteConnectionFactory(connStr, true, SqliteDialect.Provider);
			container.Register<IDbConnectionFactory>(dbFactory);
		}
		#endregion

		#endregion

		#region Miembros estáticos

		#region Start
		/// <summary>
		/// Inicio de una instancia de la aplicación
		/// </summary>
		public static void Start()
		{
			new AppHost().Init();
		}
		#endregion

		#endregion
	}
}