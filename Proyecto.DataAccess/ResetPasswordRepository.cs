using Dapper;
using Proyecto.Models;
using Proyecto.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Proyecto.DataAccess
{
    public class ResetPasswordRepository : Repository<ResetPassword>, IResetPasswordRepository
    {
        public ResetPasswordRepository(string connectionString) : base(connectionString)
        {

        }
        public string Insert(int clienteId)
        {
            string guid = Guid.NewGuid().ToString();
            DateTime fecha = DateTime.Now;
            string query = $"Insert into ResetPassword (Id,ClienteId,FechaDeModificacion) OUTPUT INSERTED.Id values ('{guid}',{clienteId},'{fecha}')";
            string cleanQuery = "DELETE FROM ResetPassword WHERE FechaDeModificacion <= DATEADD(hour, -2, GETDATE())";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Query(cleanQuery);
                return connection.QueryFirstOrDefault<string>(query);
            }
        }
        public ResetPassword GetById(string id)
        {
            string query = $"Select * from ResetPassword where Id = '{id}'";
            string cleanQuery = "DELETE FROM ResetPassword WHERE FechaDeModificacion <= DATEADD(hour, -2, GETDATE())";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Query(cleanQuery);
                return connection.QueryFirstOrDefault<ResetPassword>(query);
            }
        }
        public Password getPasswordByCliente(int clienteId)
        {
            string query = $"Select * from Password where ClienteId = {clienteId}";
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<Password>(query);
            }
        }
        public bool ValidarUID(string uid, int clienteId)
        {
            string query = $"Select * from ResetPassword where Id = '{uid}' and clienteId = {clienteId}";
            using (var connection = new SqlConnection(_connectionString))
            {
                var registro =  connection.QueryFirstOrDefault<ResetPassword>(query);
                if (registro != null) return true;
            }
            return false;
        }
    }
}
