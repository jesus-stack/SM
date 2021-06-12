using Infraestructure.Models;
using System;
using System.Collections.Generic;
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
                    lista = ctx.Entrada.ToList();
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
