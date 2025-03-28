public class Program
{
    /// <summary>
    /// Головний метод демонстрації.
    /// </summary>
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        IHotelManager manager = new HotelManager();

        Console.WriteLine("=== Управління готелями ===");
        manager.AddHotel("Grand Hotel", "Kyiv", "Main Street", 50);
        manager.AddHotel("Ocean View", "Odesa", "Beach Avenue", 30);
        Console.WriteLine("Список готелів:");
        foreach (var hotel in manager.GetHotels())
        {
            Console.WriteLine($"- {hotel.Name} в {hotel.Address.City} ({hotel.TotalRooms} кімнат)");
        }

        Console.WriteLine("\nОновлення готелю 'Grand Hotel'...");
        manager.UpdateHotel("Grand Hotel", "New Grand Hotel", "Lviv", "Central Street", 40);
        var updatedHotel = manager.GetHotel("New Grand Hotel");
        Console.WriteLine($"Оновлений готель: {updatedHotel.Name} в {updatedHotel.Address.City} ({updatedHotel.TotalRooms} кімнат)");

        Console.WriteLine("\n=== Управління клієнтами ===");
        manager.AddClient("John", "Doe");
        manager.AddClient("Alice", "Smith");
        manager.AddClient("Test", "Test");
        Console.WriteLine("Список клієнтів:");
        foreach (var client in manager.GetClients())
        {
            Console.WriteLine($"- {client.FirstName} {client.LastName}");
        }

        Console.WriteLine("\nКлієнти, відсортовані за ім'ям:");
        foreach (var client in manager.GetClientsSortedByName())
        {
            Console.WriteLine($"- {client.FirstName} {client.LastName}");
        }
        Console.WriteLine("\nСписок");
        foreach (var client in manager.GetClients())
        {
            Console.WriteLine($"- {client.FirstName} {client.LastName}");
        }

        Console.WriteLine("\nКлієнти, відсортовані за прізвищем:");
        foreach (var client in manager.GetClientsSortedByLastName())
        {
            Console.WriteLine($"- {client.FirstName} {client.LastName}");
        }


        Console.WriteLine("\nОновлення клієнта 'John Doe'...");
        manager.UpdateClient("John", "Doe", "Johnny", "Doe");
        var updatedClient = manager.GetClient("Johnny", "Doe");
        Console.WriteLine($"Оновлений клієнт: {updatedClient.FirstName} {updatedClient.LastName}");


        Console.WriteLine("\n=== Управління бронюваннями ===");
        manager.AddBooking("Johnny", "Doe", "New Grand Hotel", 1, DateTime.Now, DateTime.Now.AddDays(3), 100, "Late check-in");
        manager.AddClient("Bob", "Brown");
        manager.AddBooking("Bob", "Brown", "New Grand Hotel", 2, DateTime.Now.AddDays(1), DateTime.Now.AddDays(4), 90, "Window room");

        Console.WriteLine("\nДетальна інформація про клієнта (Johnny Doe):");
        string detailedInfo = manager.GetDetailedClientInfo("Johnny", "Doe");
        Console.WriteLine(detailedInfo);

        Console.WriteLine("Список бронювань:");
        foreach (var booking in manager.GetBookings())
        {
            Console.WriteLine($"- {booking.Client.FirstName} {booking.Client.LastName} забронював кімнату {booking.Room.Number} у {booking.Hotel.Name} з {booking.StartDate.ToShortDateString()} до {booking.EndDate.ToShortDateString()}");
            Console.WriteLine($"  Ціна за добу: {booking.PricePerNight}, Загальна вартість: {booking.CalculateTotalCost()}");
        }

        Console.WriteLine("\nЗаброньовані кімнати у готелі New Grand Hotel:");
        var bookedRooms = manager.GetBookedRooms("New Grand Hotel");
        foreach (var room in bookedRooms)
        {
            Console.WriteLine($"- Кімната {room.Number}");
        }

        Console.WriteLine("\nДоступні кімнати у готелі New Grand Hotel:");
        var availableRooms = manager.GetAvailableRooms("New Grand Hotel");
        foreach (var room in availableRooms)
        {
            Console.WriteLine($"- Кімната {room.Number}");
        }

        Console.WriteLine("\nОбчислення вартості бронювання для Johnny Doe, кімната 1:");
        decimal cost = manager.GetBookingCost("Johnny", "Doe", "New Grand Hotel", 1);
        Console.WriteLine($"Загальна вартість: {cost}");

        Console.WriteLine("\nКлієнти з бронюваннями:");
        foreach (var c in manager.GetClientsWithBookings())
        {
            Console.WriteLine($"- {c.FirstName} {c.LastName}");
        }

        Console.WriteLine("\n=== Управління запитами на бронювання ===");
        DateTime now = DateTime.Now;
        manager.AddBookingRequest("New Grand Hotel", 3, "Need a quiet room", now, now.AddDays(2));
        Console.WriteLine("Список запитів на бронювання (наступні 5 днів):");
        foreach (var req in manager.GetBookingRequests(now, now.AddDays(5)))
        {
            Console.WriteLine($"- {req.HotelName}, кімната {req.RoomNumber}: {req.RequestText} (від {req.StartDate.ToShortDateString()} до {req.EndDate.ToShortDateString()})");
        }

        Console.WriteLine("\nОновлення запиту на бронювання для New Grand Hotel, кімната 3...");
        manager.UpdateBookingRequest("New Grand Hotel", 3, "Need a quiet room with extra pillows");
        Console.WriteLine("Запити після оновлення:");
        foreach (var req in manager.GetBookingRequests(now, now.AddDays(5)))
        {
            Console.WriteLine($"- {req.HotelName}, кімната {req.RoomNumber}: {req.RequestText}");
        }

        Console.WriteLine("\nВидалення запиту на бронювання для New Grand Hotel, кімната 3...");
        manager.RemoveBookingRequest("New Grand Hotel", 3);
        manager.AddBookingRequest("New Grand Hotel", 3, "Запит для кімнати 3", now, now.AddDays(2));
        manager.AddBookingRequest("New Grand Hotel", 4, "Запит для кімнати 4", now, now.AddDays(2));
        Console.WriteLine("Запити до видалення:");
        foreach (var req in manager.GetBookingRequests(now, now.AddDays(5)))
        {
            Console.WriteLine($"- {req.HotelName}, кімната {req.RoomNumber}: {req.RequestText}");
        }
        manager.RemoveBookingRequest("New Grand Hotel", 3);
        Console.WriteLine("\nЗапити після видалення (залишився запит для кімнати 4):");
        foreach (var req in manager.GetBookingRequests(now, now.AddDays(5)))
        {
            Console.WriteLine($"- {req.HotelName}, кімната {req.RoomNumber}: {req.RequestText}");
        }

        Console.WriteLine("\n=== Пошук ===");
        Console.WriteLine("Пошук готелів за ключовим словом 'Grand':");
        foreach (var h in manager.SearchHotels("Grand"))
        {
            Console.WriteLine($"- {h.Name}");
        }
        Console.WriteLine("Пошук клієнтів за ключовим словом 'Doe':");
        foreach (var c in manager.SearchClients("Doe"))
        {
            Console.WriteLine($"- {c.FirstName} {c.LastName}");
        }
    }
}
