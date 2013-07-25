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
using Northwind.Common;
using Northwind.Common.Collections;
using Northwind.ServiceBase.Common;

namespace Northwind.ServiceBase.Meta
{
	/// <summary>
	/// Clase que representa los metadatos de una respuesta
	/// </summary>
	public class Metadata
	{
		/// <summary>
		/// Url base para la construcción de los links
		/// </summary>
		private Uri _baseUri;

		#region Propiedades

		/// <summary>
		/// Índice del primer elemento
		/// </summary>
		public int Offset { get; private set; }

		/// <summary>
		/// Número límite de elementos
		/// </summary>
		public int Limit { get; private set; }

		/// <summary>
		/// Número total de elementos
		/// </summary>
		public long TotalCount { get; private set; }

		/// <summary>
		/// Lista de enlaces
		/// </summary>
		public Dictionary<MetadataUriType, String> Links { get; private set; }

		#endregion

		#region Constructores

		/// <summary>
		/// Constructor por defecto
		/// </summary>
		public Metadata()
		{
		}

		/// <summary>
		/// Constructor a partir de una lista paginada
		/// </summary>
		/// <param name="baseUri">Url base para la construcción de los enlaces</param>
		/// <param name="list"><see cref="IPagedList"/> a partir de la que se crearán los metadatos</param>
		public Metadata(Uri baseUri, IStaticList list )
		{
			Verify.ArgumentNotNull(baseUri, "baseUri");
			Verify.ArgumentNotNull(list, "list");			

			_baseUri = baseUri;
			Offset = list.FirstItemOnPage;
			Limit = list.PageSize;
			TotalCount = list.TotalItemCount;

			// Construcción de los enlaces
			Links = new Dictionary<MetadataUriType, string>();

			AddLink(MetadataUriType.Self, Offset);

			if ( !list.IsFirstPage ) AddLink(MetadataUriType.First, 1);
			if ( !list.IsLastPage ) AddLink(MetadataUriType.Last, Convert.ToInt32(TotalCount - Limit));
			if ( list.HasNextPage ) AddLink(MetadataUriType.Next, Offset + Limit);
			if ( list.HasPreviousPage ) AddLink(MetadataUriType.Previous, Limit - Offset);
		}

		#endregion

		#region Métodos privados

		/// <summary>
		/// Creación de un enlace
		/// </summary>
		/// <param name="type"><see cref="MetadataUriType"/> del enlace</param>
		/// <param name="value">Valor del parámetros</param>
		private void AddLink( MetadataUriType type, int value )
		{
			Verify.ArgumentNotNull(type, "type");

			if ( value < 0 ) value = 1;

			var newUri = _baseUri
				.AddQuery(ServiceOperations.Offset, value.ToString())
				.AddQuery(ServiceOperations.Limit, Limit.ToString());

			Links.Add(type, newUri.ToString());
		}

		#endregion
	}
}
