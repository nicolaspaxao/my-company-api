using CompanyAPI.Repositories.Repos;

namespace CompanyAPI.Repositories.UnitOfWorks {
    public interface IUnitOfWorks {
        //Adicionar as interfaces do Repositórios
        IEmployeeRepository employeeRepo { get; }

        Task Commit ();
    }
}
