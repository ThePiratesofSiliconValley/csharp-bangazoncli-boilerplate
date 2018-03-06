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

            Console.WriteLine("Here is a list of all products");
            foreach (var product in allProducts)
            {
                var counter = 0;
                counter++;
                Console.WriteLine($"{counter}. {product.ProductName}: {product.ProductPrice}");
            }
            Console.ReadLine();
        }
    }
}
