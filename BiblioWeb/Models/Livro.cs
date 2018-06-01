using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BiblioWeb.Models
{
    public class Livro
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Titulo")]
        [Required(ErrorMessage ="Título obrigatório!")]
        [MaxLength(100, ErrorMessage = "Máximo de 100 caracteres!")]
        public string Titulo { get; set; }

        [Display(Name = "Subtitulo")]
        [MaxLength(100, ErrorMessage = "Máximo de 100 caracteres!")]
        public string Subtitulo { get; set; }

        [Display(Name = "Sinopse")]
        [Required(ErrorMessage = "Sinopse obrigatório!")]
        [MaxLength(3000, ErrorMessage = "Máximo de 3000 caracteres!")]
        public string Sinopse { get; set; }

        [Display(Name = "Edicao")]
        [Required(ErrorMessage = "Edição obrigatório!")]
        public int Edicao { get; set; }

        [Display(Name = "Ano")]
        [Required(ErrorMessage = "Ano obrigatório!")]
        public int Ano { get; set; }

        [Display(Name = "Genero")]
        [Required(ErrorMessage = "Gênero obrigatório!")]
        [MaxLength(150, ErrorMessage = "Máximo de 150 caracteres!")]
        public string Genero { get; set; }

        [Display(Name = "Editora")]
        [Required(ErrorMessage = "Editora obrigatório!")]
        [MaxLength(150, ErrorMessage = "Máximo de 150 caracteres!")]
        public string Editora { get; set; }

        [Display(Name = "Autor")]
        [Required(ErrorMessage = "Autor obrigatório!")]
        [MaxLength(150, ErrorMessage = "Máximo de 150 caracteres!")]
        public string Autor { get; set; }

        [Display(Name = "ISBN")]
        [Required(ErrorMessage = "ISBN obrigatório!")]
        [MaxLength(13, ErrorMessage = "Máximo de 13 caracteres!")]
        public string ISBN { get; set; }

        public Imagem FotoCapa { get; set; }

    }
}
