using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hotel Booking System Initialized");

            HotelManager manager = new HotelManager();

            TryExecute(() => manager.AddHotel("Grand Hotel", 50));
            TryExecute(() => manager.AddHotel("Ocean View", 30));
            TryExecute(() => manager.AddHotel("", 0));
            TryExecute(() => manager.AddClient("John", "Doe"));
            TryExecute(() => manager.AddClient("Alice", "Smith"));
            TryExecute(() => manager.AddClient("Anna", "Ledecky"));
            TryExecute(() => manager.AddClient("", ""));
            TryExecute(() => manager.AddBooking("John", "Doe", "Grand Hotel", 101, DateTime.Now, DateTime.Now.AddDays(3)));

            Console.WriteLine("--- All Hotels ---");
            foreach (var hotel in manager.GetHotels())
            {
                Console.WriteLine($"{hotel.Name} - Rooms: {hotel.TotalRooms}");
            }

            Console.WriteLine("--- All Clients ---");
            foreach (var client in manager.GetClients())
            {
                Console.WriteLine($"{client.FirstName} {client.LastName}");
            }

            Console.WriteLine("--- Search for Hotel by Keyword ---");
            var searchHotels = manager.SearchHotels("Grand");
            foreach (var hotel in searchHotels)
            {
                Console.WriteLine($"{hotel.Name}");
            }

            Console.WriteLine("--- Search for Client by Keyword ---");
            var searchClients = manager.SearchClients("Doe");
            foreach (var client in searchClients)
            {
                Console.WriteLine($"{client.FirstName} {client.LastName}");
            }

            TryExecute(() => manager.RemoveHotel("Ocean View"));
            TryExecute(() => manager.RemoveClient("John", "Doe"));
            TryExecute(() => manager.RemoveBooking("John", "Doe", "Grand Hotel", 101));

            Console.WriteLine("--- All Hotels after removal ---");
            foreach (var hotel in manager.GetHotels())
            {
                Console.WriteLine($"{hotel.Name} - Rooms: {hotel.TotalRooms}");
            }

            Console.WriteLine("--- All Clients after removal ---");
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

}
