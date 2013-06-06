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
	public class QueryExpression<T> : IQueryExpression
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

		#region Propiedades

		#region Miembros de IQueryExpression

		#region Offset

		/// <summary>
		/// Índice de partida
		/// </summary>
		public int Offset
		{
			get { return _offset; }
		}

		#endregion

		#region Limit

		/// <summary>
		/// Límite de elementos
		/// </summary>
		public int Limit
		{
			get { return _limit; }
		}

		#endregion

		#endregion		

		#region Select

		/// <summary>
		/// Expresión de selección
		/// </summary>
		public Expression<Func<T, object>> Select 
		{ 
			get { return _selectExpression; }
		}

		#endregion

		#endregion

		#region Constructores

		/// <summary>
		/// Constructor de la clase
		/// </summary>
		public QueryExpression(int offset, int limit, Expression<Func<T, object>> select)
		{
			_offset = offset;
			_limit = limit;
			_selectExpression = select;
		}

		#endregion
	}
}
