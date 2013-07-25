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
	/// Clase que representa una colección de objetos estáticos que contiene metadatos sobre sus elementos
	/// </summary>
	/// <typeparam name="T">El tipo de objeto que contiene la collección</typeparam>
	public class StaticList<T> : IStaticList<T>
	{
		/// <summary>
		/// Subconjunto de elementos de trabajo
		/// </summary>
		protected readonly List<T> Subset = new List<T>();

		#region Constructor

		/// <summary>
		/// 
		/// </summary>
		/// <param name="subset"></param>
		/// <param name="offset"></param>
		/// <param name="limit"></param>
		/// <param name="totalCount"></param>
		public StaticList(IEnumerable<T> subset, int offset, int limit, long totalCount)
		{
			Verify.ArgumentNotNull(subset, "subset");
			Verify.ArgumentInRange(offset, totalCount, "offset");

			Subset = (subset.ToList() ?? new List<T>());
			TotalItemCount = totalCount;
			FirstItemOnPage = offset;
			PageSize = limit;			
			
			PageCount = (TotalItemCount > 0 ? (int)Math.Ceiling(TotalItemCount / (double)PageSize) : 0);
			PageNumber = (PageCount > 0 ? (FirstItemOnPage / PageCount) + 1 : 1);
			HasPreviousPage = (PageNumber > 1);
			HasNextPage = (PageNumber < PageCount);
			IsFirstPage = (PageNumber == 1);
			IsLastPage = (PageNumber > PageCount);
		}

		#endregion

		#region Miembros de IStaticList<T>

		/// <summary>
		/// Obtiene el elemento que se encuentra en el índice especificado
		/// </summary>
		public T this[int index]
		{
			get { return Subset[index]; }
		}

		/// <summary>
		/// Obtiene el número de elementos de <see cref="StaticList{T}"/>
		/// </summary>
		public int Count
		{
			get { return Subset.Count; }
		}

		#endregion

		#region Miembros de IStaticList

		/// <summary>
		/// Número total de páginas
		/// </summary>
		public int PageCount { get; protected set; }

		/// <summary>
		/// Número total de elementos
		/// </summary>
		public long TotalItemCount { get; protected set; }

		/// <summary>
		/// Índice el primer elemento de la lista
		/// </summary>
		public int FirstItemOnPage { get; protected set; }

		/// <summary>
		/// Número de página correspondiente a este conjunto de datos
		/// </summary>
		public int PageNumber { get; protected set; }

		/// <summary>
		/// Tamaño máximo de elementos de la colección
		/// </summary>
		public int PageSize { get; protected set; }

		/// <summary>
		/// Indica si ésta no es la primera página del total de datos
		/// </summary>
		public bool HasPreviousPage { get; protected set; }

		/// <summary>
		/// Indica si ésta no es la última página del total de datos
		/// </summary>
		public bool HasNextPage { get; protected set; }

		/// <summary>
		/// Indica si ésta es la primera página del total de datos
		/// </summary>
		public bool IsFirstPage { get; protected set; }

		/// <summary>
		/// Indica si ésta es la última página del total de datos
		/// </summary>
		public bool IsLastPage { get; protected set; }

		#endregion

		#region Miembros de IEnumerable<T>

		/// <summary>
		/// Devuelve un enumerador que itera por la colección <see cref="StaticList{T}"/>
		/// </summary>
		public IEnumerator<T> GetEnumerator()
		{
			return Subset.GetEnumerator();
		}

		#endregion

		#region Miembros de IEnumerable

		/// <summary>
		/// Devuelve un enumerador que itera por la colección <see cref="StaticList{T}"/>
		/// </summary>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}
