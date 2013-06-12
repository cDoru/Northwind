using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using ServiceStack.Text;

namespace Northwind.ServiceBase.Query.Parser
{
	/// <summary>
	/// Representa una clase de generación de expresiones de selección
	/// </summary>
	public class SelectExpressionFactory<TEntity>
	{
		#region Campos

		/// <summary>
		/// Flags por defecto para buscar las propiedades
		/// </summary>
		private const BindingFlags Flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

		/// <summary>
		/// Diccionario donde se guardarán las selecciones
		/// </summary>
		private IDictionary<string, Expression<Func<TEntity, object>>> _selections;

		/// <summary>
		/// Factoría de creación de clases dinámicas
		/// </summary>
		private ClassFactory _classFactory = new ClassFactory();

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor de la clase
		/// </summary>
		public SelectExpressionFactory()
		{
			_selections = new Dictionary<string, Expression<Func<TEntity, object>>> { { String.Empty, null } };
		}

		#endregion

		#region Métodos

		/// <summary>
		/// Crea una expresión de selección a partir de los campos indicados en <see cref="select"/>
		/// </summary>
		/// <param name="select">Campos a seleccionar</param>
		/// <returns>La expressión resultante</returns>
		/// <seealso cref="http://stackoverflow.com/questions/606104/how-to-create-linq-expression-tree-with-anonymous-type-in-it"/>
		/// <seealso cref="http://stackoverflow.com/questions/12701737/expression-to-create-an-instance-with-object-initializer"/>
		public Expression<Func<TEntity, object>> Create( string select )
		{
			if ( String.IsNullOrEmpty(select) ) return null;

			// Separamos los campos y los guardamos en una lista
			var fields = (select ?? String.Empty)
				.Split(',')
				.Where(s => !String.IsNullOrWhiteSpace(s))
				.Select(s => s.Trim());

			// Creamos una clave para identificar la selección
			var key = select;

			if ( _selections.ContainsKey(key) )
			{
				return _selections[key];
			}

			// Recuperamos todas las propieades de T
			var elementType = typeof(TEntity);
			var elementMembers = elementType
				.GetProperties(Flags)
				.Cast<PropertyInfo>()
				//.Concat(elementType.GetFields(Flags))
				.ToList();

			// Guardamos en un Dictionary la lista de propieades que se solicitan
			var sourceMembers = fields.ToDictionary(
				name => name.ToCamelCase(), 
				source => elementMembers.First(p => p.Name.Equals(source, StringComparison.InvariantCultureIgnoreCase)));

			// Necestiamos crear un tipo dinámico que contenga los campos elegidos y sus valores
			// Ej.: Cuando ejecutamos IDbConnection.Select, le pasamos como argumento un delegado que devuelve un tipo anónimo. 
			// Algo así:
			//		lista.Select<object>(s => new { s.Id, s.Name });
			// En este caso, tenemos que crear dinámicamente el tipo.
			var dynamicType = _classFactory.Create(elementType, sourceMembers.Values);

			// Creamos la expresión
			var sourceItem = Expression.Parameter(elementType, "t");
			var bindings = dynamicType.GetProperties().Select(
				p =>
				{
					var member = sourceMembers[p.Name.ToCamelCase()];
					var expression = Expression.Property(sourceItem, (PropertyInfo)member);

					return Expression.Bind(p, expression);					
				});

			// Creamos el constructor sin parámetros del tipo dinámico
			var constructor = dynamicType.GetConstructor(Type.EmptyTypes);
			
			// Creamos la expresión completa
			var selectExpr = Expression.Lambda<Func<TEntity, object>>(Expression.MemberInit(Expression.New(constructor), bindings), sourceItem);

			// Añadimos al diccionario
			_selections.Add(key, selectExpr);

			return selectExpr;
		}

		#endregion
	}
}
