using CachingFramework.Redis.Contracts;
using Microsoft.Extensions.Logging;
using ServiceHub.DataAccess.Helpers;
using ServiceHub.DataAccess.Models;
using ServiceHub.Domain.Context;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ServiceHub.DataAccess.Base
{
    public class ViewRepository<T> : IViewRepository<T> where T : class, new()
    {
        public string CommandText { get; set; }
        public string[] CacheKeySuffix { get; set; }
        public bool IsCachedByUser { get; set; } = true;
        public int AbsoluteExpirationRelativeToNowMinute { get; set; } = 60;

        private readonly ViewContext _context;
        private readonly IContext _redisContext;
        private readonly ILogger<ViewContext> _logger;

        public ViewRepository(ViewContext context, IContext redisContext, ILogger<ViewContext> logger)
        {
            _context = context;
            _redisContext = redisContext;
            _logger = logger;
        }

        public virtual T Get(BaseSp baseSp, ParameterSp parameterSp)
        {
            var item = new T();

            var conn = _context.Database.GetDbConnection();
            conn.Open();

            using (var command = conn.CreateCommand())
            {
                command.CommandText = CommandText;
                command.CommandType = CommandType.StoredProcedure;
                if (_context.Database.GetCommandTimeout() != null)
                    command.CommandTimeout = _context.Database.GetCommandTimeout().Value;

                if (baseSp != null)
                    command.Parameters.AddRange(Helper.FillSqlParameter(baseSp).ToArray());
                if (parameterSp != null)
                    command.Parameters.AddRange(Helper.FillSqlParameter(parameterSp, false).ToArray());

                DbDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        item = Helper.ConvertDbDataReaderToObject<T>(reader);
                    }
                }
                reader.Dispose();
                command.Dispose();
            }
            conn.Close();

            return item;
        }

        public virtual async Task<T> GetAsync(BaseSp baseSp, ParameterSp parameterSp)
        {
            var item = new T();

            var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync();

            using (var command = conn.CreateCommand())
            {
                command.CommandText = CommandText;
                command.CommandType = CommandType.StoredProcedure;
                if (_context.Database.GetCommandTimeout() != null)
                    command.CommandTimeout = _context.Database.GetCommandTimeout().Value;

                if (baseSp != null)
                    command.Parameters.AddRange(Helper.FillSqlParameter(baseSp).ToArray());
                if (parameterSp != null)
                    command.Parameters.AddRange(Helper.FillSqlParameter(parameterSp, false).ToArray());

                DbDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        item = Helper.ConvertDbDataReaderToObject<T>(reader);
                    }
                }
                reader.Dispose();
                command.Dispose();
            }
            conn.Close();

            return item;
        }

        public virtual async Task<T> GetCacheAsync(BaseSp baseSp, ParameterSp parameterSp, string[] tags)
        {
            var item = new T();
            string sql = "";
            bool cacheInError = false;

            var conn = _context.Database.GetDbConnection();

            using (var command = conn.CreateCommand())
            {
                command.CommandText = CommandText;
                command.CommandType = CommandType.StoredProcedure;
                if (_context.Database.GetCommandTimeout() != null)
                    command.CommandTimeout = _context.Database.GetCommandTimeout().Value;

                if (baseSp != null)
                    command.Parameters.AddRange(Helper.FillSqlParameter(baseSp).ToArray());
                if (parameterSp != null)
                    command.Parameters.AddRange(Helper.FillSqlParameter(parameterSp, false).ToArray());

                if (_context.DisableCache == false)
                {
                    sql = Helper.CommandAsSql(command);

                    if (IsCachedByUser == false)
                        sql = sql.Replace("@UIUserId = " + baseSp.UIUserId.ToString() + ", ", "");

                    try
                    {
                        var cached = await _redisContext.Cache.GetObjectAsync<T>(sql);

                        if (cached != null)
                        {
                            command.Dispose();
                            return cached;
                        }
                    }
                    catch (Exception ex)
                    {
                        cacheInError = true;
                        _logger.LogWarning(ex, sql);
                    }
                }

                await conn.OpenAsync();
                DbDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        item = Helper.ConvertDbDataReaderToObject<T>(reader);
                    }
                }

                reader.Dispose();
                command.Dispose();

                if (_context.DisableCache == false & cacheInError == false)
                {
                    await _redisContext.Cache.SetObjectAsync<T>(sql, item, tags, TimeSpan.FromMinutes(AbsoluteExpirationRelativeToNowMinute));
                }
                cacheInError = false;
            }
            conn.Close();

            return item;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(BaseSp baseSp, ParameterSp parameterSp)
        {
            var items = new List<T>();

            var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync();

            using (var command = conn.CreateCommand())
            {
                command.CommandText = CommandText;
                command.CommandType = CommandType.StoredProcedure;
                if (_context.Database.GetCommandTimeout() != null)
                    command.CommandTimeout = _context.Database.GetCommandTimeout().Value;

                if (baseSp != null)
                    command.Parameters.AddRange(Helper.FillSqlParameter(baseSp).ToArray());
                if (parameterSp != null)
                    command.Parameters.AddRange(Helper.FillSqlParameter(parameterSp, false).ToArray());

                DbDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        var item = Helper.ConvertDbDataReaderToObject<T>(reader);
                        items.Add(item);
                    }
                }
                reader.Dispose();

                command.Dispose();
            }
            conn.Close();

            return items;
        }

        public virtual async Task<IEnumerable<T>> GetAllCacheAsync(BaseSp baseSp, ParameterSp parameterSp, string[] tags)
        {
            var items = new List<T>();
            string sql = "";
            bool cacheInError = false;

            var conn = _context.Database.GetDbConnection();

            using (var command = conn.CreateCommand())
            {
                command.CommandText = CommandText;
                command.CommandType = CommandType.StoredProcedure;
                if (_context.Database.GetCommandTimeout() != null)
                    command.CommandTimeout = _context.Database.GetCommandTimeout().Value;

                if (baseSp != null)
                    command.Parameters.AddRange(Helper.FillSqlParameter(baseSp).ToArray());
                if (parameterSp != null)
                    command.Parameters.AddRange(Helper.FillSqlParameter(parameterSp, false).ToArray());

                if (_context.DisableCache == false)
                {
                    sql = Helper.CommandAsSql(command);

                    if (IsCachedByUser == false)
                        sql = sql.Replace("@UIUserId = " + baseSp.UIUserId.ToString() + ", ", "");

                    sql += CacheKeySuffix == null ? "" : " " + string.Join("|", CacheKeySuffix);

                    try
                    {
                        var cached = await _redisContext.Cache.GetObjectAsync<List<T>>(sql);

                        if (cached != null)
                        {
                            command.Dispose();
                            return cached;
                        }
                    }
                    catch (Exception ex)
                    {
                        cacheInError = true;
                        _logger.LogWarning(ex, sql);
                    }
                }

                await conn.OpenAsync();
                DbDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        var item = Helper.ConvertDbDataReaderToObject<T>(reader);
                        items.Add(item);
                    }
                }
                reader.Dispose();
                command.Dispose();

                if (_context.DisableCache == false & cacheInError == false)
                {
                    await _redisContext.Cache.SetObjectAsync(sql, items, tags, TimeSpan.FromMinutes(AbsoluteExpirationRelativeToNowMinute));
                }
                cacheInError = false;
            }
            conn.Close();

            return items;
        }

        public virtual async Task<DataTable> GetDataTableAsync(string sqlQuery)
        {
            var dt = new DataTable();

            var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync();

            using (var command = conn.CreateCommand())
            {
                command.CommandText = sqlQuery;
                command.CommandType = CommandType.Text;
                if (_context.Database.GetCommandTimeout() != null)
                    command.CommandTimeout = _context.Database.GetCommandTimeout().Value;

                DbDataReader reader = await command.ExecuteReaderAsync();

                if (reader.FieldCount > 0)
                {
                    dt.Load(reader);
                }
                reader.Dispose();

                command.Dispose();
            }
            conn.Close();

            return dt;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}