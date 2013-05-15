using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.ServiceModel.Syndication;
using ServiceStack.ServiceHost;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints;


namespace Northwind.ServiceBase.Formats
{
	/// <summary>
	/// Clase que serializa en formato Atom Feed
	/// </summary>
	/// <seealso cref="https://github.com/ServiceStack/ServiceStack/wiki/Formats"/>
	public class AtomFeedFormat
	{
		private const String AtomFeedContentType = "application/rss+xml";

		public static void Register( IAppHost appHost )
		{
			appHost.ContentTypeFilters.Register(AtomFeedContentType, SerializeToStream, DeserializeFromStream);
		}

		public static void SerializeToStream( IRequestContext requestContext, object response, Stream stream )
		{
			using ( var xmlWriter = XmlWriter.Create(stream) )
			{
				Atom10FeedFormatter atomFormatter = new Atom10FeedFormatter(response.GetType());
				atomFormatter.WriteTo(xmlWriter);
			}
		}

		public static object DeserializeFromStream( Type type, Stream stream )
		{
			throw new NotImplementedException();
		}
	}
}
