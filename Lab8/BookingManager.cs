public interface IBookingManager
{
    void AddBooking(string clientFirstName, string clientLastName, string hotelName, int roomNumber,
                    DateTime startDate, DateTime endDate, decimal pricePerNight, string requestText);
    void RemoveBooking(string clientFirstName, string clientLastName, string hotelName, int roomNumber);
    void UpdateBookingRequest(string clientFirstName, string clientLastName, string hotelName, int roomNumber, string newRequestText);
    Booking GetBooking(string clientFirstName, string clientLastName, string hotelName, int roomNumber);
    IEnumerable<Booking> GetBookings();
    IEnumerable<Booking> GetBookingsByDateRange(DateTime startDate, DateTime endDate);
    decimal GetBookingCost(string clientFirstName, string clientLastName, string hotelName, int roomNumber);
    IEnumerable<Client> GetClientsWithBookings();
    IEnumerable<Room> GetBookedRooms(string hotelName);
    IEnumerable<Room> GetAvailableRooms(string hotelName);
    IEnumerable<BookingRequest> GetBookingRequests(DateTime startDate, DateTime endDate);
    void AddBookingRequest(string hotelName, int roomNumber, string requestText, DateTime startDate, DateTime endDate);
    void RemoveBookingRequest(string hotelName, int roomNumber);
    void UpdateBookingRequest(string hotelName, int roomNumber, string newText);
    IEnumerable<Hotel> SearchHotels(string keyword);
}
public class BookingManager : IBookingManager
{
    private List<Booking> bookings = new List<Booking>();
    private List<BookingRequest> bookingRequests = new List<BookingRequest>();
    private IHotelManager hotelManager;
    private IClientManager clientManager;

    public BookingManager(IHotelManager hotelManager, IClientManager clientManager)
    {
        this.hotelManager = hotelManager;
        this.clientManager = clientManager;
    }

    public void AddBooking(string clientFirstName, string clientLastName, string hotelName, int roomNumber,
                           DateTime startDate, DateTime endDate, decimal pricePerNight, string requestText)
    {
        Client client = clientManager.GetClient(clientFirstName, clientLastName);
        if (client == null)
            throw new ArgumentException("Клієнт не знайдений.");

        Hotel hotel = hotelManager.GetHotel(hotelName);
        if (hotel == null)
            throw new ArgumentException("Готель не знайдений.");

        Room room = hotel.GetAvailableRooms().FirstOrDefault(r => r.Number == roomNumber);
        if (room == null)
            throw new ArgumentException("Кімната не доступна.");

        Booking booking = new Booking(client, hotel, room, startDate, endDate, pricePerNight, requestText);
        bookings.Add(booking);
        room.MarkAsBooked();
    }

    public void RemoveBooking(string clientFirstName, string clientLastName, string hotelName, int roomNumber)
    {
        var booking = GetBooking(clientFirstName, clientLastName, hotelName, roomNumber);
        if (booking == null)
            throw new ArgumentException("Бронювання не знайдено.");
        booking.Room.MarkAsAvailable();
        bookings.Remove(booking);
    }

    public void UpdateBookingRequest(string clientFirstName, string clientLastName, string hotelName, int roomNumber, string newRequestText)
    {
        var booking = GetBooking(clientFirstName, clientLastName, hotelName, roomNumber);
        if (booking == null)
            throw new ArgumentException("Бронювання не знайдено.");
        booking.RequestText = newRequestText;
    }

    public Booking GetBooking(string clientFirstName, string clientLastName, string hotelName, int roomNumber)
    {
        return bookings.FirstOrDefault(b =>
            b.Client.FirstName.Equals(clientFirstName, StringComparison.OrdinalIgnoreCase) &&
            b.Client.LastName.Equals(clientLastName, StringComparison.OrdinalIgnoreCase) &&
            b.Hotel.Name.Equals(hotelName, StringComparison.OrdinalIgnoreCase) &&
            b.Room.Number == roomNumber);
    }

    public IEnumerable<Booking> GetBookings() => bookings;

    public IEnumerable<Booking> GetBookingsByDateRange(DateTime startDate, DateTime endDate)
    {
        return bookings.Where(b => b.StartDate >= startDate && b.EndDate <= endDate);
    }

    public decimal GetBookingCost(string clientFirstName, string clientLastName, string hotelName, int roomNumber)
    {
        var booking = GetBooking(clientFirstName, clientLastName, hotelName, roomNumber);
        if (booking == null)
            throw new ArgumentException("Бронювання не знайдено.");
        return booking.CalculateTotalCost();
    }

    public IEnumerable<Client> GetClientsWithBookings()
    {
        return bookings.Select(b => b.Client).Distinct();
    }

    public IEnumerable<Room> GetBookedRooms(string hotelName)
    {
        Hotel hotel = hotelManager.GetHotel(hotelName);
        if (hotel == null)
            throw new ArgumentException("Готель не знайдений.");
        return hotel.GetBookedRooms();
    }

    public IEnumerable<Room> GetAvailableRooms(string hotelName)
    {
        Hotel hotel = hotelManager.GetHotel(hotelName);
        if (hotel == null)
            throw new ArgumentException("Готель не знайдений.");
        return hotel.GetAvailableRooms();
    }

    public IEnumerable<BookingRequest> GetBookingRequests(DateTime startDate, DateTime endDate)
    {
        return bookingRequests.Where(r => r.StartDate >= startDate && r.EndDate <= endDate);
    }

    public void AddBookingRequest(string hotelName, int roomNumber, string requestText, DateTime startDate, DateTime endDate)
    {
        Hotel hotel = hotelManager.GetHotel(hotelName);
        if (hotel == null)
            throw new ArgumentException("Готель не знайдений.");
        hotel.AddBookingRequest(roomNumber, requestText, startDate, endDate);
        bookingRequests.Add(new BookingRequest(hotelName, roomNumber, requestText, startDate, endDate));
    }

    public void RemoveBookingRequest(string hotelName, int roomNumber)
    {
        Hotel hotel = hotelManager.GetHotel(hotelName);
        if (hotel == null)
            throw new ArgumentException("Готель не знайдений.");
        hotel.RemoveBookingRequest(roomNumber);
        var req = bookingRequests.FirstOrDefault(r =>
            r.HotelName.Equals(hotelName, StringComparison.OrdinalIgnoreCase) &&
            r.RoomNumber == roomNumber);
        if (req == null)
            throw new ArgumentException("Запит не знайдено.");
        bookingRequests.Remove(req);
    }

    public void UpdateBookingRequest(string hotelName, int roomNumber, string newText)
    {
        Hotel hotel = hotelManager.GetHotel(hotelName);
        if (hotel == null)
            throw new ArgumentException("Готель не знайдений.");
        hotel.UpdateBookingRequest(roomNumber, newText);
        var req = bookingRequests.FirstOrDefault(r =>
            r.HotelName.Equals(hotelName, StringComparison.OrdinalIgnoreCase) &&
            r.RoomNumber == roomNumber);
        if (req == null)
            throw new ArgumentException("Запит не знайдено.");
        req.UpdateRequestText(newText);
    }

    public IEnumerable<Hotel> SearchHotels(string keyword)
    {
        return hotelManager.SearchHotels(keyword);
    }
}