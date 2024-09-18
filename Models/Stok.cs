using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Models
{
            [Table("Stoklar")]


    public class Stok
    {
        public int Id { get; set;}
        public string Sembol { get; set;} = string.Empty;
        public string FirmaAdi { get; set;} = string.Empty;
        [Column(TypeName ="decimal(18,2)")]
        public decimal Satınalma { get; set;} 
        [Column(TypeName ="decimal(18,2)")]
        public decimal LastDiv { get; set;} 
        public string Endustri { get; set;} =   string.Empty;
        public long PazarDegeri { get; set;} 

        public List<Yorum> Yorumlar { get; set;} = new List<Yorum>();
        public List<Portfolio> Portfolios { get; set; } =new List<Portfolio>();

        
        
    }
}