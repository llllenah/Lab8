public class Client
{
    /// <summary>
    /// Ім'я клієнта.
    /// </summary>
    public string FirstName { get; set; }
    /// <summary>
    /// Прізвище клієнта.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Конструктор клієнта.
    /// </summary>
    public Client(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    /// <summary>
    /// Оновлює ім'я та прізвище клієнта.
    /// </summary>
    public void UpdateName(string newFirstName, string newLastName)
    {
        FirstName = newFirstName;
        LastName = newLastName;
    }
}