using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
   public interface IrepositoryProveedor
    {
        IEnumerable<Proveedor> GetProveedor();
        Proveedor Save(Proveedor pr, string[] selectedProductos);
        void Eliminar(long id);
        Proveedor GetProveedorById(int id);
        // Contacto GetProductoSeccionByLote(int id);












    }





}
