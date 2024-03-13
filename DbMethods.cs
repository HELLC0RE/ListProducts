using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListProducts
{
    public class DbMethods
    {
        public static List<string> GetProductTypes()
        {
            List<string> productTypes = new List<string>();

            string connectionString = "Server=localhost;Port=5432;Database=ProductList;User Id=postgres;Password=qwerty123";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT title FROM product_type"; 
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string type = reader.GetString(0); 
                            productTypes.Add(type);
                        }
                    }
                }
            }
            return productTypes;
        }
        public static List<Product> GetAllProducts()
        {
            List<Product> allProducts = new List<Product>();

            string connectionString = "Server=localhost;Port=5432;Database=ProductList;User Id=postgres;Password=qwerty123";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM product";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product = new Product
                            {
                                Article = reader.GetString(1),
                                Title = reader.GetString(2),
                                Description = reader.GetString(3),
                                ProductShopNumber = reader.GetInt32(4),
                                Cost = reader.GetDecimal(5),
                                Type = reader.GetInt32(6)
                            };
                            allProducts.Add(product);
                        }
                    }
                }
            }

            return allProducts;
        }      
    }
}
