using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Northwind.ServiceBase.Common;

namespace Northwind.ServiceBase.Query.Parser
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class QueryParametersParser<T>
	{
		#region Constructores

		/// <summary>
		/// 
		/// </summary>
		public QueryParametersParser()
		{
		}

		#endregion

		#region Métodos públicos

		public QueryExpression<T> Parse( NameValueCollection queryParams )
		{
			Verify.ArgumentNotNull(queryParams, "queryParams");

			var offsetParam = queryParams[QueryLanguageConstants.Offset];
			var limitParam = queryParams[QueryLanguageConstants.Limit];
			var selectParam = queryParams[QueryLanguageConstants.Select];			
			var orderByParam = queryParams[QueryLanguageConstants.OrderBy];

			return new QueryExpression<T>();			
		}

		#endregion
	}
}
