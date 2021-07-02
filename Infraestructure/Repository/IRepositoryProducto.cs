﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
   public  interface IRepositoryProducto
    {
        IEnumerable<Producto> GetProducto();
        IEnumerable<Producto> GetProductoByCategoria(int id);

        Producto GetProductoById(int id);
        Producto Save(Producto pro);
    }
}
