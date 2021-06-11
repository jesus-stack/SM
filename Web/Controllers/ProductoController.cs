using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            IEnumerable<Producto> lista = null;
            ServiceProducto service = new ServiceProducto();
            try
            {
                lista = service.GetProducto();
            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }

            return View(lista);
        }
        public ActionResult Ir()
        {
            IEnumerable<Producto> lista = null;
            ServiceProducto service= new ServiceProducto();
            try
            {
                lista = service.GetProducto();
            }
            catch(Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }

            return View(lista);
        }
    }
}