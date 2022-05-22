using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Model
{
    [Table("PaymentType",Schema ="dbo")]
    public class PaymentType
    {
        public int Id { get; set; }
        public string PaymentTypeName { get; set;}

        public virtual ICollection<Payments> Payments { get; set; }
    }
}
