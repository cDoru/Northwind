using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;

namespace Northwind.ServiceBase
{
	/// <summary>
	/// Clase que representa una respuesta básica
	/// </summary>
	/// <typeparam name="TDto">Tipo que contiene la respuesta</typeparam>
	public abstract class ResponseBase<TDto> : IHasResponseStatus
	{
		/// <summary>
		/// Constructor
		/// </summary>
		protected ResponseBase()
		{
			ResponseStatus = new ResponseStatus();
		}

		/// <summary>
		/// Contenido de la respuesta
		/// </summary>
		public TDto Result { get; set; }

		#region Miembros de IHasResponseStatus

		/// <summary>
		/// Status de la respuesta
		/// </summary>
		public ResponseStatus ResponseStatus { get; set; } 		

		#endregion
	}
}
