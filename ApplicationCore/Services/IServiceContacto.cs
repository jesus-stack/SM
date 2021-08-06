using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceContacto
    {
        IEnumerable<Contacto> GetContactos(long? id);


        Contacto GetContactoById(long id);
        Contacto Save(Contacto con);
        // Producto Save(Contacto con, string[] selectedProveedores);

        void Delete(long id);



    }
}
