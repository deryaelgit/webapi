using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class YorumRepository : IYorumRepository
    {
            private readonly ApplicationDbContext _context;
        public YorumRepository(ApplicationDbContext context)
        {
            _context=context;
        }

        public async Task<Yorum> CreateAsync(Yorum yorumModel)
        {
            await _context.Yorumlar.AddAsync(yorumModel);
            await _context.SaveChangesAsync();
            return yorumModel;

        }

        public async Task<Yorum?> DeleteAsync(int id)
        {
            var yorumModel= await _context.Yorumlar.FirstOrDefaultAsync(x => x.Id==id);
            if (yorumModel==null)
            {
                return null;
            }
            _context.Yorumlar.Remove(yorumModel);
            await _context.SaveChangesAsync();
            return yorumModel;

        }

            public async Task<List<Yorum>> GetAllAsync()
        {
           return await _context.Yorumlar.Include(c=> c.AppUser).ToListAsync();
        }

        public async Task<Yorum?> GetByIdAsync(int id)
        {
            return await _context.Yorumlar.Include(c=> c.AppUser).FirstOrDefaultAsync(c=>c.Id==id);

        }


     public async Task<Yorum?> UpdateAsync(int id, Yorum yorumModel)
{
    var existingComment = await _context.Yorumlar.FindAsync(id);
    
    if (existingComment == null)
    {
        return null;
    }

    existingComment.Baslik = yorumModel.Baslik;
    existingComment.Icerik = yorumModel.Icerik;

    await _context.SaveChangesAsync();

    return existingComment;
}


    }
}