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
            return View();
        }
        public PartialViewResult Entrada()
        {
            return PartialView();
        }
        public PartialViewResult Salida()
        {
            return PartialView();
        }
    }
}