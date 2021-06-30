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
            IEnumerable<Categoria> listaCategorias = null;
            ServiceCategoria serviceCategoria = new ServiceCategoria();


            try
            {
                lista = service.GetProducto();
                listaCategorias = serviceCategoria.GetCategoria();
              
            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }
            ViewBag.categorias = listaCategorias;
            return View(lista);
        }
        public PartialViewResult listarTabla()
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
          
            return PartialView("_PartialTablaP",lista);
        }
    }
}