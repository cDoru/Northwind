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
			
			AddNavigationLinks();				
			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <param name="totalCount"></param>
		public Metadata( IHttpRequest request, long totalCount )
			: this(request, totalCount, Convert.ToInt32(request.QueryString[ServiceOperations.Offset]), Convert.ToInt32(request.QueryString[ServiceOperations.Limit]))
		{ }

		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="type"></param>
		/// <param name="uri"></param>
		/// <returns></returns>
		private void AddLink( MetadataUriType type, int offset )
		{
			_queryString[ServiceOperations.Offset] = offset.ToString();
			Links.Add(type, String.Format(_uriFormat, _queryString.ToFormUrlEncoded()));
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
					AddLink(MetadataUriType.Next, (Offset + Limit));
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
					AddLink(MetadataUriType.Previous, (Offset - Limit));
				}
			}					
		}

		/// <summary>
		/// 
		/// </summary>
		private void AddFirstLink()
		{
			if ( Offset > 1 )
			{
				AddLink(MetadataUriType.First, 1);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		private void AddLastLink()
		{
			if ( Offset + Limit < TotalCount )
			{
				AddLink(MetadataUriType.Last, (Convert.ToInt32(TotalCount) - Limit));
			}
		}

		/// <summary>
		/// 
		/// </summary>
		private void AddNavigationLinks()
		{
			AddLink(MetadataUriType.Self, Convert.ToInt32(_queryString[ServiceOperations.Offset]));
			AddNextLink();
			AddPreviousLink();
			AddFirstLink();
			AddLastLink();
		}
	}	
}
