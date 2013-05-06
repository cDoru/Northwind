using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.ServiceInterface.ServiceModel;

namespace Northwind.ServiceBase
{
	/// <summary>
	/// Clase que representa una respuesta básica cuando hay una colección de elementos
	/// </summary>
	/// <typeparam name="TDto">Tipo que contiene la respuesta</typeparam>
	/// <seealso cref="https://github.com/ServiceStack/ServiceStack/wiki/New-Api#structured-error-handling"/>
	public abstract class ResponseCollectionBase<TDto> 
		where TDto : class, new()
	{
		#region Propiedades

		/// <summary>
		/// Resultado que contiene la respuesta
		/// </summary>
		public IList<TDto> Result { get; set; }

		/// <summary>
		/// Número de elementos de la colección
		/// </summary>
		public int Count 
		{
			get { return (Result != null ? Result.Count : 0); }
		}
		#endregion

		#region Constructores

		/// <summary>
		/// Constructor de la clase
		/// </summary>
		public ResponseCollectionBase()
		{
			Result = new List<TDto>();
		}

		#endregion
	}
}
