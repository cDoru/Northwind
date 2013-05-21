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
    public partial class CustomerEntity : IEntity, IHasId<string> 
    {
        [Alias("Id")]
        [StringLength(8000)]
        [Required]
        public string Id { get; set;}
        [StringLength(8000)]
        public string CompanyName { get; set;}
        [StringLength(8000)]
        public string ContactName { get; set;}
        [StringLength(8000)]
        public string ContactTitle { get; set;}
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
        public string Phone { get; set;}
        [StringLength(8000)]
        public string Fax { get; set;}

		public List<OrderEntity> Orders { get; set; }
    }
}
