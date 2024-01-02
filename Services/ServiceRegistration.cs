using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Constants;
using Repository.Repositories;
using Services.Interfaces.IStore;
using Services.Services.Store;
using Services.UnitWork;

namespace Services
{
    public static class ServiceRegistration
    {
        public static void AddInfraStructure(this IServiceCollection services, 
                                                  IConfiguration configuration)
        {
            services.AddScoped<ICategoryService<Category>, CategoryService>();
            services.AddScoped<IProductService<Product>, ProductService>();
            services.AddScoped<IUserService<User>, UserService>();


            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            CommonConstants.ConnectionString = configuration.GetConnectionString("SqlConn");

        }
    }
}
