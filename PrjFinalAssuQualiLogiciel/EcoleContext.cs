using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PrjFinalAssuQualiLogiciel;

public partial class EcoleContext : DbContext
{
    public EcoleContext()
    {
    }

    public DbSet<Cour> Cours { get; set; }

    public DbSet<Etudiant> Etudiants { get; set; }

    public DbSet<Note> Notes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=MOUHIEDDINEAMN\\SQLEXPRESS;Initial Catalog=Ecole;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;");

  
}
  
