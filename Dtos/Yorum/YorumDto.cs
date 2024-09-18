using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// DTOS
// Modelde fazla kolon var ama biz sadece bi kolonu döndürmeye çalışıyorsak bunları kullanırız

namespace api.Dtos.Yorum
{
    public class YorumDto
    {
            public int Id { get; set; }
        public string Baslik { get; set;} = string.Empty;
        public string Icerik { get; set;} = string.Empty ;

        public DateTime OlusturulmaZamani  { get; set;} = DateTime.Now ;
        public string  CrearedBy { get; set;} = string.Empty;
        public int? StokId { get; set;}

    }
}