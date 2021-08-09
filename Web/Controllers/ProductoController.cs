using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.Utils;

namespace Web.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public PartialViewResult listarNombre(string filtro)
        {
            IEnumerable<Producto> lista = null;
            ServiceProducto service = new ServiceProducto();
            

            try
            {
                if (String.IsNullOrEmpty(filtro))
                {
                    lista = service.GetProducto();
                }
                else
                {
                    lista = service.GetProductoBy(filtro);
                }
                return PartialView("_PartialTablaP", lista);

            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }
            return PartialView("_PartialTablaP", lista);
        }

        public ActionResult Index()
        {
            IEnumerable<Producto> lista = null;
            ServiceProducto service = new ServiceProducto();
            IEnumerable<Categoria> listaCategorias = null;
            ServiceCategoria serviceCategoria = new ServiceCategoria();


            try
            {
                listaCategorias = serviceCategoria.GetCategoria();
                lista = service.GetProducto();
                
              
            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }
            ViewBag.categorias = listaCategorias;
            ViewBag.ListaNombres = service.GetProDuctosNombres();
            return View(lista);
        }
        public PartialViewResult listarTabla(int? id,String sub)
        {
            IEnumerable<Producto> lista = null;
            ServiceProducto service = new ServiceProducto();
         

            try
            {
                if (id != null)
                {
                    lista = service.GetProductoByCategoria((int)id);
                    ViewBag.Subtitle = sub;
                }
                else
                {
                    lista = service.GetProducto();      
                   
                }
                

            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }
          
            return PartialView("_PartialTablaP",lista);
        }
        public  PartialViewResult _PartialModalProducto(int id)
        {
            Producto pro = null;
            ServiceProducto service = new ServiceProducto();


            try
            {
              
                    pro = service.GetProductoById(id);
                    
             


            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }

            return PartialView("_PartialModalProducto",pro);
        }
        private SelectList listaCategorias(int Categoria = 0)
        {
            IServiceCategoria serviceCategoria = new ServiceCategoria();
            IEnumerable<Categoria> categorias = serviceCategoria.GetCategoria();
            return new SelectList(categorias, "Id", "Descripcion", Categoria);
        }
        public MultiSelectList listaProveedores(ICollection<Proveedor> proveedores)
        {
            IServiceProveedor service = new ServiceProveedor();
            IEnumerable<Proveedor> listaproveedores = service.GetProveedor();
            int[] listaproveedoresSelect = null;

            if (proveedores != null)
            {

                listaproveedoresSelect = proveedores.Select(s => s.Id).ToArray();
            }

            return new MultiSelectList(listaproveedores, "Id", "NombreOrganizacion", listaproveedoresSelect);
        }
        private SelectList listaSecciones(long idSeccion = 0)
        {
            IServiceSeccion serviceSeccion = new ServiceSeccion();
            IEnumerable<Seccion> secciones = serviceSeccion.GetSeccion();
            return new SelectList(secciones, "Id", "Descripcion", idSeccion);
        }

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Crear(long? id)
        {
            ServiceProducto service = new ServiceProducto();
           
            Producto pro = new Producto();
            try
            {
                ViewBag.ListaSecciones = listaSecciones();
                if (id != null)
                {
                    pro = service.GetProductoById((long)id);
                    ViewBag.ListaCategorias = listaCategorias((int)pro.Categoria);
                    ViewBag.ListaProveedores = listaProveedores(pro.Proveedor);
                    ViewBag.Mantenimientotitulo = "Modificar";
                    TempData["Secciones"] = pro.ProductoSeccion.ToList();
                 

                }
                else
                {
                    ViewBag.ListaCategorias = listaCategorias();
                    ViewBag.ListaProveedores = listaProveedores(null);
                    ViewBag.Mantenimientotitulo = "Crear";
                    TempData["Secciones"] = new List<ProductoSeccion>();
                }
               
            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }
          

            return View(pro);
        }

        [CustomAuthorize((int)Roles.Administrador)]
        [HttpPost]
        public ActionResult save(Producto producto, HttpPostedFileBase ImageFile, string[] selectedProveedores)
        {
            ServiceProducto service = new ServiceProducto();
            List<ProductoSeccion> lista = (List<ProductoSeccion>)TempData["Secciones"];
            MemoryStream target = new MemoryStream();
            try
            {
                if (lista != null)
                {
                    producto.ProductoSeccion = lista;
                }

                if (ImageFile != null)
                    {
                        ImageFile.InputStream.CopyTo(target);
                        producto.imagen = target.ToArray();
                       
                        ModelState.Remove("Imagen");
                    }

             
                if (ModelState.IsValid)
                {
                   
                    service.Save(producto, selectedProveedores);
                    return RedirectToAction("Index");
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);
                    ViewBag.ListaCategorias = listaCategorias((int) producto.Categoria);
                    ViewBag.ListaProveedores = listaProveedores(producto.Proveedor);
                    return RedirectToAction("Crear",producto.Id);
                }

        }
            catch
            {
                return RedirectToAction("Crear", producto);
    }
}

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Delete(long id)
        {
            IserviceProducto service = new ServiceProducto();
            try
            {

                service.Delete(id);
        }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }
           
            return RedirectToAction("Index");




        }
        [CustomAuthorize((int)Roles.Administrador)]
       
        public PartialViewResult Saveps(long producto,DateTime fecha,int Seccion,int cantidad) 
        {
            IServiceProductoSeccion service = new ServiceProductoSeccion();
            IServiceSeccion sec = new ServiceSeccion();
            IserviceProducto pro = new ServiceProducto();
            List<ProductoSeccion> lista =(List<ProductoSeccion>) TempData["Secciones"];
            TempData.Keep("Secciones");
            if (cantidad>0&&Seccion>0&&fecha!=null)
            {

                ProductoSeccion ps = new ProductoSeccion();
                ps.FechaVencimiento = fecha;
                ps.Cantidad = cantidad;
                ps.IdProducto = producto;
                ps.IdSeccion = Seccion;
       
                    ps.Seccion = sec.GetSeccion().FirstOrDefault(x => x.Id == Seccion);
                    ps.Producto = pro.GetProductoById(producto);
                    lista.Add(ps);
             
                TempData["Secciones"] = lista;
                ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Producto", "Seccion Agregada", SweetAlertMessageType.info);
                return PartialView("_SeccionLista");
               
            }
            else
            {
                // Valida Errores si Javascript está deshabilitado
                Util.ValidateErrors(this);
                ViewBag.ListaSecciones = listaSecciones(Seccion);
                
                return PartialView("_SeccionLista");

            }
        }

        public PartialViewResult Eliminarps(int index, long lote)
        {

            ServiceProductoSeccion service = new ServiceProductoSeccion();
            try
            {
              List<ProductoSeccion>  lista = (List<ProductoSeccion>)TempData["Secciones"];

                if (lote != 0)
                {
               
                    service.Eliminar(lote);

                }
                lista.RemoveAt(index);
                TempData["Secciones"] = lista;
            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }
            return PartialView("_SeccionLista");
        }
        
    }

 

}