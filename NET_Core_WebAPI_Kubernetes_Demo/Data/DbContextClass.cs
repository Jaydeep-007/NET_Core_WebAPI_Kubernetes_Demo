using Microsoft.EntityFrameworkCore;
using NET_Core_WebAPI_Kubernetes_Demo.Entities;

namespace NET_Core_WebAPI_Kubernetes_Demo.Data
{
    public class DbContextClass : DbContext
    {

        public DbContextClass(DbContextOptions<DbContextClass>
options) : base(options)
        {

        }

        public DbSet<ProductDetails> Products { get; set; }
    }
}
