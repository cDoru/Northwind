using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.ServiceHost;

namespace Northwind.ServiceBase.Meta
{
	/// <summary>
	/// Clase con métodos de extensión para obtener metadatos de una lista
	/// </summary>
	public static class MetadataExtensions
	{
		/// <summary>
		/// Obtención de los metadatos para un <see cref="IEnumerable<T>"/>
		/// </summary>
		/// <typeparam name="TDto"></typeparam>
		/// <param name="list"></param>
		/// <returns></returns>
		public static Metadata GetMetadata<TDto>( this List<TDto> list, IHttpRequest request, long count ) where TDto : IDto, new()
		{
			return new Metadata(request, count);
		}
	}
}
