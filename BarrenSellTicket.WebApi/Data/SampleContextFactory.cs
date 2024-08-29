using BarrenSellTicket.Persistance.EntityFrameworks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BarrenSellTicket.WebApi.Data
{
    public class SampleContextFactory:IDesignTimeDbContextFactory<BarrenSellTicketContext>
    {
        public BarrenSellTicketContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();


            var builder = new DbContextOptionsBuilder<BarrenSellTicketContext>();
            var connectionString = configuration.GetConnectionString("local");
            builder.UseSqlServer(connectionString,b=>b.MigrationsAssembly("BarrenSellTicket.WebApi"));

            return new BarrenSellTicketContext(builder.Options);
        }
    }
}
