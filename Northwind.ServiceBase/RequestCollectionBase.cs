using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.ServiceHost;

namespace Northwind.ServiceBase
{
	/// <summary>
	/// Clase que representa una petición básica
	/// </summary>
	/// <typeparam name="TDto">Tipo que se envía</typeparam>
	/// <typeparam name="TResponse">Tipo que se devuelve</typeparam>
	/// <seealso cref="http://stackoverflow.com/questions/12612465/servicestack-is-there-an-up-to-date-complete-documentation"/>
	public abstract class RequestColletionBase<TDto, TResponse> : RequestBase<TDto, TResponse>, ISearchable
		where TDto : class
		where TResponse : class
	{
		public RequestColletionBase()
		{
			
		}

		#region Miembros de ISearchable

		/// <summary>
		/// Condiciones de búsqueda
		/// </summary>
		public string Query { get; set; }

		/// <summary>
		/// Número de resultados
		/// </summary>
		public int Limit { get; set; }

		/// <summary>
		/// Índice del primer elemento de la lista
		/// </summary>
		public int Offset { get; set; }

		#endregion
	}
}
