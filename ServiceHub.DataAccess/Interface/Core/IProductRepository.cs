using ServiceHub.DataAccess.Base;

namespace ServiceHub.DataAccess.Interface.Core
{
    public interface IProductRepository : IDataRepository<Domain.Models.Data.TblProduct>
    {
    }
}