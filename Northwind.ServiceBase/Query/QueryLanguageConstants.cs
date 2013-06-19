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
