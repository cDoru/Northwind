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
	/// <seealso cref="https://github.com/ServiceStack/ServiceStack/wiki/New-Api#structured-error-handling"/>
	public abstract class ResponseBase<TDto> 
	{
		/// <summary>
		/// Constructor
		/// </summary>
		protected ResponseBase()
		{
			
		}

		/// <summary>
		/// Contenido de la respuesta
		/// </summary>
		public TDto Result { get; set; }
		
	}
}
