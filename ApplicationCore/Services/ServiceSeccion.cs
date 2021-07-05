using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceSeccion : IServiceSeccion
    {
        public IEnumerable<Seccion> GetSeccion()
        {
            IRepositorySeccion repository = new RepositorySeccion();
            return repository.GetSeccion();
        }
    }
}
