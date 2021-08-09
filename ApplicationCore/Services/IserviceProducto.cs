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
        IEnumerable<Producto> GetProductoBy(String filtro);

        Producto Save(Producto pro, string[] selectedProveedores);
        void Delete(long id);
        IEnumerable<String> GetProDuctosNombres();
        IEnumerable<Producto> GetProductosMostOut();
    }
}
