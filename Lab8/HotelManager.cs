using System.Text;

public interface IHotelManager
{
    /// <summary>
    /// 1.1. Додає новий готель.
    /// </summary>
    void AddHotel(string name, string city, string street, int totalRooms);

    /// <summary>
    /// 1.2. Видаляє готель за назвою.
    /// </summary>
    void RemoveHotel(string name);

    /// <summary>
    /// 1.3. Повертає готель за назвою.
    /// </summary>
    Hotel GetHotel(string name);

    /// <summary>
    /// 1.4. Повертає список усіх готелів (опис і кількість місць).
    /// </summary>
    IEnumerable<Hotel> GetHotels();

    /// <summary>
    /// 1.3/1.4. Оновлює дані готелю.
    /// </summary>
    void UpdateHotel(string hotelName, string newName, string newCity, string newStreet, int newTotalRooms);

    /// <summary>
    /// 2.1. Додає нового клієнта.
    /// </summary>
    void AddClient(string firstName, string lastName);

    /// <summary>
    /// 2.2. Видаляє клієнта.
    /// </summary>
    void RemoveClient(string firstName, string lastName);

    /// <summary>
    /// 2.3. Оновлює дані клієнта.
    /// </summary>
    void UpdateClient(string firstName, string lastName, string newFirstName, string newLastName);

    /// <summary>
    /// 2.4. Повертає дані про конкретного клієнта.
    /// </summary>
    Client GetClient(string firstName, string lastName);

    /// <summary>
    /// 2.5. Повертає список усіх клієнтів.
    /// </summary>
    IEnumerable<Client> GetClients();

    /// <summary>
    /// 2.6. Повертає список клієнтів, відсортованих за іменем (повертає новий список).
    /// </summary>
    IEnumerable<Client> GetClientsSortedByName();

    /// <summary>
    /// 2.7. Повертає список клієнтів, відсортованих за прізвищем (повертає новий список).
    /// </summary>
    IEnumerable<Client> GetClientsSortedByLastName();

    /// <summary>
    /// Додатковий метод: повертає детальну інформацію про клієнта.
    /// </summary>
    /// <param name="firstName">Ім'я клієнта.</param>
    /// <param name="lastName">Прізвище клієнта.</param>
    /// <returns>Рядок з детальною інформацією про клієнта.</returns>
    string GetDetailedClientInfo(string firstName, string lastName);

    /// <summary>
    /// 3.1. Додає бронювання номера для клієнта.
    /// </summary>
    void AddBooking(string clientFirstName, string clientLastName, string hotelName, int roomNumber,
                    DateTime startDate, DateTime endDate, decimal pricePerNight, string requestText);

    /// <summary>
    /// 3.2. Видаляє бронювання.
    /// </summary>
    void RemoveBooking(string clientFirstName, string clientLastName, string hotelName, int roomNumber);

    /// <summary>
    /// 3.3. Оновлює текст заявки підтвердженого бронювання.
    /// </summary>
    void UpdateBookingRequest(string clientFirstName, string clientLastName, string hotelName, int roomNumber, string newRequestText);

    /// <summary>
    /// 3.3. Повертає дані про конкретне бронювання.
    /// </summary>
    Booking GetBooking(string clientFirstName, string clientLastName, string hotelName, int roomNumber);

    /// <summary>
    /// 3.4. Повертає дані про заброньовані місця в готелі.
    /// </summary>
    IEnumerable<Room> GetBookedRooms(string hotelName);

    /// <summary>
    /// 3.5. Повертає дані про вільні місця в готелі.
    /// </summary>
    IEnumerable<Room> GetAvailableRooms(string hotelName);

    /// <summary>
    /// 3.6. Обчислює вартість бронювання з урахуванням ціни за добу.
    /// </summary>
    decimal GetBookingCost(string clientFirstName, string clientLastName, string hotelName, int roomNumber);

    /// <summary>
    /// 3.7. Повертає дані про клієнтів, які забронювали номери.
    /// </summary>
    IEnumerable<Client> GetClientsWithBookings();

    /// <summary>
    /// 1.5. Додає запит на бронювання певного номера на певний термін.
    /// </summary>
    void AddBookingRequest(string hotelName, int roomNumber, string requestText, DateTime startDate, DateTime endDate);

    /// <summary>
    /// 1.6. Видаляє запит на бронювання певного номера.
    /// </summary>
    void RemoveBookingRequest(string hotelName, int roomNumber);

    /// <summary>
    /// 1.7. Замінює текст запиту на бронювання певного номера.
    /// </summary>
    void UpdateBookingRequest(string hotelName, int roomNumber, string newText);

    /// <summary>
    /// 1.8. Повертає дані про запити на бронювання номерів в готелі за певний термін.
    /// </summary>
    IEnumerable<BookingRequest> GetBookingRequests(DateTime startDate, DateTime endDate);

    /// <summary>
    /// 4.1. Пошук готелів за ключовим словом.
    /// </summary>
    IEnumerable<Hotel> SearchHotels(string keyword);

    /// <summary>
    /// 4.2. Пошук клієнтів за ключовим словом.
    /// </summary>
    IEnumerable<Client> SearchClients(string keyword);
    IEnumerable<Booking> GetBookings();

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
            throw new ArgumentException("Невірні дані готелю.");
        hotels.Add(new Hotel(name, new Address(city, street), totalRooms));
    }

    public void RemoveHotel(string name)
    {
        var hotel = hotels.FirstOrDefault(h => h.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (hotel == null)
            throw new ArgumentException("Готель не знайдено.");
        hotels.Remove(hotel);
    }

    public Hotel GetHotel(string name) => hotels.FirstOrDefault(h => h.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

    public IEnumerable<Hotel> GetHotels() => hotels;

    public void UpdateHotel(string hotelName, string newName, string newCity, string newStreet, int newTotalRooms)
    {
        var hotel = GetHotel(hotelName);
        if (hotel == null)
            throw new ArgumentException("Готель не знайдено.");
        hotel.UpdateHotel(newName, newCity, newStreet, newTotalRooms);
    }

    public void AddClient(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Невірні дані клієнта.");
        clients.Add(new Client(firstName, lastName));
    }

    public void RemoveClient(string firstName, string lastName)
    {
        var client = GetClient(firstName, lastName);
        if (client == null)
            throw new ArgumentException("Клієнт не знайдений.");
        clients.Remove(client);
    }

    public void UpdateClient(string firstName, string lastName, string newFirstName, string newLastName)
    {
        var client = GetClient(firstName, lastName);
        if (client == null)
            throw new ArgumentException("Клієнт не знайдений.");
        client.UpdateName(newFirstName, newLastName);
    }

    public Client GetClient(string firstName, string lastName)
        => clients.FirstOrDefault(c => c.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase)
                                      && c.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));

    public IEnumerable<Client> GetClients() => clients;

    
    public IEnumerable<Client> GetClientsSortedByName() => clients.OrderBy(c => c.FirstName).ToList();

    
    public IEnumerable<Client> GetClientsSortedByLastName() => clients.OrderBy(c => c.LastName).ToList();

    
    public string GetDetailedClientInfo(string firstName, string lastName)
    {
        var client = GetClient(firstName, lastName);
        if (client == null)
            throw new ArgumentException("Клієнт не знайдений.");

        var clientBookings = bookings
            .Where(b => b.Client.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase)
                     && b.Client.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase))
            .ToList();

        int bookedRoomsCount = clientBookings.Select(b => b.Room.Number).Distinct().Count();

        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Клієнт: {client.FirstName} {client.LastName}");
        sb.AppendLine($"Кількість заброньованих кімнат: {bookedRoomsCount}");
        sb.AppendLine("Бронювання:");
        foreach (var b in clientBookings)
        {
            sb.AppendLine($"- Готель: {b.Hotel.Name}, кімната: {b.Room.Number}, " +
                          $"Дати: {b.StartDate.ToShortDateString()} - {b.EndDate.ToShortDateString()}, " +
                          $"Ціна за добу: {b.PricePerNight}, Загальна вартість: {b.CalculateTotalCost()}");
        }
        return sb.ToString();
    }

    
    public void AddBooking(string clientFirstName, string clientLastName, string hotelName, int roomNumber,
                           DateTime startDate, DateTime endDate, decimal pricePerNight, string requestText)
    {
        Client client = GetClient(clientFirstName, clientLastName);
        if (client == null)
            throw new ArgumentException("Клієнт не знайдений.");

        Hotel hotel = GetHotel(hotelName);
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

    
    public IEnumerable<Room> GetBookedRooms(string hotelName)
    {
        Hotel hotel = GetHotel(hotelName);
        if (hotel == null)
            throw new ArgumentException("Готель не знайдений.");
        var booked = hotel.GetBookedRooms();
        Console.WriteLine($"Кількість заброньованих кімнат: {booked.Count()}");
        return booked;
    }

    
    public IEnumerable<Room> GetAvailableRooms(string hotelName)
    {
        Hotel hotel = GetHotel(hotelName);
        if (hotel == null)
            throw new ArgumentException("Готель не знайдений.");
        var available = hotel.GetAvailableRooms();
        Console.WriteLine($"Кількість доступних кімнат: {available.Count()}");
        return available;
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

    
    public void AddBookingRequest(string hotelName, int roomNumber, string requestText, DateTime startDate, DateTime endDate)
    {
        Hotel hotel = GetHotel(hotelName);
        if (hotel == null)
            throw new ArgumentException("Готель не знайдений.");
        hotel.AddBookingRequest(roomNumber, requestText, startDate, endDate);
        bookingRequests.Add(new BookingRequest(hotelName, roomNumber, requestText, startDate, endDate));
    }

    
    public void RemoveBookingRequest(string hotelName, int roomNumber)
    {
        Hotel hotel = GetHotel(hotelName);
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
        Hotel hotel = GetHotel(hotelName);
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

    
    public IEnumerable<BookingRequest> GetBookingRequests(DateTime startDate, DateTime endDate)
    {
        return bookingRequests.Where(r => r.StartDate >= startDate && r.EndDate <= endDate);
    }

    
    public IEnumerable<Hotel> SearchHotels(string keyword)
    {
        return hotels.Where(h => h.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    }

    
    public IEnumerable<Client> SearchClients(string keyword)
    {
        return clients.Where(c => c.FirstName.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                                || c.LastName.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    }

}