using System.Threading.Tasks;
using BDSA2017.Lecture07.Models;
using BDSA2017.Lecture07.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BDSA2017.Lecture07
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly IFuturamaContext _context;

        public CharacterRepository(IFuturamaContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(CharacterCreateUpdateDTO character)
        {
            var entity = new Character
            {
                ActorId = character.ActorId,
                Name = character.Name,
                Species = character.Species,
                Planet = character.Planet
            };

            _context.Characters.Add(entity);

            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<CharacterDTO> FindAsync(int characterId)
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

            return await characters.FirstOrDefaultAsync();
        }

        public IQueryable<CharacterDTO> Read()
        {
            return from c in _context.Characters
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
        }

        public async Task<bool> UpdateAsync(CharacterCreateUpdateDTO character)
        {
            var entity = await _context.Characters.FindAsync(character.Id);

            if (entity == null)
            {
                return false;
            }

            entity.ActorId = character.ActorId;
            entity.Name = character.Name;
            entity.Species = character.Species;
            entity.Planet = character.Planet;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int characterId)
        {
            var character = await _context.Characters.FindAsync(characterId);

            if (character == null)
            {
                return false;
            }

            _context.Characters.Remove(character);

            await _context.SaveChangesAsync();

            return true;
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
