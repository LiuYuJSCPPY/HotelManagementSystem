using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace HotelManagementSystem.Model
{
    [Table("dbo",Schema ="RoomTypes")]
    public class RoomTypes
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="請務必填寫")]
        public string RoomType { get; set; }

        public virtual ICollection<RoomTypes> roomTypes { get; set; }
    }
}
