using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stok;
using api.Models;

namespace api.Mappers
{
    public static class StokMappers
    {
        public static StokDto ToStokDto(this Stok stokModel)
        {
            return new StokDto
            {
                Id = stokModel.Id,
                Sembol = stokModel.Sembol,
                FirmaAdi = stokModel.FirmaAdi,
                Satınalma = stokModel.Satınalma,
                LastDiv = stokModel.LastDiv,
                Endustri = stokModel.Endustri,
                PazarDegeri = stokModel.PazarDegeri,
                Yorumlar = stokModel.Yorumlar.Select(c=> c.ToYorumDto()).ToList()
            };
        }

        public static Stok ToStokFromCreateDTO( this CreateStokRequestDto stokDto)
        {
            return new Stok
            {
                Sembol= stokDto.Sembol,
                FirmaAdi= stokDto.FirmaAdi,
                Satınalma= stokDto.Satınalma,
                LastDiv= stokDto.LastDiv,
                Endustri=   stokDto.Endustri,
                PazarDegeri= stokDto.PazarDegeri,
            };
        }
    }
}
