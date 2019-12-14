using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QLHV_OOAD_Core.Models;

namespace QLHV_OOAD_Core.Models
{
    public class QLHVContext : DbContext
    {
        public QLHVContext (DbContextOptions<QLHVContext> options)
            : base(options)
        {
        }

        public DbSet<QLHV_OOAD_Core.Models.Users> Users { get; set; }

        public DbSet<QLHV_OOAD_Core.Models.KiemTra> KiemTra { get; set; }
    }
}
