using ServiceHub.DataAccess.Base;
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

            #endregion Core repository

            return services;
        }
    }
}