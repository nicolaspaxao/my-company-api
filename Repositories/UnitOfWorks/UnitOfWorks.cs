
using CompanyAPI.DbContext;

namespace CompanyAPI.Repositories.UnitOfWorks {
    public class UnitOfWorks : IUnitOfWorks {
        //Instancias dos repos
        //private ExempleRepository _exempleRepository;

        public AppDbContext? _context;

        public UnitOfWorks ( AppDbContext context )
        {
            _context = context;
        }

        public async Task Commit () {
            await _context!.SaveChangesAsync();
        }

        public void Dispose () {
            _context!.Dispose();
        }
    }
}
