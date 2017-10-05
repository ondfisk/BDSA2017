using System;
using System.Collections.Generic;
using BDSA2017.Lecture05.Models;
using BDSA2017.Lecture05.Entities;
using System.Linq;

namespace BDSA2017.Lecture05
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly IFuturamaContext _context;

        public CharacterRepository(IFuturamaContext context)
        {
            _context = context;
        }

        public int Create(CharacterCreateUpdateDTO character)
        {
            var entity = new Character
            {
                ActorId = character.ActorId,
                Name = character.Name,
                Species = character.Species,
                Planet = character.Planet,
            };

            _context.Characters.Add(entity);

            _context.SaveChanges();

            return entity.Id;
        }

        public bool Delete(int characterId)
        {
            var character = _context.Characters.Find(characterId);

            if (character == null)
            {
                return false;
            }

            _context.Characters.Remove(character);

            _context.SaveChanges();

            return true;
        }

        public CharacterDTO Find(int characterId)
        {
            var characters = from c in _context.Characters
                             where c.Id == characterId
                             select new CharacterDTO
                             {
                                 Id = c.Id,
                                 ActorId = c.ActorId,
                                 ActorName = c.Actor.Name,
                                 Name = c.Name,
                                 Species = c.Species,
                                 Planet = c.Planet,
                                 NumberOfEpisodes = c.Episodes.Count()
                             };

            return characters.FirstOrDefault();
        }

        public ICollection<CharacterDTO> Read()
        {
            var characters = from c in _context.Characters
                             select new CharacterDTO
                             {
                                 Id = c.Id,
                                 ActorId = c.ActorId,
                                 ActorName = c.Actor.Name,
                                 Name = c.Name,
                                 Species = c.Species,
                                 Planet = c.Planet,
                                 NumberOfEpisodes = c.Episodes.Count()
                             };

            return characters.ToList();
        }

        public void Update(CharacterCreateUpdateDTO character)
        {
            throw new NotImplementedException();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~CharacterRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
