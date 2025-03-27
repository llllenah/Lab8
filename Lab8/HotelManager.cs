using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IHotelManager
{
    void AddHotel(string name, string city, string street, int totalRooms);
    void RemoveHotel(string name);
    Hotel GetHotel(string name);
    IEnumerable<Hotel> GetHotels();
    void AddClient(string firstName, string lastName);
    void RemoveClient(string firstName, string lastName);
    void UpdateClient(string firstName, string lastName, string newFirstName, string newLastName);
    Client GetClient(string firstName, string lastName);
    IEnumerable<Client> GetClients();
    IEnumerable<Client> GetClientsSortedByName();
    IEnumerable<Client> GetClientsSortedByLastName();
    void AddBooking(string clientFirstName, string clientLastName, string hotelName, int roomNumber, DateTime startDate, DateTime endDate, decimal pricePerNight, string requestText);
    void RemoveBooking(string clientFirstName, string clientLastName, string hotelName, int roomNumber);
    void UpdateBookingRequest(string clientFirstName, string clientLastName, string hotelName, int roomNumber, string newRequestText);
    IEnumerable<Booking> GetBookings();
    IEnumerable<Booking> GetBookingsByDateRange(DateTime startDate, DateTime endDate);
    Booking GetBooking(string clientFirstName, string clientLastName, string hotelName, int roomNumber);
    IEnumerable<Room> GetBookedRooms(string hotelName);
    IEnumerable<Room> GetAvailableRooms(string hotelName);
    decimal GetBookingCost(string clientFirstName, string clientLastName, string hotelName, int roomNumber);
    IEnumerable<Client> GetClientsWithBookings();
    IEnumerable<BookingRequest> GetBookingRequests(DateTime startDate, DateTime endDate);
    void AddBookingRequest(string hotelName, int roomNumber, string requestText, DateTime startDate, DateTime endDate);
    void RemoveBookingRequest(string hotelName, int roomNumber);
    void UpdateBookingRequest(string hotelName, int roomNumber, string newText);
    IEnumerable<Hotel> SearchHotels(string keyword);
    IEnumerable<Client> SearchClients(string keyword);
}

public class HotelManager : IHotelManager
{
    private List<Hotel> hotels = new List<Hotel>();
    private List<Client> clients = new List<Client>();
    private List<Booking> bookings = new List<Booking>();
    private List<BookingRequest> bookingRequests = new List<BookingRequest>();

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

    public Hotel GetHotel(string name) => hotels.FirstOrDefault(h => h.Name == name);

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

    public void UpdateClient(string firstName, string lastName, string newFirstName, string newLastName)
    {
        var client = clients.FirstOrDefault(c => c.FirstName == firstName && c.LastName == lastName);
        if (client == null)
            throw new ArgumentException("Client not found.");

        client.UpdateName(newFirstName, newLastName);
    }

    public Client GetClient(string firstName, string lastName) => clients.FirstOrDefault(c => c.FirstName == firstName && c.LastName == lastName);

    public IEnumerable<Client> GetClients() => clients;

    public IEnumerable<Client> GetClientsSortedByName() => clients.OrderBy(c => c.FirstName);

    public IEnumerable<Client> GetClientsSortedByLastName() => clients.OrderBy(c => c.LastName);

    public void AddBooking(string clientFirstName, string clientLastName, string hotelName, int roomNumber, DateTime startDate, DateTime endDate, decimal pricePerNight, string requestText)
    {
        Client client = GetClient(clientFirstName, clientLastName);
        Hotel hotel = GetHotel(hotelName);
        Room room = hotel?.Rooms?.FirstOrDefault(r => r.Number == roomNumber && !r.IsBooked);

        if (client != null && hotel != null && room != null)
        {
            Booking booking = new Booking(client, hotel, room, startDate, endDate, pricePerNight, requestText);
            bookings.Add(booking);
            room.IsBooked = true;
            Console.WriteLine("Booking successfully created.");
        }
        else
        {
            Console.WriteLine("Error: Client, Hotel, or Room not found.");
        }
    }

    public void RemoveBooking(string clientFirstName, string clientLastName, string hotelName, int roomNumber)
    {
        var booking = bookings.FirstOrDefault(b => b.Client.FirstName == clientFirstName && b.Client.LastName == clientLastName && b.Hotel.Name == hotelName && b.Room.Number == roomNumber);
        if (booking == null)
            throw new ArgumentException("Booking not found.");

        booking.Room.IsBooked = false;
        bookings.Remove(booking);
    }

    public void UpdateBookingRequest(string clientFirstName, string clientLastName, string hotelName, int roomNumber, string newRequestText)
    {
        var booking = bookings.FirstOrDefault(b => b.Client.FirstName == clientFirstName && b.Client.LastName == clientLastName && b.Hotel.Name == hotelName && b.Room.Number == roomNumber);
        if (booking == null)
            throw new ArgumentException("Booking not found.");

        booking.RequestText = newRequestText;
    }

    public IEnumerable<Booking> GetBookings() => bookings;

    public IEnumerable<Booking> GetBookingsByDateRange(DateTime startDate, DateTime endDate)
    {
        return bookings.Where(b => b.StartDate >= startDate && b.EndDate <= endDate);
    }

    public Booking GetBooking(string clientFirstName, string clientLastName, string hotelName, int roomNumber)
    {
        return bookings.FirstOrDefault(b =>
            b.Client.FirstName == clientFirstName &&
            b.Client.LastName == clientLastName &&
            b.Hotel.Name == hotelName &&
            b.Room.Number == roomNumber);
    }

    public IEnumerable<Room> GetBookedRooms(string hotelName)
    {
        var hotel = hotels.FirstOrDefault(h => h.Name == hotelName);
        if (hotel == null)
            throw new ArgumentException("Hotel not found.");

        return hotel.Rooms.Where(r => r.IsBooked);
    }

    public IEnumerable<Room> GetAvailableRooms(string hotelName)
    {
        var hotel = hotels.FirstOrDefault(h => h.Name == hotelName);
        if (hotel == null)
            throw new ArgumentException("Hotel not found.");

        return hotel.Rooms.Where(r => !r.IsBooked);
    }

    public decimal GetBookingCost(string clientFirstName, string clientLastName, string hotelName, int roomNumber)
    {
        var booking = GetBooking(clientFirstName, clientLastName, hotelName, roomNumber);

        if (booking != null)
        {
            var numberOfNights = (booking.EndDate - booking.StartDate).Days;
            return booking.PricePerNight * numberOfNights;
        }

        Console.WriteLine("Error calculating cost: Booking not found.");
        return 0;
    }

    public IEnumerable<Client> GetClientsWithBookings()
    {
        return bookings.Select(b => b.Client).Distinct();
    }


    public IEnumerable<Hotel> SearchHotels(string keyword)
    {
        return hotels.Where(h => h.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Client> SearchClients(string keyword)
    {
        return clients.Where(c => c.FirstName.Contains(keyword, StringComparison.OrdinalIgnoreCase) || c.LastName.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<BookingRequest> GetBookingRequests(DateTime startDate, DateTime endDate)
    {
        return bookingRequests.Where(r => r.StartDate >= startDate && r.EndDate <= endDate);
    }

    public void AddBookingRequest(string hotelName, int roomNumber, string requestText, DateTime startDate, DateTime endDate)
    {
        var hotel = hotels.FirstOrDefault(h => h.Name == hotelName);
        if (hotel == null)
            throw new ArgumentException("Hotel not found.");

        var room = hotel.Rooms.FirstOrDefault(r => r.Number == roomNumber);
        if (room == null)
            throw new ArgumentException("Room not found.");

        bookingRequests.Add(new BookingRequest(hotelName, roomNumber, requestText, startDate, endDate));
    }

    public void RemoveBookingRequest(string hotelName, int roomNumber)
    {
        var request = bookingRequests.FirstOrDefault(r => r.HotelName == hotelName && r.RoomNumber == roomNumber);
        if (request == null)
            throw new ArgumentException("Booking request not found.");

        bookingRequests.Remove(request);
    }

    public void UpdateBookingRequest(string hotelName, int roomNumber, string newRequestText)
    {
        var request = bookingRequests.FirstOrDefault(r => r.HotelName == hotelName && r.RoomNumber == roomNumber);
        if (request == null)
            throw new ArgumentException("Booking request not found.");

        request.RequestText = newRequestText;
    }
}
