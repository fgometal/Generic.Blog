using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundiPagg.Blog.Repository.DatabaseContext
{
    public sealed class DbInstance
    {
        private static readonly DbInstance instance = new DbInstance();
        private static BlogDBContext _context;

        public static DbInstance Instance
        {
            get
            {
                return instance;
            }
        }

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
