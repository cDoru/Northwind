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
		/// Constante que indica el elemento inicial de una lista
		/// </summary>
		internal const String Offset = "offset";

		/// <summary>
		/// Constante que indica el número límite de elementos que se recuperarán
		/// </summary>
		internal const String Limit = "limit";

		/// <summary>
		/// Cosntante que indica los campos a recuperar
		/// </summary>
		internal const String Select = "select";

		/// <summary>
		/// Constante que indica las expansiones que se incluirán
		/// </summary>
		internal const String Include = "include";

		/// <summary>
		/// Constante que indica que se deben expandir todas las propiedades de navegación
		/// </summary>
		internal const String IncludeAll = "all";

		/// <summary>
		/// Constante que indica la ordenación
		/// </summary>
		internal const String OrderBy = "orderby";

		/// <summary>
		/// Constante que indica una ordenación ascendente
		/// </summary>
		internal const String OrderByAscending = "asc";

		/// <summary>
		/// Constante que indica una ordenación descendente
		/// </summary>
		internal const String OrderByDescending = "desc";
	}
}
