﻿using System;
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
		#region Campos

		/// <summary>
		/// Referencia a IAppHost
		/// </summary>
		private IAppHost _appHost;

		/// <summary>
		/// Diccionario donde se guardarán las asociaciones entre clases
		/// </summary>
		private Dictionary<Type, Type> _associations = new Dictionary<Type, Type>();

		#endregion

		#region Propiedades

		#region IsEnabled
		/// <summary>
		/// Indica si el plugin está habilitado o no
		/// </summary>
		public static bool IsEnabled
		{
			get { return EndpointHost.Plugins.Any(p => p is QueryLanguageFeature); }
		}
		#endregion		

		#endregion

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

		#region Métodos privados

		#region ProcessRequest
		/// <summary>
		/// 
		/// </summary>
		/// <param name="req"></param>
		/// <param name="res"></param>
		/// <param name="dto"></param>
		private void ProcessRequest( IHttpRequest req, IHttpResponse res, object dto )
		{
			Verify.ArgumentNotNull(req, "req");
			Verify.ArgumentNotNull(res, "res");
			Verify.ArgumentNotNull(dto, "dto");

			if ( req.HttpMethod != "GET" ) return;			

			if ( dto is ISearchable )
			{					
				var typeOfDto = dto.GetType().GetGenericArguments();
				
				Type associatedType;

				if ( _associations.TryGetValue(typeOfDto.First(), out associatedType) )
				{
					var parserType = typeof(QueryParametersParser<>).MakeGenericType(associatedType);
					var parser = parserType.CreateInstance();

					var queryExpr = Expression.Call(
						Expression.Constant(parser, parserType),
						"Parse",
						null,
						Expression.Constant(req.QueryString));
					
					var lambda = Expression.Lambda<Func<IQueryExpression>>(queryExpr);					

					//dto.GetType().GetProperty("Query").SetValue(dto, queryExpr, null);
					dto.GetType().GetProperty("Query").SetValue(dto, lambda.Compile()(), null);
				}				
			}
		}
		#endregion

		#endregion

		#region Métodos públicos

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T1"></typeparam>
		/// <typeparam name="T2"></typeparam>
		public void RegisterAssociation(Type t1, Type t2)
		{
			Type type;

			if ( !(_associations.TryGetValue(t1, out type)) ) 
			{
				_associations.Add(t1, t2);
			}
		}

		#endregion

	}
}
