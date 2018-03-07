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
            var orderModifier = new OrderModifier();

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

                Console.WriteLine("What product would you like to add to the order?");
                var productToAdd = Console.ReadKey();
                Console.WriteLine("How many would you like to add?");
                var numberToAdd = Console.ReadKey();
                var selectedProductIndex = int.Parse(productToAdd.KeyChar.ToString());
                if (selectedProductIndex == counter)
                {
                    run = false;
                    break;
                }

                var selectedProduct = allProducts[selectedProductIndex - 1];
                var createOrder = orderModifier.CreateOrder();
                var addNewProduct = addProduct.AddProductToOrder(selectedProduct.ProductId, int.Parse(numberToAdd.ToString()), createOrder);
                if (addNewProduct)
                {
                    Console.WriteLine($"You added {numberToAdd} {selectedProduct.ProductName} to your order!");
                }
            }
        }
    }
}
