using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
 public   interface IserviceProducto
    {
        IEnumerable<Producto> GetProducto();
        IEnumerable<Producto> GetProductoByCategoria(int id);
        Producto GetProductoById(long id);
        Producto Save(Producto pro, string[] selectedProveedores);
        void Delete(long id);
    }
}
