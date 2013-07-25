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
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using ServiceStack.Text;
using Northwind.Common;

namespace Northwind.ServiceBase.Query.Parser
{
	/// <summary>
	/// Clase que representa una factoría de expresiones de filtrado
	/// </summary>
	public class FilterExpressionFactory
	{
		/// <summary>
		/// Crea una expresión de filtro a partir de su representación como cadena
		/// </summary>
		/// <typeparam name="TEntity">Elemento a filtrar</typeparam>
		/// <param name="filter">Representación de la expresión de filtrado</param>
		/// <returns>Una <see cref="Expression{TDelegate}"/> si el filtro es válido. Si no lo es, null</returns>
		public Expression<Func<TEntity, bool>> Create<TEntity>( string filter )
		{
			if ( String.IsNullOrWhiteSpace(filter) )
			{
				return x => true;
			}

			// Parámetro de la expresión
			var param = Expression.Parameter(typeof(TEntity), "x");

			// Expresión
			var expression = CreateExpression<TEntity>(filter, param, new List<ParameterExpression>(), null);

			if ( expression != null )
			{
				return Expression.Lambda<Func<TEntity, bool>>(expression, param);
			}

			throw new InvalidOperationException("No se puede crear la expresión a partir de: " + filter);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TEntity"></typeparam>
		/// <param name="filter"></param>
		/// <param name="sourceParam"></param>
		/// <param name="lambdaParams"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		private Expression CreateExpression<TEntity>( string filter, ParameterExpression sourceParam, List<ParameterExpression> lambdaParams, Type type )
		{
			return null;
		}
	}
}
