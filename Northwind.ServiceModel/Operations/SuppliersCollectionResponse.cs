﻿#region Licencia
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
using Northwind.ServiceBase;
using Northwind.ServiceModel.Dto;

namespace Northwind.ServiceModel.Operations
{
	/// <summary>
	/// Clase que representa una respuesta para una colección de <seealso cref="Supplier"/>
	/// </summary>
	public class SuppliersCollectionResponse : CollectionResponse<Supplier>
	{
		/// <summary>
		/// Lista de <see cref="Supplier"/>
		/// </summary>
		public List<Supplier> Suppliers
		{
			get { return base.Result; }
			set { base.Result = value; }
		}

		#region Constructores

		/// <summary>
		/// Constructor de la clase
		/// </summary>
		public SuppliersCollectionResponse()
			: base()
		{ }

		/// <summary>
		/// Constructor de la clase
		/// </summary>
		/// <param name="suppliers"></param>
		public SuppliersCollectionResponse( List<Supplier> suppliers )
			: base(suppliers)
		{ }		

		#endregion
	}
}
