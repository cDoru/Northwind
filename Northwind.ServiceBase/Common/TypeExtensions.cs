﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Northwind.ServiceBase.Common
{
	/// <summary>
	/// Clase con métodos de extensión para <see cref="Type"/>
	/// </summary>
	/// <seealso cref="http://rogeralsing.com/2008/02/28/linq-expressions-creating-objects/"/>
	public static class TypeExtensions
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

			// Obtención del primer constructor
			var constructor = type.GetConstructors().First();
			var paramsInfo = constructor.GetParameters();

			// Creación de un parámetro de tipo object[] (parámetros variables)
			var paramExpr = Expression.Parameter(typeof(object[]), "args");
			var argsExpr = new Expression[paramsInfo.Length];

			// Creación de una expresión tipada para cada parámetro del array
			for ( var item = 0; item < paramsInfo.Length; item++ )
			{
				var index = Expression.Constant("i");
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
			
	}
}