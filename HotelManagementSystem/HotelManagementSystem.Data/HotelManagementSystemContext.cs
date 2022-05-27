using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagementSystem.Model;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HotelManagementSystem.Data
{
    public class HotelManagementSystemContext : IdentityDbContext<AdminUser>
    {
        public HotelManagementSystemContext():base("name =HotelManagementSystemContext") {
        
        }

        public static HotelManagementSystemContext Create()
       {
            return new HotelManagementSystemContext ();
        }

        public virtual DbSet<BookingStatus> bookingStatuses { get; set; }
        public virtual DbSet<Payments> payments { get; set; }
        public virtual DbSet<PaymentType> paymentTypes { get; set; }
        public virtual DbSet<RoomBookings> roomBookings { get; set; }
        public virtual DbSet<Rooms> rooms { get; set; }
        public virtual DbSet<RoomTypes> roomTypes { get; set; }

        
    }
}
