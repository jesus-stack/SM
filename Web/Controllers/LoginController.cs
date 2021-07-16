using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Utils;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(Usuario usuario)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario oUsuario = null;
            try
            {
                if (ModelState.IsValid)
                {
                    oUsuario = _ServiceUsuario.GetUsuario(usuario.Id, usuario.contrasena);

                    if (oUsuario != null)
                    {
                        Session["User"] = oUsuario;
                        Log.Info($"Accede {oUsuario.Nombre}  con el rol {oUsuario.TipoUsuario}-{oUsuario.TipoUsuario1.Descripcion}");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        Log.Warn($"{usuario.Id} se intentó conectar  y falló");
                        ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Login", "Error al autenticarse", SweetAlertMessageType.warning);

                    }
                }

                return View("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                //Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData.Keep();
                ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Información", "Error al conectar con el servidor", SweetAlertMessageType.warning);
                return View();
            }
        }
        public ActionResult NoAutorizado()
        {
            try
            {
                ViewBag.Message = "No autorizado";

                if (Session["User"] != null)
                {
                    Usuario oUsuario = Session["User"] as Usuario;
                    Log.Warn($"El usuario {oUsuario.Nombre} con el rol {oUsuario.TipoUsuario}-{oUsuario.TipoUsuario1.Descripcion}, intentó acceder una página sin permisos  ");
                }

                ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Login", "No Autorizado", SweetAlertMessageType.error);
                return View("~/Views/Home/Index.cshtml");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData["Redirect"] = "Login";
                TempData["Redirect-Action"] = "Index";
                ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Información", "Error", SweetAlertMessageType.warning);
                return View("~/Views/Home/Index.cshtml");
            }
        }
        public ActionResult Logout()
        {
            try
            {
                Log.Info("Usuario desconectado!");
                Session["User"] = null;
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData["Redirect"] = "Login";
                TempData["Redirect-Action"] = "Index";
                ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Información", "Error", SweetAlertMessageType.warning);
                return View("~/Views/Home/Index.cshtml"); return RedirectToAction("Index", "Home");
            }
        }
    }
}