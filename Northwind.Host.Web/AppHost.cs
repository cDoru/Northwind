using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Common;
using ServiceStack.Logging;
using ServiceStack.Logging.Support.Logging;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints;
using System.Configuration;
using Northwind.Data.Repositories;
using Northwind.ServiceInterface.Services;

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
			JsConfig.IncludeNullValues = true;
			JsConfig.DateHandler = JsonDateHandler.ISO8601;

			// ServiceStack
			SetConfig(new EndpointHostConfig
			{
				DebugMode = true,
			});

			// Caché
			container.Register<ICacheClient>(new MemoryCacheClient());

			// Dependencias
			container.RegisterAs<CategoryEntityRepository, ICategoryEntityRepository>();
			container.RegisterAs<CustomerEntityRepository, ICustomerEntityRepository>();
			container.RegisterAs<EmployeeEntityRepository, IEmployeeEntityRepository>();
			container.RegisterAs<OrderDetailEntityRepository, IOrderDetailEntityRepository>();	

			// Acceso a datos
			var connectionString = ConfigurationManager.ConnectionStrings["Northwind.Data.DataSource"].ConnectionString;
			var dbFactory = new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider);
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