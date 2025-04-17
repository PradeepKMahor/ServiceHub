using ServiceHub.DataAccess.Base;
using ServiceHub.DataAccess.Interface.Core;
using ServiceHub.DataAccess.Repositories.Core;

namespace ServiceHub.WebApp.Classes
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddDataAccessService(this IServiceCollection services)
        {
            #region Core repository

            services.AddTransient<IUsersCustomerRepository, UsersCustomerRepository>();
            services.AddTransient<IUserClintRepository, UserClintRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICustomerProductProfileRepository, CustomerProductProfileRepository>();

            #endregion Core repository

            return services;
        }
    }
}