using csharp_bangazoncli.app.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_bangazoncli.app
{
    class Program
    {
        static void Main(string[] args)
        {
            var productQuery = new ProductQuery();
            var allProducts = productQuery.GetAllProducts();
            var addProduct = new AddProduct();

            var run = true;
            while (run)
            {

                Console.WriteLine("All Products");
                var counter = 0;
                foreach (var product in allProducts)
                {
                    counter++;
                    Console.WriteLine($"{counter}. {product.ProductName}: {product.ProductPrice}");
                }
                counter++;
                Console.WriteLine($"{counter}. Done adding products.");

                var productToAdd = Console.ReadKey();

                if (int.Parse(productToAdd.KeyChar.ToString()) == counter)
                {
                    run = false;
                    break;
                }

                
            }
        }
    }
}
