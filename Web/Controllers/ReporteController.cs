using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ReporteController : Controller
    {
        // GET: Reporte
        public ActionResult Index()
        {
            IEnumerable<Producto> lista = null;
            ServiceProducto service = new ServiceProducto();
          


            try
            {
               
                lista = service.GetProductosMostOut();


            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }
            TempData["Productos"]= lista;
            
            return View();
        }

    }
}