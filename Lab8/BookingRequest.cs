﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class BookingRequest
{
    public string HotelName { get; set; }
    public int RoomNumber { get; set; }
    public string RequestText { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public BookingRequest(string hotelName, int roomNumber, string requestText, DateTime startDate, DateTime endDate)
    {
        HotelName = hotelName;
        RoomNumber = roomNumber;
        RequestText = requestText;
        StartDate = startDate;
        EndDate = endDate;
    }
}
