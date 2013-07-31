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
	[Alias("Employees")]
	[ETag("Id", "LastUpdated")]
	public partial class EmployeeEntity : IEntity, IHasId<long> 
    {
        [Alias("Id")]
        [Required]
        public long Id { get; set;}

        [StringLength(8000)]
        public string LastName { get; set;}

        [StringLength(8000)]
        public string FirstName { get; set;}

        [StringLength(8000)]
        public string Title { get; set;}

        [StringLength(8000)]
        public string TitleOfCourtesy { get; set;}

        [StringLength(8000)]
        public string BirthDate { get; set;}

        [StringLength(8000)]
        public string HireDate { get; set;}

        [StringLength(8000)]
        public string Address { get; set;}

        [StringLength(8000)]
        public string City { get; set;}

        [StringLength(8000)]
        public string Region { get; set;}

        [StringLength(8000)]
        public string PostalCode { get; set;}

        [StringLength(8000)]
        public string Country { get; set;}

        [StringLength(8000)]
        public string HomePhone { get; set;}

        [StringLength(8000)]
        public string Extension { get; set;}

        public string Photo { get; set;}

        [StringLength(8000)]
        public string Notes { get; set;}

        public long? ReportsTo { get; set;}

        [StringLength(8000)]
        public string PhotoPath { get; set;}

		public DateTime LastUpdated { get; set; }
    }
}

