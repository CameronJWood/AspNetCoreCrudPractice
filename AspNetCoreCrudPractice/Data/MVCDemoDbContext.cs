using AspNetCoreCrudPractice.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreCrudPractice.Data
{
    public class MVCDemoDbContext : DbContext
    {
        //create constructor for this class:
        public MVCDemoDbContext(DbContextOptions options) : base(options)
        {
        }

        //create properties:
        //used to access data in our tables that entity framework core will create.

        public DbSet<Employee> Employees { get; set; }     //domain model? This will be the name of the table. Of data type DbSet of Type class employee.
    }
}
