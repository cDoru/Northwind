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

		/// <summary>
		/// Factoría de expresiones de filtrado
		/// </summary>
		private FilterExpressionFactory _filterExpressionFactory;

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
