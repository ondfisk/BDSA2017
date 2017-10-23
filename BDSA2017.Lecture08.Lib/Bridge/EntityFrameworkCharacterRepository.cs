﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BDSA2017.Lecture08.Lib.Bridge
{
    public class EntityFrameworkCharacterRepository : ICharacterRepository
    {
        private readonly ICharacterContext _context;

        public EntityFrameworkCharacterRepository(ICharacterContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Character character)
        {
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            return character.Id;
        }

        public async Task<Character> FindAsync(int id)
        {
            return await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Character>> ReadAsync()
        {
            return await _context.Characters.ToListAsync();
        }

        public async Task<bool> UpdateAsync(Character character)
        {
            var entity = await _context.Characters.FirstOrDefaultAsync(c => c.Id == character.Id);

            if (entity == null)
            {
                return false;
            }

            entity.GivenName = character.GivenName;
            entity.Surname = character.Surname;
            entity.Species = character.Species;
            entity.Origin = character.Origin;
            entity.Year = character.Year;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);

            if (entity == null)
            {
                return false;
            }

            _context.Characters.Remove(entity);

            await _context.SaveChangesAsync();

            return true;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
