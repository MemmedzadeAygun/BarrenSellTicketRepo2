using BarrenSellTicket.Application.Interfaces;
using BarrenSellTicket.Persistance.EntityFrameworks;
using BarrenSellTicket.Persistance.EntityFrameworks.Repositories;
using BarrenSellTicket.Persistance.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Persistance;

public static class ServiceRegistration
{
    public static void AddPersistanceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
//#if DEBUG
//        var connectionString = configuration.GetConnectionString("local");
//#endif
        var connectionString = configuration.GetConnectionString("local");

        services
            .AddDbContext<BarrenSellTicketContext>(options=>options
            .UseSqlServer(connectionString)
            .AddInterceptors(new UpdateBaseEntityInterceptors()));

        services.AddHttpContextAccessor();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        //services.AddCors();
    }
}
