using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Mvc;
using api.Mappers;
using api.Dtos.Stok;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using api.Interfaces;
using api.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers

{
    [Route("api/stok")]
    [ApiController]
    public class StokController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStokRepository _stokRepo;


        public StokController(ApplicationDbContext context, IStokRepository stokRepo)
        {
            _stokRepo= stokRepo;
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
              if(!ModelState.IsValid)
               return BadRequest(ModelState);
            var stoklar =await _stokRepo.GetAllAsync(query);
            var stokDto = stoklar.Select(s => s.ToStokDto()).ToList();

            return Ok(stoklar);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
              if(!ModelState.IsValid)
               return BadRequest(ModelState);
            var stok =await _stokRepo.GetByIdAsync(id);

            if (stok == null)
            {
                return NotFound();
            }
            return Ok(stok.ToStokDto());
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStokRequestDto stokDto)
        {
              if(!ModelState.IsValid)
               return BadRequest(ModelState);
            var stokModel = stokDto.ToStokFromCreateDTO();
            // await _context.Stoklar.AddAsync(stokModel);
            // await _context.SaveChangesAsync();
            await _stokRepo.CreateAsync(stokModel);
            return CreatedAtAction(nameof(GetById), new {id=stokModel.Id}, stokModel.ToStokDto());
          
        }


        // [HttpPut]
        // [Route("{id}")]
        // public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStokRequestDto updateDto )
        // {
        //     var stokModel =await _context.Stoklar.FirstOrDefaultAsync(x => x.Id==id);
        //         if (stokModel == null)
        //         {
        //             return NotFound();
        //         }
        //         stokModel.Sembol = updateDto.Sembol;
        //         stokModel.FirmaAdi = updateDto.FirmaAdi;
        //         stokModel.Satınalma=updateDto.Satınalma;
        //         stokModel.LastDiv=updateDto.LastDiv;
        //         stokModel.Endustri=updateDto.Endustri;
        //         stokModel.PazarDegeri=updateDto.PazarDegeri;

        //     await _context.SaveChangesAsync();
        //     return Ok(stokModel.ToStokDto());
        // }
        //interface ekledikten sonra asagıdaki sekilde
              [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStokRequestDto updateDto )
        {
              if(!ModelState.IsValid)
               return BadRequest(ModelState);
            var stokModel =await _stokRepo.UpdateAsync(id, updateDto);
                if (stokModel == null)
                {
                    return NotFound();
                }
         

            return Ok(stokModel.ToStokDto());
        }



        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
              if(!ModelState.IsValid)
               return BadRequest(ModelState);
            // var stokModel=await _context.Stoklar.FirstOrDefaultAsync(x=>x.Id == id);
            var stokModel=await _stokRepo.DeleteAsync(id);

            if (stokModel == null)
            {
                return NotFound();

            }
        //     _context.Stoklar.Remove(stokModel);
        //    await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

