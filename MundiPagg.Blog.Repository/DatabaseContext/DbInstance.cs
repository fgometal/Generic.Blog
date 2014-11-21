
namespace MundiPagg.Blog.Repository.DatabaseContext
{
    /// <summary>
    /// Classe singleton para armazenar a instância do banco.
    /// </summary>
    public sealed class DbInstance
    {
        /// <summary>
        /// Referência estática para a instância da classe.
        /// </summary>
        private static readonly DbInstance instance = new DbInstance();
        /// <summary>
        /// Rerência estática para o banco.
        /// </summary>
        private static BlogDBContext _context;
        /// <summary>
        /// Propriedade de acesso a instância única da classe.
        /// </summary>
        public static DbInstance Instance
        {
            get { return instance; }
        }
        /// <summary>
        /// Propriedade de acesso à instância estática do banco.
        /// </summary>
        public BlogDBContext Context
        {
            get
            {
                if (_context == null)
                {
                    _context = new BlogDBContext();
                }

                return _context;
            }
        }
        /// <summary>
        /// Efetua a liberação da base realizando o dispose.
        /// </summary>
        public static void Dispose()
        {
            if (_context != null)
            {
                _context.DatabaseDispose(false);
                _context = null;
            }
        }
    }
}
