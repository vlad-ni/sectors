using DomainModel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel.Context
{
    public class DomainDbContext : DbContext, IDomainDbContext
    {
        public DomainDbContext() { }

        public DomainDbContext(DbContextOptions<DomainDbContext> options) : base(options) { }

        public DbSet<UserAnswer> UserAnswer { get; set; }
        public DbSet<Sector> Sector { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
