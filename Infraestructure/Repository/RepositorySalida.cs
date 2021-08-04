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
    public class RepositorySalida : IRepositorySalida
    {
        public IEnumerable<Salida> GetSalida()
        {
            IEnumerable<Salida> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Salida.Include("Movimiento").Include("Usuario").ToList();
                    return lista;
                }
            }
            catch (UpdateException ex)
            {
                String mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch(Exception e)
            {
                String mensaje = "";
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }



        }
        public void Save(Salida sal)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            IRepositoryProductoSeccion repositoryps = new RepositoryProductoSeccion();
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    using (var tran = ctx.Database.BeginTransaction())
                    {
                        long id= ctx.Salida.Max(x => x.Id);
                        id = id +1;
                        sal.Id = id;
                        ICollection<SalidaProducto> salidaProductos = sal.SalidaProducto;
                        sal.SalidaProducto = null;
                        ctx.Salida.Add(sal);
                        
                        foreach (var item in salidaProductos)
                        {
                            item.IdSalida = id;
                            ctx.Producto.FirstOrDefault(x => x.Id == item.IdProducto).Total = item.Producto.Total - item.Cantidad;
                            
                            item.Producto = null;
                            item.Seccion = null;
                            item.Salida = null;
                            ctx.SalidaProducto.Add(item);
                           
                           
                            IEnumerable<ProductoSeccion> lista = repository.GetProductoBySeccion(item.IdProducto, (int)item.IdSeccion);
                            long cant =(long) item.Cantidad;
                            foreach (var ps in lista)
                            {
                                
                                if (ps.Cantidad <= item.Cantidad)
                                {
                                    cant -=(long) ps.Cantidad;
                                    ctx.ProductoSeccion.Remove(ctx.ProductoSeccion.FirstOrDefault(x=>x.Lote==ps.Lote));
                                 
                                }
                                else
                                {
                                   ProductoSeccion prosec = ctx.ProductoSeccion.FirstOrDefault(x => x.Lote == ps.Lote);
                                    prosec.Cantidad -= item.Cantidad;
                                    cant= 0;
                                    Producto p = ctx.Producto.FirstOrDefault(x => x.Id == item.IdProducto);
                                    p.Total -= item.Cantidad;

                                }
                                if (cant == 0)
                                {
                                    break;
                                }

                        }
                          
                        }
                        ctx.SaveChanges();
                        tran.Commit();
                    }
                   
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


