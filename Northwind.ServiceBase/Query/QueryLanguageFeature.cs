using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;
using ServiceStack.Common.Reflection;
using ServiceStack.ServiceHost;
using ServiceStack.WebHost.Endpoints;
using Northwind.ServiceBase.Common;
using Northwind.ServiceBase.Query.Parser;

namespace Northwind.ServiceBase.Query
{
	/// <summary>
	/// Clase que interroga una petición
	/// </summary>
	public class QueryLanguageFeature : IPlugin
	{
		private IAppHost _appHost;

		/// <summary>
		/// Indica si el plugin está habilitado o no
		/// </summary>
		public static bool IsEnabled
		{
			get { return EndpointHost.Plugins.Any(p => p is QueryLanguageFeature); }
		}

		#region Miembros de IPlugin

		/// <summary>
		/// 
		/// </summary>
		/// <param name="appHost"></param>
		public void Register( IAppHost appHost )
		{
			Verify.ArgumentNotNull(appHost, "appHost");

			_appHost = appHost;
			_appHost.RequestFilters.Add(ProcessRequest);
		}

		#endregion		

		/// <summary>
		/// 
		/// </summary>
		/// <param name="req"></param>
		/// <param name="res"></param>
		/// <param name="dto"></param>
		public void ProcessRequest( IHttpRequest req, IHttpResponse res, object dto )
		{
			Verify.ArgumentNotNull(req, "req");
			Verify.ArgumentNotNull(res, "res");
			Verify.ArgumentNotNull(dto, "dto");

			if ( req.HttpMethod != "GET" ) return;			

			if ( dto is ISearchable )
			{					
				var typeOfDto = dto.GetType().GetGenericArguments();
				var parserType = typeof(QueryParametersParser<>).MakeGenericType(typeOfDto);
				var parser = Activator.CreateInstance(parserType);
				
				var parseMethod = parser.GetType().GetMethod("Parse");
				var queryExpr = parseMethod.Invoke(parser, new object[] { req.QueryString });

				dto.GetType().GetProperty("Query").SetValue(dto, queryExpr, null);				
			}
		}
		
	}
}
