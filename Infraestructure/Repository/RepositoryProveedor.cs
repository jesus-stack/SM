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
                    lista = ctx.Proveedor.Include("Contacto").Include("Producto").ToList();

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



        public void Eliminar(long id)
        {
            throw new NotImplementedException();
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
                                int i = Convert.ToInt32(prov);
                                var Productotoadd = ctx.Producto.FirstOrDefault(x => x.Id == i);
                                ctx.Producto.Attach(Productotoadd);
                                pr.Producto.Add(Productotoadd);


                            }
                        }
                        pr.Estado = true;
                        pr.NombreOrganizacion = ctx.Proveedor.FirstOrDefault(x => x.Id == prove.Id).NombreOrganizacion;
                        pr.Direccion = ctx.Proveedor.FirstOrDefault(x => x.Id == prove.Id).Direccion;
                        pr.Pais = ctx.Proveedor.FirstOrDefault(x => x.Id == prove.Id).Pais;
                        pr.Producto = ctx.Proveedor.FirstOrDefault(X => X.Id == prove.Id).Producto;
                        ctx.SaveChanges();
                    }
                    else
                    {
                        ctx.Proveedor.Add(pr);
                        ctx.Entry(pr).State = EntityState.Modified;
                        ctx.SaveChanges();
                        //Actualizar Categorias
                        // var selectedproveedoresID = new HashSet<string>(selectedProveedores);
                        //if (selectedProveedores != null)
                        //{
                        //    ctx.Entry(pro).Collection(p => p.Proveedor).Load();
                        //    var newProveedorforProducto = ctx.Proveedor
                        //     .Where(x => selectedproveedoresID.Contains(x.Id.ToString())).ToList();
                        //    pro.Proveedor = newProveedorforProducto;

                        //    ctx.Entry(pro).State = EntityState.Modified;
                        //    ctx.SaveChanges();
                        //}


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

