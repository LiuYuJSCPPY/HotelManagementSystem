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
        public HotelManagementSystemContext():base("name = HotelManagementSystemContext") {
        
        }

        public DbSet<BookingStatus> bookingStatuses { get; set; }
        public DbSet<Payments> payments { get; set; }
        public DbSet<PaymentType> paymentTypes { get; set; }
        public DbSet<RoomBookings> roomBookings { get; set; }
        public DbSet<Rooms> rooms { get; set; }
        public DbSet<RoomTypes> roomTypes { get; set; }
    }
}
