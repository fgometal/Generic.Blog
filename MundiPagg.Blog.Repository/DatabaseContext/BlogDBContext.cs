using System.Data.Entity;
using MundiPagg.Blog.Domain.Entities;

namespace MundiPagg.Blog.Repository.DatabaseContext
{
    /// <summary>
    /// Classe de mapeamento do Entity Framework com o banco de dados.
    /// </summary>
    public class BlogDBContext : DbContext
    {
        /// <summary>
        /// Propriedades que armazenam os  registros das entidades mapeadas.
        /// </summary>
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCommentary> Commentaries { get; set; }
        /// <summary>
        /// Construtor estático de inicialização da base. Define que a base
        /// deve ser recriada quando houver alterações no domínio.
        /// </summary>
        static BlogDBContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BlogDBContext>());   
            //Configuration.ValidateOnSaveEnabled = false;
        }
        /// <summary>
        /// Libera o banco realizando um dispose. 
        /// </summary>
        /// <param name="disposing">Booleano que define se o dispose deve ser realizado ou não</param>
        public virtual void DatabaseDispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        /// <summary>
        /// Sobrescrita do método de criação do modelo de dados.
        /// Define os relacionamentos das entidades.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().HasRequired(x => x.User);
            modelBuilder.Entity<Post>().HasMany(x => x.Commentaries);
            modelBuilder.Entity<PostCommentary>().HasRequired(x => x.User);
        }
    }
}
