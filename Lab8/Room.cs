using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Room
{
    public int Number { get; }
    public bool IsBooked { get; set; }

    public Room(int number)
    {
        Number = number;
        IsBooked = false;
    }
}

