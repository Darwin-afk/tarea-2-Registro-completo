using System;
using System.Collections.Generic;
using System.Text;
using Registro.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Registro.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Persona> Personas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server = DESKTOP-575M417\SQLEXPRESS; Database = PersonasDB; Trusted_Connection = True;");
        }
    }
}
