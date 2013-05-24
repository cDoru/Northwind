using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Northwind.ServiceBase.Meta;

namespace Northwind.ServiceBase
{
	public class CollectionResponse<TDto> : ICollectionResponse<TDto>
		where TDto : IDto, new()
	{
		#region Miembros de ICollectionResponse<TDto>

		/// <summary>
		/// Número de elementos de la colección
		/// </summary>
		public int Count
		{
			get { return (Result != null ? Result.Count : 0); }
		}

		public List<TDto> Result { get; set; }

		public Metadata Metadata { get; set; }

		#endregion		

		#region Constructores

		/// <summary>
		/// Constructor de la clase
		/// </summary>
		public CollectionResponse()
		{
			Result = new List<TDto>();
		}

		public CollectionResponse( List<TDto> result )
		{
			Result = result;
		}

		#endregion		
		
	
	}
}
