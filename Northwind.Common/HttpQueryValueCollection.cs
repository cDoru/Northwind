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
	/// Clase que representa una colección de elementos de consulta en una Url
	/// </summary>
	public class HttpQueryValueCollection : NameValueCollection
	{
		#region Constructores de la clase

		/// <summary>
		/// Constructor de la clase
		/// </summary>
		public HttpQueryValueCollection()
			: base()
		{
		}

		/// <summary>
		/// Constructor de la clase
		/// </summary>
		/// <param name="col"><see cref="NameValueCollection"/> a partir de la cual se generará</param>
		public HttpQueryValueCollection( NameValueCollection col )
			: base(col)
		{
		}

		/// <summary>
		/// Constructor de la clase
		/// </summary>
		/// <param name="uri"><see cref="Uri"/> a partir de la cual se generará</param>
		public HttpQueryValueCollection( Uri uri )
			: base(HttpUtility.ParseQueryString(uri.Query))
		{
		}

		/// <summary>
		/// Constructor de la clase
		/// </summary>
		/// <param name="query">Cadena a partir de la cual se generará</param>
		public HttpQueryValueCollection( string query )
			: base(HttpUtility.ParseQueryString(query))
		{
		}

		#endregion

		/// <summary>
		/// Devuelve la representación de la clase en forma de <see cref="String"/>
		/// </summary>
		/// <returns>Representación de la clase en forma de <see cref="String"/></returns>
		public override string ToString()
		{
			var list = new List<string>();

			foreach ( string key in Keys )
			{
				list.Add(String.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(this[key])));
			}

			return String.Join("&", list);
		}
	}
}
