using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Address
{
    public string City { get; set; }
    public string Street { get; set; }

    public Address(string city, string street)
    {
        City = city;
        Street = street;
    }
}