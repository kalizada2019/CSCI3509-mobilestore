using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
 
namespace AspMvcWebshop.Models
{
    [Bind(Exclude = "ProductId")]
    public class Product
    {
        [ScaffoldColumn(false)]
        public int productId { get; set; }
        [DisplayName("Category")]
        public int categoryId { get; set; }
        [DisplayName("Brand")]
        public int brandId { get; set; }
        [DisplayName("Product Name")]
        [Required(ErrorMessage = "Product Name is mandatory")]
        [StringLength(160)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 7000.00,
            ErrorMessage = "Price must be between 0.01 and 7000.00")]
        public decimal Price { get; set; }
        [DisplayName("Product Image URL")]
        [StringLength(1024)]
        public string ProductImg { get; set; }
        public virtual Category Category { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}