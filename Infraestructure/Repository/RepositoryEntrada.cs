using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
  public  class RepositoryEntrada : IRepositoryEntrada
    {
        public IEnumerable<Entrada> GetEntrada()
        {
            IEnumerable<Entrada> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    var res = (from e in ctx.Entrada
                              join m in ctx.Movimiento on e.IdMovimiento equals m.Id
                              join u in ctx.Usuario on e.IdUsuario equals u.Id select new { e,e.Movimiento,e.Usuario});

                    lista =ctx.Entrada.Include("Movimiento").Include("Usuario").ToList();
                    return lista;
                }
            }
            catch (UpdateException ex)
            {
                String mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception e)
            {
                String mensaje = "";
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
        }
    }
}
