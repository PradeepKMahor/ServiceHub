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

            #endregion Core repository

            return services;
        }
    }
}