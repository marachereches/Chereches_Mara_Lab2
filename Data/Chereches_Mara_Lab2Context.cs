using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Chereches_Mara_Lab2.Models;

namespace Chereches_Mara_Lab2.Data
{
    public class Chereches_Mara_Lab2Context : DbContext
    {
        public Chereches_Mara_Lab2Context (DbContextOptions<Chereches_Mara_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Chereches_Mara_Lab2.Models.Book> Book { get; set; } = default!;

        public DbSet<Chereches_Mara_Lab2.Models.Publisher> Publisher { get; set; }

        public DbSet<Chereches_Mara_Lab2.Models.Author> Author { get; set; }

        public DbSet<Chereches_Mara_Lab2.Models.Category> Category { get; set; }
    }
}
