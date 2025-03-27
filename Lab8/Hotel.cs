public class Hotel
{
    /// <summary>
    /// Назва готелю.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Адреса готелю.
    /// </summary>
    public Address Address { get; set; }
    /// <summary>
    /// Список кімнат.
    /// </summary>
    public List<Room> Rooms { get; set; }
    /// <summary>
    /// Загальна кількість кімнат.
    /// </summary>
    public int TotalRooms { get; set; }

    /// <summary>
    /// Конструктор готелю.
    /// </summary>
    public Hotel(string name, Address address, int totalRooms)
    {
        Name = name;
        Address = address;
        TotalRooms = totalRooms;
        Rooms = new List<Room>();

        // Ініціалізація кімнат від 1 до totalRooms.
        for (int i = 1; i <= totalRooms; i++)
        {
            Rooms.Add(new Room(i));
        }
    }

    /// <summary>
    /// Оновлює дані готелю та перевизначає список кімнат.
    /// </summary>
    public void UpdateHotel(string newName, string newCity, string newStreet, int newTotalRooms)
    {
        Name = newName;
        Address.City = newCity;
        Address.Street = newStreet;
        TotalRooms = newTotalRooms;
        Rooms.Clear();
        for (int i = 1; i <= newTotalRooms; i++)
        {
            Rooms.Add(new Room(i));
        }
    }

    /// <summary>
    /// Повертає список заброньованих кімнат.
    /// </summary>
    public IEnumerable<Room> GetBookedRooms()
    {
        return Rooms.Where(r => r.IsBooked);
    }

    /// <summary>
    /// Повертає список вільних кімнат.
    /// </summary>
    public IEnumerable<Room> GetAvailableRooms()
    {
        return Rooms.Where(r => !r.IsBooked);
    }
}