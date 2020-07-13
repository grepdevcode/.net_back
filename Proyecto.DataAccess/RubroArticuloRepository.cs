﻿using Dapper;
using Proyecto.Models;
using Proyecto.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Proyecto.DataAccess
{
    public class RubroArticuloRepository : Repository<RubroArticulo>, IRubroArticuloRepository
    {
        public RubroArticuloRepository(string connectionString): base(connectionString)
        {

        }
        public IEnumerable<RubroArticulo> GetPaginatedList(int page, int rows)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@page", page);
            parameters.Add("@rows", rows);
            parameters.Add("@name", "RubroArticulo");
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<RubroArticulo>("dbo.PaginatedSelect", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
