using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind.ServiceBase
{
	/// <summary>
	/// Interfaz que representa una petición de colección
	/// </summary>
	public interface ICollectionRequest
	{
		/// <summary>
		/// Primer elemento de la petición
		/// </summary>
		int Offset { get; set; }

		/// <summary>
		/// Límite de elementos
		/// </summary>
		int Limit { get; set; }
	}
}
