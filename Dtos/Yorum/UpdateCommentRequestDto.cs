using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Yorum
{
    public class UpdateCommentRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "en az 5 karakterli başlık giriniz")]
        [MaxLength(250, ErrorMessage = "başlık 250 karakteri geçemez")]
        public string Baslik { get; set;} = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "en az 5 karakterli başlık giriniz")]
        [MaxLength(250, ErrorMessage = "başlık 250 karakteri geçemez")]
        public string Icerik { get; set;} = string.Empty ;
    }
}