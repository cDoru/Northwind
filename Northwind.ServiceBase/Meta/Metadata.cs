using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using ServiceStack.ServiceHost;
using ServiceStack.Text;
using Northwind.ServiceBase.Common;

namespace Northwind.ServiceBase.Meta
{
	/// <summary>
	/// Clase que representa los metadatos de una <see cref="IResponse"/>
	/// </summary>
	public class Metadata
	{
		private String _uriFormat = String.Empty;
		private NameValueCollection _queryString;

		#region Propiedades

		/// Lista de enlaces
		public Dictionary<MetadataUriType, String> Links { get; private set; }

		/// Total de elementos
		public long TotalCount { get; private set; }

		/// Elementos actuales
		public int Offset { get; private set; }

		// Límite actual
		public int Limit { get; private set; }

		#endregion

		#region Constructores

		/// <summary>
		/// 
		/// </summary>
		public Metadata()
		{

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="absoluteUri"></param>
		/// <param name="totalCount"></param>
		/// <param name="offset"></param>
		/// <param name="limit"></param>
		public Metadata( IHttpRequest request, long totalCount, int offset, int limit )
		{
			TotalCount = totalCount;
			Offset = offset;
			Limit = limit;
			Links = new Dictionary<MetadataUriType, String>();

			_uriFormat = request.GetPathUrl() + "?{0}";
			_queryString = new NameValueCollection(request.QueryString);

			AddLink(MetadataUriType.Self, request.AbsoluteUri);
			AddNextLink();
			AddPreviousLink();
			AddFirstLink();
			AddLastLink();						
		}		

		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="type"></param>
		/// <param name="uri"></param>
		/// <returns></returns>
		private void AddLink( MetadataUriType type, String uri )
		{
			Links.Add(type, uri);
		}

		/// <summary>
		/// 
		/// </summary>
		private void AddNextLink()
		{
			if ( Offset > 0 && Limit > 0 )
			{
				// Link a la siguiente página
				if ( Offset + Limit < TotalCount )
				{
					_queryString[ServiceOperations.Offset] = (Offset + Limit).ToString();
					AddLink(MetadataUriType.Next, String.Format(_uriFormat, _queryString.ToFormUrlEncoded()));
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		private void AddPreviousLink()
		{
			if ( Offset > 0 && Limit > 0 )
			{
				// Link a la página anterior
				if ( Offset - Limit > 0 )
				{
					_queryString[ServiceOperations.Offset] = (Offset - Limit).ToString();
					AddLink(MetadataUriType.Previous, String.Format(_uriFormat, _queryString.ToFormUrlEncoded()));
				}
			}					
		}

		/// <summary>
		/// 
		/// </summary>
		private void AddFirstLink()
		{
			_queryString[ServiceOperations.Offset] = "1";
			AddLink(MetadataUriType.First, String.Format(_uriFormat, _queryString.ToFormUrlEncoded()));
		}

		/// <summary>
		/// 
		/// </summary>
		private void AddLastLink()
		{
			_queryString[ServiceOperations.Offset] = (TotalCount - Limit).ToString();
			AddLink(MetadataUriType.Last, String.Format(_uriFormat, _queryString.ToFormUrlEncoded()));
		}
	}	
}
