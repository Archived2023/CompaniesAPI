using Companies.API.Repositorys;
using Companies.API.Services;

namespace Companies.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped(provider => new Lazy<ICompanyRepository>(() => provider.GetRequiredService<ICompanyRepository>()));
            services.AddScoped(provider => new Lazy<ICompanyService>(() => provider.GetRequiredService<ICompanyService>()));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IServiceManager, ServiceManager>();
        }

        public static void AddCorsPolicy(this IServiceCollection services) => 
            services.AddCors(opt =>
            {
                opt.AddPolicy("CompaniesCorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
    }
}
