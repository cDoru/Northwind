using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Northwind.ServiceBase.Query
{
	/// <summary>
	/// Clase que representa una expresión de consulta
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class QueryExpression<T>
	{
		#region Campos

		/// <summary>
		/// Primer elemento que se recupera
		/// </summary>
		private int _offset;

		/// <summary>
		/// Límite de elementos
		/// </summary>
		private int _limit;

		/// <summary>
		/// Expressión de selección
		/// </summary>
		private Expression<Func<T, object>> _selectExpression;		

		/// <summary>
		/// Expresión de filtrado
		/// </summary>
		private Expression<Func<T, bool>> _filterExpression;		

		#endregion

		#region Constructores

		/// <summary>
		/// 
		/// </summary>
		public QueryExpression()
		{
		}

		#endregion
	}
}
