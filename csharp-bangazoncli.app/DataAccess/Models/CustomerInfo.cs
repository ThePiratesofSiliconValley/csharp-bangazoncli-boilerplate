using System;

namespace csharp_bangazoncli.app.DataAccess.Models
{
    // I added this for the CreateCustomerAccount class, not sure if we'll need it
    class CustomerInfo
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
    }
}
