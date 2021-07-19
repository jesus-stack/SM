using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Utils;

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
        public  ActionResult Save()
        {
            Session["out"] =null;
            TempData["productos"] = new List<Producto>();
            
            ViewBag.NotificationMessage = Utils.SweetAlertHelper.extra("Salida", "Registrada Exitosamente", SweetAlertMessageType.success, ",showConfirmButton: false,timer: 1500");
            return View("Index");
        }

        public PartialViewResult saveSalidaProducto(int cantidad,int Seccion, long producto)
        {
            List<SalidaProducto> salidas = (List<SalidaProducto>)TempData["detalle"];
            IserviceProducto iservice = new ServiceProducto();
            IServiceSeccion seccion = new ServiceSeccion();
            SalidaProducto SalidaProducto = salidas.FirstOrDefault(x => x.IdProducto == producto);
            if (SalidaProducto == null)
            {
              
                 SalidaProducto = new SalidaProducto();
                SalidaProducto.Cantidad = cantidad;
                SalidaProducto.IdSeccion = Seccion;
                SalidaProducto.Seccion = seccion.GetSeccion().FirstOrDefault(x => x.Id == Seccion);
                SalidaProducto.IdProducto = producto;
                SalidaProducto.Producto = iservice.GetProductoById(producto);

                salidas.Add(SalidaProducto);
            }
            else
            {
                if (SalidaProducto.IdSeccion != Seccion)
                {
                    SalidaProducto = new SalidaProducto();
                    SalidaProducto.Cantidad = cantidad;
                    SalidaProducto.IdSeccion = Seccion;
                    SalidaProducto.Seccion = seccion.GetSeccion().FirstOrDefault(x => x.Id == Seccion);
                    SalidaProducto.IdProducto = producto;
                    SalidaProducto.Producto = iservice.GetProductoById(producto);

                    salidas.Add(SalidaProducto);
                }
                else
                {
                    int c = salidas.IndexOf(SalidaProducto);
                  SalidaProducto = new SalidaProducto();
                    SalidaProducto.Cantidad = cantidad;
                    SalidaProducto.IdSeccion = Seccion;
                    SalidaProducto.Seccion = seccion.GetSeccion().FirstOrDefault(x => x.Id == Seccion);
                    SalidaProducto.IdProducto = producto;
                    SalidaProducto.Producto = iservice.GetProductoById(producto);
                    salidas[c] = SalidaProducto;
                }
              
            }
            ViewBag.NotificationMessage = Utils.SweetAlertHelper.extra("Salida", "Registro Agregado Exitosamente", SweetAlertMessageType.success, ",showConfirmButton: false,timer: 1500");
            return PartialView("_producto");
        }
        public PartialViewResult eliminarpro(int index)
        {
            List<SalidaProducto> salidas = (List<SalidaProducto>)TempData["detalle"];
            salidas.RemoveAt(index);
            TempData["detalle"] = salidas;
            ViewBag.NotificationMessage = Utils.SweetAlertHelper.extra("Salida", "Registro Eliminado Exitosamente", SweetAlertMessageType.success, ",showConfirmButton: false,timer: 1500");
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