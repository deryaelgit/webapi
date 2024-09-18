using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Yorum;
using api.Models;

namespace api.Mappers
{
    public static class YorumMapper
    {
        public static YorumDto ToYorumDto(this Yorum yorumModel)
        {
            return new YorumDto
            {
                Id = yorumModel.Id,
                Baslik=yorumModel.Baslik,
                Icerik=yorumModel.Icerik,
                OlusturulmaZamani=yorumModel.OlusturulmaZamani,
               CrearedBy=yorumModel.AppUser.UserName,
                StokId=yorumModel.StokId

            };
        }

            public static Yorum ToYorumFromCreate(this CreateCommentDto yorumDto,int stokId )
        {
            return new Yorum
            {
         
                Baslik=yorumDto.Baslik,
                Icerik=yorumDto.Icerik,
         
                StokId=stokId

            };
        }
                

               public static Yorum ToYorumFromUpdate(this UpdateCommentRequestDto yorumDto)
                {
                    return new Yorum
                    {
                
                        Baslik=yorumDto.Baslik,
                        Icerik=yorumDto.Icerik
                
                    };
                }


     
    }
}