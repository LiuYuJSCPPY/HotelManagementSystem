using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace HotelManagementSystem.Model
{
    [Table("RoomBookings",Schema ="dbo")]
    public class RoomBookings
    {
        public int Id { get; set; }
        public string CutomerName { get; set; }
        public string CutomerAddress { get; set; }
        public string CutomerPhone { get; set; }
        public DateTime BookingFrom { get; set; }
        public DateTime BookingTo { get; set; }
        public int RoomsId { get; set; }
        public Rooms rooms { get; set; }
        public int NoOfMeMbers { get; set; }

        public decimal TotalAmount { get; set; }


        public virtual ICollection<Payments> Payments { get; set; }

    }
}
