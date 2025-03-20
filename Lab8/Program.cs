using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelBooking
{
    /// <summary>
    /// Клас із методом Main, що демонструє використання системи.
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            BookingSystem system = new BookingSystem();

            // 1. Створимо кілька готелів
            Hotel h1 = new Hotel { Name = "Hotel Sunrise", Description = "Near the sea" };
            h1.Rooms.Add(new Room { RoomNumber = 101, Type = RoomType.Single, PricePerDay = 500 });
            h1.Rooms.Add(new Room { RoomNumber = 102, Type = RoomType.Double, PricePerDay = 800 });

            Hotel h2 = new Hotel { Name = "Mountain View", Description = "High in the mountains" };
            h2.Rooms.Add(new Room { RoomNumber = 1, Type = RoomType.Single, PricePerDay = 400 });
            h2.Rooms.Add(new Room { RoomNumber = 2, Type = RoomType.Suite, PricePerDay = 1500 });

            system.AddHotel(h1);
            system.AddHotel(h2);

            // 2. Додамо клієнтів
            Client c1 = new Client { FirstName = "Ivan", LastName = "Petrenko" };
            Client c2 = new Client { FirstName = "Olga", LastName = "Shevchenko" };
            system.AddClient(c1);
            system.AddClient(c2);

            // 3. Зробимо бронювання
            try
            {
                Reservation r1 = system.BookRoom(
                    clientId: c1.Id,
                    hotelId: h1.Id,
                    roomNumber: 101,
                    start: DateTime.Today,
                    end: DateTime.Today.AddDays(3));

                Console.WriteLine($"Бронювання створено. Id={r1.Id}, Вартість за 1 день: {r1.GetCostPerDay()}, " +
                                  $"Всього за період: {r1.GetTotalCost()}");

                // 4. Перевіримо кількість вільних та зайнятих кімнат у h1
                Console.WriteLine($"Вільних кімнат у '{h1.Name}': {system.GetFreeRoomsCountInHotel(h1.Id)}");
                Console.WriteLine($"Зайнятих кімнат у '{h1.Name}': {system.GetBookedRoomsCountInHotel(h1.Id)}");

                // 5. Пошукаємо клієнтів, які забронювали номери у h1
                var clientsInH1 = system.GetClientsByHotel(h1.Id);
                Console.WriteLine("Клієнти, що бронювали у h1:");
                foreach (var cl in clientsInH1)
                {
                    Console.WriteLine($"   {cl.GetInfo()}");
                }

                // 6. Скасуємо бронювання
                system.CancelReservation(r1.Id);
                Console.WriteLine("Бронювання скасовано.");

                // 7. Перевіримо, що кімната знову вільна
                Console.WriteLine($"Вільних кімнат у '{h1.Name}': {system.GetFreeRoomsCountInHotel(h1.Id)}");
            }
            catch (BookingException ex)
            {
                Console.WriteLine("Помилка бронювання: " + ex.Message);
            }

            Console.WriteLine("Натисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}
