using DomainModels;
using Microsoft.EntityFrameworkCore;
using System;


namespace DataLayer
{
    public class ProdDbContext:DbContext
    {
        public ProdDbContext()
        {
        }

        public ProdDbContext(DbContextOptions<ProdDbContext> options): base(options)
        {
             
        }
        public DbSet<Product> Products { get; set; }
    }
}
