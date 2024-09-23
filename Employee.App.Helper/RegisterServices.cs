using Employee.Business.Contracts;
using Employee.Business.Implementations;
using Employee.DAL.Contracts;
using Employee.DAL.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Employee.App.Helper
{
    public static class RegisterServices
    {
        public static void RegisterBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeBusinessService, EmployeeBusinessService>();
        }

        public static void RegisterDataServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeDataService, EmployeeDataServices>();
        }
    }
}
