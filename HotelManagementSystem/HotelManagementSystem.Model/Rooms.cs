using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Model
{
    [Table("Rooms",Schema ="dbo")]
    public class Rooms
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public string RoomImage { get; set; }
        public decimal RoomPrice { get; set; }
        public int BookingStatusId { get; set; }
        public BookingStatus bookingStatus { get; set; }

        public int RoomTypesId { get; set; }
        public RoomTypes roomTypes { get; set; }
        public int RoomCapacity { get; set; }
        public string RoomDescription { get; set; }
        public bool IsActive { get; set; }


        public virtual ICollection<RoomBookings> RoomBookings { get; set; }
    }
}
