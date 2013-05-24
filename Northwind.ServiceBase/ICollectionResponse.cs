using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Northwind.ServiceBase.Meta;

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
		List<TDto> Result { get; set; }
		Metadata Metadata { get; set; }
	}
}
