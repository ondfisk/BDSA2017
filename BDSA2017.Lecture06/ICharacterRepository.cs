using BDSA2017.Lecture06.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BDSA2017.Lecture06
{
    public interface ICharacterRepository : IDisposable
    {
        Task<int> CreateAsync(CharacterCreateUpdateDTO character);

        Task<CharacterDTO> FindAsync(int characterId);

        Task<ICollection<CharacterDTO>> ReadAsync();

        Task UpdateAsync(CharacterCreateUpdateDTO character);

        Task<bool> DeleteAsync(int characterId);
    }
}
