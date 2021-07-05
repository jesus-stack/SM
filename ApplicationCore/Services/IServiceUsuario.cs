using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
  public   interface IServiceUsuario
    {
        Usuario GetUsuarioByID(long id);
        Usuario Save(Usuario usuario);
        Usuario GetUsuario(long id, string password);
    }
}
