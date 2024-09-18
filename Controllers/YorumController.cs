using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Yorum;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api/yorum")]
    [ApiController]
    public class YorumController : ControllerBase
    {
        private readonly IYorumRepository _yorumRepo;
        private readonly IStokRepository _stokRepo;
        private readonly UserManager<AppUser> _userManager;

        public YorumController(IYorumRepository yorumRepo, IStokRepository stokRepo,UserManager<AppUser> userManager)
        {
            _yorumRepo = yorumRepo;
            _stokRepo = stokRepo;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
               return BadRequest(ModelState);
            var yorumlar = await _yorumRepo.GetAllAsync();
            var yorumDto= yorumlar.Select(s=>s.ToYorumDto());

            return Ok(yorumDto);;


        }  

            [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            if(!ModelState.IsValid)
               return BadRequest(ModelState);

            var yorum = await _yorumRepo.GetByIdAsync(id);
            if (yorum == null)
            {
                return NotFound();
            }
            return Ok(yorum.ToYorumDto());
        }


        [HttpPost("{stokId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stokId, CreateCommentDto yorumDto )
        {
               if(!ModelState.IsValid)
               return BadRequest(ModelState);

            if(!await _stokRepo.StokExists(stokId))
            {
                return BadRequest("Stok does not exist");
            }

            var username =User.GetUsername();
            var appUser= await _userManager.FindByNameAsync(username);

            var yorumModel = yorumDto.ToYorumFromCreate(stokId);
            yorumModel.AppUserId = appUser.Id;
            await _yorumRepo.CreateAsync(yorumModel);
            return CreatedAtAction(nameof(GetById), new{id=yorumModel.Id}, yorumModel.ToYorumDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateDto)
        {
               if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var yorum = await _yorumRepo.UpdateAsync(id, updateDto.ToYorumFromUpdate());

                if (yorum == null)
                {
                return NotFound("Comment not found");  
                }

                return Ok(yorum.ToYorumDto());


        }



        [HttpDelete]
        [Route("{id:int}")]

        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid)
               return BadRequest(ModelState);
           
            var yorumModel = await _yorumRepo.DeleteAsync(id);
            if (yorumModel == null)
            {
                return NotFound("Comments does not exist");
            }
            return Ok(yorumModel);
        }

    }
}