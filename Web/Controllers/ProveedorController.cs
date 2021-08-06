using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.Utils;

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
                lista = service.GetProveedor();


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

                    TempData["contac"] = pr.Contacto.ToList();
                    ViewBag.Mantenimientotitulo = "Modificar";
                }
                else
                {
                    TempData["contac"] = new List<Contacto>();
                    
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
            List<Contacto> lista = (List<Contacto>)TempData["contac"];

            try
            {
                if (lista != null)
                {
                    proveedor.Contacto = lista;
                }



                if (ModelState.IsValid)
                {
                    proveedor.Contacto = (List<Infraestructure.Models.Contacto>)TempData["contac"];
                    TempData.Keep("contac");
                    proveedor.Estado = true;
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


        [CustomAuthorize((int)Roles.Administrador)]
        public PartialViewResult _ListaContacto(long? id)
        {
            IEnumerable<Contacto> lista = null;
            IEnumerable<Contacto> listaTem=(List<Infraestructure.Models.Contacto>)TempData["contac"];
            TempData.Keep("contac");
            ServiceContacto service = new ServiceContacto();


            try
            {

                if (listaTem == null)
                {

                    lista = service.GetContactos(id).ToList();
                }
                else {
                    lista = listaTem;
                }



            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }
            ViewBag.listaContacto = service.GetContactos(id);
            return PartialView("_ListaContacto", lista);
        }



        [CustomAuthorize((int)Roles.Administrador)]
        public PartialViewResult SaveContac(long identificacion, String nombre, String telefono, String email, int idProveedor)
        {
            List<Infraestructure.Models.Contacto> lista = ((List<Infraestructure.Models.Contacto>)TempData["contac"]); 
            TempData.Keep("contac");

            if (lista == null)
            {
                lista = new List<Contacto>();
            }


            try
            {

                if (identificacion!=0 && !nombre.Equals("")&& !email.Equals(""))
                {
                    Contacto c = new Contacto();

                    c.Id = identificacion;
                    c.Nombre = nombre;
                    c.Telefono = telefono;
                    c.Correo = email;
                    c.IdProveedor = idProveedor;


                    lista.Add(c);

                    TempData["contac"] = lista;


                    return PartialView("_ListaContacto");
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);

                    return PartialView("_ListaContacto");
                }

            }
            catch
            {
                return PartialView("_ListaContacto");
            }
        }





    }
}