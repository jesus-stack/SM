using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}