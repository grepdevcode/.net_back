using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Repositories
{
    public interface IArticuloManufacturadoRepository: IRepository<ArticuloManufacturado>
    {
        IEnumerable<ArticuloManufacturado> GetPaginatedList(int page, int rows);
    }
}
