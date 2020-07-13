using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Repositories
{
    public interface IResetPasswordRepository: IRepository<ResetPassword>
    {
        Password getPasswordByCliente(int clienteId);
        bool ValidarUID(string uid, int clienteId);
        string Insert(int clienteId);
        ResetPassword GetById(string id);
    }
}
