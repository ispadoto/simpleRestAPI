
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleRestAPI.Application.EmployeesApplication;
using SimpleRestAPI.Domain.Entities.Employees;
using SimpleRestAPI.Domain.Entities.EmployeesPhones;
using SimpleRestAPI.Infra.Database.Context;
using SimpleRestAPI.Infra.Database.Repositories;
using SimpleRestAPI.Shared.Shared.Utils;

namespace SimpleRestAPI.Infra.IoC
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services
                .CoreProjectAndDatabase()
                .ApplicationsServices();

            return services;
        }

        private static IServiceCollection ApplicationsServices(this IServiceCollection services)
        {
            services
                .AddScoped<IEmployeeApplication, EmployeeApplication>()
                .AddScoped<IEmployeeService, EmployeeService>()
                .AddScoped<IEmployeeRepository, EmployeeRepository>()
                .AddScoped<IEmployeePhoneService, EmployeePhoneService>()
                .AddScoped<IEmployeePhoneRepository, EmployeePhoneRepository>();          

            return services;
        }

        private static IServiceCollection CoreProjectAndDatabase(this IServiceCollection services)
        {
           /* services
                .AddDbContext<SimpleRestDB>(options => options.UseSqlServer(AppSettings.ConnectionString.ToString()));

            services
                .AddScoped<IAccountService, AccountService>()
                .AddScoped<IAccountRepository, AccountRepository>();

            services
                .AddScoped<ISessionService, SessionService>()
                .AddScoped<ISessionRepository, SessionRepository>();*/

            return services;
        }
    }
}