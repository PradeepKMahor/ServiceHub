using ServiceHub.DataAccess.Models;
using System.Data;

namespace ServiceHub.DataAccess.Base
{
    public interface IViewRepository<T> : IDisposable
    {
        Task<T> GetAsync(BaseSp baseSp, ParameterSp parameterSp);

        Task<T> GetCacheAsync(BaseSp baseSp, ParameterSp parameterSp, string[] tags);

        Task<IEnumerable<T>> GetAllAsync(BaseSp baseSp, ParameterSp parameterSp);

        Task<IEnumerable<T>> GetAllCacheAsync(BaseSp baseSp, ParameterSp parameterSp, string[] tags);

        Task<DataTable> GetDataTableAsync(string sqlQuery);
    }
}