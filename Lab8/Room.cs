using HotelBooking;

/// <summary>
/// Перерахування, що описує можливі типи кімнат.
/// </summary>
public enum RoomType
{
    Single,
    Double,
    Suite
}

/// <summary>
/// Клас, що описує кімнату в готелі.
/// Демонструє композицію (кімната існує в межах готелю).
/// </summary>
public class Room
{
    /// <summary>
    /// Номер кімнати (у сенсі "№12").
    /// </summary>
    public int RoomNumber { get; set; }

    /// <summary>
    /// Тип кімнати (Single, Double, Suite).
    /// </summary>
    public RoomType Type { get; set; }

    /// <summary>
    /// Ціна за добу.
    /// </summary>
    public decimal PricePerDay { get; set; }

    /// <summary>
    /// Прапорець, чи зайнята кімната.
    /// </summary>
    public bool IsBooked { get; set; }
}