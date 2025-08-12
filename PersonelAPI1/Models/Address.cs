namespace PersonelAPI1.Models
{
    public class Address
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string AddressType { get; set; } // ev,iş...
        public string Street { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public Employee? Employee { get; set; }
    }

}
