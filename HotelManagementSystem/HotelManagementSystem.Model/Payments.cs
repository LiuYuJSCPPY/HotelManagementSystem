using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Model
{
    [Table("Payments",Schema ="dbo")]
    public class Payments
    {
        public int Id { get; set; }
        public int RoomBookingsId { get; set; }
        public RoomBookings roomBookings { get; set; }
        public int PaymentTypeId { get; set; }
        public PaymentType paymentType { get; set; }
        public decimal PaymentAmount { get; set; }
        public bool IsActive { get; set; }
    }
}
