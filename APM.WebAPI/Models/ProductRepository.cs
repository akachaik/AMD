using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace APM.WebAPI.Models
{
    public class ProductRepository : IProductRepository
    {
        public Product Create()
        {
            return new Product
            {
                ReleaseDate = DateTime.Now
            };
        }

        public List<Product> Retrieve()
        {
            var filePath = HostingEnvironment.MapPath(@"~/App_Data/product.json");

            var json = System.IO.File.ReadAllText(filePath);

            var products = JsonConvert.DeserializeObject<List<Product>>(json);

            return products;
        }

        public Product Save(Product product)
        {
            var products = this.Retrieve();

            var maxId = products.Max(p => p.ProductId);
            product.ProductId = maxId + 1;
            products.Add(product);

            writeDate(products);

            return product;
        }

        public Product Save(int id, Product product)
        {
            var products = this.Retrieve();
            var itemIndex = products.FindIndex(p => p.ProductId == id);
            if (itemIndex > 0)
            {
                products[itemIndex] = product;
            }
            else
            {
                return null;
            }

            writeDate(products);

            return product;
        }

        private bool writeDate(List<Product> products)
        {
            var filePath = HostingEnvironment.MapPath(@"~/App_Data/product.json");
            var json = JsonConvert.SerializeObject(products, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json);

            return true;
        }

    }
}