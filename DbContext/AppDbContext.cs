using CompanyAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CompanyAPI.DbContext;

public class AppDbContext : IdentityDbContext<IdentityUser>{

    public AppDbContext(DbContextOptions<AppDbContext> options)
        :base(options) { }

    public DbSet<Employee>? Employees { get; set; }
   
}
