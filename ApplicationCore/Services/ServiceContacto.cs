using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceContacto : IServiceContacto
    {
        public void Delete(long id)
        {
            IRepositoyContacto repo = new RepositoryContacto();
            repo.Delete(id);
        }

        public Contacto GetContactoById(long id)
        {
            IRepositoyContacto repo = new RepositoryContacto();
           return repo.GetContactoById(id);
        }

        public IEnumerable<Contacto> GetContactos()
        {
            IRepositoyContacto repo = new RepositoryContacto();
            return repo.GetContactos();
        }

        public Contacto Save(Contacto con)
        {
            IRepositoyContacto repo = new RepositoryContacto();
            return repo.Save(con);
        }
    }
}
