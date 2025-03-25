using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IHotelManager
{
    void AddHotel(string name, string city, string street, int totalRooms);
    void RemoveHotel(string name);
    IEnumerable<Hotel> GetHotels();
    void AddClient(string firstName, string lastName);
    void RemoveClient(string firstName, string lastName);
    IEnumerable<Client> GetClients();
    void AddBooking(string clientFirstName, string clientLastName, string hotelName, int roomNumber, DateTime startDate, DateTime endDate, decimal pricePerNight);
    void RemoveBooking(string clientFirstName, string clientLastName, string hotelName, int roomNumber);
    IEnumerable<Booking> GetBookings();
    IEnumerable<Hotel> SearchHotels(string keyword);
    IEnumerable<Client> SearchClients(string keyword);
}

public class HotelManager : IHotelManager
{
    private List<Hotel> hotels = new List<Hotel>();
    private List<Client> clients = new List<Client>();
    private List<Booking> bookings = new List<Booking>();

    public void AddHotel(string name, string city, string street, int totalRooms)
    {
        if (string.IsNullOrWhiteSpace(name) || totalRooms <= 0)
            throw new ArgumentException("Invalid hotel details.");

        hotels.Add(new Hotel(name, new Address(city, street), totalRooms));
    }

    public void RemoveHotel(string name)
    {
        var hotel = hotels.FirstOrDefault(h => h.Name == name);
        if (hotel == null)
            throw new ArgumentException("Hotel not found.");

        hotels.Remove(hotel);
    }

    public IEnumerable<Hotel> GetHotels() => hotels;

    public void AddClient(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Invalid client details.");

        clients.Add(new Client(firstName, lastName));
    }

    public void RemoveClient(string firstName, string lastName)
    {
        var client = clients.FirstOrDefault(c => c.FirstName == firstName && c.LastName == lastName);
        if (client == null)
            throw new ArgumentException("Client not found.");

        clients.Remove(client);
    }

    public IEnumerable<Client> GetClients() => clients;

    public void AddBooking(string clientFirstName, string clientLastName, string hotelName, int roomNumber, DateTime startDate, DateTime endDate, decimal pricePerNight)
    {
        var client = clients.FirstOrDefault(c => c.FirstName == clientFirstName && c.LastName == clientLastName);
        var hotel = hotels.FirstOrDefault(h => h.Name == hotelName);

        if (client == null || hotel == null)
            throw new ArgumentException("Client or Hotel not found.");

        var room = hotel.Rooms.FirstOrDefault(r => r.Number == roomNumber && !r.IsBooked);
        if (room == null)
            throw new ArgumentException("Room not available.");

        room.IsBooked = true;

        bookings.Add(new Booking(client, hotel, room, startDate, endDate, pricePerNight));
    }

    public void RemoveBooking(string clientFirstName, string clientLastName, string hotelName, int roomNumber)
    {
        var booking = bookings.FirstOrDefault(b => b.Client.FirstName == clientFirstName && b.Client.LastName == clientLastName && b.Hotel.Name == hotelName && b.Room.Number == roomNumber);
        if (booking == null)
            throw new ArgumentException("Booking not found.");

        booking.Room.IsBooked = false;
        bookings.Remove(booking);
    }

    public IEnumerable<Booking> GetBookings() => bookings;

    public IEnumerable<Hotel> SearchHotels(string keyword)
    {
        return hotels.Where(h => h.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Client> SearchClients(string keyword)
    {
        return clients.Where(c => c.FirstName.Contains(keyword, StringComparison.OrdinalIgnoreCase) || c.LastName.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    }
}