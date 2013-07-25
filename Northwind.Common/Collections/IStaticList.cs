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
using System.Text;

namespace Northwind.Common.Collections
{
	/// <summary>
	/// Interfaz que representa una colección de objetos estáticos que contiene metadatos sobre sus elementos
	/// </summary>
	/// <typeparam name="T">El tipo de objeto que contiene la collección</typeparam>
	public interface IStaticList
	{
		/// <summary>
		/// Número total de páginas
		/// </summary>
		int PageCount { get; }

		/// <summary>
		/// Número total de elementos
		/// </summary>
		long TotalItemCount { get; }

		/// <summary>
		/// Índice el primer elemento de la lista
		/// </summary>
		int FirstItemOnPage { get; }

		/// <summary>
		/// Número de página correspondiente a este conjunto de datos
		/// </summary>
		int PageNumber { get; }

		/// <summary>
		/// Tamaño máximo de elementos de la colección
		/// </summary>
		int PageSize { get; }

		/// <summary>
		/// Indica si ésta no es la primera página del total de datos
		/// </summary>
		bool HasPreviousPage { get; }

		/// <summary>
		/// Indica si ésta no es la última página del total de datos
		/// </summary>
		bool HasNextPage { get; }

		/// <summary>
		/// Indica si ésta es la primera página del total de datos
		/// </summary>
		bool IsFirstPage { get; }

		/// <summary>
		/// Indica si ésta es la última página del total de datos
		/// </summary>
		bool IsLastPage { get; }
	}

	/// <summary>
	/// Interfaz que representa una colección de objetos estáticos que contiene metadatos sobre sus elementos
	/// </summary>
	/// <typeparam name="T">El tipo de objeto que contiene la collección</typeparam>
	public interface IStaticList<out T> : IStaticList, IEnumerable<T>
	{
		/// <summary>
		/// Obtiene el elemento que se encuentra en el índice especificado
		/// </summary>
		T this[int index] { get; }

		/// <summary>
		/// Obtiene el número de elementos de <see cref="StaticList{T}"/>
		/// </summary>
		int Count { get; }
	}
}
