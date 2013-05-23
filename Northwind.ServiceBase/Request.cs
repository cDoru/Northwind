using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.ServiceHost;

namespace Northwind.ServiceBase
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class Request<TDto, TResponse> : IReturn<TResponse>
		where TDto : IDto, new()
		where TResponse : class, new()
	{
		
	}
}
