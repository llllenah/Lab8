public class Hotel
{
    public string Name { get; set; }
    public Address Address { get; set; }
    public List<Room> Rooms { get; set; }
    public int TotalRooms { get; set; }
    public List<BookingRequest> BookingRequests { get; set; }

    public Hotel(string name, Address address, int totalRooms)
    {
        Name = name;
        Address = address;
        TotalRooms = totalRooms;
        Rooms = new List<Room>();
        BookingRequests = new List<BookingRequest>();
        for (int i = 1; i <= totalRooms; i++)
        {
            Rooms.Add(new Room(i, "Стандарт", 100));
        }
    }

    public void UpdateHotel(string newName, string newCity, string newStreet, int newTotalRooms)
    {
        Name = newName;
        Address.City = newCity;
        Address.Street = newStreet;
        TotalRooms = newTotalRooms;
        Rooms.Clear();
        for (int i = 1; i <= newTotalRooms; i++)
        {
            Rooms.Add(new Room(i, "Стандарт", 100));
        }
    }

    public IEnumerable<Room> GetBookedRooms()
    {
        return Rooms.Where(r => !r.IsAvailable);
    }

    public IEnumerable<Room> GetAvailableRooms()
    {
        return Rooms.Where(r => r.IsAvailable);
    }

    /// <summary>
    /// Додає запит на бронювання для певного номера.
    /// </summary>
    public void AddBookingRequest(int roomNumber, string requestText, DateTime startDate, DateTime endDate)
    {
        BookingRequests.Add(new BookingRequest(Name, roomNumber, requestText, startDate, endDate));
    }

    /// <summary>
    /// Видаляє запит на бронювання для певного номера.
    /// </summary>
    public void RemoveBookingRequest(int roomNumber)
    {
        var req = BookingRequests.FirstOrDefault(r => r.RoomNumber == roomNumber);
        if (req == null)
            throw new ArgumentException("Запит не знайдено.");
        BookingRequests.Remove(req);
    }

    /// <summary>
    /// Оновлює текст запиту на бронювання для певного номера.
    /// </summary>
    public void UpdateBookingRequest(int roomNumber, string newText)
    {
        var req = BookingRequests.FirstOrDefault(r => r.RoomNumber == roomNumber);
        if (req == null)
            throw new ArgumentException("Запит не знайдено.");
        req.UpdateRequestText(newText);
    }
}