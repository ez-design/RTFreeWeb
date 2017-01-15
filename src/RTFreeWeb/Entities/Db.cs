using Microsoft.EntityFrameworkCore;
using RTFreeWeb.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RTFreeWeb.Entities
{
    public class Db : DbContext
    {
        public DbSet<Config>  Configs   { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Program> Programs  { get; set; }
        public DbSet<Station> Stations  { get; set; }
        public DbSet<User>    Users     { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(AppSettings.ConnectionString);
        }
    }
}
