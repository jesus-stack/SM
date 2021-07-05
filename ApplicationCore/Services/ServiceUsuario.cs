using ApplicationCore.Utils;
using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
  public   class ServiceUsuario : IServiceUsuario
    {
        public Usuario GetUsuario(long id, string password)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            // Encriptar el password para poder compararlo
            string crytpPasswd = Cryptography.EncrypthAES(password);
            return repository.GetUsuario(id, password);
        }

        public Usuario GetUsuarioByID(long id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            Usuario oUsuario = repository.GetUsuarioByID(id);
            oUsuario.contrasena = Cryptography.DecrypthAES(oUsuario.contrasena);
            return oUsuario;
        }

        public Usuario Save(Usuario usuario)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            usuario.contrasena = Cryptography.EncrypthAES(usuario.contrasena);
            return repository.Save(usuario);
        }
    }
}
