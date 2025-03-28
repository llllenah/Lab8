using System.Text;

public interface IClientManager
{
    void AddClient(string firstName, string lastName);
    void RemoveClient(string firstName, string lastName);
    void UpdateClient(string firstName, string lastName, string newFirstName, string newLastName);
    Client GetClient(string firstName, string lastName);
    IEnumerable<Client> GetClients();
    IEnumerable<Client> GetClientsSortedByName();
    IEnumerable<Client> GetClientsSortedByLastName();
    string GetDetailedClientInfo(string firstName, string lastName);
    IEnumerable<Client> SearchClients(string keyword);
    void SetBookings(IEnumerable<Booking> bookings); 
}
public class ClientManager : BaseManager<Client>, IClientManager
{
    private List<Booking> allBookings = new List<Booking>();

    public void AddClient(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Невірні дані клієнта.");
        AddItem(new Client(firstName, lastName));
    }

    public void RemoveClient(string firstName, string lastName)
    {
        var client = GetClient(firstName, lastName);
        if (client == null)
            throw new ArgumentException("Клієнт не знайдений.");
        RemoveItem(client);
    }

    public void UpdateClient(string firstName, string lastName, string newFirstName, string newLastName)
    {
        var client = GetClient(firstName, lastName);
        if (client == null)
            throw new ArgumentException("Клієнт не знайдений.");
        client.UpdateName(newFirstName, newLastName);
    }

    public Client GetClient(string firstName, string lastName)
    {
        return items.FirstOrDefault(c => c.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase)
                                        && c.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Client> GetClients() => GetItems();

    public IEnumerable<Client> GetClientsSortedByName() => GetItems().OrderBy(c => c.FirstName).ToList();

    public IEnumerable<Client> GetClientsSortedByLastName() => GetItems().OrderBy(c => c.LastName).ToList();

    public string GetDetailedClientInfo(string firstName, string lastName)
    {
        var client = GetClient(firstName, lastName);
        if (client == null)
            throw new ArgumentException("Клієнт не знайдений.");

        var clientBookings = allBookings.Where(b =>
            b.Client.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
            b.Client.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase)).ToList();

        int bookedRoomsCount = clientBookings.Select(b => b.Room.Number).Distinct().Count();

        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Клієнт: {client.FirstName} {client.LastName}");
        sb.AppendLine($"Кількість заброньованих кімнат: {bookedRoomsCount}");
        sb.AppendLine("Бронювання:");
        foreach (var b in clientBookings)
        {
            sb.AppendLine($"- Готель: {b.Hotel.Name}, кімната: {b.Room.Number}, " +
                          $"Дати: {b.StartDate.ToShortDateString()} - {b.EndDate.ToShortDateString()}, " +
                          $"Ціна за добу: {b.PricePerNight}, Загальна вартість: {b.CalculateTotalCost()}");
        }
        return sb.ToString();
    }

    public IEnumerable<Client> SearchClients(string keyword)
    {
        return items.Where(c => c.FirstName.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                              || c.LastName.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    }

    public void SetBookings(IEnumerable<Booking> bookings)
    {
        allBookings = bookings.ToList();
    }
}
