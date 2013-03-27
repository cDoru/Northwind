// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ServiceStack.OrmLite;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Northwind
{
	[Alias("Products")]
    public partial class ProductEntity : IHasId<int> 
    {
        [Alias("Product ID")]
        [AutoIncrement]
        public int Id { get; set;}
        [Alias("Supplier ID")]
        public int? SupplierID { get; set;}
        [Alias("Category ID")]
        public int? CategoryID { get; set;}
        [Alias("Product Name")]
        [Required]
        public string ProductName { get; set;}
        [Alias("English Name")]
        public string EnglishName { get; set;}
        [Alias("Quantity Per Unit")]
        public string QuantityPerUnit { get; set;}
        [Alias("Unit Price")]
        public decimal? UnitPrice { get; set;}
        [Alias("Units In Stock")]
        public short? UnitsInStock { get; set;}
        [Alias("Units On Order")]
        public short? UnitsOnOrder { get; set;}
        [Alias("Reorder Level")]
        public short? ReorderLevel { get; set;}
        [Required]
        public bool Discontinued { get; set;}
    }
}
#pragma warning restore 1591
