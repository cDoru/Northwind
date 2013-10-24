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
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace Northwind.Common
{
	/// <summary>
	/// Clases de extensión para <see cref="Uri"/>
	/// </summary>
	public static class UriExtensionHelper
	{
		/// <summary>
		/// Devuelve la información de consulta de una <see cref="Uri"/> en forma de <see cref="HttpQueryValueCollection"/>
		/// </summary>
		/// <param name="uri"><see cref="Uri"/> a consultar</param>
		/// <returns><see cref="HttpQueryValueCollection"/> con la información de consulta</returns>
		public static HttpQueryValueCollection GetQuery( this Uri uri )
		{
			Verify.ArgumentNotNull(uri, "uri");

			return new HttpQueryValueCollection(uri);
		}		

		/// <summary>
		/// Añade una nueva clave de consulta a una <see cref="Uri"/>
		/// </summary>
		/// <remarks>
		/// Si la clave ya existe en la colección, se modifica con el valor indicado
		/// </remarks>
		/// <param name="uri"><see cref="Uri"/> a modificar</param>
		/// <param name="key">Clave que se añadirá</param>
		/// <param name="value">Valor de la clave</param>
		/// <returns><see cref="Uri"/> con la nueva clave de consulta</returns>
		public static Uri AddQuery( this Uri uri, string key, string value )
		{
			Verify.ArgumentNotNull(uri, "uri");

			var builder = new UriBuilder(uri);
			var queryCol = uri.GetQuery();			

			// Comprobamos si la clave existe en la colección
			if ( queryCol.AllKeys.Contains(key) )
			{
				queryCol[key] = value;
			}
			else
			{
				queryCol.Add(key, value);				
			}

			builder.Query = queryCol.ToString();

			return new Uri(builder.ToString());
		}


	}
}
