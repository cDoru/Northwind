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

namespace Northwind.Common
{
	/// <summary>
	/// Utilidades para lanzar excepciones
	/// </summary>
	public static class Verify
	{
		/// <summary>
		/// Comprueba si value es nulo. En caso afirmativo lanza una excepción
		/// </summary>
		/// <param name="value">Valor a comprobar</param>
		/// <param name="paramName">Nombre del parámetro</param>
		public static void ArgumentNotNull( object value, string paramName )
		{
			if ( value == null )
			{
				throw new ArgumentNullException(paramName);
			}
		}

		/// <summary>
		/// Comprueba si value es nulo o una cadena vacía
		/// </summary>
		/// <param name="value">Valor a comprobar</param>
		/// <param name="paramName">Nombre del parámetro</param>
		public static void ArgumentStringNotNullOrEmpty( string value, string paramName )
		{
			if ( String.IsNullOrEmpty(value) )
			{
				throw new ArgumentNullException(paramName);
			}
		}		
	
	}
}
