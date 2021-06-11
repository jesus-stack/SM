using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryCategoria : IRepositoryCategoria
    {
        public IEnumerable<Categoria> GetCategoria()
        {
            IEnumerable<Categoria> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    lista = ctx.Categoria.ToList<Categoria>();
                    return lista;
                }
            }
            catch (DbUpdateException dbEx)
            {

                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
        
        }
    }
}
