using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelManagementSystem.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace HotelManagementSystem.Web.Areas.Dashborad.ViewModel
{
    public class RoomBookingsViewModel
    {
        public int Id { get; set; }
        public string CutomerName { get; set; }
        public string CutomerAddress { get; set; }
        public string CutomerPhone { get; set; }

        [DisplayFormat(DataFormatString ="{0:d yyyy-MM-dd}")]
        public DateTime BookingFrom { get; set; }
        [DisplayFormat(DataFormatString = "{0:d yyyy-MM-dd}")]
        public DateTime BookingTo { get; set; }
        public int RoomsId { get; set; }
        public int NoOfMeMbers { get; set; }        
        public IEnumerable<SelectListItem> rooms { get; set; }

      
    }
    public class RoomBookingList
    {
        public IEnumerable<RoomBookings> roomBookings { get; set; }
    }
}