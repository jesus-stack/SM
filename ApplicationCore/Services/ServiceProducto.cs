using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceProducto : IserviceProducto
    {
        public IEnumerable<Producto> GetProducto()
        {
        
            IRepositoryProducto repository= new RepositoryProducto();
            return repository.GetProducto();
           
        }
    }
}
