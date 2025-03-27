public class Room
{
    /// <summary>
    /// Номер кімнати.
    /// </summary>
    public int Number { get; set; }
    /// <summary>
    /// Прапорець, що вказує, чи заброньована кімната.
    /// </summary>
    public bool IsBooked { get; set; }

    /// <summary>
    /// Конструктор кімнати.
    /// </summary>
    public Room(int number)
    {
        Number = number;
        IsBooked = false;
    }

    /// <summary>
    /// Встановлює статус кімнати як заброньована.
    /// </summary>
    public void MarkAsBooked()
    {
        IsBooked = true;
    }

    /// <summary>
    /// Встановлює статус кімнати як вільна.
    /// </summary>
    public void MarkAsAvailable()
    {
        IsBooked = false;
    }
}