using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace HotelManagementSystem.Data
{
    public class HotelManagementSystemContext : DbContext
    {
        public HotelManagementSystemContext():base("name = HotelManagementSystemContext") {
        
        }
    }
}
