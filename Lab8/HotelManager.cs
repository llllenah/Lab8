using HotelBookingSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class HotelManager
    {
        private List<Hotel> hotels = new List<Hotel>();
        private List<Client> clients = new List<Client>();
        private List<Booking> bookings = new List<Booking>();

        public void AddHotel(string name, int totalRooms)
        {
            var hotel = new Hotel(name, totalRooms);
            hotels.Add(hotel);
            Console.WriteLine($"Hotel '{name}' successfully added with {totalRooms} rooms.");
        }

        public void AddClient(string firstName, string lastName)
        {
            var client = new Client(firstName, lastName);
            clients.Add(client);
            Console.WriteLine($"Client '{firstName} {lastName}' successfully added.");
        }

        public void AddBooking(string firstName, string lastName, string hotelName, int roomNumber, DateTime start, DateTime end)
        {
            Client client = clients.FirstOrDefault(c => c.FirstName == firstName && c.LastName == lastName);
            Hotel hotel = hotels.FirstOrDefault(h => h.Name == hotelName);

            if (client == null) throw new ArgumentException("Client not found.");
            if (hotel == null) throw new ArgumentException("Hotel not found.");
            Room room = hotel.Rooms.FirstOrDefault(r => r.Number == roomNumber && !r.IsBooked);
            if (room == null) throw new ArgumentException("Room not available.");

            room.IsBooked = true;
            bookings.Add(new Booking(client, hotel, room, start, end));
            Console.WriteLine($"Booking successful: {firstName} {lastName} -> {hotelName} Room {roomNumber}");
        }

        public void RemoveHotel(string hotelName)
        {
            var hotel = hotels.FirstOrDefault(h => h.Name == hotelName);
            if (hotel != null)
            {
                hotels.Remove(hotel);
                Console.WriteLine($"Hotel '{hotelName}' removed.");
            }
            else
            {
                Console.WriteLine($"Error: Hotel '{hotelName}' not found.");
            }
        }

        public void RemoveClient(string firstName, string lastName)
        {
            var client = clients.FirstOrDefault(c => c.FirstName == firstName && c.LastName == lastName);
            if (client != null)
            {
                clients.Remove(client);
                Console.WriteLine($"Client '{firstName} {lastName}' removed.");
            }
            else
            {
                Console.WriteLine($"Error: Client '{firstName} {lastName}' not found.");
            }
        }

        public void RemoveBooking(string firstName, string lastName, string hotelName, int roomNumber)
        {
            var booking = bookings.FirstOrDefault(b => b.Client.FirstName == firstName && b.Client.LastName == lastName &&
                                                       b.Hotel.Name == hotelName && b.Room.Number == roomNumber);
            if (booking != null)
            {
                booking.Room.IsBooked = false;
                bookings.Remove(booking);
                Console.WriteLine($"Booking for {firstName} {lastName} in room {roomNumber} at {hotelName} removed.");
            }
            else
            {
                Console.WriteLine($"Error: Booking not found for {firstName} {lastName} in room {roomNumber}.");
            }
        }
        public List<Hotel> GetHotels() => hotels;

        public List<Client> GetClients() => clients;

        public List<Hotel> SearchHotels(string keyword) => hotels.Where(h => h.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();

        public List<Client> SearchClients(string keyword) => clients.Where(c => c.FirstName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                                                              c.LastName.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
    }
