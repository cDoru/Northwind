using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using ServiceStack.Common;
using ServiceStack.OrmLite;
using ServiceStack.Text;
using Northwind.Common;
using Northwind.Data.Model;
using Northwind.Data.Expressions;

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
			// TODO: hay un bug en OrmLite que impide hacer esto directamente
			//ev.Select(selector);

			var modelDef = ModelDefinition<T>.Definition;
			var visitor = new SqlSelectExpressionTranslator();

			var selectStr = GenerateSelectExpression(ModelDefinition<T>.Definition, visitor.Translate(selector), false);
			ev.Select(selectStr);
			
			return dbConn.Select(ev);			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fields"></param>
		/// <param name="distinct"></param>
		/// <returns></returns>
		private static String GenerateSelectExpression(ModelDefinition modelDef, String fields, bool distinct )
		{
			return String.Format("SELECT {0}{1} \n FROM {2}", 
				(distinct ? "DISTINCT " : ""),
				(String.IsNullOrEmpty(fields) ? OrmLiteConfig.DialectProvider.GetColumnNames(modelDef) : fields),
				OrmLiteConfig.DialectProvider.GetQuotedTableName(modelDef));
		}
	}
}
