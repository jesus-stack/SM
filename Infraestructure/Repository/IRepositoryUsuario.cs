using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
   public interface IRepositoryUsuario
    {
        Usuario GetUsuarioByID(long id);
        Usuario Save(Usuario usuario);
        Usuario GetUsuario(long id, string password);
    }
}
