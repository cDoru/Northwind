using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind.ServiceBase
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TDto"></typeparam>
	public interface ICollectionResponse<TDto> 
		where TDto : IDto, new()
	{
		int Count { get; }
		IList<TDto> Result { get; set; }
	}
}
