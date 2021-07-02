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

        public IEnumerable<Producto> GetProductoByCategoria(int id)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetProductoByCategoria(id);
        }

        public Producto GetProductoById(int id)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetProductoById(id);
        }

        public Producto Save(Producto pro)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.Save(pro);
        }
    }
}
