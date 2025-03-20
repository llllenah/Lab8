/// <summary>
/// Клас, що описує бронювання номера в готелі.
/// Містить посилання на клієнта, готель, кімнату, дати заїзду/виїзду тощо.
/// </summary>
public class Reservation
{
    /// <summary>
    /// Ідентифікатор бронювання.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Клієнт, що зробив бронювання.
    /// </summary>
    public Client Client { get; set; }

    /// <summary>
    /// Готель, у якому заброньовано кімнату.
    /// </summary>
    public Hotel Hotel { get; set; }

    /// <summary>
    /// Конкретна кімната, що заброньована.
    /// </summary>
    public Room Room { get; set; }

    /// <summary>
    /// Дата заїзду.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Дата виїзду.
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Обчислює вартість за весь період (різниця днів * ціна кімнати).
    /// </summary>
    public decimal GetTotalCost()
    {
        int totalDays = (EndDate - StartDate).Days;
        if (totalDays < 1) totalDays = 1; // мінімум одна доба
        return Room.PricePerDay * totalDays;
    }

    /// <summary>
    /// Обчислює вартість за одну добу.
    /// </summary>
    public decimal GetCostPerDay()
    {
        return Room.PricePerDay;
    }
}