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
        public PartialViewResult listarTabla(int? id,String sub)
        {
            IEnumerable<Producto> lista = null;
            ServiceProducto service = new ServiceProducto();
         

            try
            {
                if (id != null)
                {
                    lista = service.GetProductoByCategoria((int)id);
                    ViewBag.Subtitle = sub;
                }
                else
                {
                    lista = service.GetProducto();      
                   
                }
                

            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }
          
            return PartialView("_PartialTablaP",lista);
        }
        public  PartialViewResult _PartialModalProducto(int id)
        {
            Producto pro = null;
            ServiceProducto service = new ServiceProducto();


            try
            {
              
                    pro = service.GetProductoById(id);
                    
             


            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }

            return PartialView("_PartialModalProducto",pro);
        }
        private SelectList listaCategorias(int idCategoria = 0)
        {
            IServiceCategoria serviceCategoria = new ServiceCategoria();
            IEnumerable<Categoria> categorias = serviceCategoria.GetCategoria();
            return new SelectList(categorias, "Id", "Descripcion", idCategoria);
        }
        public ActionResult Crear()
        {
            ViewBag.ListaCategorias = listaCategorias();
            
            return View();
        }
        [HttpPost]
        public ActionResult save(Producto producto)
        {
            try
            {
                ServiceProducto service = new ServiceProducto();
                service.Save(producto);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Crear");
            }
        }

        public ActionResult Editar(int? id)
        {
            Producto pro = null;
            ServiceProducto service = new ServiceProducto();


            try
            {
                if (id != null)
                {
                    pro = service.GetProductoById((int)id);
                   
                }
                else
                {
                    pro = new Producto();

                }


            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }
            return View();
        }
    }

}