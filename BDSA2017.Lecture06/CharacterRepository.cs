using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BDSA2017.Lecture06.Models;
using BDSA2017.Lecture06.Entities;

namespace BDSA2017.Lecture06
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly IFuturamaContext _context;

        public CharacterRepository(IFuturamaContext context)
        {
            _context = context;
        }

        public Task<int> CreateAsync(CharacterCreateUpdateDTO character)
        {
            throw new NotImplementedException();
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

        public Task<CharacterDTO> FindAsync(int characterId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<CharacterDTO>> ReadAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CharacterCreateUpdateDTO character)
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
