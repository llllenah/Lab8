public interface IHotelManager
{
    /// <summary>
    /// Додає новий готель.
    /// </summary>
    void AddHotel(string name, string city, string street, int totalRooms);

    /// <summary>
    /// Видаляє готель за назвою.
    /// </summary>
    void RemoveHotel(string name);

    /// <summary>
    /// Повертає готель за назвою.
    /// </summary>
    Hotel GetHotel(string name);

    /// <summary>
    /// Повертає список усіх готелів.
    /// </summary>
    IEnumerable<Hotel> GetHotels();

    /// <summary>
    /// Оновлює дані готелю.
    /// </summary>
    void UpdateHotel(string hotelName, string newName, string newCity, string newStreet, int newTotalRooms);

    /// <summary>
    /// Додає нового клієнта.
    /// </summary>
    void AddClient(string firstName, string lastName);

    /// <summary>
    /// Видаляє клієнта.
    /// </summary>
    void RemoveClient(string firstName, string lastName);

    /// <summary>
    /// Оновлює дані клієнта.
    /// </summary>
    void UpdateClient(string firstName, string lastName, string newFirstName, string newLastName);

    /// <summary>
    /// Повертає клієнта за іменем та прізвищем.
    /// </summary>
    Client GetClient(string firstName, string lastName);

    /// <summary>
    /// Повертає список усіх клієнтів.
    /// </summary>
    IEnumerable<Client> GetClients();

    /// <summary>
    /// Повертає клієнтів, відсортованих за іменем.
    /// </summary>
    IEnumerable<Client> GetClientsSortedByName();

    /// <summary>
    /// Повертає клієнтів, відсортованих за прізвищем.
    /// </summary>
    IEnumerable<Client> GetClientsSortedByLastName();

    /// <summary>
    /// Додає бронювання номера для клієнта.
    /// </summary>
    void AddBooking(string clientFirstName, string clientLastName, string hotelName, int roomNumber,
        DateTime startDate, DateTime endDate, decimal pricePerNight, string requestText);

    /// <summary>
    /// Видаляє бронювання.
    /// </summary>
    void RemoveBooking(string clientFirstName, string clientLastName, string hotelName, int roomNumber);

    /// <summary>
    /// Оновлює текст заявки бронювання (для підтвердженого бронювання).
    /// </summary>
    void UpdateBookingRequest(string clientFirstName, string clientLastName, string hotelName, int roomNumber,
        string newRequestText);

    /// <summary>
    /// Повертає список усіх бронювань.
    /// </summary>
    IEnumerable<Booking> GetBookings();

    /// <summary>
    /// Повертає бронювання за заданим періодом.
    /// </summary>
    IEnumerable<Booking> GetBookingsByDateRange(DateTime startDate, DateTime endDate);

    /// <summary>
    /// Повертає конкретне бронювання.
    /// </summary>
    Booking GetBooking(string clientFirstName, string clientLastName, string hotelName, int roomNumber);

    /// <summary>
    /// Повертає список заброньованих номерів у готелі.
    /// </summary>
    IEnumerable<Room> GetBookedRooms(string hotelName);

    /// <summary>
    /// Повертає список вільних номерів у готелі.
    /// </summary>
    IEnumerable<Room> GetAvailableRooms(string hotelName);

    /// <summary>
    /// Розраховує вартість бронювання (з урахуванням ціни за добу).
    /// </summary>
    decimal GetBookingCost(string clientFirstName, string clientLastName, string hotelName, int roomNumber);

    /// <summary>
    /// Повертає список клієнтів, які зробили бронювання.
    /// </summary>
    IEnumerable<Client> GetClientsWithBookings();

    /// <summary>
    /// Додає запит на бронювання номера.
    /// </summary>
    void AddBookingRequest(string hotelName, int roomNumber, string requestText, DateTime startDate, DateTime endDate);

    /// <summary>
    /// Видаляє запит на бронювання номера.
    /// </summary>
    void RemoveBookingRequest(string hotelName, int roomNumber);

    /// <summary>
    /// Оновлює текст запиту на бронювання.
    /// </summary>
    void UpdateBookingRequest(string hotelName, int roomNumber, string newText);

    /// <summary>
    /// Повертає запити на бронювання за заданим періодом.
    /// </summary>
    IEnumerable<BookingRequest> GetBookingRequests(DateTime startDate, DateTime endDate);

    /// <summary>
    /// Шукає готелі за ключовим словом.
    /// </summary>
    IEnumerable<Hotel> SearchHotels(string keyword);

    /// <summary>
    /// Шукає клієнтів за ключовим словом.
    /// </summary>
    IEnumerable<Client> SearchClients(string keyword);
}

public class HotelManager : IHotelManager
{
    private List<Hotel> hotels = new List<Hotel>();
    private List<Client> clients = new List<Client>();
    private List<Booking> bookings = new List<Booking>();
    private List<BookingRequest> bookingRequests = new List<BookingRequest>();

    /// <summary>
    /// Додає новий готель.
    /// </summary>
    public void AddHotel(string name, string city, string street, int totalRooms)
    {
        if (string.IsNullOrWhiteSpace(name) || totalRooms <= 0)
            throw new ArgumentException("Invalid hotel details.");
        hotels.Add(new Hotel(name, new Address(city, street), totalRooms));
    }

    /// <summary>
    /// Видаляє готель за назвою.
    /// </summary>
    public void RemoveHotel(string name)
    {
        var hotel = hotels.FirstOrDefault(h => h.Name == name);
        if (hotel == null)
            throw new ArgumentException("Hotel not found.");
        hotels.Remove(hotel);
    }

    /// <summary>
    /// Повертає готель за назвою.
    /// </summary>
    public Hotel GetHotel(string name) => hotels.FirstOrDefault(h => h.Name == name);

    /// <summary>
    /// Повертає список усіх готелів.
    /// </summary>
    public IEnumerable<Hotel> GetHotels() => hotels;

    /// <summary>
    /// Оновлює дані готелю через делегування методу класу Hotel.
    /// </summary>
    public void UpdateHotel(string hotelName, string newName, string newCity, string newStreet, int newTotalRooms)
    {
        var hotel = GetHotel(hotelName);
        if (hotel == null)
            throw new ArgumentException("Hotel not found.");
        hotel.UpdateHotel(newName, newCity, newStreet, newTotalRooms);
    }

    /// <summary>
    /// Додає нового клієнта.
    /// </summary>
    public void AddClient(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Invalid client details.");
        clients.Add(new Client(firstName, lastName));
    }

    /// <summary>
    /// Видаляє клієнта.
    /// </summary>
    public void RemoveClient(string firstName, string lastName)
    {
        var client = GetClient(firstName, lastName);
        if (client == null)
            throw new ArgumentException("Client not found.");
        clients.Remove(client);
    }

    /// <summary>
    /// Оновлює дані клієнта.
    /// </summary>
    public void UpdateClient(string firstName, string lastName, string newFirstName, string newLastName)
    {
        var client = GetClient(firstName, lastName);
        if (client == null)
            throw new ArgumentException("Client not found.");
        client.UpdateName(newFirstName, newLastName);
    }

    /// <summary>
    /// Повертає клієнта за іменем та прізвищем.
    /// </summary>
    public Client GetClient(string firstName, string lastName)
        => clients.FirstOrDefault(c => c.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase)
                                      && c.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));

    /// <summary>
    /// Повертає список усіх клієнтів.
    /// </summary>
    public IEnumerable<Client> GetClients() => clients;

    /// <summary>
    /// Повертає клієнтів, відсортованих за іменем.
    /// </summary>
    public IEnumerable<Client> GetClientsSortedByName() => clients.OrderBy(c => c.FirstName);

    /// <summary>
    /// Повертає клієнтів, відсортованих за прізвищем.
    /// </summary>
    public IEnumerable<Client> GetClientsSortedByLastName() => clients.OrderBy(c => c.LastName);

    /// <summary>
    /// Додає бронювання для клієнта.
    /// </summary>
    public void AddBooking(string clientFirstName, string clientLastName, string hotelName, int roomNumber,
        DateTime startDate, DateTime endDate, decimal pricePerNight, string requestText)
    {
        Client client = GetClient(clientFirstName, clientLastName);
        if (client == null)
            throw new ArgumentException("Client not found.");

        Hotel hotel = GetHotel(hotelName);
        if (hotel == null)
            throw new ArgumentException("Hotel not found.");

        Room room = hotel.GetAvailableRooms().FirstOrDefault(r => r.Number == roomNumber);
        if (room == null)
            throw new ArgumentException("Room not available.");

        // Створення бронювання
        Booking booking = new Booking(client, hotel, room, startDate, endDate, pricePerNight, requestText);
        bookings.Add(booking);
        room.MarkAsBooked();
    }

    /// <summary>
    /// Видаляє бронювання.
    /// </summary>
    public void RemoveBooking(string clientFirstName, string clientLastName, string hotelName, int roomNumber)
    {
        var booking = GetBooking(clientFirstName, clientLastName, hotelName, roomNumber);
        if (booking == null)
            throw new ArgumentException("Booking not found.");
        booking.Room.MarkAsAvailable();
        bookings.Remove(booking);
    }

    /// <summary>
    /// Оновлює текст заявки підтвердженого бронювання.
    /// </summary>
    public void UpdateBookingRequest(string clientFirstName, string clientLastName, string hotelName, int roomNumber,
        string newRequestText)
    {
        var booking = GetBooking(clientFirstName, clientLastName, hotelName, roomNumber);
        if (booking == null)
            throw new ArgumentException("Booking not found.");
        booking.RequestText = newRequestText;
    }

    /// <summary>
    /// Повертає список усіх бронювань.
    /// </summary>
    public IEnumerable<Booking> GetBookings() => bookings;

    /// <summary>
    /// Повертає бронювання за заданим періодом.
    /// </summary>
    public IEnumerable<Booking> GetBookingsByDateRange(DateTime startDate, DateTime endDate)
    {
        return bookings.Where(b => b.StartDate >= startDate && b.EndDate <= endDate);
    }

    /// <summary>
    /// Повертає конкретне бронювання.
    /// </summary>
    public Booking GetBooking(string clientFirstName, string clientLastName, string hotelName, int roomNumber)
    {
        return bookings.FirstOrDefault(b =>
            b.Client.FirstName.Equals(clientFirstName, StringComparison.OrdinalIgnoreCase) &&
            b.Client.LastName.Equals(clientLastName, StringComparison.OrdinalIgnoreCase) &&
            b.Hotel.Name.Equals(hotelName, StringComparison.OrdinalIgnoreCase) &&
            b.Room.Number == roomNumber);
    }

    /// <summary>
    /// Повертає список заброньованих номерів у готелі (делегується до класу Hotel).
    /// </summary>
    public IEnumerable<Room> GetBookedRooms(string hotelName)
    {
        Hotel hotel = GetHotel(hotelName);
        if (hotel == null)
            throw new ArgumentException("Hotel not found.");
        return hotel.GetBookedRooms();
    }

    /// <summary>
    /// Повертає список вільних номерів у готелі (делегується до класу Hotel).
    /// </summary>
    public IEnumerable<Room> GetAvailableRooms(string hotelName)
    {
        Hotel hotel = GetHotel(hotelName);
        if (hotel == null)
            throw new ArgumentException("Hotel not found.");
        return hotel.GetAvailableRooms();
    }

    /// <summary>
    /// Розраховує вартість бронювання з урахуванням ціни за добу.
    /// </summary>
    public decimal GetBookingCost(string clientFirstName, string clientLastName, string hotelName, int roomNumber)
    {
        var booking = GetBooking(clientFirstName, clientLastName, hotelName, roomNumber);
        if (booking == null)
            throw new ArgumentException("Booking not found.");
        return booking.CalculateTotalCost();
    }

    /// <summary>
    /// Повертає список клієнтів, які зробили бронювання.
    /// </summary>
    public IEnumerable<Client> GetClientsWithBookings()
    {
        return bookings.Select(b => b.Client).Distinct();
    }

    /// <summary>
    /// Додає запит на бронювання.
    /// </summary>
    public void AddBookingRequest(string hotelName, int roomNumber, string requestText, DateTime startDate, DateTime endDate)
    {
        var hotel = GetHotel(hotelName);
        if (hotel == null)
            throw new ArgumentException("Hotel not found.");

        // Перевірка існування кімнати в готелі
        var room = hotel.Rooms.FirstOrDefault(r => r.Number == roomNumber);
        if (room == null)
            throw new ArgumentException("Room not found.");

        bookingRequests.Add(new BookingRequest(hotelName, roomNumber, requestText, startDate, endDate));
    }

    /// <summary>
    /// Видаляє запит на бронювання.
    /// </summary>
    public void RemoveBookingRequest(string hotelName, int roomNumber)
    {
        var request = bookingRequests.FirstOrDefault(r =>
            r.HotelName.Equals(hotelName, StringComparison.OrdinalIgnoreCase) &&
            r.RoomNumber == roomNumber);
        if (request == null)
            throw new ArgumentException("Booking request not found.");
        bookingRequests.Remove(request);
    }

    /// <summary>
    /// Оновлює текст запиту на бронювання.
    /// </summary>
    public void UpdateBookingRequest(string hotelName, int roomNumber, string newText)
    {
        var request = bookingRequests.FirstOrDefault(r =>
            r.HotelName.Equals(hotelName, StringComparison.OrdinalIgnoreCase) &&
            r.RoomNumber == roomNumber);
        if (request == null)
            throw new ArgumentException("Booking request not found.");
        request.RequestText = newText;
    }

    /// <summary>
    /// Повертає запити на бронювання за заданим періодом.
    /// </summary>
    public IEnumerable<BookingRequest> GetBookingRequests(DateTime startDate, DateTime endDate)
    {
        return bookingRequests.Where(r => r.StartDate >= startDate && r.EndDate <= endDate);
    }

    /// <summary>
    /// Шукає готелі за ключовим словом.
    /// </summary>
    public IEnumerable<Hotel> SearchHotels(string keyword)
    {
        return hotels.Where(h => h.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Шукає клієнтів за ключовим словом.
    /// </summary>
    public IEnumerable<Client> SearchClients(string keyword)
    {
        return clients.Where(c => c.FirstName.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                                || c.LastName.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    }
}