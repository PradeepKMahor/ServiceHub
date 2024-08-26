using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServiceHub.Domain.Models.Data;

namespace ServiceHub.Domain.Context
{
    public class DataContext : IdentityDbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<TblUserCustomer> TblUserCustomer { get; set; }
        public DbSet<TblUserClint> TblUserClint { get; set; }
    }
}