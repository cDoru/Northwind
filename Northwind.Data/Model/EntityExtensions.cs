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
