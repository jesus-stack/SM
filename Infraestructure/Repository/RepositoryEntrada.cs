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
    public class RepositoryEntrada : IRepositoryEntrada
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
                               join u in ctx.Usuario on e.IdUsuario equals u.Id select new { e, e.Movimiento, e.Usuario });

                    lista = ctx.Entrada.Include("Movimiento").Include("Usuario").ToList();
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

        public void Save(Entrada entrada)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            IRepositoryProductoSeccion repositoryps = new RepositoryProductoSeccion();
            try
            {

                using (MyContext ctx = new MyContext()) {


                using (var tran = ctx.Database.BeginTransaction())
                {
                    long id = ctx.Entrada.Max(x => x.Id);
                    id = id + 1;
                    entrada.Id = id;
                    ICollection<EntradaProducto> EntradaProductos = entrada.EntradaProducto;
                    entrada.EntradaProducto = null;
                    ctx.Entrada.Add(entrada);

                    foreach (var item in EntradaProductos)
                    {
                        item.IdEntrada = id;

                        Producto pro=ctx.Producto.FirstOrDefault(x => x.Id == item.IdProducto);
                        pro.Total += item.cantidad;
                        item.Producto = null;
                        item.Seccion = null;

                        ctx.EntradaProducto.Add(item);

                        ProductoSeccion productoSeccion = new ProductoSeccion();
                        productoSeccion.Cantidad = item.cantidad;
                        productoSeccion.FechaVencimiento = item.FechaVencimiento;
                        productoSeccion.IdProducto = item.IdProducto;
                        productoSeccion.IdSeccion = (int)item.idSeccion;
                        productoSeccion.Lote = ctx.ProductoSeccion.Max(x=>x.Lote)+1;
                        ctx.ProductoSeccion.Add(productoSeccion);
                        



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

