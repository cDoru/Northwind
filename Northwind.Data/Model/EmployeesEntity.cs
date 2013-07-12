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
    }
}

