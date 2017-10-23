using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BDSA2017.Lecture08.Lib.Bridge
{
    public interface ICharacterRepository : IDisposable
    {
        Task<int> CreateAsync(Character character);

        Task<Character> FindAsync(int id);

        Task<IEnumerable<Character>> ReadAsync();

        Task<bool> UpdateAsync(Character character);

        Task<bool> DeleteAsync(int id);
    }
}