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
using System.Text;

namespace Northwind.Common
{
	/// <summary>
	/// Métodos de extensión para <see cref="Expression"/>
	/// </summary>
	public static class ExpressionExtensions
	{
		/// <summary>
		/// Elimina todas las conversiones existentes en la expresión
		/// </summary>
		/// <param name="expression">Expresión a comprobar</param>
		/// <returns><see cref="Expression"/> resultante</returns>
		public static Expression RemoveConvert( this Expression expression )
		{
			Verify.ArgumentNotNull(expression, "expression");

			while ( (expression != null) &&
					(expression.NodeType == ExpressionType.Convert || expression.NodeType == ExpressionType.ConvertChecked) )
			{
				expression = RemoveConvert(((UnaryExpression)expression).Operand);
			}

			return expression;
					
		}
	}
}
