using System.Data.Entity;
using System.Linq;
using Model.Entity;

namespace Model
{
    public static class Extensions
    {
        public static IQueryable<dPackedStocks> EagerPackedStocks(this ContextContainer _context)
        {
            return _context.dPackedStocks
                .Include(p => p.Mix)
                .Include(p => p.Package);
        }

        public static dPackedStocks EagerPackedStockById(this ContextContainer _context, int argId)
        {
            return _context.dPackedStocks
                .Include(p => p.Mix)
                .Include(p => p.Package)
                .FirstOrDefault(c => c.Id == argId);
        }
    }
}
