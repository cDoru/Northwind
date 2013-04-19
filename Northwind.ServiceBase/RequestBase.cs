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
	public abstract class RequestBase<TDto, TResponse> : IReturn<TResponse>
		where TDto : class
		where TResponse : class
	{
		public RequestBase()
		{
		}
	}
}
