using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ServiceHub.Domain.Context;
using ServiceHub.WebApp.Data;
using ServiceHub.WebApp.Interfaces;
using ServiceHub.WebApp.Models;
using System.Data;

namespace ServiceHub.WebApp.Repositories
{
    public class CategoriesRepository : ICategories
    {
        //private readonly IConfiguration _configuration;
        //private readonly SqlConnection _connection;
        private readonly System.Data.Common.DbConnection _connection;

        private readonly DataContext _applicationDbContext;

        public CategoriesRepository(IConfiguration configuration, DataContext applicationDbContext)
        {
            // _configuration = configuration;
            //_connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            _applicationDbContext = applicationDbContext;
            _connection = _applicationDbContext.Database.GetDbConnection();
        }

        public async Task<IEnumerable<CategoriesModel>> Get()
        {
            return await _connection.QueryAsync<CategoriesModel>("GetAllCategories", commandType: CommandType.StoredProcedure);
        }

        public async Task<CategoriesModel> Find(Guid uid)
        {
            var parmeters = new DynamicParameters();
            parmeters.Add("@f_uid", uid);
            return await _connection.QueryFirstOrDefaultAsync<CategoriesModel>("GetCategoryById", parmeters, commandType: CommandType.StoredProcedure);
        }

        public async Task<CategoriesModel> Add(CategoriesModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@f_uid", Guid.NewGuid());
            parameters.Add("@f_category_name", model.f_category_name);
            parameters.Add("", model.f_create_date = DateTime.Now);
            await _connection.ExecuteAsync("AddNewCategoty", parameters, commandType: CommandType.StoredProcedure);
            return model;
        }

        public async Task<CategoriesModel> Update(CategoriesModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@f_category_name", model.f_category_name);
            parameters.Add("@f_update_date", model.f_update_date = DateTime.Now);

            await _connection.ExecuteAsync("UpdateCategory", parameters, commandType: CommandType.StoredProcedure);
            return model;
        }

        public async Task<int> Remove(CategoriesModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@f_uid", model.f_uid);
            parameters.Add("f_delete_date", model.f_delete_date = DateTime.Now);
            return await _connection.ExecuteAsync("DeleteCategory", parameters, commandType: CommandType.StoredProcedure);
        }
    }
}