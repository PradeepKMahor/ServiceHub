using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServiceHub.Domain.Models.Data;

namespace ServiceHub.Domain.Context
{
    public class DataContext : IdentityDbContext<AppUser>
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
        public DbSet<TblProduct> TblProduct { get; set; }
    }
}