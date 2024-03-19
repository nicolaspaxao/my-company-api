
using CompanyAPI.DbContext;
using CompanyAPI.Repositories.Repos;

namespace CompanyAPI.Repositories.UnitOfWorks {
    public class UnitOfWorks : IUnitOfWorks {

        private EmployeeRepository _employeeRepo;

        public AppDbContext? _context;

        public UnitOfWorks ( AppDbContext context )
        {
            _context = context;
        }

        public IEmployeeRepository employeeRepo {
            get {
                return _employeeRepo = _employeeRepo ?? new EmployeeRepository(_context);
            }
        }

        public async Task Commit () {
            await _context!.SaveChangesAsync();
        }

        public void Dispose () {
            _context!.Dispose();
        }
    }
}
