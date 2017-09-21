using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDSA2017.Lecture04
{
    public class FuturamaContext : DbContext
    {
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Character> Characters { get; set; }


        public FuturamaContext(DbContextOptions<FuturamaContext> options)
            :base(options)
        {
        }
    }
}
