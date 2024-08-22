using ServiceHub.DataAccess.Base;
using ServiceHub.Domain.Context;
using ServiceHub.Domain.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.DataAccess.Interface
{
    public interface IUserCustomerRepository : IDataRepository<TblUserCustomer>
    {
        void Add(TblUserCustomer tblUserCustomer);

        void Update(TblUserCustomer tblUserCustomer);

        void Delete(TblUserCustomer tblUserCustomer);
    }
}