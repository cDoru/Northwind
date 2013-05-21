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
	[Alias("EmployeeTerritories")]
    public partial class EmployeeTerritoryEntity : IEntity, IHasId<string> 
    {
        [Alias("Id")]
        [StringLength(8000)]
        [Required]
        public string Id { get; set;}
        [Required]
        public long EmployeeId { get; set;}
        [StringLength(8000)]
        public string TerritoryId { get; set;}

		public EmployeeEntity Employee { get; set; }
		public TerritoryEntity Territory { get; set; }
    }
}
