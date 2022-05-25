using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelManagementSystem.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace HotelManagementSystem.Web.Areas.Dashborad.ViewModel
{
    public class RoomViewModel
    {
        public int Id { get; set; }

       [DisplayName("房間型號")]
        public string RoomNumber { get; set; }

        [DisplayName("房間圖片")]
        public string RoomImage { get; set; }
        [DisplayName("房間價錢")]
        public decimal RoomPrice { get; set; }

        [DisplayName("訂房狀態")]
        public int BookingStatusId { get; set; }
        public List<SelectListItem> bookingStatus { get; set; }

        [DisplayName("房間型別")]
        public int RoomTypesId { get; set; }
        public List<SelectListItem> roomTypes { get; set; }

        [DisplayName("可住宿人數")]
        public int RoomCapacity { get; set; }

        [DisplayName("房間介紹")]
        public string RoomDescription { get; set; }

        [DisplayName("啟動")]
        public bool IsActive { get; set; }
    }
}