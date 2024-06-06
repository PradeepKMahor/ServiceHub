using ServiceHub.DataAccess.Models;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.DataAccess.Base
{
    public interface IExecRepository<Tin, Tout> : IDisposable
    {
        Task<Tout> ExecAsync(BaseSp baseSp, Tin tInput);

        Task<Tout> ExecCacheAsync(BaseSp baseSp, Tin tInput, string[] tags);

        Task<Tout> StorageValuedFunctionAsync(BaseSp baseSp, Tin tInput);

        Task<Tout> StorageValuedFunctionCacheAsync(BaseSp baseSp, Tin tInput, string[] tags);
    }
}