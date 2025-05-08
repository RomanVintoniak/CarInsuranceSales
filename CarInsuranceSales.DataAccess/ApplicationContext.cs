using CarInsuranceSales.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarInsuranceSales.DataAccess;

public class ApplicationContext : DbContext
{
    public DbSet<Client> Clients { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {

    }
}
