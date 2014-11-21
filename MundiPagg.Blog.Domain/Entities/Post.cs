using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;

namespace MundiPagg.Blog.Domain.Entities
{
    /// <summary>
    /// Classe de domínio para Postagem
    /// </summary>
    [Table("Post")]
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string PostContent { get; set; }
        public string Tags { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime EditDate { get; set; }
        public bool IsActive { get; set; }

        /// <summary>
        /// Relaciona postagem com usuário.
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Propriedade para acesso ao usuário autor do
        /// post em tempo de execução.
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// Propriedade para acesso de uma coleção de
        /// comentários do post em tempo de execução.
        /// </summary>
        public virtual Collection<PostCommentary> Commentaries { get; set; }
    }
}
