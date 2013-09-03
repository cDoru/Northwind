using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using ServiceStack.CacheAccess;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using Northwind.Common;

namespace Northwind.ServiceBase
{
	/// <summary>
	/// Clase factoría de HttpResult
	/// </summary>
	public class HttpResultFactory
	{
		#region Métodos públicos

		#region AddETagHeader
		/// <summary>
		/// Añade la cabecera ETag
		/// </summary>
		/// <param name="headers">Cabecera</param>
		/// <param name="etag">Valor de etag</param>
		public void AddETagHeader( IDictionary<string, string> headers, string etag )
		{
			Verify.ArgumentNotNull(headers, "headers");

			headers[HttpHeaders.ETag] = etag;
		}
		#endregion

		#region ThrowError
		/// <summary>
		/// 
		/// </summary>
		/// <param name="statusCode"></param>
		/// <param name="message"></param>
		/// <param name="headers"></param>
		public void ThrowError( HttpStatusCode statusCode, string message, IDictionary<string, string> headers )
		{			
			var error = new HttpError
				{
					StatusCode = statusCode, 
					ErrorCode = message
				};

			if ( headers != null )
			{
				AddResponseHeaders(error, headers);
			}

			throw error;
		}
		#endregion

		#region GetResult
		/// <summary>
		/// 
		/// </summary>
		/// <param name="content"></param>
		/// <param name="contentType"></param>
		/// <param name="headers"></param>
		/// <returns></returns>
		public IHasOptions GetResult( object content, string contentType, IDictionary<string, string> headers )
		{
			var result = new HttpResult(content, contentType);

			if ( headers != null )
			{
				AddResponseHeaders(result, headers);
			}

			return result;
		}
		#endregion

		#region GetOptimizedResult
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="requestContext"></param>
		/// <param name="result"></param>
		/// <param name="headers"></param>
		/// <returns></returns>
		public object GetOptimizedResult<T>( IRequestContext requestContext, T result, IDictionary<string, string> headers ) where T : class
		{
			Verify.ArgumentNotNull(requestContext, "requestContext");
			Verify.ArgumentNotNull(result, "result");

			var optimizedResult = requestContext.ToOptimizedResult(result);

			if ( headers != null )
			{
				AddResponseHeaders(optimizedResult as IHasOptions, headers);
			}

			return optimizedResult;
		}
		#endregion

		#region GetOptimizedResultUsingCache
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="requestContext"></param>
		/// <returns></returns>
		public object GetOptimizedResultUsingCache<T>( IRequestContext requestContext, ICacheClient cacheClient, string cacheKey, Func<T> factoryFn, IDictionary<string, string> headers ) where T : class
		{
			Verify.ArgumentNotNull(requestContext, "requestContext");
			Verify.ArgumentNotNull(cacheClient, "cacheClient");
			Verify.ArgumentStringNotNullOrEmpty(cacheKey, "cacheKey");
			Verify.ArgumentNotNull(factoryFn, "factoryFn");

			var result = requestContext.ToOptimizedResultUsingCache(cacheClient, cacheKey, factoryFn);		

			if ( headers != null )
			{
				AddResponseHeaders(result as IHasOptions, headers);
			}

			return result;
		}
		#endregion

		#endregion

		#region Métodos privados

		#region AddResponseHeaders
		/// <summary>
		/// Añade los encabezados indicados a la respuesta
		/// </summary>
		/// <param name="response">Respuesta</param>
		/// <param name="headers">Cabeceras a añadir</param>
		private void AddResponseHeaders( IHasOptions response, IDictionary<string, string> headers )
		{
			Verify.ArgumentNotNull(response, "response");
			Verify.ArgumentNotNull(headers, "headers");

			foreach ( var item in headers )
			{
				response.Options[item.Key] = item.Value;
			}
		}
		#endregion

		#endregion		
	}
}
