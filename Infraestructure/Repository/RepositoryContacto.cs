using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryContacto : IRepositoyContacto
    {
        public void Delete(long id)
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    Contacto contacto = ctx.Contacto.FirstOrDefault(x => x.Id == id);
                    ctx.Contacto.Remove(contacto);
                    ctx.SaveChanges();
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

        public Contacto GetContactoById(long id)
        {
            Contacto contacto = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    contacto = ctx.Contacto.
                     Include("IdProveedor").
                    Where(p => p.Id == id).
                    FirstOrDefault<Contacto>();
                }
                return contacto;
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
                throw;
            }
        }

        public IEnumerable<Contacto> GetContactos()
        {
            IEnumerable<Contacto> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    lista = ctx.Contacto.ToList<Contacto>();
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

        public Contacto Save(Contacto con)
        {
            int retorno = 0;
            Contacto oContacto = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oContacto = GetContactoById(con.Id);
                    if (oContacto == null)
                    {
                        ctx.Contacto.Add(con);
                    }
                    else
                    {
                        ctx.Entry(con).State = EntityState.Modified;
                    }
                    retorno = ctx.SaveChanges();
                }
                if (retorno >= 0)
                    oContacto = GetContactoById(con.Id);
                return oContacto;
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
                throw;
            }

        }
    }
}
