using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}