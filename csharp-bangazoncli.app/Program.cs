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
            var counter = 0;
            var customerList = new SelectCustomer();
            var listOfCustomerNames = customerList.GetCustomerName();
            var orderModifier = new OrderModifier();
            Console.WriteLine("Which customer will be active?");
            foreach(var list in listOfCustomerNames)
            {
                counter++;
                Console.WriteLine($"{counter} {list.FirstName} {list.LastName}");
            }
            
            var selectedCustomer = Console.ReadKey();
            var selectedCustomerIndex = int.Parse(selectedCustomer.KeyChar.ToString());
            var customer = listOfCustomerNames[selectedCustomerIndex - 1];
            Console.WriteLine($"the selected customer is {customer.FirstName} {customer.LastName}");

            Console.WriteLine("You've chosen to create a new customer account.");

            Console.WriteLine("Enter your first name");
            var firstName = Console.ReadLine();

            Console.WriteLine("Enter your last name");
            var lastName = Console.ReadLine();

            
            Console.WriteLine("Enter your street address");
            var address = Console.ReadLine();


           
            Console.WriteLine("Enter your city");
            var city = Console.ReadLine();

            Console.WriteLine("Enter your state");
            var state = Console.ReadLine();

            Console.WriteLine("Enter your postal code");
            var postalCode = Console.ReadLine();

            Console.WriteLine("Finally, enter your phone number");
            var phone = Console.ReadLine();


            var newCustomerInfo = new CreateCustomerAccount();
            var newCustomer = newCustomerInfo.AddNewCustomerInfo(firstName, lastName, address, city, state, postalCode, phone);

            if (newCustomer)
            {
                Console.WriteLine("You added a customer!");
            }

            var order = 0;
            var run = true;
            while (run)
            {

                Console.WriteLine("All Products");
                var counter2 = 0;
                foreach (var product in allProducts)
                {
                    counter2++;
                    Console.WriteLine($"{counter2}. {product.ProductName}: {product.ProductPrice}");
                }
                counter2++;
                Console.WriteLine($"{counter2}. Done adding products.");

                Console.WriteLine("What product would you like to add to the order?");
                var productToAdd = Console.ReadKey();
                Console.WriteLine("");
                var selectedProductIndex = int.Parse(productToAdd.KeyChar.ToString());
                if (selectedProductIndex == counter2)
                {
                    run = false;
                    break;
                }
                Console.WriteLine("How many would you like to add?");
                var numberToAdd = Console.ReadKey();
                var addedNumber = int.Parse(numberToAdd.KeyChar.ToString());
                Console.WriteLine("");

                var selectedProduct = allProducts[selectedProductIndex - 1];
                if (order == 0)
                {
                    order = orderModifier.CreateOrder(customer.customerId);
                }
                var addNewProduct = addProduct.AddProductToOrder(selectedProduct.ProductId, addedNumber, order);
                if (addNewProduct)
                {
                    Console.WriteLine($"You added {selectedProduct.ProductName} to your order!");

                }
            }
        }
    }
}
