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
		where TDto : class
	{
		/// <summary>
		/// Resultado que contiene la respuesta
		/// </summary>
		public IList<TDto> Result { get; set; }

		/// <summary>
		/// Constructor de la clase
		/// </summary>
		protected ResponseCollectionBase()
		{
			Result = new List<TDto>();
		}		
	}
}
