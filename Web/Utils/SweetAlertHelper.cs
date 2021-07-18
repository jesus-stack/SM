using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Web.Utils
{
    public class SweetAlertHelper
    {
        public static string Mensaje(string titulo, string mensaje, SweetAlertMessageType
       tipoMensaje)
        {
            return "swal({title: '" + titulo + "',text: '" + mensaje + "',type: '" + tipoMensaje + "'});";
        }
        public static string extra(string titulo, string mensaje, SweetAlertMessageType
      tipoMensaje,String extra)
        {
            return "swal({title: '" + titulo + "',text: '" + mensaje + "',type: '" + tipoMensaje + "'"+extra+"});";
        }
    }
    public enum SweetAlertMessageType
    {
        warning, error, success, info
    }
}