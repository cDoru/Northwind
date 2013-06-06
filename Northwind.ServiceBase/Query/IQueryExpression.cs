using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind.ServiceBase.Query
{
	/// <summary>
	/// Interfaz que representa una expresión de query
	/// </summary>
	public interface IQueryExpression
	{
		/// <summary>
		/// Índice de partida
		/// </summary>
		int Offset { get; }

		/// <summary>
		/// Límite de elementos
		/// </summary>
		int Limit { get; }
	}
}
