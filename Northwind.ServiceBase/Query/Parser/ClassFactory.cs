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
	/// <seealso cref="http://msdn.microsoft.com/en-us/library/5kyyyeh2(v=vs.100).aspx"/>
	/// <remarks>Adaptado de System.Linq.Dynamic</remarks>
	internal class ClassFactory
	{
		#region Campos

		/// <summary>
		/// Nombre del ensamblado virtual que se necesita para la creación de tipos
		/// </summary>
		private static readonly AssemblyName AssemblyName = new AssemblyName { Name = "QueryLanguageFeatureClass" };

		/// <summary>
		/// Atributos por defecto para una clase
		/// </summary>
		private static readonly TypeAttributes ClassAttributes = TypeAttributes.Class | TypeAttributes.Public | TypeAttributes.Serializable;

		/// <summary>
		/// Módulo de ensamblado dinámico
		/// </summary>
		private static ModuleBuilder _moduleBuilder;

		/// <summary>
		/// Número de instancias creadas
		/// </summary>
		private static ConcurrentDictionary<ClassSignature, Type> CreatedClasses = new ConcurrentDictionary<ClassSignature, Type>();

		/// <summary>
		/// Attributos creados por cada propiedad para cada tipo
		/// </summary>
		private static ConcurrentDictionary<Type, IEnumerable<CustomAttributeBuilder>> ClassAttributeBuilders = new ConcurrentDictionary<Type, IEnumerable<CustomAttributeBuilder>>();

		/// <summary>
		/// Attributos para cada propiedad creados por cada tipo
		/// </summary>
		private static ConcurrentDictionary<PropertyInfo, IEnumerable<CustomAttributeBuilder>> PropertyAttributeBuilders = new ConcurrentDictionary<PropertyInfo, IEnumerable<CustomAttributeBuilder>>();

		/// <summary>
		/// Gestor de bloqueos
		/// </summary>
		private static ReaderWriterLock _rwLock;

		/// <summary>
		/// Nombre por defecto para todas las clases
		/// </summary>
		private const string DefaultClassNameSuffix = "QueryLanguageClass";		

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
				.DefineDynamicAssembly(AssemblyName, AssemblyBuilderAccess.Run)
				.DefineDynamicModule(AssemblyName.Name);

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
				return CreatedClasses.GetOrAdd(
					new ClassSignature(properties),
					c =>
					{
						return CreateClass(sourceType, properties);
					});								

			}
			finally
			{
				_rwLock.ReleaseReaderLock();
			}
		}

		#endregion

		#region Métodos privados

		#region CreateClass
		/// <summary>
		/// 
		/// </summary>
		/// <param name="properties"></param>
		/// <returns></returns>
		private Type CreateClass(Type sourceType, IEnumerable<PropertyInfo> properties )
		{
			LockCookie cookie = _rwLock.UpgradeToWriterLock(Timeout.Infinite);

			try
			{
				var typeName = sourceType.Name + DefaultClassNameSuffix + (CreatedClasses.Count + 1).ToString();
				var typeBuilder = _moduleBuilder.DefineType(typeName, ClassAttributes);

				// Establecemos los mismos atributos que la clase original
				SetAttributes(typeBuilder, sourceType);

				// Creamos las propiedades
				CreateProperties(typeBuilder, properties);

				var type = typeBuilder.CreateType();

				return type;
			}
			finally
			{
				_rwLock.DowngradeFromWriterLock(ref cookie);
			}
		}

		#endregion

		#region CreateProperties
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
		#endregion

		#region #region CreateProperty
		/// <summary>
		/// Creación de una propiedad
		/// </summary>
		/// <param name="typeBuilder"><see cref="TypeBuilder"/> donde se creará la propiedad</param>
		/// <param name="property"><see cref="PropertyInfo"/> de la propiedad a crear</param>
		/// <seealso cref="http://msdn.microsoft.com/en-us/library/t17athh1(v=vs.100).aspx"/>
		/// <seealso cref="http://msdn.microsoft.com/en-us/library/h1zby21a(v=vs.100).aspx"/>
		private void CreateProperty( TypeBuilder typeBuilder, PropertyInfo property )
		{
			// Creación del campo
			var fieldBuilder = typeBuilder.DefineField("_" + property.Name, property.PropertyType, FieldAttributes.Private);
			// Creación de la propiedad
			var propBuilder = typeBuilder.DefineProperty(property.Name, PropertyAttributes.HasDefault, property.PropertyType, null);

			// Establecemos los atributos
			SetAttributes(propBuilder, property);

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

		#region CreateCustomAttributeBuilder

		/// <summary>
		/// Creación de un constructor de atributos
		/// </summary>
		/// <param name="attributes">Datos de los atributos a construir</param>
		/// <returns><see cref="IEnumerable"/> con los constructores de atributos</returns>
		/// <seealso cref="http://msdn.microsoft.com/es-es/library/system.reflection.emit.customattributebuilder.aspx"/>
		private IEnumerable<CustomAttributeBuilder> CreateCustomAttributeBuilders( IEnumerable<CustomAttributeData> attributes )
		{
			Verify.ArgumentNotNull(attributes, "attributes");

			var attrBuilders = attributes
				.Select(attr =>
					{
						var namedArgs = attr.NamedArguments;
						var props = namedArgs.Select(a => a.MemberInfo).OfType<PropertyInfo>().ToArray();
						var values = namedArgs.Select(a => a.TypedValue.Value).ToArray();
						var constructorArgs = attr.ConstructorArguments.Select(ca => ca.Value).ToArray();
						var constructorAttr = attr.Constructor;

						return new CustomAttributeBuilder(constructorAttr, constructorArgs, props, values);
					});

			return attrBuilders;
		}

		#endregion

		#region SetAttributes(TypeBuilder)

		/// <summary>
		/// Creación de atributos para la clase según el tipo indicado
		/// </summary>
		/// <param name="typeBuilder">Constructor de tipo</param>
		/// <param name="type">Tipo fuente de los atributos</param>
		private void SetAttributes( TypeBuilder typeBuilder, Type type )
		{
			Verify.ArgumentNotNull(typeBuilder, "typeBuilder");
			Verify.ArgumentNotNull(type, "type");

			var attrBuilders = ClassAttributeBuilders
				.GetOrAdd(type,
						  t =>
						  {
							  var customAttr = t.GetCustomAttributesData();
							  return CreateCustomAttributeBuilders(customAttr);
						  });

			attrBuilders.ForEach(ab => typeBuilder.SetCustomAttribute(ab));
		}

		#endregion

		#region SetAttributes(PropertyBuilder)

		/// <summary>
		/// Establece los atributos para la propiedad indicada
		/// </summary>
		/// <param name="propertyBuilder">Constructor de propiedades</param>
		/// <param name="propertyInfo">Información de la propiedad</param>
		private void SetAttributes( PropertyBuilder propertyBuilder, PropertyInfo propertyInfo )
		{
			Verify.ArgumentNotNull(propertyBuilder, "propertyBuilder");
			Verify.ArgumentNotNull(propertyInfo, "propertyInfo");

			var propBuilders = PropertyAttributeBuilders
				.GetOrAdd(propertyInfo,
						  p =>
						  {
							  var customAttr = p.GetCustomAttributesData();
							  return CreateCustomAttributeBuilders(customAttr);
						  });

			propBuilders.ForEach(pb => propertyBuilder.SetCustomAttribute(pb));
		}

		#endregion

		#endregion

	}
}
