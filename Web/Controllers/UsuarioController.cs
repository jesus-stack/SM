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
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            IEnumerable<Usuario> lista = null;
            ServiceUsuario service = new ServiceUsuario();
           
           


            try
            {
                lista = service.GetUsuarios();
               

            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }
          
            return View(lista);
        }
        public ActionResult Desactivar(long id)
        {
            IEnumerable<Usuario> lista = null;
            ServiceUsuario service = new ServiceUsuario();




            try
            {
                service.desactivar(id);
                lista = service.GetUsuarios();
               


            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }

            return View("Index",lista) ;
        }
        public ActionResult Activar(long id)
        {
            IEnumerable<Usuario> lista = null;
            ServiceUsuario service = new ServiceUsuario();




            try
            {
                service.activar(id);
                lista = service.GetUsuarios();



            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }

            return View("Index", lista);
        }
        private SelectList ListaTipoUsuarios(int tipoUsuario = 0)
        {
            IServiceTipoUsuario serviceTipoUsuario = new ServiceTipoUsuario();
            IEnumerable<TipoUsuario> tipoUsuarios = serviceTipoUsuario.GetTipoUsuarios();
            return new SelectList(tipoUsuarios, "Id", "Descripcion", tipoUsuario);
           
        }
        private SelectList ListaTipoUsuariosInferior(int tipoUsuario = 0)
        {
            IServiceTipoUsuario serviceTipoUsuario = new ServiceTipoUsuario();
            List<TipoUsuario> tipoUsuarios = serviceTipoUsuario.GetTipoUsuarios().ToList();
            tipoUsuarios.Remove(tipoUsuarios.FirstOrDefault(x => x.Id == 1));
            return new SelectList(tipoUsuarios, "Id", "Descripcion", tipoUsuario);

        }
        public ActionResult Crear()
        {

          
            ServiceUsuario service = new ServiceUsuario();




            try
            {
                ViewBag.ListaTipoUsuarios = ListaTipoUsuarios();


            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }

            return View(new Usuario());
        }
        public ActionResult CrearInferiorAdministrador()
        {


            ServiceUsuario service = new ServiceUsuario();




            try
            {
              
               
                ViewBag.ListaTipoUsuarios = ListaTipoUsuariosInferior();


            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }

            return View(new Usuario());
        }
        public ActionResult save(Usuario usuario)
        {
            IServiceUsuario service = new ServiceUsuario();
         

            try
            {
             



                if (ModelState.IsValid)
                {
                    Usuario user = service.GetUsuarioByID(usuario.Id);
                    if (user != null)
                    {
                        ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("!Error", "Usuario con este ID ya existe", SweetAlertMessageType.warning);
                        ViewBag.ListaTipoUsuarios = ListaTipoUsuarios();
                        return View("Crear");
                    }
                    usuario.Estado = true;
                    service.Save(usuario);



                    return RedirectToAction("Index");
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);
                    ViewBag.ListaTipoUsuarios = ListaTipoUsuarios();


                    return View(usuario);
                }

            }
            catch
            {
                return RedirectToAction("Crear");
            }

        }
        public ActionResult saveI(Usuario usuario)
        {
            IServiceUsuario service = new ServiceUsuario();


            try
            {




                if (ModelState.IsValid)
                {
                    Usuario user = service.GetUsuarioByID(usuario.Id);
                    if (user != null)
                    {
                        ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("!Error", "Usuario con este ID ya existe", SweetAlertMessageType.warning);
                        ViewBag.ListaTipoUsuarios = ListaTipoUsuariosInferior();
                        return View("CrearInferiorAdministrador");
                    }
                    usuario.Estado = true;
                    service.Save(usuario);



                    return RedirectToAction("Index","Login");
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);
                    ViewBag.ListaTipoUsuarios = ListaTipoUsuarios();


                    return View(usuario);
                }

            }
            catch
            {
                return RedirectToAction("CrearInferiorAdministrador");
            }

        }

    }
}