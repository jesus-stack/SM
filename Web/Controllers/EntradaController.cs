using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class EntradaController : Controller
    {
        // GET: Entrada
        public ActionResult Index()
        {
            IEnumerable<Entrada> lista = null;
            ServiceEntrada service = new ServiceEntrada();
            try
            {
                lista = service.GetEntrada();
            }
            catch(Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }
            return View(lista);
        }
    }
}