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
            List<Producto> listaSolicitar = null;
            List<Entrada> listaE = null;
            List<Salida> listaS = null;

            ServiceProducto service = new ServiceProducto();
            ServiceEntrada serviceE = new ServiceEntrada();
            ServiceSalida serviceS = new ServiceSalida();




            try
            {

                lista = service.GetProducto();
                listaSolicitar = service.GetProducto().ToList();
                listaSolicitar = listaSolicitar.Where(x => x.Total < x.cantidad_minima||x.Total==null).ToList();
                listaE = serviceE.GetEntrada().ToList();
                listaE = listaE.Where(x =>x.fecha.Value.Day==DateTime.Now.Day&& x.fecha.Value.Month == DateTime.Now.Month&& x.fecha.Value.Year == DateTime.Now.Year).ToList();
                listaS = serviceS.GetSalida().ToList();
                listaS = listaS.Where(x => x.fecha.Value.Day == DateTime.Now.Day && x.fecha.Value.Month == DateTime.Now.Month && x.fecha.Value.Year == DateTime.Now.Year).ToList();

            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }
            TempData["Productos"] = lista;
            TempData["ProductosS"] = listaSolicitar;
            TempData["Entradas"] = listaE;
            TempData["Salidas"] = listaS;

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