using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListProducts
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Article { get; set; }
        public int ProductShopNumber { get; set; }
        public decimal Cost { get; set; }
        public int Type { get; set; }

        public Product() { }

        public Product(int id, string title, string description, string article, int productShopNumber, decimal cost, int type)
        {
            Id = id;
            Title = title;
            Description = description;
            Article = article;
            ProductShopNumber = productShopNumber;
            Cost = cost;
            Type = type;
        }
    }
}
