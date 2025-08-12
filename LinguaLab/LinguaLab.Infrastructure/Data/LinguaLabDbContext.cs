using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinguaLab.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LinguaLab.Infrastructure.Data
{
    public class LinguaLabDbContext : DbContext
    {
        public LinguaLabDbContext(DbContextOptions<LinguaLabDbContext> options)
            :base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
