namespace CompanyAPI.Repositories.UnitOfWorks {
    public interface IUnitOfWorks {
        //Adicionar as interfaces do Repositórios
        // IExempleRepository ExempleRepository { get; }

        Task Commit ();
    }
}
