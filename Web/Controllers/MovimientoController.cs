using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class MovimientoController : Controller
    {
        // GET: Movimiento
        public ActionResult Index()
        {
            
            IserviceProducto producto = new ServiceProducto();
            IServiceSeccion serviceSeccion = new ServiceSeccion();
            try
            {
               
                TempData["productos"] = producto.GetProducto();
                Usuario u =(Usuario) Session["User"];
                TempData["usuario"] = u.Nombre;
                TempData["Secciones"] = serviceSeccion.GetSeccion();
            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }
            return View();
        }
        public PartialViewResult Entrada()
        {
            return PartialView();
        }
        public PartialViewResult Salida()
        {
            Salida ou = (Salida)Session["out"];
            if (ou == null)
            {
                ou = new Salida();
                Session["out"] = ou;
                TempData["detalle"]= ou.SalidaProducto;
               

            }
            ou.fecha = DateTime.Now;
            return PartialView(ou);
        }
        

       
    }
}