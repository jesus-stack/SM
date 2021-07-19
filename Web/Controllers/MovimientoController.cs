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
        private SelectList ListaSalidas(int movimiento = 0)
        {
            IServiceMovimiento service = new ServiceMovimiento();
            IEnumerable<Movimiento> movimientos = service.GetSalidas();
            return new SelectList(movimientos, "Id", "Nombre", movimiento);
        }

        private SelectList ListaEntrada(int movimiento = 0)
        {
            IServiceMovimiento service = new ServiceMovimiento();
            IEnumerable<Movimiento> movimientos = service.GetEntradas();
            return new SelectList(movimientos, "Id", "Nombre", movimiento);
        }
        private SelectList ListaProveedor()
        {
            IServiceProveedor service = new ServiceProveedor();
            IEnumerable<Proveedor> movimientos = service.GetProveedor(); ;
            return new SelectList(movimientos, "Id", "Nombre");
        }

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
                ViewBag.movimientos = ListaSalidas();
                ViewBag.MEntradas = ListaEntrada();
                ViewBag.Proveedores = ListaProveedor();
            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }
            return View();
        }
        public PartialViewResult Entrada()
        {
            ViewBag.MEntradas = ListaEntrada();

            Entrada ou = (Entrada)Session["in"];
            if (ou == null)
            {
                ou = new Entrada();
                Session["in"] = ou;
                TempData["detall"] = ou.EntradaProducto.ToList();


            }
            ou.fecha = DateTime.Now;
            return PartialView(ou);
        }
        public PartialViewResult Salida()
        {
            ViewBag.movimientos = ListaSalidas();
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

            
            return View("~/Views/Home/Index.cshtml");
        }
        public ActionResult SaveEntrada()
        {
            Session["in"] = null;
            TempData["productos"] = new List<Producto>();

            ViewBag.NotificationMessage = Utils.SweetAlertHelper.extra("Entrada", "Registrada Exitosamente", SweetAlertMessageType.success, ",showConfirmButton: false,timer: 1500");


            return View("~/Views/Home/Index.cshtml");
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





        public PartialViewResult saveEntradaProducto(int cantidad, int Seccion, int Proveedor, long producto, DateTime fechaV)
        {
            List<EntradaProducto> entradas = (List<EntradaProducto>)TempData["detall"];
            IserviceProducto iservice = new ServiceProducto();
            IServiceSeccion seccion = new ServiceSeccion();
            IServiceProveedor isProveedor = new ServiceProveedor();
            EntradaProducto EntradaProducto = entradas.FirstOrDefault(x => x.IdProducto == producto);
            if (EntradaProducto == null)
            {

                EntradaProducto = new EntradaProducto();
                EntradaProducto.cantidad = cantidad;
                EntradaProducto.idSeccion = Seccion;
                EntradaProducto.Seccion = seccion.GetSeccion().FirstOrDefault(x => x.Id == Seccion);
                EntradaProducto.IdProducto = producto;
                EntradaProducto.Producto = iservice.GetProductoById(producto);
                EntradaProducto.IdProveedor = Proveedor;
                EntradaProducto.Proveedor = isProveedor.GetProveedor().FirstOrDefault(x => x.Id == Proveedor);
                EntradaProducto.FechaVencimiento = fechaV;

                entradas.Add(EntradaProducto);
            }
            else
            {
                if (EntradaProducto.idSeccion != Seccion)
                {
                    EntradaProducto = new EntradaProducto();
                    EntradaProducto.cantidad = cantidad;
                    EntradaProducto.idSeccion = Seccion;
                    EntradaProducto.Seccion = seccion.GetSeccion().FirstOrDefault(x => x.Id == Seccion);
                    EntradaProducto.IdProducto = producto;
                    EntradaProducto.Producto = iservice.GetProductoById(producto);
                    EntradaProducto.IdProveedor = Proveedor;
                    EntradaProducto.Proveedor = isProveedor.GetProveedor().FirstOrDefault(x => x.Id == Proveedor);
                    EntradaProducto.FechaVencimiento = fechaV;

                    entradas.Add(EntradaProducto);
                }
                else
                {
                    int c = entradas.IndexOf(EntradaProducto);
                    EntradaProducto = new EntradaProducto();
                    EntradaProducto.cantidad = cantidad;
                    EntradaProducto.idSeccion = Seccion;
                    EntradaProducto.Seccion = seccion.GetSeccion().FirstOrDefault(x => x.Id == Seccion);
                    EntradaProducto.IdProducto = producto;
                    EntradaProducto.Producto = iservice.GetProductoById(producto);
                    EntradaProducto.IdProveedor = Proveedor;
                    EntradaProducto.Proveedor = isProveedor.GetProveedor().FirstOrDefault(x => x.Id == Proveedor);
                    EntradaProducto.FechaVencimiento = fechaV;
                    entradas[c] = EntradaProducto;
                }

            }
            ViewBag.NotificationMessage = Utils.SweetAlertHelper.extra("Entrada", "Registro Agregado Exitosamente", SweetAlertMessageType.success, ",showConfirmButton: false,timer: 1500");
            return PartialView("_productoEntrada");
        }







    }
}