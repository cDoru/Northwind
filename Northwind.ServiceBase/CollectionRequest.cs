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
	public class CollectionRequest<TDto> : Request<TDto, CollectionResponse<TDto>>, ISelectable
		where TDto : IDto, new()
	{
		/// <summary>
		/// 
		/// </summary>
		public int Offset { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int Limit { get; set; }

		#region Miembros de ISelectable

		public List<string> Select { get; set; }

		#endregion
	}
}
