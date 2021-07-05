using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            IEnumerable<Producto> lista = null;
            ServiceProducto service = new ServiceProducto();
            IEnumerable<Categoria> listaCategorias = null;
            ServiceCategoria serviceCategoria = new ServiceCategoria();


            try
            {
                lista = service.GetProducto();
                listaCategorias = serviceCategoria.GetCategoria();
              
            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }
            ViewBag.categorias = listaCategorias;
          
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
        private SelectList listaCategorias(int idCategoria = 0)
        {
            IServiceCategoria serviceCategoria = new ServiceCategoria();
            IEnumerable<Categoria> categorias = serviceCategoria.GetCategoria();
            return new SelectList(categorias, "Id", "Descripcion", idCategoria);
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
        public ActionResult Crear(long? id)
        {
            ServiceProducto service = new ServiceProducto();
           
            Producto pro = new Producto();
            try
            {
               
                if (id != null)
                {    
                    pro = service.GetProductoById((long)id);
                    ViewBag.ListaCategorias = listaCategorias((int) pro.Categoria);
                    ViewBag.ListaProveedores = listaProveedores(pro.Proveedor);
                   
                    ViewBag.Mantenimientotitulo = "Modificar";
                }
                else
                {
                    ViewBag.ListaCategorias = listaCategorias();
                    ViewBag.ListaProveedores = listaProveedores(null);
                    ViewBag.Mantenimientotitulo = "Crear";
                }
               
            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }
          

            return View(pro);
        }
        [HttpPost]
        public ActionResult save(Producto producto, HttpPostedFileBase ImageFile, string[] selectedProveedores)
        {
            ServiceProducto service = new ServiceProducto();
            MemoryStream target = new MemoryStream();
            try
            {

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
                    return View("Crear", producto);
                }

            }
            catch
            {
                return RedirectToAction("Crear");
            }
        }

        public ActionResult Editar(int? id)
        {
            Producto pro = null;
            ServiceProducto service = new ServiceProducto();


            try
            {
                if (id != null)
                {
                    pro = service.GetProductoById((int)id);
                   
                }
                else
                {
                    pro = new Producto();

                }


            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }
            return View();
        }
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

        public ActionResult saveps(ProductoSeccion ps) 
        {
            IServiceProductoSeccion serviceProductoSeccion = new ServiceProductoSeccion();
            ps.IdProducto = (long) 1002;
            serviceProductoSeccion.Save(ps);
            return View("Crear");
        }
    }

 

}