using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Model
{
    [Table("dbo",Schema ="BookingStatus")]
    public class BookingStatus
    {
        public int Id { get; set; }


        [Required(ErrorMessage ="請填寫名稱")]
        public string BookingStatusName { get; set; }
        public virtual ICollection<Rooms> rooms { get; set; }

    }
}
