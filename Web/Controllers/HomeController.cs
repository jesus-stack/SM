using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<Producto> lista = null;
            List<Entrada> listaE = null;
            ServiceProducto service = new ServiceProducto();
            ServiceEntrada serviceE = new ServiceEntrada();



            try
            {

                lista = service.GetProducto();
                listaE = serviceE.GetEntrada().ToList();
                

            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }
            TempData["Productos"] = lista;
            TempData["Entradas"] = listaE;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Login()
        {
            ViewBag.Message = "Bienvenido a login.";

            return View();
        }
        public ActionResult Mantenimiento()
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
            return View(lista);
        }



    }
}