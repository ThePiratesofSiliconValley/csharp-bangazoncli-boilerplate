using csharp_bangazoncli.app.DataAccess;
using csharp_bangazoncli.app.DataAccess.Models;
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

            var customer = new CustomerList();
            var run = true;
            while (run)
            


            {
                var userInput = MainMenu();
                switch (int.Parse(userInput))
                {
                    case 1:
                        Console.Clear();
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
                        break;
                    case 2:
                        Console.Clear();
                        var counter = 0;
                        var customerList = new SelectCustomer();
                        var listOfCustomerNames = customerList.GetCustomerName();
                        Console.WriteLine("Which customer will be active?");
                        foreach (var list in listOfCustomerNames)
                        {
                            counter++;
                            Console.WriteLine($"{counter} {list.FirstName} {list.LastName}");
                        }

                        var selectedCustomer = Console.ReadLine();
                        var selectedCustomerIndex = int.Parse(selectedCustomer.ToString());
                        customer = listOfCustomerNames[selectedCustomerIndex - 1];
                        Console.WriteLine($"the selected customer is {customer.FirstName} {customer.LastName}");

                        break;
                    case 3:
                        Console.Clear();
                        // Create a payment option"
                        break;
                    case 4:
                        Console.Clear();
                        //Add product to sell
                        Console.WriteLine("Enter the Product Name: ");
                        var productName = Console.ReadLine();
                        Console.WriteLine("Enter the Product Description: ");
                        var productDescription = Console.ReadLine();
                        Console.WriteLine("Enter the Product Price: ");
                        var productPrice = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Enter the Product Quantity: ");
                        var quantity = Convert.ToInt32(Console.ReadLine());

                        var productAdder = new ProductAdder();
                        var newProduct = productAdder.AddNewProduct(productName, productDescription, productPrice, quantity);
                        break;
                    case 5:
                        Console.Clear();
                        //Add product to shopping cart
                        var productQuery = new ProductQuery();
                        var allProducts = productQuery.GetAllProducts();
                        var addProduct = new AddProduct();
                        var orderModifier = new OrderModifier();

                        var order = 0;
                        var addingProducts = true;
                        while (addingProducts)
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
                            var productToAdd = Console.ReadLine();
                            Console.WriteLine("");
                            var selectedProductIndex = int.Parse(productToAdd.ToString());
                            if (selectedProductIndex == counter2)
                            {
                                addingProducts = false;
                                break;
                            }
                            Console.WriteLine("How many would you like to add?");
                            var numberToAdd = Console.ReadLine();
                            var addedNumber = int.Parse(numberToAdd.ToString());
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
                            System.Threading.Thread.Sleep(1000);
                            Console.Clear();
                        }
                        break;
                    case 6:
                        Console.Clear();
                        //Complete an order
                        break;
                    case 7:
                        Console.Clear();
                        //Remove customer product
                        break;
                    case 8:
                        Console.Clear();
                        //Update product information
                        break;
                    case 9:
                        Console.Clear();
                        //Show stale products
                        break;
                    case 10:
                        Console.Clear();
                        //Show customer revenue report
                        break;
                    case 11:
                        Console.Clear();
                        //Show overall product popularity
                        break;
                    case 12:
                        //Leave Bangazon!
                        run = false;
                        break;
                }
            }

            string MainMenu()
            {
                View mainMenu = new View()
                        .AddMenuOption("Create a customer account")
                        .AddMenuOption("Choose active customer")
                        .AddMenuOption("Create a payment option")
                        .AddMenuOption("Add product to sell")
                        .AddMenuOption("Add product to shopping cart")
                        .AddMenuOption("Complete an order")
                        .AddMenuOption("Remove customer product")
                        .AddMenuOption("Update product information")
                        .AddMenuOption("Show stale products")
                        .AddMenuOption("Show customer revenue report")
                        .AddMenuOption("Show overall product popularity")
                        .AddMenuOption("Leave Bangazon!");

                Console.Write(mainMenu.GetFullMenu());
                var userOption = Console.ReadLine();
                return userOption;
            }
        }
    }
}
