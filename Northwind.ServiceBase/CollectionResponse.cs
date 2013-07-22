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
