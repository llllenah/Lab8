public class BookingRequest
{
    /// <summary>
    /// Назва готелю.
    /// </summary>
    public string HotelName { get; set; }
    /// <summary>
    /// Номер кімнати.
    /// </summary>
    public int RoomNumber { get; set; }
    /// <summary>
    /// Текст запиту.
    /// </summary>
    public string RequestText { get; set; }
    /// <summary>
    /// Дата початку запиту.
    /// </summary>
    public DateTime StartDate { get; set; }
    /// <summary>
    /// Дата завершення запиту.
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Конструктор запиту на бронювання.
    /// </summary>
    public BookingRequest(string hotelName, int roomNumber, string requestText, DateTime startDate, DateTime endDate)
    {
        HotelName = hotelName;
        RoomNumber = roomNumber;
        RequestText = requestText;
        StartDate = startDate;
        EndDate = endDate;
    }
}