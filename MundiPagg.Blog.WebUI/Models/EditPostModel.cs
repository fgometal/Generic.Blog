using System.ComponentModel.DataAnnotations;

namespace MundiPagg.Blog.WebUI.Models
{
    /// <summary>
    /// Classe para a data model de edição de posts.
    /// </summary>
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
