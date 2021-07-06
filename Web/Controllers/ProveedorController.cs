using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Security;

namespace Web.Controllers
{
    public class ProveedorController : Controller
    {
        // GET: Proveedor
        public ActionResult Index()
        {
            IEnumerable<Proveedor> lista = null;
            
            ServiceProveedor service = new ServiceProveedor();
           


            try
            {
                lista = service.GetProveedor() ;
               

            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }
            //ViewBag.categorias = listaCategorias;
            return View(lista);
        }
        public PartialViewResult ListarTabla()
        {
            IEnumerable<Proveedor> lista = null;
            ServiceProveedor service = new ServiceProveedor();


            try
            {
                lista = service.GetProveedor();


            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }

            return PartialView("_PartialProveedor", lista);
        }
        public MultiSelectList listaProductos(ICollection<Producto> productos)
        {
            IserviceProducto service = new ServiceProducto();
            IEnumerable<Producto> listapros = service.GetProducto();
            long[] listaproSelect = null;

            if (productos != null)
            {

                listaproSelect = productos.Select(s => s.Id).ToArray();
            }

            return new MultiSelectList(listapros, "Id", "Nombre", listaproSelect);
        }


        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Crear(int? id)
        {
            ServiceProducto service = new ServiceProducto();
            ServiceProveedor servicep = new ServiceProveedor();

            Proveedor pr = new Proveedor();
            try
            {

                if (id != null)
                {
                    pr = servicep.GetProveedorById((int)id);
                    ViewBag.ListaProductos = listaProductos(pr.Producto);
                    

                    ViewBag.Mantenimientotitulo = "Modificar";
                }
                else
                {
                    ViewBag.ListaProductos = listaProductos(null);
                    ViewBag.Mantenimientotitulo = "Crear";
                }

            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }


            return View(pr);
        }

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult save(Proveedor proveedor, string[] selectedPrpductos)
        {
            IServiceProveedor service = new ServiceProveedor();

            try
            {




                if (ModelState.IsValid)
                {

                    service.Save(proveedor, selectedPrpductos);
                    return RedirectToAction("Index");
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);
                    ViewBag.ListaProductos = listaProductos(proveedor.Producto);
                    return RedirectToAction("Crear", proveedor.Id);
                }

            }
            catch
            {
                return RedirectToAction("Crear", proveedor);
            }
        }

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Delete(int id)
        {
            IServiceProveedor service = new ServiceProveedor();
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

    }
}