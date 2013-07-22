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

namespace Northwind.ServiceBase.Query
{
	/// <summary>
	/// Constantes relacionados con la sinstaxis de query en la Uri
	/// </summary>
	internal static class QueryLanguageConstants
	{
		/// <summary>
		/// Constante que indica todos los resulstados
		/// </summary>
		public const String All = "*";

		/// <summary>
		/// Constante que indica el elemento inicial de una lista
		/// </summary>
		public const String Offset = "offset";

		/// <summary>
		/// Constante que indica el número límite de elementos que se recuperarán
		/// </summary>
		public const String Limit = "limit";

		/// <summary>
		/// Cosntante que indica los campos a recuperar
		/// </summary>
		public const String Select = "select";		

		/// <summary>
		/// Constante que indica las expansiones que se incluirán
		/// </summary>
		public const String Include = "include";		

		/// <summary>
		/// Constante que indica la ordenación
		/// </summary>
		public const String OrderBy = "orderby";

		/// <summary>
		/// Constante que indica una ordenación ascendente
		/// </summary>
		public const String OrderByAscending = "asc";

		/// <summary>
		/// Constante que indica una ordenación descendente
		/// </summary>
		public const String OrderByDescending = "desc";
	}
}
