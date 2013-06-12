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
