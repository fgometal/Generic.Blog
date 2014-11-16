using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundiPagg.Blog.WebUI.Models
{
    public class MailModel
    {
        [Required(ErrorMessage="Nome é requerido")]
        public string Name { get; set; }
        [Required(ErrorMessage = "E-mail é requerido")]
        public string Mail { get; set; }
        [Required(ErrorMessage = "Telefone é requerido")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Mensagem é requerida")]
        public string Message { get; set; }
    }
}
