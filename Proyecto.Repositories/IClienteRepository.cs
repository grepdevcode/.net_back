using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Repositories
{
    public interface IClienteRepository: IRepository<Cliente>
    {
        int InsertClienteDomicilio(Cliente cliente, Domicilio domiclio);
        Cliente ValidarCliente(string email, string password);
        bool BorrarCliente(Cliente cliente);
        IEnumerable<Cliente> GetPaginatedList(int page, int rows);
        Password GetPasswordByClienteId(int Id);
        Cliente GetClienteByEmail(string email);
    }
}
