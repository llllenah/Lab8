public class Room
{
    public int Number { get; set; }
    public string Type { get; set; }
    public decimal PricePerNight { get; set; }
    public bool IsAvailable { get; set; }

    public Room(int number, string type, decimal pricePerNight)
    {
        Number = number;
        Type = type;
        PricePerNight = pricePerNight;
        IsAvailable = true;
    }

    /// <summary>
    /// Позначає кімнату як заброньовану.
    /// </summary>
    public void MarkAsBooked()
    {
        IsAvailable = false;
    }

    /// <summary>
    /// Позначає кімнату як доступну.
    /// </summary>
    public void MarkAsAvailable()
    {
        IsAvailable = true;
    }
}