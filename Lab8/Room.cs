using HotelBookingSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Room
{
    public int Number { get; }
    public bool IsBooked { get; set; }
    public decimal PricePerDay { get; }

    public Room(int number, decimal pricePerDay)
    {
        Number = number;
        PricePerDay = pricePerDay;
    }
}