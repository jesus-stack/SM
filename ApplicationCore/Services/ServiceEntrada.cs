using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceEntrada : IServiceEntrada
    {
        public IEnumerable<Entrada> GetEntrada()
        {
            IRepositoryEntrada repository = new RepositoryEntrada();
            return repository.GetEntrada();
        }
    }
}
