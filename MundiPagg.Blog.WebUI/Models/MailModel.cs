using System.ComponentModel.DataAnnotations;

namespace MundiPagg.Blog.WebUI.Models
{
    /// <summary>
    /// Classe de data model para o formulário de contato.
    /// </summary>
    public class MailModel
    {
        [Required]
        [Display(Name = "Nome")]
        public string Name { get; set; }
        // Contém uma expressão regular de validação de email.
        [Required]
        [RegularExpression( "^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$" , ErrorMessage = "Formato de mail inválido" )]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Display(Name = "Telefone")]
        public string Phone { get; set; }
        [Required]
        [Display(Name = "Mensagem")]
        public string Message { get; set; }
    }
}
