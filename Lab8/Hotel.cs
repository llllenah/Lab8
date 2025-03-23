using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class Hotel
    {
        public string Name { get; }
        public int TotalRooms { get; }
        public List<Room> Rooms { get; } = new List<Room>();

        public Hotel(string name, int totalRooms)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Hotel name cannot be empty.");
            if (totalRooms <= 0) throw new ArgumentException("Total rooms must be greater than zero.");

            Name = name;
            TotalRooms = totalRooms;
        }

        public void AddRoom(int number, decimal price)
        {
            if (price <= 0) throw new ArgumentException("Price must be greater than zero.");
            Rooms.Add(new Room(number, price));
        }

        public bool RemoveRoom(int number)
        {
            var room = Rooms.FirstOrDefault(r => r.Number == number);
            if (room != null)
            {
                Rooms.Remove(room);
                return true;
            }
            return false;
        }
    }
