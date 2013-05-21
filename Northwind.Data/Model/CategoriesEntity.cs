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
	[Alias("Categories")]
    public partial class CategoryEntity : IEntity, IHasId<long> 
    {
        [Alias("Id")]
        [Required]
        public long Id { get; set;}
        [StringLength(8000)]
        public string CategoryName { get; set;}
        [StringLength(8000)]
        public string Description { get; set;}

		public List<ProductEntity> Products { get; set; }
    }
}

