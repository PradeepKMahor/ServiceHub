using ServiceHub.DataAccess.Base;
using ServiceHub.Domain.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.DataAccess.Repositories.Core
{
    public interface IUsersCustomerRepository : IDataRepository<TblUserCustomer>
    {
    }
}