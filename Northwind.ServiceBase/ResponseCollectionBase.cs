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
	public abstract class ResponseCollectionBase<TDto> : IHasResponseStatus
		where TDto : class
	{
		/// <summary>
		/// Resultado que contiene la respuesta
		/// </summary>
		public IEnumerable<TDto> Result { get; set; }

		/// <summary>
		/// Constructor de la clase
		/// </summary>
		public ResponseCollectionBase()
		{
			Result = new List<TDto>();
		}

		#region Miembros de IHasResponseStatus

		/// <summary>
		/// Status de la respuesta
		/// </summary>
		public ResponseStatus ResponseStatus { get; set; }		

		#endregion
	}
}
