using Dapper.Contrib.Extensions;
using Proyecto.Repositories;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Proyecto.DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public string _connectionString;
        public Repository(string connectionString)
        {
            SqlMapperExtensions.TableNameMapper = (type) => { return $"{type.Name }"; };
            _connectionString = connectionString;
        }

        public bool Delete(T entity)
        {
            using(var Connection = new SqlConnection(_connectionString))
            {
                return Connection.Delete(entity);
            }
            
        }

        public T GetById(int id)
        {
            using (var Connection = new SqlConnection(_connectionString))
            {
                return Connection.Get<T>(id);
            }
        }

        public IEnumerable<T> GetList()
        {
            using (var Connection = new SqlConnection(_connectionString))
            {
                return Connection.GetAll<T>();
            }
        }

        public int Insert(T entity)
        {
            using (var Connection = new SqlConnection(_connectionString))
            {
                return (int) Connection.Insert(entity);
            }
        }
        public bool Update(T entity)
        {
            using (var Connection = new SqlConnection(_connectionString))
            {
                return Connection.Update(entity);
            }
        }


    }
}
