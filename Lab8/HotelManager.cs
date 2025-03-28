public interface IHotelManager
{
    void AddHotel(string name, string city, string street, int totalRooms);
    void RemoveHotel(string name);
    Hotel GetHotel(string name);
    IEnumerable<Hotel> GetHotels();
    void UpdateHotel(string hotelName, string newName, string newCity, string newStreet, int newTotalRooms);
    IEnumerable<Hotel> SearchHotels(string keyword);
}
public class HotelManager : BaseManager<Hotel>, IHotelManager
{
    public void AddHotel(string name, string city, string street, int totalRooms)
    {
        if (string.IsNullOrWhiteSpace(name) || totalRooms <= 0)
            throw new ArgumentException("Невірні дані готелю.");
        AddItem(new Hotel(name, new Address(city, street), totalRooms));
    }

    public void RemoveHotel(string name)
    {
        var hotel = GetHotel(name);
        if (hotel == null)
            throw new ArgumentException("Готель не знайдено.");
        RemoveItem(hotel);
    }

    public Hotel GetHotel(string name)
    {
        return items.FirstOrDefault(h => h.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Hotel> GetHotels() => GetItems();

    public void UpdateHotel(string hotelName, string newName, string newCity, string newStreet, int newTotalRooms)
    {
        var hotel = GetHotel(hotelName);
        if (hotel == null)
            throw new ArgumentException("Готель не знайдено.");
        hotel.UpdateHotel(newName, newCity, newStreet, newTotalRooms);
    }

    public IEnumerable<Hotel> SearchHotels(string keyword)
    {
        return items.Where(h => h.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    }
}

