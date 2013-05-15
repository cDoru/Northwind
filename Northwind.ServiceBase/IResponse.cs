using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind.ServiceBase
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TResult"></typeparam>
	public interface IResponse<TDto>
		where TDto : IDto, new()
	{
		TDto Result { get; set; }
	}
}
