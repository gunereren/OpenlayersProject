using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class OpenlayersContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString: @"Server=(localdb)\mssqllocaldb;Database=Openlayers;Trusted_Connection=true");
        }

        public DbSet<Parcel> Parcels { get; set; }
        // Veritabanı ile ilgili nesnenin bağlandığı yer.
    }
}

