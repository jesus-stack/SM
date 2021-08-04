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
        public void Delete(long id)
        {
            IRepositoryProducto repository = new RepositoryProducto();
             repository.Delete(id);
        }


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

        public Producto GetProductoById(long id)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetProductoById(id);
        }

        public IEnumerable<Producto> GetProductoBy(String filtro)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetProducto().Where(x => x.Nombre.ToLower().Contains(filtro.ToLower()));
        }

        public IEnumerable<string> GetProDuctosNombres()
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetProducto().Select(x=> x.Nombre);
        }

        public Producto Save(Producto pro, string[] selectedProveedores)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.Save(pro,selectedProveedores);
        }

        public IEnumerable<ProductoSeccion> GetProductoBySeccion(long id, int seccion)
        {
            RepositoryProducto repository = new RepositoryProducto();
            return repository.GetProductoBySeccion(id,seccion);
        }


    }
}
