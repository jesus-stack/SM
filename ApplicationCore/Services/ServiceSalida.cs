using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceSalida : IServiceSalida
    {
        public IEnumerable<Salida> GetSalida()
        {
            IRepositorySalida repository = new RepositorySalida();
            return repository.GetSalida();
        }
    }
}
