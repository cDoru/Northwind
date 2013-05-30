﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.ServiceHost;
using Northwind.ServiceBase.Query;

namespace Northwind.ServiceBase
{	
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class CollectionRequest<TDto> : Request<TDto, CollectionResponse<TDto>>, ICollectionRequest, ISearchable<TDto>
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

		#region Miembros de ISearchable<TDto>
	
		/// <summary>
		/// 
		/// </summary>
		public QueryExpression<TDto> Query { get; set; }

		#endregion
	}
}
