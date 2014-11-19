using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace MundiPagg.Blog.WebUI.Models
{
    public class MailModel
    {
        [Required]
        [Display(Name = "Nome")]
        public string Name { get; set; }
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
