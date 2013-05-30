using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Northwind.ServiceBase.Query;

namespace Northwind.ServiceBase
{
	/// <summary>
	/// Interfaz que representa una entidad que permite funciones de búsqueda
	/// </summary>	
	public interface ISearchable
	{

	}

	/// <summary>
	/// Interfaz que representa una entidad que permite funciones de búsqueda
	/// </summary>	
	public interface ISearchable<TDto> : ISearchable
	{
		/// <summary>
		/// Condiciones de búsqueda
		/// </summary>
		QueryExpression<TDto> Query { get; set; }
		
	}
}
