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
	[Alias("Customers")]
	[ETag("Id", "LastUpdated")]
	public partial class CustomerEntity : IEntity, IHasId<string> 
    {
        [Alias("Id")]
        [StringLength(8000)]
        [Required]
        public string Id { get; set; }

        [StringLength(8000)]
        public string CompanyName { get; set; }

        [StringLength(8000)]		
        public string ContactName { get; set; }

        [StringLength(8000)]
        public string ContactTitle { get; set; }

        [StringLength(8000)]
        public string Address { get; set; }

        [StringLength(8000)]
        public string City { get; set; }

        [StringLength(8000)]
        public string Region { get; set; }

        [StringLength(8000)]
        public string PostalCode { get; set; }

        [StringLength(8000)]
        public string Country { get; set; }

        [StringLength(8000)]
        public string Phone { get; set; }

        [StringLength(8000)]
        public string Fax { get; set; }

		public DateTime LastUpdated { get; set; }

		public virtual List<OrderEntity> Orders { get; set; }
    }
}
