/// <summary>
/// Базовий абстрактний клас, що описує людину.
/// Демонструє абстракцію та успадкування.
/// </summary>
public abstract class Person
{
    /// <summary>
    /// Ідентифікатор особи.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Ім'я.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Прізвище.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Абстрактний метод для отримання інформації про особу.
    /// </summary>
    public abstract string GetInfo();
}