using System;
using System.Collections.Generic;

namespace BDSA2017.Lecture04.Futurama
{
    public class FuturamaRepository : IDisposable
    {
        public FuturamaRepository()
        {
        }

        public IEnumerable<(string character, string voicedBy)> GetCharactersAndActors()
        {
            throw new NotImplementedException();
        }

        public void Update(Actor actor)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
