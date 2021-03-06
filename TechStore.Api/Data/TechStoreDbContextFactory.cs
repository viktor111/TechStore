using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TechStore.Api
{
    public class TechStoreDbContextFactory : IDesignTimeDbContextFactory<TechStoreDbContext>
    {
        public TechStoreDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            return new TechStoreDbContext(new DbContextOptionsBuilder<TechStoreDbContext>().Options, config);
        }
    }
}
    