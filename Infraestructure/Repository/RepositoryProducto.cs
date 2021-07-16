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
    public class RepositoryProducto : IRepositoryProducto
    {
       

        public void Delete(long id )
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                   
                    Producto prod = ctx.Producto.FirstOrDefault(x => x.Id == id);
                    prod.Estado = false;
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

        public IEnumerable<Producto> GetProducto()
        {
            IEnumerable<Producto> lista = null;
            try
            {
                using (MyContext ctx= new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.Producto.Include("Categoria1").Include("Proveedor").Include("ProductoSeccion").Include("ProductoSeccion.Seccion").Where(x => x.Estado==true).ToList();

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

        public IEnumerable<Producto> GetProductoByCategoria(int id)
        {
            IEnumerable<Producto> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    
                    lista = ctx.Producto.Include("Categoria1").Include("Proveedor").Include("ProductoSeccion").Include("ProductoSeccion.Seccion").Where(x => x.Categoria == id && x.Estado==true).ToList();

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

        public Producto GetProductoById(long id)
        {
            Producto pro = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    pro = ctx.Producto.Include("Categoria1").Include("Proveedor").Include("ProductoSeccion").Include("ProductoSeccion.Seccion").FirstOrDefault(x => x.Id == id);

                    return pro;
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

        public Producto Save(Producto pro, string[] selectedProveedores)
        {
            ICollection<ProductoSeccion> lista = pro.ProductoSeccion;
            pro.ProductoSeccion=null;
           
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    long c = ctx.ProductoSeccion.Max(x => x.Lote) + 1;
                    ctx.Configuration.LazyLoadingEnabled = false;
                    Producto prod = GetProductoById(pro.Id);
                   
                   
                    if (prod==null)
                    {

                        if (selectedProveedores != null)
                        {

                            pro.Proveedor = new List<Proveedor>();
                            foreach (var prov in selectedProveedores)
                            {
                                int i = Convert.ToInt32(prov);
                                var proveedortoadd = ctx.Proveedor.FirstOrDefault(x => x.Id == i);
                                ctx.Proveedor.Attach(proveedortoadd);
                                pro.Proveedor.Add(proveedortoadd);


                            }
                        }
                       
                        pro.Estado = true;
                        pro.Total = 0;
                        pro.Id = ctx.Producto.Max(x => x.Id) + 1;
                        pro = ctx.Producto.Add(pro);
                        ctx.SaveChanges();
                        foreach (var item in lista)
                        {
                            if (item.Lote == 0)
                            {
                                item.Producto = null;
                                item.Seccion = null;
                                item.IdProducto=pro.Id;
                                item.Lote = c;
                                ctx.ProductoSeccion.Add(item);

                                c++;
                            }

                        }
                        ctx.SaveChanges();
                        ctx.SaveChanges();
                      
                    }
                    else
                    {
                      
                        ctx.Producto.Add(pro);
                        ctx.Entry(pro).State = EntityState.Modified;
                         ctx.SaveChanges();
                        //Actualizar Categorias
                        var selectedproveedoresID = new HashSet<string>(selectedProveedores);
                        if (selectedProveedores != null)
                        {
                            ctx.Entry(pro).Collection(p => p.Proveedor).Load();
                            var newProveedorforProducto = ctx.Proveedor
                             .Where(x => selectedproveedoresID.Contains(x.Id.ToString())).ToList();
                            pro.Proveedor = newProveedorforProducto;

                            ctx.Entry(pro).State = EntityState.Modified;
                             ctx.SaveChanges();
                         

                        }
                      
                        foreach(var item in lista)
                        {
                            if (item.Lote == 0)
                            {
                                item.Producto = null;
                                item.Seccion = null;
                                item.Lote = c;
                                ctx.ProductoSeccion.Add(item);
                               
                                c++;
                            }
                         
                        }
                        ctx.SaveChanges();



                    }
                   
                    return pro;
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
