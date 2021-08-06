using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoyContacto
    {

        IEnumerable<Contacto> GetContactos(long? id);
       

        Contacto GetContactoById(long id);
        Contacto Save(Contacto con);
       // Producto Save(Contacto con, string[] selectedProveedores);

        void Delete(long id);




    }
}
