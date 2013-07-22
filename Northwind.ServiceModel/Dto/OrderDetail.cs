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
using Northwind.ServiceBase;
using Northwind.ServiceBase.Relations;

namespace Northwind.ServiceModel.Dto
{
	/// <summary>
	/// Clase que representa una entidad <see cref="OrderDetail"/>
	/// </summary>	
	public class OrderDetail : CommonDto
	{
		public string Id { get; set; }

		[Relation(RelationType.BelongsTo, typeof(Order))]
		public Order Order { get; set; }

		public long ProductId { get; set; }

		public decimal UnitPrice { get; set; }

		public long Quantity { get; set; }

		public double Discount { get; set; }
	}
}
