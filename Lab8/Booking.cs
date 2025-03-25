using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class Booking
{
    public Client Client { get; set; }
    public Hotel Hotel { get; set; }
    public Room Room { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal PricePerNight { get; set; }
    public string RequestText { get; set; }

    public Booking(Client client, Hotel hotel, Room room, DateTime startDate, DateTime endDate, decimal pricePerNight, string requestText)
    {
        Client = client;
        Hotel = hotel;
        Room = room;
        StartDate = startDate;
        EndDate = endDate;
        PricePerNight = pricePerNight;
        RequestText = requestText;
    }
}