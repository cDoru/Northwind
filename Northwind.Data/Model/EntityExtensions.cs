using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ServiceStack.DataAnnotations;

namespace Northwind.Data.Model
{
	/// <summary>
	/// Clase que representa una entidad
	/// </summary>
	public static class EntityExtensions
	{
		/// <summary>
		/// Devuelve todas entidades relacionadas con la actual
		/// </summary>
		/// <returns></returns>
		public static Dictionary<string, Type> GetRelatedEntities(this IEntity entity)
		{
			var relations = new Dictionary<string, Type>();

			var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass).ToList();

			types.ForEach(
				t =>
				{
					// Obtenemos las propiedades que tienen atributo [References] sobre el tipo actual
					var props = t
						.GetProperties()
						.Where(p => p
							.GetCustomAttributes(typeof(ReferencesAttribute), true)
							.Where(a => ((ReferencesAttribute)a).Type == entity.GetType())
							.Any()
						)
						.Select(p => p)
						.All(p =>
							{
								relations.Add(p.Name, t);
								return true;
							});

				});

			return relations;
		}
	}
}
