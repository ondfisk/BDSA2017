using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BDSA2017.Lecture11.Common;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;

namespace BDSA2017.Lecture11.App.Models
{
    public class RestCharacterRepository : ICharacterRepository
    {
        private readonly HttpClient _client;

        public RestCharacterRepository(ISettings settings, DelegatingHandler handler)
        {
            var client = new HttpClient(handler)
            {
                BaseAddress = settings.ApiBaseAddress
            };
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _client = client;
        }

        public async Task<int> CreateAsync(CharacterCreateDTO character)
        {
            var response = await _client.PostAsync("api/characters", character.ToHttpContent());

            if (response.IsSuccessStatusCode)
            {
                var location = response.Headers.GetValues("Location").First();
                return int.Parse(location.Split('/').Last());
            }

            return -1;
        }

        public async Task<bool> DeleteAsync(int characterId)
        {
            var response = await _client.DeleteAsync($"api/characters/{characterId}");

            return response.IsSuccessStatusCode;
        }

        public async Task<CharacterDetailsDTO> FindAsync(int characterId)
        {
            var response = await _client.GetAsync($"api/characters/{characterId}");

            if (response.IsSuccessStatusCode)
            {
                var character = await response.Content.To<CharacterDetailsDTO>();

                character.Image = new Uri(_client.BaseAddress, $"api/characters/{characterId}/image").ToString();

                return character;
            }

            return null;
        }

        public async Task<IReadOnlyCollection<CharacterDTO>> ReadAsync()
        {
            var response = await _client.GetAsync("api/characters");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.To<IReadOnlyCollection<CharacterDTO>>();
            }

            return null;
        }

        public async Task<bool> UpdateAsync(CharacterUpdateDTO character)
        {
            var response = await _client.PutAsync($"api/characters/{character.Id}", character.ToHttpContent());

            return response.IsSuccessStatusCode;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _client.Dispose();
                    // TODO: dispose managed state (managed objects).
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
