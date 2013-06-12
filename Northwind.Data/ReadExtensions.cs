using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using ServiceStack.Common;
using ServiceStack.OrmLite;
using Northwind.Common;
using Northwind.Data.Model;

namespace Northwind.Data
{
	/// <summary>
	/// Clase con métodos de extensión que complementan los de OrmLite
	/// </summary>
	internal static class ReadExtensions
	{		
		/// <summary>
		/// Devuelve en una lista los datos que corresponden únicamente a las propiedades indicadas en <paramref name="selector"/>
		/// </summary>
		/// <typeparam name="T">Tipo que se devolverá</typeparam>
		/// <param name="dbConn">Conexión</param>
		/// <param name="selector">Expresión que contiene una clase anónica con las propiedades que se recuperarán</param>
		/// <returns><see cref="List"/> con el resultado</returns>
		public static List<T> Select<T>( this IDbConnection dbConn, Expression<Func<T, object>> selector )
		{
			Verify.ArgumentNotNull(dbConn, "dbConn");
			Verify.ArgumentNotNull(selector, "selector");

			var ev = dbConn.CreateExpression<T>();
			ev.Select<object>(selector);			
			
			return dbConn.Select(ev);
		}
	}
}
