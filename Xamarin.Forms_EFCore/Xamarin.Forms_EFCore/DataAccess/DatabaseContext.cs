using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms_EFCore.Helpers;
using Xamarin.Forms_EFCore.Models;
using System.Linq;

namespace Xamarin.Forms_EFCore.DataAccess {

    public class DatabaseContext : DbContext {

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }

        
        
        public DbSet<Profile> Profiles { get; set; }

        public DbSet<Izby> Rooms { get; set; }

        public DbSet<Lieky> Drugs { get; set; }

        public DbSet<Cas_Davkovania> DrugTakeTime { get; set; }
        public DbSet<Akcelerometer> Akcelerometers { get; set; }

        public DbSet<Hranice_Akcelerometer> AkcelerometersLimit { get; set; }

        public DbSet<Pohyb_Sekvencia> MovementSekv { get; set; }
        public DbSet<Hranice_Pohyb> MovementLimit { get; set; }

        public DbSet<Pohyb> Movement { get; set; }

        public DbSet<Teplota_Sekvencia> TemperatureSekv { get; set; }
        public DbSet<Hranice_Teplota> TemperatureLimit { get; set; }

        public DbSet<Teplota> Temperature { get; set; }

        public DbSet<Tep_Sekvencia> PulseSekv { get; set; }

        public DbSet<Hranice_Tep> PulseLimit { get; set; }

        public DbSet<Tep> Pulse { get; set; }

        public DbSet<Lieky_Sekvencia> DrugsSekv { get; set; }

        public DbSet<User> Users { get; set; }
        

        public DatabaseContext() {
            //this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

            var dbPath = DependencyService.Get<IDBPath>().GetDbPath();
            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }


       /* protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }*/
    }
}
