public class Program
{
    public static void Main()
    {
        IHotelManager manager = new HotelManager();

        Console.WriteLine("=== HOTEL MANAGEMENT ===");

        manager.AddHotel("Grand Hotel", "Kyiv", "Main Street", 50);
        manager.AddHotel("Ocean View", "Odesa", "Beach Avenue", 30);
        manager.AddHotel("Mountain Inn", "Lviv", "Hill Road", 20);

        var hotel1 = manager.GetHotel("Grand Hotel");
        Console.WriteLine($"\nHotel Details: {hotel1.Name} in {hotel1.Address.City} with {hotel1.TotalRooms} rooms.");

        Console.WriteLine("\nAll Hotels:");
        foreach (var h in manager.GetHotels())
        {
            Console.WriteLine($"- {h.Name}: City = {h.Address.City}, Total Rooms = {h.TotalRooms}");
        }

        Console.WriteLine("\nRemoving 'Ocean View' hotel...");
        manager.RemoveHotel("Ocean View");

        Console.WriteLine("Hotels after removal:");
        foreach (var h in manager.GetHotels())
        {
            Console.WriteLine($"- {h.Name}");
        }

        Console.WriteLine("\n=== CLIENT MANAGEMENT ===");

        manager.AddClient("John", "Doe");
        manager.AddClient("Alice", "Smith");
        manager.AddClient("Bob", "Brown");

        var clientJohn = manager.GetClient("John", "Doe");
        Console.WriteLine($"\nClient Details: {clientJohn.FirstName} {clientJohn.LastName}");

        Console.WriteLine("\nUpdating 'John Doe' to 'Johnny Doe'...");
        manager.UpdateClient("John", "Doe", "Johnny", "Doe");

        var updatedClient = manager.GetClient("Johnny", "Doe");
        Console.WriteLine($"Updated Client: {updatedClient.FirstName} {updatedClient.LastName}");

        Console.WriteLine("\nAll Clients:");
        foreach (var c in manager.GetClients())
        {
            Console.WriteLine($"- {c.FirstName} {c.LastName}");
        }

        Console.WriteLine("\nClients Sorted by First Name:");
        foreach (var c in manager.GetClientsSortedByName())
        {
            Console.WriteLine($"- {c.FirstName} {c.LastName}");
        }

        Console.WriteLine("\nClients Sorted by Last Name:");
        foreach (var c in manager.GetClientsSortedByLastName())
        {
            Console.WriteLine($"- {c.FirstName} {c.LastName}");
        }

        Console.WriteLine("\nRemoving 'Alice Smith'...");
        manager.RemoveClient("Alice", "Smith");

        Console.WriteLine("Clients after removal:");
        foreach (var c in manager.GetClients())
        {
            Console.WriteLine($"- {c.FirstName} {c.LastName}");
        }

        Console.WriteLine("\n=== BOOKING MANAGEMENT ===");

        manager.AddBooking("Johnny", "Doe", "Grand Hotel", 1, DateTime.Now, DateTime.Now.AddDays(3), 100, "Late check-in");
        manager.AddBooking("Bob", "Brown", "Mountain Inn", 5, DateTime.Now.AddDays(1), DateTime.Now.AddDays(3), 80, "Near window");

        var booking1 = manager.GetBooking("Johnny", "Doe", "Grand Hotel", 1);
        Console.WriteLine($"\nBooking Details: {booking1.Client.FirstName} booked Room {booking1.Room.Number} at {booking1.Hotel.Name}");

        var booking = manager.GetBooking("Johnny", "Doe", "Grand Hotel", 1);
        if (booking != null)
        {
            decimal totalCost = manager.GetBookingCost("Johnny", "Doe", "Grand Hotel", 1);
            decimal pricePerNight = booking.PricePerNight;

            Console.WriteLine($"\nBooking Details for {booking.Client.FirstName} {booking.Client.LastName}:");
            Console.WriteLine($"Hotel: {booking.Hotel.Name}");
            Console.WriteLine($"Room: {booking.Room.Number}");
            Console.WriteLine($"Stay: {booking.StartDate.ToShortDateString()} to {booking.EndDate.ToShortDateString()}");
            Console.WriteLine($"Price per Night: {pricePerNight}");
            Console.WriteLine($"Total Booking Cost: {totalCost}");
        }
        else
        {
            Console.WriteLine("Booking not found.");
        }


        Console.WriteLine("\nBooked Rooms in Grand Hotel:");
        foreach (var room in manager.GetBookedRooms("Grand Hotel"))
        {
            Console.WriteLine($"- Room {room.Number}");
        }

        Console.WriteLine("\nAvailable Rooms in Grand Hotel:");
        foreach (var room in manager.GetAvailableRooms("Grand Hotel"))
        {
            Console.WriteLine($"- Room {room.Number}");
        }

        decimal costJohnny = manager.GetBookingCost("Johnny", "Doe", "Grand Hotel", 1);
        Console.WriteLine($"\nBooking Cost for Johnny Doe in Grand Hotel, Room 1: {costJohnny}");

        Console.WriteLine("\nClients with Bookings:");
        foreach (var c in manager.GetClientsWithBookings())
        {
            Console.WriteLine($"- {c.FirstName} {c.LastName}");
        }

        Console.WriteLine("\nCancelling Bob Brown's booking in Mountain Inn (Room 5)...");
        manager.RemoveBooking("Bob", "Brown", "Mountain Inn", 5);

        Console.WriteLine("Booked Rooms in Mountain Inn after cancellation:");
        foreach (var room in manager.GetBookedRooms("Mountain Inn"))
        {
            Console.WriteLine($"- Room {room.Number}");
        }

        Console.WriteLine("\nAll Bookings:");
        foreach (var b in manager.GetBookings())
        {
            Console.WriteLine($"- {b.Client.FirstName} {b.Client.LastName} booked {b.Hotel.Name} Room {b.Room.Number} from {b.StartDate.ToShortDateString()} to {b.EndDate.ToShortDateString()}");
        }

        Console.WriteLine("\nBookings within the next 5 days:");
        foreach (var b in manager.GetBookingsByDateRange(DateTime.Now, DateTime.Now.AddDays(5)))
        {
            Console.WriteLine($"- {b.Client.FirstName} {b.Client.LastName} in {b.Hotel.Name} Room {b.Room.Number}");
        }

        Console.WriteLine("\n=== BOOKING REQUEST MANAGEMENT ===");

        manager.AddBookingRequest("Grand Hotel", 2, "Need a quiet room", DateTime.Now, DateTime.Now.AddDays(2));
        manager.AddBookingRequest("Mountain Inn", 10, "High floor requested", DateTime.Now.AddDays(1), DateTime.Now.AddDays(4));

        Console.WriteLine("\nBooking Requests (within next 5 days):");
        foreach (var br in manager.GetBookingRequests(DateTime.Now, DateTime.Now.AddDays(5)))
        {
            Console.WriteLine($"- {br.HotelName} Room {br.RoomNumber}: {br.RequestText} (from {br.StartDate.ToShortDateString()} to {br.EndDate.ToShortDateString()})");
        }

        Console.WriteLine("\nUpdating booking request for Grand Hotel Room 2...");
        manager.UpdateBookingRequest("Grand Hotel", 2, "Need a quiet room with extra pillows");

        Console.WriteLine("Booking Requests after update:");
        foreach (var br in manager.GetBookingRequests(DateTime.Now, DateTime.Now.AddDays(5)))
        {
            Console.WriteLine($"- {br.HotelName} Room {br.RoomNumber}: {br.RequestText}");
        }

        Console.WriteLine("\nRemoving booking request for Mountain Inn Room 10...");
        manager.RemoveBookingRequest("Mountain Inn", 10);

        Console.WriteLine("Booking Requests after removal:");
        foreach (var br in manager.GetBookingRequests(DateTime.Now, DateTime.Now.AddDays(5)))
        {
            Console.WriteLine($"- {br.HotelName} Room {br.RoomNumber}: {br.RequestText}");
        }

        Console.WriteLine("\n=== SEARCH FUNCTIONALITY ===");

        Console.WriteLine("\nSearching for hotels with keyword 'Inn':");
        foreach (var h in manager.SearchHotels("Inn"))
        {
            Console.WriteLine($"- {h.Name}");
        }

        Console.WriteLine("\nSearching for clients with keyword 'Doe':");
        foreach (var c in manager.SearchClients("Doe"))
        {
            Console.WriteLine($"- {c.FirstName} {c.LastName}");
        }
    }
}
