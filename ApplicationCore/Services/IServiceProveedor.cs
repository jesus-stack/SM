using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
   public interface IServiceProveedor
    {
        IEnumerable<Proveedor> GetProveedor();
     
       
        Proveedor GetProveedorById(int id);

       

        Proveedor Save(Proveedor pro, string[] selectedProducto);


        void Delete(int id);
        //IEnumerable<String> GetProDuctosNombres();
    }
}
