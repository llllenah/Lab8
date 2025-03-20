/// <summary>
/// Власний виняток, що може виникати в системі бронювання.
/// </summary>
public class BookingException : Exception
{
    public BookingException(string message) : base(message)
    {
    }
}

/// <summary>
/// Клас, що керує списками готелів, клієнтів та бронювань.
/// Містить методи для додавання/видалення/редагування та пошуку.
/// </summary>
public class BookingSystem
{
    private List<Hotel> _hotels = new List<Hotel>();
    private List<Client> _clients = new List<Client>();
    private List<Reservation> _reservations = new List<Reservation>();

    private int _nextHotelId = 1;
    private int _nextClientId = 1;
    private int _nextReservationId = 1;

    /// <summary>
    /// Додає новий готель до системи.
    /// </summary>
    public void AddHotel(Hotel hotel)
    {
        hotel.Id = _nextHotelId++;
        _hotels.Add(hotel);
    }

    /// <summary>
    /// Видаляє готель за вказаним Id.
    /// </summary>
    public void RemoveHotel(int hotelId)
    {
        var hotel = _hotels.FirstOrDefault(h => h.Id == hotelId);
        if (hotel == null)
        {
            throw new BookingException($"Готель з Id={hotelId} не знайдено.");
        }
        _hotels.Remove(hotel);
    }

    /// <summary>
    /// Повертає готель за Id.
    /// </summary>
    public Hotel GetHotelById(int hotelId)
    {
        return _hotels.FirstOrDefault(h => h.Id == hotelId);
    }

    /// <summary>
    /// Повертає список усіх готелів.
    /// </summary>
    public List<Hotel> GetAllHotels()
    {
        return _hotels;
    }

    /// <summary>
    /// Додає клієнта до системи.
    /// </summary>
    public void AddClient(Client client)
    {
        client.Id = _nextClientId++;
        _clients.Add(client);
    }

    /// <summary>
    /// Видаляє клієнта за Id.
    /// </summary>
    public void RemoveClient(int clientId)
    {
        var client = _clients.FirstOrDefault(c => c.Id == clientId);
        if (client == null)
        {
            throw new BookingException($"Клієнта з Id={clientId} не знайдено.");
        }
        _clients.Remove(client);
    }

    /// <summary>
    /// Повертає клієнта за Id.
    /// </summary>
    public Client GetClientById(int clientId)
    {
        return _clients.FirstOrDefault(c => c.Id == clientId);
    }

    /// <summary>
    /// Повертає список усіх клієнтів.
    /// </summary>
    public List<Client> GetAllClients()
    {
        return _clients;
    }

    /// <summary>
    /// Створює бронювання для клієнта (clientId) у готелі (hotelId) 
    /// на конкретну кімнату (roomNumber) з вказаними датами.
    /// </summary>
    public Reservation BookRoom(int clientId, int hotelId, int roomNumber, DateTime start, DateTime end)
    {
        var client = GetClientById(clientId);
        if (client == null)
        {
            throw new BookingException($"Клієнт з Id={clientId} не знайдений.");
        }

        var hotel = GetHotelById(hotelId);
        if (hotel == null)
        {
            throw new BookingException($"Готель з Id={hotelId} не знайдений.");
        }

        var room = hotel.Rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
        if (room == null)
        {
            throw new BookingException($"Кімната №{roomNumber} не знайдена у готелі {hotel.Name}.");
        }

        if (room.IsBooked)
        {
            throw new BookingException($"Кімната №{roomNumber} вже заброньована.");
        }

        room.IsBooked = true;
        var reservation = new Reservation
        {
            Id = _nextReservationId++,
            Client = client,
            Hotel = hotel,
            Room = room,
            StartDate = start,
            EndDate = end
        };
        _reservations.Add(reservation);

        return reservation;
    }

    /// <summary>
    /// Скасовує бронювання за Id.
    /// </summary>
    public void CancelReservation(int reservationId)
    {
        var reservation = _reservations.FirstOrDefault(r => r.Id == reservationId);
        if (reservation == null)
        {
            throw new BookingException($"Бронювання з Id={reservationId} не знайдено.");
        }
        // звільняємо кімнату
        reservation.Room.IsBooked = false;
        _reservations.Remove(reservation);
    }

    /// <summary>
    /// Повертає об'єкт бронювання за його Id.
    /// </summary>
    public Reservation GetReservationById(int reservationId)
    {
        return _reservations.FirstOrDefault(r => r.Id == reservationId);
    }

    /// <summary>
    /// Повертає список усіх бронювань.
    /// </summary>
    public List<Reservation> GetAllReservations()
    {
        return _reservations;
    }

    /// <summary>
    /// Повертає кількість вільних місць у вказаному готелі.
    /// </summary>
    public int GetFreeRoomsCountInHotel(int hotelId)
    {
        var hotel = GetHotelById(hotelId);
        if (hotel == null)
        {
            throw new BookingException($"Готель з Id={hotelId} не знайдено.");
        }
        return hotel.GetFreeRoomCount();
    }

    /// <summary>
    /// Повертає кількість заброньованих місць у вказаному готелі.
    /// </summary>
    public int GetBookedRoomsCountInHotel(int hotelId)
    {
        var hotel = GetHotelById(hotelId);
        if (hotel == null)
        {
            throw new BookingException($"Готель з Id={hotelId} не знайдено.");
        }
        return hotel.GetBookedRoomCount();
    }

    /// <summary>
    /// Шукає готелі за ключовим словом у назві або описі.
    /// </summary>
    public List<Hotel> SearchHotels(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword)) return new List<Hotel>();
        keyword = keyword.ToLower();
        return _hotels
            .Where(h => h.Name.ToLower().Contains(keyword) ||
                        (h.Description != null && h.Description.ToLower().Contains(keyword)))
            .ToList();
    }

    /// <summary>
    /// Шукає клієнтів за ключовим словом (у імені чи прізвищі).
    /// </summary>
    public List<Client> SearchClients(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword)) return new List<Client>();
        keyword = keyword.ToLower();
        return _clients
            .Where(c => c.FirstName.ToLower().Contains(keyword) ||
                        c.LastName.ToLower().Contains(keyword))
            .ToList();
    }

    /// <summary>
    /// Повертає всіх клієнтів, які забронювали кімнати у вказаному готелі.
    /// </summary>
    public List<Client> GetClientsByHotel(int hotelId)
    {
        var hotel = GetHotelById(hotelId);
        if (hotel == null)
        {
            throw new BookingException($"Готель з Id={hotelId} не знайдено.");
        }

        return _reservations
            .Where(r => r.Hotel.Id == hotelId)
            .Select(r => r.Client)
            .Distinct()
            .ToList();
    }
}