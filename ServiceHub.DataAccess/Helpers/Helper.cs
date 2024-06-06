using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FastMember;

namespace ServiceHub.DataAccess.Helpers
{
    public static class Helper
    {
        private static readonly Dictionary<Type, SqlDbType> typeMap;

        static Helper()
        {
            typeMap = new Dictionary<Type, SqlDbType>
            {
                [typeof(string)] = SqlDbType.NVarChar,
                [typeof(char[])] = SqlDbType.NVarChar,
                [typeof(byte)] = SqlDbType.TinyInt,
                [typeof(short)] = SqlDbType.SmallInt,
                [typeof(int)] = SqlDbType.Int,
                [typeof(long)] = SqlDbType.BigInt,
                [typeof(byte[])] = SqlDbType.Image,
                [typeof(bool)] = SqlDbType.Bit,
                [typeof(DateTime)] = SqlDbType.DateTime2,
                [typeof(DateTimeOffset)] = SqlDbType.DateTimeOffset,
                [typeof(decimal)] = SqlDbType.Money,
                [typeof(float)] = SqlDbType.Real,
                [typeof(double)] = SqlDbType.Float,
                [typeof(TimeSpan)] = SqlDbType.Time,
                [typeof(Guid)] = SqlDbType.UniqueIdentifier,
                [typeof(object)] = SqlDbType.NVarChar
            };
        }

        /// <summary>
        /// Maps a DbDataReader record to an object. Ignoring case.
        /// </summary>
        public static T ConvertDbDataReaderToObject<T>(this DbDataReader rd) where T : class, new()
        {
            Type type = typeof(T);
            var accessor = TypeAccessor.Create(type);
            var members = accessor.GetMembers();
            var t = new T();

            for (int i = 0; i < rd.FieldCount; i++)
            {
                if (!rd.IsDBNull(i))
                {
                    string fieldName = rd.GetName(i);

                    if (members.Any(m => string.Equals(m.Name, fieldName, StringComparison.OrdinalIgnoreCase)))
                    {
                        accessor[t, fieldName] = rd.GetValue(i);
                    }
                }
            }

            return t;
        }

        /// <summary>
        /// Maps the output records to an object. Ignoring case.
        /// </summary>
        public static T ConvertDbParameterToObject<T>(this DbParameterCollection pars) where T : class, new()
        {
            Type type = typeof(T);
            var accessor = TypeAccessor.Create(type);
            var members = accessor.GetMembers();
            var t = new T();

            foreach (DbParameter par in pars)
            {
                if (par.Direction == ParameterDirection.Output || par.Direction == ParameterDirection.ReturnValue)
                {
                    string fieldName = par.ParameterName.Replace("@", "");

                    if (members.Any(m => string.Equals(m.Name, fieldName, StringComparison.OrdinalIgnoreCase)))
                    {
                        if (par.Value != DBNull.Value)
                            accessor[t, fieldName] = par.Value;
                    }
                }
            }

            return t;
        }

        /// <summary>
        /// Fill SqlParameters based on a list of items.
        /// </summary>
        public static List<SqlParameter> FillSqlParameter<T>(T t, bool AddAllProperty = true) where T : class, new()
        {
            var pars = new List<SqlParameter>();

            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (AddAllProperty == false)
                {
                    if (property.GetValue(t, null) != null && !string.IsNullOrEmpty(property.GetValue(t, null).ToString()))
                    {
                        pars.Add(new SqlParameter
                        {
                            ParameterName = "@" + property.Name,
                            Value = property.GetValue(t, null)
                        });
                    }
                }
                else
                {
                    pars.Add(
                    new SqlParameter
                    {
                        ParameterName = "@" + property.Name,
                        Value = property.GetValue(t, null)
                    });
                }
            }

            return pars;
        }

        /// <summary>
        /// Fill SqlParameters output based on a class.
        /// </summary>
        public static List<SqlParameter> FillSqlParameterOutput<T>() where T : class, new()
        {
            var pars = new List<SqlParameter>();

            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                var sqlParameter = new SqlParameter
                {
                    ParameterName = "@" + property.Name,
                    Direction = ParameterDirection.Output,
                    SqlDbType = GetDbType(property.PropertyType)
                };

                if (property.PropertyType == typeof(string))
                    sqlParameter.Size = 4000;

                if (property.PropertyType == typeof(object))
                    sqlParameter.Size = -1;

                pars.Add(sqlParameter);
            }

            return pars;
        }

        /// <summary>
        /// Fill SqlParameters output based on a class.
        /// </summary>
        public static List<SqlParameter> FillSqlParameterReturnValue<T>() where T : class, new()
        {
            var pars = new List<SqlParameter>();

            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                var sqlParameter = new SqlParameter
                {
                    ParameterName = "@" + property.Name,
                    Direction = ParameterDirection.ReturnValue,
                    SqlDbType = GetDbType(property.PropertyType)
                };

                if (property.PropertyType == typeof(string))
                    sqlParameter.Size = 4000;

                pars.Add(sqlParameter);
            }

            return pars;
        }

        /// <summary>
        /// Return the SQL statement of a stored procedure
        /// </summary>
        public static string CommandAsSql(DbCommand sc)
        {
            string sql = sc.CommandText;

            sql = sql.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
            sql = System.Text.RegularExpressions.Regex.Replace(sql, @"\s+", " ");

            foreach (SqlParameter sp in sc.Parameters)
            {
                string spName = sp.ParameterName;
                string spValue = ParameterValueForSql(sp);
                sql = sql + " " + spName + " = " + spValue + ",";
            }

            sql = sql.Trim().TrimEnd(',');
            sql = sql.Replace("= NULL", "IS NULL");
            sql = sql.Replace("!= NULL", "IS NOT NULL");
            return sql;
        }

        private static string ParameterValueForSql(SqlParameter sp)
        {
            string retval;

            switch (sp.SqlDbType)
            {
                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.NText:
                case SqlDbType.NVarChar:
                case SqlDbType.Text:
                case SqlDbType.Time:
                case SqlDbType.VarChar:
                case SqlDbType.Xml:
                case SqlDbType.Date:
                case SqlDbType.DateTime:
                case SqlDbType.DateTime2:
                case SqlDbType.DateTimeOffset:
                    if (sp.Value == DBNull.Value)
                    {
                        retval = "NULL";
                    }
                    else
                    {
                        retval = "'" + sp.Value.ToString().Replace("'", "''") + "'";
                    }
                    break;

                case SqlDbType.Bit:
                    if (sp.Value == DBNull.Value)
                    {
                        retval = "NULL";
                    }
                    else
                    {
                        retval = ((bool)sp.Value == false) ? "0" : "1";
                    }
                    break;

                default:
                    if (sp.Value == DBNull.Value)
                    {
                        retval = "NULL";
                    }
                    else
                    {
                        retval = sp.Value.ToString().Replace("'", "''");
                    }
                    break;
            }

            return retval;
        }

        private static SqlDbType GetDbType(Type propertyType)
        {
            // Allow nullable types to be handled
            propertyType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;

            if (typeMap.ContainsKey(propertyType))
            {
                return typeMap[propertyType];
            }

            throw new ArgumentException($"{propertyType.FullName} is not a supported .NET class");
        }
    }
}