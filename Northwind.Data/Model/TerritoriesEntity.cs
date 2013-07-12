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
	[Alias("Territories")]
	public partial class TerritoryEntity : IEntity, IHasId<string> 
    {
        [Alias("Id")]
        [StringLength(8000)]
        [Required]
        public string Id { get; set;}

        [StringLength(8000)]
        public string TerritoryDescription { get; set;}
        
		[Required]
		[References(typeof(RegionEntity))]
        public long RegionId { get; set;}		
    }
}
