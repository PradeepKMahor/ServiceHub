using ServiceHub.DataAccess.Base;
using ServiceHub.DataAccess.Interface;
using ServiceHub.Domain.Context;
using ServiceHub.Domain.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.DataAccess.Repositories
{
    public class UserCustomerRepository(DataContext context) : DataRepository<TblUserCustomer>(context), IUserCustomerRepository
    {
        public void Add(TblUserCustomer UserCustomer)
        {
            context.Add(UserCustomer);
        }

        public void Update(TblUserCustomer UserCustomer)
        {
            context.Update(UserCustomer);
        }

        public void Delete(TblUserCustomer UserCustomer)
        {
            context.Remove(UserCustomer);
        }
    }
}