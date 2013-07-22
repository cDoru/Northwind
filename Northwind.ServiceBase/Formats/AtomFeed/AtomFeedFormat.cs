#region Licencia
/*
   Copyright 2013 Juan Diego Martinez

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

     http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.

*/        
#endregion
          
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
