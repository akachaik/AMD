using System.Collections.Generic;

namespace APM.WebAPI.Models
{
    public interface IProductRepository
    {
        Product Create();
        List<Product> Retrieve();
        Product Save(Product product);
        Product Save(int id, Product product);
    }
}