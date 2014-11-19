using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundiPagg.Blog.WebUI.Models
{
    public class EditPostModel
    {
        public int PostId { get; set; }
        [Required]
        [StringLength(300, ErrorMessage = "Título deve possuir no máximo 300 caracteres.")]
        [Display(Name = "Título")]
        public string Title { get; set; }
        [Required]
        [StringLength(300, ErrorMessage = "Resumo deve possuir no máximo 300 caracteres.")]
        [Display(Name = "Resumo")]
        public string Summary { get; set; }
        [Required]
        [Display(Name = "Conteúdo")]
        public string Content { get; set; }
        [StringLength(50, ErrorMessage = "Tags deve possuir no máximo 50 caracteres.")]
        [Display(Name = "Tags")]
        public string Tags { get; set; }
    }
}
