using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using ServiceStack.Common.Extensions;
using Northwind.ServiceBase.Common;

namespace Northwind.ServiceBase.Query.Parser
{
	/// <summary>
	/// Define una clase que genera tipos de manera dinámica
	/// </summary>
	/// <seealso cref="http://mironabramson.com/blog/post/2008/06/Create-you-own-new-Type-and-use-it-on-run-time-(C).aspx"/>
	/// <remarks>Adaptado de System.Linq.Dynamic</remarks>
	internal class ClassFactory
	{
		#region Campos

		/// <summary>
		/// Nombre del ensamblado virtual que se necesita para la creación de tipos
		/// </summary>
		private static readonly AssemblyName _assemblyName = new AssemblyName { Name = "QueryLanguageFeatureClass" };

		/// <summary>
		/// Atributos por defecto para una clase
		/// </summary>
		private static readonly TypeAttributes _defaultClassAttr = TypeAttributes.Class | TypeAttributes.Public | TypeAttributes.Serializable;

		/// <summary>
		/// Módulo de ensamblado dinámico
		/// </summary>
		private static ModuleBuilder _moduleBuilder;

		/// <summary>
		/// Número de instancias creadas
		/// </summary>
		private static ConcurrentDictionary<ClassSignature, Type> _createdClasses = new ConcurrentDictionary<ClassSignature, Type>();

		/// <summary>
		/// Gestor de bloqueos
		/// </summary>
		private static ReaderWriterLock _rwLock;

		#endregion

		#region Constructores		

		/// <summary>
		/// 
		/// </summary>
		static ClassFactory()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		public ClassFactory()
		{
			_moduleBuilder = Thread
				.GetDomain()
				.DefineDynamicAssembly(_assemblyName, AssemblyBuilderAccess.Run)
				.DefineDynamicModule(_assemblyName.Name);

			_rwLock = new ReaderWriterLock();
		}		

		#endregion

		#region Métodos públicos

		/// <summary>
		/// Obtiene el <see cref="Type"/> que concuerda con las propiedades indicadas
		/// </summary>
		/// <param name="sourceType"><see cref="Type"/> desde el que crear el nuevo tipo</param>
		/// <param name="properties">Propiedades que se utilizarán</param>
		/// <returns>Un nuevo <see cref="Type"/> con las propiedades indicadas</returns>
		public Type Create( Type sourceType, IEnumerable<PropertyInfo> properties )
		{	
			// Comprobamos si hay propiedades
			if ( !properties.Any() )
			{
				throw new ArgumentOutOfRangeException("properties");
			}

			var dictionary = properties.ToDictionary(p => p.Name, propInfo => propInfo);

			_rwLock.AcquireReaderLock(Timeout.Infinite);

			try
			{
				return _createdClasses.GetOrAdd(
					new ClassSignature(properties),
					c =>
					{
						return CreateClass(properties);
					});								

			}
			finally
			{
				_rwLock.ReleaseReaderLock();
			}
		}

		#endregion

		#region Métodos privados

		/// <summary>
		/// 
		/// </summary>
		/// <param name="properties"></param>
		/// <returns></returns>
		private Type CreateClass( IEnumerable<PropertyInfo> properties )
		{
			LockCookie cookie = _rwLock.UpgradeToWriterLock(Timeout.Infinite);

			try
			{
				var typeName = "QueryClass" + (_createdClasses.Count + 1).ToString();
				var typeBuilder = _moduleBuilder.DefineType(typeName, _defaultClassAttr);
				CreateProperties(typeBuilder, properties);

				var type = typeBuilder.CreateType();

				return type;
			}
			finally
			{
				_rwLock.DowngradeFromWriterLock(ref cookie);
			}
		}

		/// <summary>
		/// Creación de propiedades
		/// </summary>
		/// <param name="typeBuilder"></param>
		/// <param name="properties"></param>
		/// <returns></returns>
		private void CreateProperties( TypeBuilder typeBuilder, IEnumerable<PropertyInfo> properties )
		{
			Verify.ArgumentNotNull(typeBuilder, "typeBuilder");
			Verify.ArgumentNotNull(properties, "properties");			

			properties.ForEach(p => CreateProperty(typeBuilder, p));
		}

		/// <summary>
		/// Creación de una propiedad
		/// </summary>
		/// <param name="typeBuilder"><see cref="TypeBuilder"/> donde se creará la propiedad</param>
		/// <param name="property"><see cref="PropertyInfo"/> de la propiedad a crear</param>
		private void CreateProperty( TypeBuilder typeBuilder, PropertyInfo property )
		{
			// Creación del campo
			var fieldBuilder = typeBuilder.DefineField("_" + property.Name, property.PropertyType, FieldAttributes.Private);
			// Creación de la propiedad
			var propBuilder = typeBuilder.DefineProperty(property.Name, PropertyAttributes.HasDefault, property.PropertyType, null);
			// Creación del getter
			var getter = typeBuilder.DefineMethod(
				"get_" + property.Name,
				MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig,
				property.PropertyType,
				Type.EmptyTypes);

			// Generación del código IL para el getter
			ILGenerator getterGen = getter.GetILGenerator();
			getterGen.Emit(OpCodes.Ldarg_0);
			getterGen.Emit(OpCodes.Ldfld, fieldBuilder);
			getterGen.Emit(OpCodes.Ret);

			// Creación del setter
			var setter = typeBuilder.DefineMethod(
				"set_" + property.Name,
				MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig,
				null,
				new Type[] { property.PropertyType });

			// Generación del código IL para el setter
			ILGenerator setterGen = setter.GetILGenerator();
			setterGen.Emit(OpCodes.Ldarg_0);
			setterGen.Emit(OpCodes.Ldarg_1);
			setterGen.Emit(OpCodes.Stfld, fieldBuilder);
			setterGen.Emit(OpCodes.Ret);

			propBuilder.SetGetMethod(getter);
			propBuilder.SetSetMethod(setter);
		}

		#endregion

	}
}
