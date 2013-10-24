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
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Northwind.Common
{
	/// <summary>
	/// Clase con métodos de extensión para <see cref="Type"/>
	/// </summary>
	/// <seealso cref="http://rogeralsing.com/2008/02/28/linq-expressions-creating-objects/"/>
	public static class TypeExtensionHelper
	{		
		/// <summary>
		/// Crea una instancia del <paramref name="type"/> con los parámetros indicados
		/// </summary>
		/// <param name="type"><see cref="Type"/> que se instanciará</param>
		/// <param name="args">Argumentos</param>
		/// <returns>Un <see cref="System.Object"/> del tipo indicado</returns>
		public static object CreateInstance( this Type type, params object[] args )
		{
			Verify.ArgumentNotNull(type, "type");
			Verify.ArgumentNotNull(args, "args");

			// Obtención del constructor adecuado para los parámetros
			//var constructor = type.GetConstructors().First();
			var types = args.Select(a => a.GetType()).ToArray();
			var constructor = type.GetConstructor(types);
			var paramsInfo = constructor.GetParameters();

			// Creación de un parámetro de tipo object[] (parámetros variables)
			var paramExpr = Expression.Parameter(typeof(object[]), "args");
			var argsExpr = new Expression[paramsInfo.Length];

			// Creación de una expresión tipada para cada parámetro del array
			for ( var item = 0; item < paramsInfo.Length; item++ )
			{
				var index = Expression.Constant(item);
				var paramAccessorExpr = Expression.ArrayIndex(paramExpr, index);
				var paramCastExpr = Expression.Convert(paramAccessorExpr, paramsInfo[item].ParameterType);

				argsExpr[item] = paramCastExpr;
			}
			
			// Creación de una expresión que llama al constructor con los argumentos recién creados
			var newExpr = Expression.New(constructor, argsExpr);

			// Creamos la lambda con newExpr como cuerpo y param object[] como argumentos
			var lambda = Expression.Lambda(typeof(Func<object[], object>), newExpr, paramExpr);

			// Compilamos para generar el delegado
			Func<object[], object> instanceFunc = (Func<object[], object>)lambda.Compile();

			return instanceFunc(args);
		}

		/// <summary>
		/// Crea una instancia del <paramref name="type"/> indicado
		/// </summary>
		/// <param name="type"><see cref="Type"/> que se instanciará</param>
		/// <returns>Un <see cref="System.Object"/> del tipo indicado</returns>
		public static object CreateInstance( this Type type )
		{
			return CreateInstance(type, new object[] { });
		}

		/// <summary>
		/// Crea una instancia de <typeparamref name="T"/> según los argumentos indicados
		/// </summary>
		/// <typeparam name="T"><see cref="Type"/> que se creará</typeparam>
		/// <returns>Un objeto de tipo <typeparamref name="T"/></returns>
		public static T CreateInstance<T>()
		{
			return (T)CreateInstance(typeof(T));
		}

		/// <summary>
		/// Crea una instancia de <typeparamref name="T"/> según los argumentos indicados
		/// </summary>
		/// <typeparam name="T"><see cref="Type"/> que se creará</typeparam>
		/// <param name="args">Argumentos</param>
		/// <returns>Un objeto de tipo <typeparamref name="T"/></returns>
		public static T CreateInstance<T>( params object[] args )
		{
			return (T)CreateInstance(typeof(T), args);
		}
	}
}
