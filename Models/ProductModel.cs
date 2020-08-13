using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Entity_CRUD.Models
{
    public class ProductModel
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string Recievername { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}