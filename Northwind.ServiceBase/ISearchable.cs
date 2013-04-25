using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind.ServiceBase
{
	/// <summary>
	/// Interfaz que representa una entidad que permite funciones de búsqueda y paginación
	/// </summary>
	/// <see cref="https://groups.google.com/d/msg/servicestack/uoMzASmvxho/CtqpZdju7NcJ"/>
	public interface ISearchable
	{
		/// <summary>
		/// Condiciones de búsqueda
		/// </summary>
		string Query {get; set; }

		/// <summary>
		/// Número de resultados
		/// </summary>
		int Limit { get; set; }

		/// <summary>
		/// Índice del primer elemento de la lista
		/// </summary>
		int Offset { get; set; }
	}
}
