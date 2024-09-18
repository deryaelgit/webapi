using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Yorumlar")]
        public class Yorum
    {
        public int Id { get; set; }
        public string Baslik { get; set;} = string.Empty;
        public string Icerik { get; set;} = string.Empty ;

        public DateTime OlusturulmaZamani  { get; set;} = DateTime.Now ;
        public int? StokId { get; set;}

        public Stok? Stok { get; set;}

        public string AppUserId { get; set;}
        public AppUser AppUser { get; set;}
        
    }
}