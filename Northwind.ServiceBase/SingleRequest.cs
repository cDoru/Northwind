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
	public class SingleRequest<TDto> : Request<TDto, SingleResponse<TDto>>
		where TDto : IDto, new()
	{
		public object Id { get; set; }
	}
}
