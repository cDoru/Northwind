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
using Northwind.Common;

namespace Northwind.ServiceBase.Relations
{
	/// <summary>
	/// Atributo que define una relación entre clases
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class RelationAttribute : Attribute
	{
		/// <summary>
		/// Tipo de relación
		/// </summary>
		public RelationType Type { get; private set; }

		/// <summary>
		/// Entidad destino de la relación
		/// </summary>
		public Type TargetEntity { get; private set; }

		/// <summary>
		/// Constructor de clase
		/// </summary>
		/// <param name="relationType"><see cref="RelationType"/></param>
		/// <param name="targetEntity">Entidad destino de la relación</param>
		public RelationAttribute( RelationType relationType, Type targetEntity )
		{
			Verify.ArgumentNotNull(relationType, "relationType");
			Verify.ArgumentNotNull(targetEntity, "relationType");

			Type = relationType;
			TargetEntity = targetEntity;
		}
	}
}
