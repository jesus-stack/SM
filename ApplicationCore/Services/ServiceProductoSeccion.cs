using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceProductoSeccion : IServiceProductoSeccion
    {
        public void Eliminar(long id)
        {
            IRepositoryProductoSeccion repository = new RepositoryProductoSeccion();
            repository.Eliminar(id);
        }

        public ProductoSeccion GetProductoSeccionByLote(long lote)
        {
            IRepositoryProductoSeccion repository = new RepositoryProductoSeccion();
            return repository.GetProductoSeccionByLote(lote);
        }

        public IEnumerable<ProductoSeccion> GetProductoSeccionByProducto(long id)
        {
            IRepositoryProductoSeccion repository = new RepositoryProductoSeccion();
            return  repository.GetProductoSeccionByProducto(id);
        }

        public ProductoSeccion Save(ProductoSeccion ps)
        {
            IRepositoryProductoSeccion repository = new RepositoryProductoSeccion();
           return repository.Save(ps);
        }
    }
}
