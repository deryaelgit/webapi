using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using api.Extensions;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDbContext _context;
        public PortfolioRepository(ApplicationDbContext context)
        {
            _context=context;

        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<Portfolio> DeletePortfolio(AppUser appUser,string sembol)
        {
            var portfolioModel = await _context.Portfolios.FirstOrDefaultAsync(x=> x.AppUserId == appUser.Id && x.Stok.Sembol.ToLower() == sembol.ToLower());
            if (portfolioModel == null)
            {
                return null;
            }
            _context.Portfolios.Remove(portfolioModel);
            await _context.SaveChangesAsync();
            return portfolioModel;
        }

        public async Task<List<Stok>> GetUserPortfolio(AppUser user)
        {
           return await _context.Portfolios.Where(u=>u.AppUserId == user.Id)
           .Select(stok => new Stok
           {
            Id=stok.StokId,
            Sembol=stok.Stok.Sembol,
            Satınalma=stok.Stok.Satınalma,
            FirmaAdi=stok.Stok.FirmaAdi,
            PazarDegeri=stok.Stok.PazarDegeri,
            LastDiv=stok.Stok.LastDiv,
            Endustri=stok.Stok.Endustri
            

        }).ToListAsync();
        }
    }
}