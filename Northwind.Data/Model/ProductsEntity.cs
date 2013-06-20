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

