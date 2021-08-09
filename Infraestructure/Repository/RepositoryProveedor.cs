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
    public class RepositoryProveedor : IrepositoryProveedor
    {

        public IEnumerable<Proveedor> GetProveedor()
        {
            IEnumerable<Proveedor> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Proveedor.Include("Contacto").Include("Producto").Where(x=>x.Estado==true).ToList();

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



        public void Eliminar(int id)
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    Proveedor prod = ctx.Proveedor.FirstOrDefault(x => x.Id == id);
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



        public Proveedor Save(Proveedor pr, string[] selectedProducto)
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {

                    ctx.Configuration.LazyLoadingEnabled = false;
                    Proveedor prove = GetProveedorById(pr.Id);
                    if (prove == null)
                    {


                        if (selectedProducto != null)
                        {

                            pr.Producto = new List<Producto>();
                            foreach (var prov in selectedProducto)
                            {
                                long i = Convert.ToInt64(prov);
                                var protoadd = ctx.Producto.FirstOrDefault(x => x.Id == i);
                                ctx.Producto.Attach(protoadd);
                                pr.Producto.Add(protoadd);


                            }
                        }
                            pr.Estado = true;

                            pr.Id = ctx.Proveedor.Max(x => x.Id) + 1;
                            pr = ctx.Proveedor.Add(pr);
                            ctx.SaveChanges();
                        }

                    
                    else
                    {
                        ICollection<Contacto> lista = pr.Contacto;
                        pr.Contacto = null;
                       
                        foreach(var item in lista)
                        {
                            if (item.Proveedor == null)
                            {
                                ctx.Contacto.Add(item);
                                ctx.SaveChanges();
                            }
                        }
                        ctx.Proveedor.Add(pr);
                        ctx.Entry(pr).State = EntityState.Modified;
                        ctx.SaveChanges();
                        //Actualizar Categorias
                        var selectedpro = new HashSet<string>(selectedProducto);
                        if (selectedProducto != null)
                        {
                            ctx.Entry(pr).Collection(p => p.Producto).Load();
                            var newpro = ctx.Producto
                             .Where(x => selectedpro.Contains(x.Id.ToString())).ToList();
                            pr.Producto = newpro;

                            ctx.Entry(pr).State = EntityState.Modified;
                            ctx.SaveChanges();


                        }


                    }
                    return prove;

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

        public Proveedor GetProveedorById(int id)
        {
            Proveedor pro = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    pro = ctx.Proveedor.Include("Contacto").Include("Producto").FirstOrDefault(x => x.Id == id);

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

