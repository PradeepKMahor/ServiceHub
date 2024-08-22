using ServiceHub.DataAccess.Base;
using ServiceHub.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IUserCustomerRepository UserCustomer { get; }

        void Save();
    }
}