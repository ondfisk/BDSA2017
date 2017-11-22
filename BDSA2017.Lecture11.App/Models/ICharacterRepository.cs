using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BDSA2017.Lecture11.Common
{
    public interface ICharacterRepository : IDisposable
    {
        Task<string> CreateAsync(CharacterCreateDTO character);

        Task<CharacterDetailsDTO> FindAsync(int characterId);

        Task<byte[]> FindImageAsync(int characterId);

        Task<IReadOnlyCollection<CharacterDTO>> ReadAsync();

        Task<bool> UpdateAsync(CharacterUpdateDTO character);

        Task<bool> DeleteAsync(int characterId);
    }
}
