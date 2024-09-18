using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stok;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StokRepository : IStokRepository
    {
        private readonly ApplicationDbContext _context;
        public StokRepository(ApplicationDbContext context)
        {
            _context=context;
        }

        public async Task<Stok> CreateAsync(Stok stokModel)
        {
            await _context.Stoklar.AddAsync(stokModel);
            await _context.SaveChangesAsync();
            return stokModel;
        }

        public async Task<Stok?> DeleteAsync(int id)
        {
            var stokModel= await _context.Stoklar.FirstOrDefaultAsync(x=>x.Id==id);

            if(stokModel==null)
            {
                return null;
            }
            _context.Stoklar.Remove(stokModel);
            await _context.SaveChangesAsync();
            return stokModel;

        }

        public async Task<List<Stok>> GetAllAsync(QueryObject query)
        {
            // return await _context.Stoklar.Include(c=>c.Yorumlar).ToListAsync();
               var stoklar= _context.Stoklar.Include(c=>c.Yorumlar).ThenInclude(a=> a.AppUser).AsQueryable();
            if(!string.IsNullOrWhiteSpace(query.FirmaAdi))
            {
                stoklar = stoklar.Where(x=>x.FirmaAdi.Contains(query.FirmaAdi));
            }
            if(!string.IsNullOrWhiteSpace(query.Sembol))
            {
                stoklar = stoklar.Where(s=>s.Sembol.Contains(query.Sembol));

            }
            //sorting
            if(!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy.Equals("Sembol",StringComparison.OrdinalIgnoreCase))
                {
                    stoklar=query.IsDecsending ? stoklar.OrderByDescending(s=>s.Sembol): stoklar.OrderBy(s=>s.Sembol);

                }
            }

            var skipNumber=(query.PageNumber - 1)* query.PageSize;



            return await stoklar.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stok?> GetByIdAsync(int id)
        {
           return await _context.Stoklar.Include(c=> c.Yorumlar).FirstOrDefaultAsync(i => i.Id==id);
        }

        public async Task<Stok?> GetBySembolAsync(string sembol)
        {
            return await _context.Stoklar.FirstOrDefaultAsync(s => s.Sembol == sembol);
        }

        public Task<bool> StokExists(int id)
        {
            return _context.Stoklar.AnyAsync(s=> s.Id==id);
            
        }

        public async Task<Stok> UpdateAsync(int id, UpdateStokRequestDto stokDto)
        {
            var existringStok= await _context.Stoklar.FirstOrDefaultAsync(x=>x.Id==id);
            if(existringStok == null)
            {
                return null;
            }

                existringStok.Sembol = stokDto.Sembol;
                existringStok.FirmaAdi = stokDto.FirmaAdi;
                existringStok.Satınalma=stokDto.Satınalma;
                existringStok.LastDiv=stokDto.LastDiv;
                existringStok.Endustri=stokDto.Endustri;
                existringStok.PazarDegeri=stokDto.PazarDegeri;

                await _context.SaveChangesAsync();
                return existringStok;
        }
    }
}