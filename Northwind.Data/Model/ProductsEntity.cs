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
using System.Web;
using System.ComponentModel.DataAnnotations;
using ServiceStack.OrmLite;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Northwind.Data.Model
{
	[Alias("Products")]
	public partial class ProductEntity : IEntity, IHasId<long> 
    {
        [Alias("Id")]
        [Required]
        public long Id { get; set;}

        [StringLength(8000)]
        public string ProductName { get; set;}
        
		[Required]
		[References(typeof(SupplierEntity))]
        public long SupplierId { get; set;}

        [Required]
		[References(typeof(CategoryEntity))]
        public long CategoryId { get; set;}

        [StringLength(8000)]
        public string QuantityPerUnit { get; set;}
        
		[Required]
        public decimal UnitPrice { get; set;}
        
		[Required]
        public long UnitsInStock { get; set;}
        
		[Required]
        public long UnitsOnOrder { get; set;}
        
		[Required]
        public long ReorderLevel { get; set;}
        
		[Required]
        public long Discontinued { get; set;}		
    }
}

