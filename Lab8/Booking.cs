public class Booking
{
    /// <summary>
    /// Клієнт, який зробив бронювання.
    /// </summary>
    public Client Client { get; set; }
    /// <summary>
    /// Готель, де зроблено бронювання.
    /// </summary>
    public Hotel Hotel { get; set; }
    /// <summary>
    /// Номер, який заброньовано.
    /// </summary>
    public Room Room { get; set; }
    /// <summary>
    /// Дата початку бронювання.
    /// </summary>
    public DateTime StartDate { get; set; }
    /// <summary>
    /// Дата завершення бронювання.
    /// </summary>
    public DateTime EndDate { get; set; }
    /// <summary>
    /// Ціна за добу.
    /// </summary>
    public decimal PricePerNight { get; set; }
    /// <summary>
    /// Додатковий текст заявки.
    /// </summary>
    public string RequestText { get; set; }

    /// <summary>
    /// Конструктор бронювання.
    /// </summary>
    public Booking(Client client, Hotel hotel, Room room, DateTime startDate, DateTime endDate, decimal pricePerNight, string requestText)
    {
        Client = client;
        Hotel = hotel;
        Room = room;
        StartDate = startDate;
        EndDate = endDate;
        PricePerNight = pricePerNight;
        RequestText = requestText;
    }

    /// <summary>
    /// Розраховує загальну вартість бронювання (кількість ночей * ціна за добу).
    /// </summary>
    public decimal CalculateTotalCost()
    {
        int numberOfNights = (EndDate - StartDate).Days;
        return PricePerNight * numberOfNights;
    }
}