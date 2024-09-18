using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Yorum;


// DTOS
// Modelde fazla kolon var ama biz sadece bi kolonu döndürmeye çalışıyorsak bunları kullanırız

namespace api.Dtos.Stok
{
    public class StokDto
    {
           public int Id { get; set;}
        public string Sembol { get; set;} = string.Empty;
        public string FirmaAdi { get; set;} = string.Empty;
        public decimal Satınalma { get; set;} 
        public decimal LastDiv { get; set;} 
        public string Endustri { get; set;} =   string.Empty;
        public long PazarDegeri { get; set;} 
        public List<YorumDto> Yorumlar {get;set;}

        
    }
}