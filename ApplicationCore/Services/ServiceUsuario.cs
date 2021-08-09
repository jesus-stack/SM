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
        public void desactivar(long id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();

            repository.desactivar(id);
        }
        public void activar(long id)
        {
            RepositoryUsuario repository = new RepositoryUsuario();

            repository.activar(id);
        }


        public Usuario GetUsuario(long id, string password)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            // Encriptar el password para poder compararlo
            string crytpPasswd = Cryptography.EncrypthAES(password);
            return repository.GetUsuario(id, crytpPasswd);
        }

        public Usuario GetUsuarioByID(long id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            Usuario oUsuario = repository.GetUsuarioByID(id);
            if (oUsuario != null) { 
                oUsuario.contrasena = Cryptography.DecrypthAES(oUsuario.contrasena);
        }
            return oUsuario;
        }

        public IEnumerable<Usuario> GetUsuarios()
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
        
            return repository.GetUsuarios();
        }

        public Usuario Save(Usuario usuario)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            usuario.contrasena = Cryptography.EncrypthAES(usuario.contrasena);
            return repository.Save(usuario);
        }
    }
}
