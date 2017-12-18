using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 
namespace AspMvcWebshop.Models
{
    public partial class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Product> Products { get; set; }
    }
}