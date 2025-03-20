using HotelBooking;

/// <summary>
/// Клас, що описує готель.
/// Містить список кімнат як композицію.
/// </summary>
public class Hotel
{
    /// <summary>
    /// Ідентифікатор готелю.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Назва готелю.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Опис або коротка інформація про готель.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Список кімнат у готелі.
    /// </summary>
    public List<Room> Rooms { get; set; } = new List<Room>();

    /// <summary>
    /// Повертає кількість вільних кімнат.
    /// </summary>
    public int GetFreeRoomCount()
    {
        return Rooms.Count(r => !r.IsBooked);
    }

    /// <summary>
    /// Повертає кількість заброньованих кімнат.
    /// </summary>
    public int GetBookedRoomCount()
    {
        return Rooms.Count(r => r.IsBooked);
    }
}