using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class Booking
{
    public Client Client { get; }
    public Hotel Hotel { get; }
    public Room Room { get; }
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }
    public decimal PricePerNight { get; }

    public Booking(Client client, Hotel hotel, Room room, DateTime startDate, DateTime endDate, decimal pricePerNight)
    {
        Client = client;
        Hotel = hotel;
        Room = room;
        StartDate = startDate;
        EndDate = endDate;
        PricePerNight = pricePerNight;
    }
}