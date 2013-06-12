using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Northwind.Common;
using Northwind.ServiceBase.Common;

namespace Northwind.ServiceBase.Query.Parser
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class QueryParametersParser<T>
	{
		#region Campos

		/// <summary>
		/// Factoría de expresiones de selección
		/// </summary>
		private SelectExpressionFactory<T> _selectExpressionFactory;

		#endregion

		#region Constructores

		/// <summary>
		/// Constructor de la clase
		/// </summary>
		public QueryParametersParser()
		{
			_selectExpressionFactory = new SelectExpressionFactory<T>();
		}

		#endregion

		#region Métodos públicos
		/// <summary>
		/// 
		/// </summary>
		/// <param name="queryParams"></param>
		/// <returns></returns>
		public QueryExpression<T> Parse( NameValueCollection queryParams )
		{
			Verify.ArgumentNotNull(queryParams, "queryParams");

			var offsetParam = queryParams[QueryLanguageConstants.Offset];
			var limitParam = queryParams[QueryLanguageConstants.Limit];
			var selectParam = queryParams[QueryLanguageConstants.Select];			
			var orderByParam = queryParams[QueryLanguageConstants.OrderBy];

			var selectExpression = _selectExpressionFactory.Create(selectParam);

			var queryExpression = new QueryExpression<T>(
				String.IsNullOrWhiteSpace(offsetParam) ? 0 : Convert.ToInt32(offsetParam),
				String.IsNullOrWhiteSpace(limitParam) ? 0 : Convert.ToInt32(limitParam), 
				selectExpression
			);

			return queryExpression;
		}

		#endregion
	}
}
