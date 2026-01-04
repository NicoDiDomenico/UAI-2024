using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MindFit.Application.Contracts.Persistence;
using MindFit.Infrastructure.Persistence;
using MindFit.Infrastructure.Repositories;

namespace MindFit.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MindFitDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("MindFitConnection")));

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
