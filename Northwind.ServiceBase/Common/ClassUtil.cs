using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Northwind.ServiceBase.Common
{
	/// <summary>
	/// Clases de ayuda
	/// </summary>
	internal static class ClassUtil
	{
		/// <summary>
		/// Comprueba si TDto tiene una propiedad Id
		/// </summary>
		/// <typeparam name="TDto"></typeparam>
		/// <returns></returns>
		public static bool HasId(object obj)
		{
			return obj.GetType().GetProperties().Any(p => p.Name == "Id");
		}

		/// <summary>
		/// Obtiene el valor de la propiedad Id de TDto
		/// </summary>
		/// <typeparam name="TDto"></typeparam>
		/// <returns></returns>
		public static object GetIdValue(object dto)
		{
			return (HasId(dto) ? dto.GetType().GetProperty("Id").GetValue(dto, null) : null);

		}		
				
	}
}
