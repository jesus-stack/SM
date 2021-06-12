using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class SalidaController : Controller
    {
        // GET: Salida
        public ActionResult Index()
        {
            IEnumerable<Salida> lista = null;
            ServiceSalida service = new ServiceSalida();
            try
            {
                lista = service.GetSalida();
            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }
            return View(lista);
        }
    }
}