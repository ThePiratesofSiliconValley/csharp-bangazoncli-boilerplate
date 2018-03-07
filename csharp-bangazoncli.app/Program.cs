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
            var counter = 0;
            var run = true;
            var customerList = new SelectCustomer();
            var lists = customerList.GetCustomerName();

            Console.WriteLine("Which customer will be active?");
            foreach(var list in lists)
            {
                counter++;
                Console.WriteLine($"{counter} {list.FirstName} {list.LastName}");
            }
            

            var selectedCustomer = Console.ReadKey();
            var selectedCustomerIndex = int.Parse(selectedCustomer.KeyChar.ToString());


            var customer = lists[selectedCustomerIndex - 1];
            Console.WriteLine($"the selected customer is {customer.FirstName} {customer.LastName}");
            Console.ReadLine();
        }
    }
}
