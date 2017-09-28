using Microsoft.EntityFrameworkCore;
using System;

namespace BDSA2017.Lecture05.Entities
{
    public interface IFuturamaContext : IDisposable
    {
        DbSet<Actor> Actors { get; set; }
        DbSet<Character> Characters { get; set; }
        DbSet<EpisodeCharacter> EpisodeCharacters { get; set; }
        DbSet<Episode> Episodes { get; set; }
        int SaveChanges();
    }
}