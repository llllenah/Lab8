using System;
using System.Collections.Generic;
using System.Linq;
class Program
{
    static void Main()
    {
        Console.WriteLine("Hotel Booking System Initialized");

        IHotelManager manager = new HotelManager();

        TryExecute(() => manager.AddHotel("Grand Hotel", "Kyiv", "Main Street", 50));
        TryExecute(() => manager.AddHotel("Ocean View", "Odesa", "Beach Avenue", 30));
        TryExecute(() => manager.AddHotel("Luxury Stay", "Lviv", "Central Street", 100));

        TryExecute(() => manager.AddClient("John", "Doe"));
        TryExecute(() => manager.AddClient("Alice", "Smith"));
        TryExecute(() => manager.AddClient("Anna", "Ledecky"));
        TryExecute(() => manager.AddClient("Bob", "Johnson"));

        TryExecute(() => manager.AddBooking("John", "Doe", "Grand Hotel", 101, DateTime.Now, DateTime.Now.AddDays(3), 100));
        TryExecute(() => manager.AddBooking("Alice", "Smith", "Ocean View", 102, DateTime.Now, DateTime.Now.AddDays(2), 150));
        TryExecute(() => manager.AddBooking("Anna", "Ledecky", "Luxury Stay", 201, DateTime.Now, DateTime.Now.AddDays(5), 200));

        Console.WriteLine("--- All Hotels ---");
        foreach (var hotel in manager.GetHotels())
        {
            Console.WriteLine($"{hotel.Name} - Rooms: {hotel.TotalRooms}, Location: {hotel.Address.City}, {hotel.Address.Street}");
        }

        Console.WriteLine("--- All Clients ---");
        foreach (var client in manager.GetClients())
        {
            Console.WriteLine($"{client.FirstName} {client.LastName}");
        }

        Console.WriteLine("--- All Bookings ---");
        foreach (var booking in manager.GetBookings())
        {
            Console.WriteLine($"{booking.Client.FirstName} {booking.Client.LastName} - {booking.Hotel.Name}, Room {booking.Room.Number}, {booking.StartDate.ToShortDateString()} to {booking.EndDate.ToShortDateString()}");
        }

        Console.WriteLine("\n--- Search for Hotels (Keyword: 'Grand') ---");
        var searchHotels = manager.SearchHotels("Grand");
        foreach (var hotel in searchHotels)
        {
            Console.WriteLine($"{hotel.Name}");
        }

        Console.WriteLine("\n--- Search for Clients (Keyword: 'Doe') ---");
        var searchClients = manager.SearchClients("Doe");
        foreach (var client in searchClients)
        {
            Console.WriteLine($"{client.FirstName} {client.LastName}");
        }

        TryExecute(() => manager.RemoveBooking("John", "Doe", "Grand Hotel", 101));

        Console.WriteLine("\n--- All Bookings after removing John Doe's booking ---");
        foreach (var booking in manager.GetBookings())
        {
            Console.WriteLine($"{booking.Client.FirstName} {booking.Client.LastName} - {booking.Hotel.Name}, Room {booking.Room.Number}");
        }

        TryExecute(() => manager.RemoveHotel("Ocean View"));
        TryExecute(() => manager.RemoveClient("Alice", "Smith"));

        Console.WriteLine("\n--- All Hotels after removing 'Ocean View' ---");
        foreach (var hotel in manager.GetHotels())
        {
            Console.WriteLine($"{hotel.Name}");
        }

        Console.WriteLine("\n--- All Clients after removing 'Alice Smith' ---");
        foreach (var client in manager.GetClients())
        {
            Console.WriteLine($"{client.FirstName} {client.LastName}");
        }

        TryExecute(() => manager.AddHotel("Ocean View", "Odesa", "Beach Avenue", 30));
        TryExecute(() => manager.AddClient("Alice", "Smith"));

        Console.WriteLine("\n--- All Hotels after adding 'Ocean View' back ---");
        foreach (var hotel in manager.GetHotels())
        {
            Console.WriteLine($"{hotel.Name}");
        }

        Console.WriteLine("\n--- All Clients after adding 'Alice Smith' back ---");
        foreach (var client in manager.GetClients())
        {
            Console.WriteLine($"{client.FirstName} {client.LastName}");
        }
    }

    static void TryExecute(Action action)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
