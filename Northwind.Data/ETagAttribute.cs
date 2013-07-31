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
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Northwind.Common;

namespace Northwind.Data
{
	/// <summary>
	/// Este atributo con una clase identifica las propiedades que indicarán cambios en el contenido
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class ETagAttribute : Attribute
	{
		public readonly ReadOnlyCollection<string> PropertyNames { get; private set; }

		/// <summary>
		/// Constructor de la clase
		/// </summary>
		/// <param name="propertyName"></param>
		public ETagAttribute( string propertyName )
		{
			Verify.ArgumentStringNotNullOrEmpty(propertyName, "propertyName");

			PropertyNames = new ReadOnlyCollection<string>(new string[] { propertyName });
		}

		/// <summary>
		/// Constructor de la clase
		/// </summary>
		/// <param name="propertyNames"></param>
		public ETagAttribute( params string[] propertyNames )
		{
			Verify.ArgumentNotNull(propertyNames, "propertyNames");

			PropertyNames = new ReadOnlyCollection<string>(propertyNames);
		}
	}
}
