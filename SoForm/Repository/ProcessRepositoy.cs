using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoForm.Models;
using SoForm.Mappers;
using SoForm.Data;
using Microsoft.EntityFrameworkCore;

namespace SoForm.Repository
{
    internal class ProcessRepositoy
    {
        private readonly AppDbContext _context;


        public ProcessRepositoy(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProcessDbModel>> GetAll()
        {
            return await _context.Process.AsNoTracking().ToListAsync();
        }

        public async Task<ProcessDbModel> GetById(int id)
        {
            var process = await _context.Process.FindAsync(id);
            if (process == null)
            {
                throw new InvalidOperationException($"Process with ID {id} not found.");
            }
            return process;
        }
        public async Task<ProcessDbModel> Create(ProcessDbModel process)
        {
            _context.Process.Add(process);
            await _context.SaveChangesAsync();
            return process;
        }
        public async Task<ProcessDbModel> Update(ProcessDbModel process)
        {
            var existingEntity = await _context.Process.FindAsync(process.Id);

            if (existingEntity != null)
            {
                _context.Entry(existingEntity).State = EntityState.Detached; // Desasociar la entidad original
            }

            _context.Process.Update(process);
            await _context.SaveChangesAsync();
            return process;
        }

        public async Task Delete(int id)
        {
            var process = await _context.Process.FindAsync(id);

            // Verifica si el proceso es nulo
            if (process == null)
            {
                throw new ArgumentNullException(nameof(process), $"No se encontró un proceso con el ID {id}.");
            }

            _context.Process.Remove(process);
            await _context.SaveChangesAsync();
        }


    }
}
