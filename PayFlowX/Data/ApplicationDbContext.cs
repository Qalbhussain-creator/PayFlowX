using Microsoft.EntityFrameworkCore;
using PayFlowX.Models;

namespace PayFlowX.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Transaction> transactions { get; set; }

}