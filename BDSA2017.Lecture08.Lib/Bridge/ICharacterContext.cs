using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BDSA2017.Lecture08.Lib.Bridge
{   
    public interface ICharacterContext : IDisposable
    {
        DbSet<Character> Characters { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
