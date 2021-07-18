using ApplicationCore.Services;
using Infraestructure.Models;
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
            
            IserviceProducto producto = new ServiceProducto();
            IServiceSeccion serviceSeccion = new ServiceSeccion();
            try
            {
               
                TempData["productos"] = producto.GetProducto();
                Usuario u =(Usuario) Session["User"];
                TempData["usuario"] = u.Nombre;
                TempData["Secciones"] = serviceSeccion.GetSeccion();
            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }
            return View();
        }
        public PartialViewResult Entrada()
        {
            return PartialView();
        }
        public PartialViewResult Salida()
        {
            Salida ou = (Salida)Session["out"];
            if (ou == null)
            {
                ou = new Salida();
                Session["out"] = ou;
                TempData["detalle"]= ou.SalidaProducto.ToList();
               

            }
            ou.fecha = DateTime.Now;
            return PartialView(ou);
        }
        
        public PartialViewResult saveSalidaProducto(int cantidad,int Seccion, long producto)
        {
            List<SalidaProducto> salidas = (List<SalidaProducto>)TempData["detalle"];
            IserviceProducto iservice = new ServiceProducto();
            IServiceSeccion seccion = new ServiceSeccion();
            SalidaProducto salidaProducto = salidas.FirstOrDefault(x => x.IdProducto == producto);
            if (salidaProducto == null)
            {
              
                 salidaProducto = new SalidaProducto();
                salidaProducto.Cantidad = cantidad;
                salidaProducto.IdSeccion = Seccion;
                salidaProducto.Seccion = seccion.GetSeccion().FirstOrDefault(x => x.Id == Seccion);
                salidaProducto.IdProducto = producto;
                salidaProducto.Producto = iservice.GetProductoById(producto);

                salidas.Add(salidaProducto);
            }
            else
            {
                if (salidaProducto.IdSeccion != Seccion)
                {
                    salidaProducto = new SalidaProducto();
                    salidaProducto.Cantidad = cantidad;
                    salidaProducto.IdSeccion = Seccion;
                    salidaProducto.Seccion = seccion.GetSeccion().FirstOrDefault(x => x.Id == Seccion);
                    salidaProducto.IdProducto = producto;
                    salidaProducto.Producto = iservice.GetProductoById(producto);

                    salidas.Add(salidaProducto);
                }
                else
                {
                    int c = salidas.IndexOf(salidaProducto);
                  salidaProducto = new SalidaProducto();
                    salidaProducto.Cantidad = cantidad;
                    salidaProducto.IdSeccion = Seccion;
                    salidaProducto.Seccion = seccion.GetSeccion().FirstOrDefault(x => x.Id == Seccion);
                    salidaProducto.IdProducto = producto;
                    salidaProducto.Producto = iservice.GetProductoById(producto);
                    salidas[c] = salidaProducto;
                }
              
            }

            return PartialView("_producto");
        }
        public PartialViewResult eliminarpro(int index)
        {
            List<SalidaProducto> salidas = (List<SalidaProducto>)TempData["detalle"];
            salidas.RemoveAt(index);
            TempData["detalle"] = salidas;
            return PartialView("_producto");
        }
        public PartialViewResult listarNombre(string filtro)
        {
            IEnumerable<Producto> lista = null;
            ServiceProducto service = new ServiceProducto();


            try
            {
                if (String.IsNullOrEmpty(filtro))
                {
                    lista = service.GetProducto();
                }
                else
                {
                    lista = service.GetProductoBy(filtro);
                }
                TempData["productos"] =lista;
                return PartialView("_listaproducto", lista);

            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }
            TempData["productos"] = lista;
            return PartialView("_listaproducto", lista);
        }



    }
}