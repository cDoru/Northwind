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
using System.Web;
using ServiceStack;
using ServiceStack.WebHost.Endpoints.Extensions;
using Northwind.Common.Collections;
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

		/// <summary>
		/// Resultado
		/// </summary>
		public List<TDto> Result { get; set; }

		/// <summary>
		/// Metadatos
		/// </summary>
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

		/// <summary>
		/// Constructor de la clase
		/// </summary>
		/// <param name="result">Resultados de la respuesta</param>
		public CollectionResponse( List<TDto> result )
		{
			Result = result;
		}

		/// <summary>
		/// Constructor de la clase
		/// </summary>
		/// <param name="result">Resultado de la respuesta</param>
		/// <param name="offset">Primer elemento de la respuesta</param>
		/// <param name="limit">Número de elementos de la respuesta</param>
		/// <param name="totalCount">Número total de elementos</param>
		public CollectionResponse( List<TDto> result, int offset, int limit, long totalCount ) 
			: this(result)
		{
			var staticList = new StaticList<TDto>(result, offset, limit, totalCount);

			Metadata = new Metadata(HttpContext.Current.Request.Url, staticList);
		}

		#endregion
	
	}
}
