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
            var customerList = new SelectCustomer();
            var lists = customerList.GetCustomerName();

            foreach(var list in lists)
            {
                counter++;
                Console.WriteLine($"{counter} {list.FirstName} {list.LastName}");
            }
            Console.ReadLine();
        }
    }
}
