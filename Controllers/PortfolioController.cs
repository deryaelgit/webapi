using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController: ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStokRepository _stokRepo;
        private readonly IPortfolioRepository _portfolioRepo;
        public PortfolioController(UserManager<AppUser> userManager, IStokRepository stokRepo, IPortfolioRepository portfolioRepo){
        
            _stokRepo = stokRepo;
            _userManager = userManager;
            _portfolioRepo = portfolioRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);           
           return Ok(userPortfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string sembol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var stok = await _stokRepo.GetBySembolAsync(sembol);

            if(stok == null) return BadRequest("Stok bulunamadı");
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);

            if(userPortfolio.Any(e=>e.Sembol.ToLower() == sembol.ToLower())) return BadRequest("Cannot add same stok to portfolio");

            var portfolioModel = new Portfolio
            {
                StokId =stok.Id,
                AppUserId = appUser.Id,

            };
            await _portfolioRepo.CreateAsync(portfolioModel);
            if(portfolioModel == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }


        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult>  DeletePortfolio(string sembol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);

            var filteredStok= userPortfolio.Where(s=>s.Sembol.ToLower() == sembol.ToLower());
            if(filteredStok.Count()==1)
            {
                await _portfolioRepo.DeletePortfolio(appUser,sembol);

            } else
            {
               return BadRequest("Stok is not in your portfolio");
            }
            return Ok();
        } 
    }
}