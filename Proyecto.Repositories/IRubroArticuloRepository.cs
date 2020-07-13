using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Repositories
{
    public interface IRubroArticuloRepository:IRepository<RubroArticulo>
    {
        IEnumerable<RubroArticulo> GetPaginatedList(int page, int rows);
    }
}
