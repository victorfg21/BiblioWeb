using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BiblioWeb.Models
{
    public class Imagem
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        [Required]
        public string Extension { get; set; }
        [Required]
        public int Length { get; set; }
        public byte[] Picture { get; set; }
        [Required]
        public string ContentType { get; set; }
    }
}
