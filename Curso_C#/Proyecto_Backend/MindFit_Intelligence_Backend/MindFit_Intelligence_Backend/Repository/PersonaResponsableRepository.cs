using Microsoft.EntityFrameworkCore;
using MindFit_Intelligence_Backend.Models;
using System;
using System.Collections.Generic;

namespace MindFit_Intelligence_Backend.Repository
{
    public class PersonaResponsableRepository : IPersonaResponsableRepository
    {
        private MindFitIntelligenceContext _context;

        public PersonaResponsableRepository(MindFitIntelligenceContext context)
        {
            _context = context;
        } 

        public async Task<IEnumerable<PersonaResponsable>> Get()
        {
            IEnumerable<PersonaResponsable> personaResponsables = await _context.PersonaResponsables.ToListAsync();
            return personaResponsables;
        }
        public async Task<PersonaResponsable?> GetById(int id)
        {
            PersonaResponsable? personaResponsable = await _context.PersonaResponsables.FindAsync(id);
            return personaResponsable;
        }

        public async Task Add(PersonaResponsable personaResponsable)
            => await _context.PersonaResponsables.AddAsync(personaResponsable);

        public void Update(PersonaResponsable personaResponsable)
        {
            _context.PersonaResponsables.Attach(personaResponsable); 
            _context.PersonaResponsables.Entry(personaResponsable).State = EntityState.Modified;
        }

        public void Delete(PersonaResponsable entity)
            => _context.PersonaResponsables.Remove(entity);

        public async Task Save()
            => await _context.SaveChangesAsync();

        public async Task<PersonaResponsable?> GetByEmail(string email)
        {
            return await _context.PersonaResponsables
                .FirstOrDefaultAsync(p => p.Email == email);
        }
    }
}
