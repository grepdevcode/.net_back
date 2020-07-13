using Dapper;
using Proyecto.Models;
using Proyecto.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Proyecto.DataAccess
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(string connectionString) : base(connectionString)
        {
        }

        public int InsertClienteDomicilio(Cliente cliente, Domicilio domiclio)
        {
            return 1;
        }

        public Cliente ValidarCliente(string email, string password)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@email", email);
            parameters.Add("@password", password);
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<Cliente>("dbo.ValidarCliente", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public bool BorrarCliente(Cliente cliente)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ClienteId", cliente.Id);
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<bool>("dbo.BorrarCliente", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }

        }

        public IEnumerable<Cliente> GetPaginatedList( int page, int rows)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@page", page);
            parameters.Add("@rows", rows);
            parameters.Add("@name", "Cliente");
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Cliente>("dbo.PaginatedSelect", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public Password GetPasswordByClienteId(int Id)
        {
            string query = $"Select * from Password Where ClienteId = {Id}";
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<Password>(query);
            }
        }

        public Cliente GetClienteByEmail(string email)
        {
            string query = $"Select * from Cliente Where Email = '{email}'";
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<Cliente>(query);
            }
        }
    }
}
