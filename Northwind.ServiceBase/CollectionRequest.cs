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
	public class CollectionRequest<TDto> : Request<TDto, CollectionResponse<TDto>>
		where TDto : IDto, new()
	{

	}
}
