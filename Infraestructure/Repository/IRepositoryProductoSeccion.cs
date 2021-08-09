using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
 public   interface IRepositoryProductoSeccion
    {
        IEnumerable<ProductoSeccion> GetProductoSeccionByProducto(long id);
        ProductoSeccion Save(ProductoSeccion ps);
        void Eliminar(long id);
        ProductoSeccion GetProductoSeccionByLote(long lote);

    }
}
