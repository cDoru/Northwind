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
using ServiceStack.ServiceHost;
using Northwind.ServiceBase.Query;

namespace Northwind.ServiceBase
{	
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class CollectionRequest<TDto> : Request<TDto, CollectionResponse<TDto>>, ICollectionRequest, ISearchable
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

		#region Miembros de ISearchable
	
		/// <summary>
		/// Representa una expresión de búsqueda y filtrado de datos
		/// </summary>
		public IQueryExpression Query { get; set; }

		#endregion
	}
}
