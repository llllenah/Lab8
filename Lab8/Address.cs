public class Address
{
    /// <summary>
    /// Місто.
    /// </summary>
    public string City { get; set; }
    /// <summary>
    /// Вулиця.
    /// </summary>
    public string Street { get; set; }

    /// <summary>
    /// Конструктор адреси.
    /// </summary>
    public Address(string city, string street)
    {
        City = city;
        Street = street;
    }
}