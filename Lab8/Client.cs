/// <summary>
/// Клас, що описує клієнта системи.
/// Успадковує абстрактний клас Person.
/// </summary>
public class Client : Person
{
    /// <summary>
    /// Переозначений метод, що повертає інформацію про клієнта.
    /// </summary>
    public override string GetInfo()
    {
        return $"Client: {FirstName} {LastName}, Id={Id}";
    }
}