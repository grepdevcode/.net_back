using Dapper;
using Dapper.Contrib.Extensions;
using Proyecto.Models;
using Proyecto.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Proyecto.DataAccess
{
    public class ArticuloManufacturadoDetalleRepository : Repository<ArticuloManufacturadoDetalle>, IArticuloManufacturadoDetalleRepository
    {
        public ArticuloManufacturadoDetalleRepository(string connectionString) : base(connectionString)
        {
        }

        public int InsertList(List<ArticuloManufacturadoDetalle> lista)
        {
            using (var Connection = new SqlConnection(_connectionString))
            {
                return (int) Connection.Insert(lista);
            }
        }

        public bool CascadeDelete(int ArticuloManufacturadoId)
        {
            string sql = $"Select * from ArticuloManufacturadoDetalle where ArticuloManufacturadoId = {ArticuloManufacturadoId}";
            using (var Connection = new SqlConnection(_connectionString))
            {
                var listArtManDetalles = Connection.Query<ArticuloManufacturadoDetalle>(sql);
                if(listArtManDetalles.Count() > 0)
                {
                    foreach (var item in listArtManDetalles)
                    {
                        this.Delete(item);
                    }
                }

                return true;
            }
        }


    }
}
