using BDSA2017.Lecture07.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BDSA2017.Lecture07.Models
{
    public interface ICharacterRepository : IDisposable
    {
        Task<int> CreateAsync(CharacterCreateUpdateDTO character);

        Task<CharacterDTO> FindAsync(int characterId);

        IQueryable<CharacterDTO> Read();

        Task<bool> UpdateAsync(CharacterCreateUpdateDTO character);

        Task<bool> DeleteAsync(int characterId);
    }
}
