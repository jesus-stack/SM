using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceProveedor : IServiceProveedor
    {
        public void Delete(int id)
        {
            IrepositoryProveedor repository = new RepositoryProveedor();
            repository.Eliminar(id);
        }

        public IEnumerable<Proveedor> GetProveedor()
        {
            IrepositoryProveedor repository = new RepositoryProveedor();
            return repository.GetProveedor();
        }

        public Proveedor GetProveedorById(int id)
        {
            IrepositoryProveedor repository = new RepositoryProveedor();
          return  repository.GetProveedorById(id);
        }

        public Proveedor Save(Proveedor pro, string[] selectedProducto)
        {
            IrepositoryProveedor repository = new RepositoryProveedor();
           return repository.Save(pro,selectedProducto);
        }
    }
}
