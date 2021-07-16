using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ContactoController : Controller
    {
        // GET: Contacto
        public ActionResult Index()
        {
            return View();
        }





        public PartialViewResult _ListaContacto()
        {
            IEnumerable<Contacto> lista = null;
           ServiceContacto service = new ServiceContacto();


            try
            {

                lista = service.GetContactos().ToList();




            }
            catch (Exception e)
            {
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod());
            }
            ViewBag.listaContacto = service.GetContactos();
            return PartialView("_ListaContacto", lista);
        }




    }
}