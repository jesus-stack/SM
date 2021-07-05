using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ProductoSeccionController : Controller
    {
        // GET: ProductoSeccion
        public PartialViewResult Index(long id)
        {
            IEnumerable<ProductoSeccion> lista = null;
            IServiceProductoSeccion service = new ServiceProductoSeccion();
            try
            {
                lista= service.GetProductoSeccionByProducto(id);
               
             

            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }
            return PartialView(lista);

        }
        private SelectList listaSecciones(long idSeccion = 0)
        {
            IServiceSeccion serviceSeccion = new ServiceSeccion();
            IEnumerable<Seccion> secciones = serviceSeccion.GetSeccion();
            return new SelectList(secciones, "Id", "Descripcion", idSeccion);
        }

        public ActionResult create(long idproducto, long? lote)
        {
            ServiceProductoSeccion service = new ServiceProductoSeccion();
            ServiceProducto serviceProducto = new ServiceProducto();

            ProductoSeccion pro = new ProductoSeccion();
            pro.IdProducto = idproducto;
            try
            {

                if (lote != null)
                {
                    pro = service.GetProductoSeccionByLote((long)lote);
                    ViewBag.ListaSecciones = listaSecciones(pro.Lote);
                   

                   

                    ViewBag.Mantenimientotitulo = "Modificar";
                }
                else
                {
                    ViewBag.ListaSecciones = listaSecciones();
         
                    ViewBag.Mantenimientotitulo = "Crear";
                }

            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }
            return View(pro);
        }

        public ActionResult save (ProductoSeccion ps)
        {
            IServiceProductoSeccion service = new ServiceProductoSeccion();
            //try
            //{

                


                if (ModelState.IsValid)
                {

                    service.Save(ps);
                    return RedirectToAction("Crear","Producto",ps.IdProducto);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);
                    ViewBag.ListaSecciones = listaSecciones(ps.Lote);
                    return View("Create", ps);
                }

            //}
            //catch
            //{
            //    return RedirectToAction("Crear");
            //}
        }
        public ActionResult eliminar(long idproducto, long lote)
        {
            ServiceProductoSeccion service = new ServiceProductoSeccion();
            try
            {

                service.Eliminar(lote);
               

            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }
            return RedirectToAction("Crear", "Producto", idproducto);
        }

        

        
    }
}