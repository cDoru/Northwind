﻿#region Licencia
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
