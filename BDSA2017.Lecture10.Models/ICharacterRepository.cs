using BDSA2017.Lecture10.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BDSA2017.Lecture10.Models
{
    public interface ICharacterRepository : IDisposable
    {
        Task<int> CreateAsync(CharacterCreateDTO character);

        Task<CharacterDetailsDTO> FindAsync(int characterId);

        Task<IReadOnlyCollection<CharacterDTO>> ReadAsync();

        Task<bool> UpdateAsync(CharacterUpdateDTO character);

        Task<bool> DeleteAsync(int characterId);
    }
}
