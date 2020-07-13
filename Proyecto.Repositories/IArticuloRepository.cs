using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Repositories
{
    public interface IArticuloRepository : IRepository<Articulo>
    {
        IEnumerable<Articulo> GetPaginatedList(int page, int rows);

    }
}
