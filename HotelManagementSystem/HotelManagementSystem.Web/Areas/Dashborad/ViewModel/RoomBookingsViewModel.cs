using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelManagementSystem.Model;

namespace HotelManagementSystem.Web.Areas.Dashborad.ViewModel
{
    public class RoomBookingsViewModel
    {
        public int Id { get; set; }
        public string CutomerName { get; set; }
        public string CutomerAddress { get; set; }
        public DateTime BookingFrom { get; set; }
        public DateTime BookingTo { get; set; }
        public int RoomsId { get; set; }
        public Rooms rooms { get; set; }
        public int NoOfMeMbers { get; set; }
        public decimal TotalAmount { get; set; }
    }
    public class RoomBookingList
    {
        public IEnumerable<RoomBookings> roomBookings { get; set; }
    }
}