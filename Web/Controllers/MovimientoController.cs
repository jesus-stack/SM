using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Security;
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
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.encargado)]
        public ActionResult Index()
        {
            
            IserviceProducto producto = new ServiceProducto();
            IServiceSeccion serviceSeccion = new ServiceSeccion();
            try
            {
               
                TempData["productos"] = producto.GetProducto();
                Usuario u =(Usuario) Session["User"];
                if (u != null)
                {
                    TempData["usuario"] = u.Nombre;
                }
                else{
                    TempData["usuario"] = "";
                }
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
        [CustomAuthorize((int)Roles.Administrador)]
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

        public  ActionResult Save(Salida salida)
        {
           
            ICollection < SalidaProducto > list= (ICollection<SalidaProducto>) TempData["detalle"];
            try
            {
                if (ModelState.IsValid)
                {
                    Usuario u = (Usuario)Session["User"];
                    salida.IdUsuario = u.Id;
                    salida.SalidaProducto = list;
                    Session["out"] = null;

                    ServiceSalida service = new ServiceSalida();

                    service.Save(salida);

                    Salida ou = (Salida)Session["out"];
                    ViewBag.NotificationMessage = Utils.SweetAlertHelper.extra("Salida", "Registrada Exitosamente", SweetAlertMessageType.success, ",showConfirmButton: false,timer: 1500");


                    return View("~/Views/Movimiento/Index.cshtml");
                }
                else
                {
                    ViewBag.movimientos = ListaSalidas();
                   
                    Salida ou = (Salida)Session["out"];

                    return View("Salida",ou);

                }
            }
            catch(Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
                ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Salida", "ERROR! al registrar", SweetAlertMessageType.error);


                return View("~/Views/Home/Index.cshtml");

            }
        }
        public ActionResult SaveEntrada()
        {
            Session["in"] = null;
           

            ViewBag.NotificationMessage = Utils.SweetAlertHelper.extra("Entrada", "Registrada Exitosamente", SweetAlertMessageType.success, ",showConfirmButton: false,timer: 1500");

            return View("~/Views/Movimiento/Index.cshtml");
        }


        public PartialViewResult saveSalidaProducto(int cantidad,int Seccion, long producto)
        {
            try
            {

                List<SalidaProducto> salidas = (List<SalidaProducto>)TempData["detalle"];
                ServiceProducto iservice = new ServiceProducto();
                long cant =(long) iservice.GetProductoBySeccion(producto, Seccion).Sum(x=>x.Cantidad);
                if (cant>=cantidad)
                {
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
                else
                {
                    ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Productos Insuficientes!", "cantidad actual en Seccion : "+cant, SweetAlertMessageType.error);
                    return PartialView("_producto");
                }
            }catch(Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
                ViewBag.NotificationMessage = Utils.SweetAlertHelper.extra("Salida", "ERROR!", SweetAlertMessageType.success, ",showConfirmButton: false,timer: 1500");
                return PartialView("_producto");
            }
        }

        public PartialViewResult eliminarpro(int index)
        {
            List<SalidaProducto> salidas = (List<SalidaProducto>)TempData["detalle"];
            salidas.RemoveAt(index);
            TempData["detalle"] = salidas;
            ViewBag.NotificationMessage = Utils.SweetAlertHelper.extra("Salida", "Registro Eliminado Exitosamente", SweetAlertMessageType.success, ",showConfirmButton: false,timer: 1500");
            return PartialView("_producto");
        }
        public PartialViewResult eliminarproentrada(int index)
        {
            List<EntradaProducto> entradas = (List<EntradaProducto>)TempData["detall"];
            entradas.RemoveAt(index);
            TempData["detall"] = entradas;
            ViewBag.NotificationMessage = Utils.SweetAlertHelper.extra("Entrada", "Registro Eliminado Exitosamente", SweetAlertMessageType.success, ",showConfirmButton: false,timer: 1500");
            return PartialView("_ProductoEntrada");
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
                EntradaProducto.Proveedor = isProveedor.GetProveedorById(Proveedor);
                EntradaProducto.FechaVencimiento = Convert.ToDateTime(fechaV.ToString("dd/MM/yyyy"));

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
                    EntradaProducto.Proveedor = isProveedor.GetProveedorById(Proveedor);
                    EntradaProducto.FechaVencimiento = Convert.ToDateTime(fechaV.ToString("dd/MM/yyyy"));

                    entradas.Add(EntradaProducto);
                }
                else
                {
                    if (EntradaProducto.IdProveedor != Proveedor)
                    {
                        EntradaProducto = new EntradaProducto();
                        EntradaProducto.cantidad = cantidad;
                        EntradaProducto.idSeccion = Seccion;
                        EntradaProducto.Seccion = seccion.GetSeccion().FirstOrDefault(x => x.Id == Seccion);
                        EntradaProducto.IdProducto = producto;
                        EntradaProducto.Producto = iservice.GetProductoById(producto);
                        EntradaProducto.IdProveedor = Proveedor;
                        EntradaProducto.Proveedor = isProveedor.GetProveedorById(Proveedor);
                        EntradaProducto.FechaVencimiento = Convert.ToDateTime(fechaV.ToString("dd/MM/yyyy"));

                        entradas.Add(EntradaProducto);


                    }
                    else { 

                    int c = entradas.IndexOf(EntradaProducto);
                    EntradaProducto = new EntradaProducto();
                    EntradaProducto.cantidad = cantidad;
                    EntradaProducto.idSeccion = Seccion;
                    EntradaProducto.Seccion = seccion.GetSeccion().FirstOrDefault(x => x.Id == Seccion);
                    EntradaProducto.IdProducto = producto;
                    EntradaProducto.Producto = iservice.GetProductoById(producto);
                    EntradaProducto.IdProveedor = Proveedor;
                        EntradaProducto.Proveedor = isProveedor.GetProveedorById(Proveedor);
                        EntradaProducto.FechaVencimiento = Convert.ToDateTime(fechaV.ToString("dd/MM/yyyy"));
                        entradas[c] = EntradaProducto;
                }
                }

            }
            ViewBag.NotificationMessage = Utils.SweetAlertHelper.extra("Entrada", "Registro Agregado Exitosamente", SweetAlertMessageType.success, ",showConfirmButton: false,timer: 1500");
            return PartialView("_productoEntrada");
        }







    }
}