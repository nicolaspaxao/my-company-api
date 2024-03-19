using CompanyAPI.DbContext;
using CompanyAPI.Models;
using CompanyAPI.Repositories.__base;

namespace CompanyAPI.Repositories.Repos {
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository {
        public EmployeeRepository ( AppDbContext context ) : base(context) {}

    }
}
