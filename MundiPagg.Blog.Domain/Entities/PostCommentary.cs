using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MundiPagg.Blog.Domain.Entities
{
    /// <summary>
    /// Classe de domínio para comentários de postagem
    /// </summary>
    [Table("PostCommentary")]
    public class PostCommentary
    {
        [Key]
        public int PostCommentaryId { get; set; }
        public string Commentary { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsActive { get; set; }

        /// <summary>
        /// Relaciona comentário com usuário
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Propriedade para acesso ao usuário 
        /// relacionado em tempo de execução.
        /// </summary>
        public virtual User User { get; set; }
    }
}
