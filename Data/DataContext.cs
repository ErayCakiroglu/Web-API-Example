using System;
using Microsoft.EntityFrameworkCore;
using MyWebAPI.Core;
namespace MyWebAPI.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options): base(options)
    {

    }
    public DbSet<ProductEntity> ProductEntities {get; set;}
}