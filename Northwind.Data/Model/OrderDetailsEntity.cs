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
	[Alias("OrderDetails")]
    public partial class OrderDetailEntity : IEntity, IHasId<string> 
    {
        [Alias("Id")]
        [StringLength(8000)]
        [Required]
        public string Id { get; set;}
        [Required]
        public long OrderId { get; set;}
        [Required]
        public long ProductId { get; set;}
        [Required]
        public decimal UnitPrice { get; set;}
        [Required]
        public long Quantity { get; set;}
        [Required]
        public double Discount { get; set;}

		public OrderEntity Order { get; set; }
		public ProductEntity Product { get; set; }
    }
}
