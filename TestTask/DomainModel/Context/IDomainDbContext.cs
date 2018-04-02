using System;
using System.Collections.Generic;
using System.Text;
using DomainModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DomainModel.Context
{
    public interface IDomainDbContext : IDisposable
    {
        DbSet<UserAnswer> UserAnswer { get; set; }
        DbSet<Sector> Sector { get; set; }

        int SaveChanges();
    }
}
