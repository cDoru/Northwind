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

namespace Northwind.ServiceBase.Caching
{
	/// <summary>
	/// Clase que representa una clave de caché
	/// </summary>
	public class CacheKey
	{
		private string _resourceUri;
		private NameValueCollection _headerValues;
		private string _toString = String.Empty;

		private const string CacheKeyFormat = "{0}::{1}";

		#region Constructor

		/// <summary>
		/// Constructor de la clase
		/// </summary>
		/// <param name="resourceUri">Uri del recurso</param>
		/// <param name="headerValues">Valores de los encabezados de la petición</param>
		public CacheKey( string resourceUri, NameValueCollection headerValues )
		{
			_resourceUri = resourceUri;
			_headerValues = headerValues;
		}

		#endregion		

		#region Overrides

		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals( object obj )
		{
			if ( obj == null ) return false;

			var key = obj as CacheKey;
			if ( key == null ) return false;

			return ToString() == key.ToString();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return _toString.GetHashCode();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{			
			if ( String.IsNullOrEmpty(_toString) )
			{
				_toString = String.Format(CacheKeyFormat, _resourceUri, String.Join("-", _headerValues));
			}

			return _toString;
		}

		#endregion
	}
}
