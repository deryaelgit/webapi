using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stok
{
    public class UpdateStokRequestDto
    {
         [Required]
        [MaxLength(10, ErrorMessage = "Sembol 10 karakterden fazla olamaz")]
          public string Sembol { get; set;} = string.Empty;

            [Required]
        [MaxLength(10, ErrorMessage = "Firma adı 10 karakterden fazla olamaz")]
        public string FirmaAdi { get; set;} = string.Empty;
           [Required]
        [Range(1,100000)]
        public decimal Satınalma { get; set;} 
         [Required]
        [Range(0.001,100)]
        public decimal LastDiv { get; set;} 
               [Required]
        [MaxLength(10,ErrorMessage ="Endustri 10 karakterin üstünde olamaz")]
        public string Endustri { get; set;} =   string.Empty;
             [Range(1,5000000000000)]

        public long PazarDegeri { get; set;} 
    }
}