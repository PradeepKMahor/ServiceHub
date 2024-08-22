using ServiceHub.DataAccess.Base;
using ServiceHub.DataAccess.Interface;
using ServiceHub.DataAccess.Repositories;
using ServiceHub.Domain.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _db;

        //   public IEmployeesRepository EmployeesRepository { get; private set; }
        public IUserCustomerRepository UserCustomer { get; private set; }

        //IExaRepository IUnitOfWork.ExaRepository => throw new NotImplementedException();

        public UnitOfWork(DataContext db)
        {
            _db = db;
            UserCustomer = new UserCustomerRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}