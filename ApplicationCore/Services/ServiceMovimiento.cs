using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceMovimiento : IServiceMovimiento
    {
        public IEnumerable<Movimiento> GetSalidas()
        {
           IRepositoryMovimiento repository=new RepositoryMovimiento();
            return repository.GetSalidas();
        }
    }
}
