
/// <summary>
/// Клас, що описує менеджера або працівника готелю.
/// Демонструє успадкування від Person.
/// </summary>
public class Manager : Person
{
    /// <summary>
    /// Посада менеджера.
    /// </summary>
    public string Position { get; set; }

    /// <summary>
    /// Переозначений метод, що повертає інформацію про менеджера.
    /// </summary>
    public override string GetInfo()
    {
        return $"Manager: {FirstName} {LastName}, Pos={Position}, Id={Id}";
    }
}

