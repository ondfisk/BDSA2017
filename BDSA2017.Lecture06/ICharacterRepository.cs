using BDSA2017.Lecture06.Models;
using System;
using System.Collections.Generic;

namespace BDSA2017.Lecture06
{
    public interface ICharacterRepository : IDisposable
    {
        int Create(CharacterCreateUpdateDTO character);

        CharacterDTO Find(int characterId);

        ICollection<CharacterDTO> Read();

        void Update(CharacterCreateUpdateDTO character);

        bool Delete(int characterId);
    }
}
