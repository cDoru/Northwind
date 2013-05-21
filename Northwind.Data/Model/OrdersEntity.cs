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
	[Alias("Orders")]
    public partial class OrderEntity : IEntity, IHasId<long> 
    {
        [Alias("Id")]
        [Required]
        public long Id { get; set;}
        [StringLength(8000)]
        public string CustomerId { get; set;}
        [Required]
        public long EmployeeId { get; set;}
        [StringLength(8000)]
        public string OrderDate { get; set;}
        [StringLength(8000)]
        public string RequiredDate { get; set;}
        [StringLength(8000)]
        public string ShippedDate { get; set;}
        public long? ShipVia { get; set;}
        [Required]
        public decimal Freight { get; set;}
        [StringLength(8000)]
        public string ShipName { get; set;}
        [StringLength(8000)]
        public string ShipAddress { get; set;}
        [StringLength(8000)]
        public string ShipCity { get; set;}
        [StringLength(8000)]
        public string ShipRegion { get; set;}
        [StringLength(8000)]
        public string ShipPostalCode { get; set;}
        [StringLength(8000)]
        public string ShipCountry { get; set;}

		public CustomerEntity Customer { get; set; }
		public List<OrderDetailEntity> Details { get; set; }
		public EmployeeEntity Employee { get; set; }

    }
}

