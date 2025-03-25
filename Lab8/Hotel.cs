using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
public class Hotel
{
    public string Name { get; set; }
    public Address Address { get; set; }
    public List<Room> Rooms { get; set; }
    public int TotalRooms { get; set; }

    public Hotel(string name, Address address, int totalRooms)
    {
        Name = name;
        Address = address;
        TotalRooms = totalRooms;
        Rooms = new List<Room>();

        for (int i = 1; i <= totalRooms; i++)
        {
            Rooms.Add(new Room(i));
        }
    }
}