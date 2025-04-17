using ServiceHub.DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.DataAccess.Interface.Core
{
    public interface ICustomerProductProfileRepository : IDataRepository<Domain.Models.Data.TblCustomerProductProfile>
    {
    }
}