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
            Console.WriteLine("Enter the Product Id: ");
            var productId = Console.ReadLine();
            Console.WriteLine("Enter the Product Name: ");
            var productName = Console.ReadLine();
            Console.WriteLine("Enter the Product Description: ");
            var productDescription = Console.ReadLine();
            Console.WriteLine("Enter the Product Price: ");
            var productPrice = Console.ReadLine();
            Console.WriteLine("Enter the Product Quantity: ");
            var quantity = Console.ReadLine();

            var productAdder = new ProductAdder();
            productAdder.Insert();
        }
    }
}
