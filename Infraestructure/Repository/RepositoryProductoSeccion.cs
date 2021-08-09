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
    public class RepositoryProductoSeccion : IRepositoryProductoSeccion
    {
        public void Eliminar(long id)
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    ProductoSeccion ps = ctx.ProductoSeccion.FirstOrDefault(x => x.Lote == id);
                    
                    ctx.ProductoSeccion.Remove(ps);
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

        public ProductoSeccion GetProductoSeccionByLote(long lote)
        {
            ProductoSeccion lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.ProductoSeccion.Include("Seccion").Include("Producto").FirstOrDefault(x => x.Lote == lote); 
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

        public IEnumerable<ProductoSeccion> GetProductoSeccionByProducto(long id)
        {
            IEnumerable<ProductoSeccion> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.ProductoSeccion.Where(x => x.IdProducto == id).Include("Seccion").Include("Producto").ToList();
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

        public ProductoSeccion Save(ProductoSeccion ps)
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                   

                    ProductoSeccion prse = GetProductoSeccionByLote(ps.Lote);
                    long lote= ctx.ProductoSeccion.Max(x => x.Lote) + 1;
                    if (prse != null)
                    {
                        if (prse.IdSeccion != ps.IdSeccion)
                        {
                            Eliminar(prse.Lote);
                            prse = null;
                            lote = ps.Lote;
                        }
                    }
                   
                    if (prse == null)
                    {
                        ps.Lote = lote;
                       
                        ctx.ProductoSeccion.Add(ps);
                            ctx.SaveChanges();
                    }
                    else
                    {
                        ProductoSeccion pro = ctx.ProductoSeccion.FirstOrDefault(x => x.Lote==ps.Lote);

                        pro.Cantidad = ps.Cantidad;
                        pro.FechaVencimiento = ps.FechaVencimiento;
                       
                        ctx.SaveChanges();
                    }
                    return ps;
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
