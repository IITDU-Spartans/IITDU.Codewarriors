using CodeWarriors.IITDU.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeWarriors.IITDU.Repository
{
    interface IProductRepository
    {
        int AddProduct(Product product);
        bool RemoveProduct(Product product);
        bool UpdateProduct(Product product);
        List<Product> GetAllProduct();
        Product GetProductByProductId(int productId);

    }


    public class NullProduct : Product
    {

    }
}
