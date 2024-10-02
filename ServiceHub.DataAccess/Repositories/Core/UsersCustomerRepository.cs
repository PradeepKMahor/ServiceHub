using ServiceHub.DataAccess.Base;
using ServiceHub.DataAccess.Interface.Core;
using ServiceHub.Domain.Context;
using ServiceHub.Domain.Models.Data;

namespace ServiceHub.DataAccess.Repositories.Core
{
    public class UsersCustomerRepository : DataRepository<TblUserCustomer>, IUsersCustomerRepository
    {
        public UsersCustomerRepository(DataContext context) : base(context)
        {
        }
    }
}