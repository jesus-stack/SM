//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infraestructure.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ProductoSeccionMetadata ))]
    public partial class ProductoSeccion
    {
        public int IdSeccion { get; set; }
        public long IdProducto { get; set; }
        public long Lote { get; set; }
        public Nullable<long> Cantidad { get; set; }
        public Nullable<System.DateTime> FechaVencimiento { get; set; }
    
        public virtual Producto Producto { get; set; }
        public virtual Seccion Seccion { get; set; }
    }
}
