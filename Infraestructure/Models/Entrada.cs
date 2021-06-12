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
    
    public partial class Entrada
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Entrada()
        {
            this.EntradaProducto = new HashSet<EntradaProducto>();
        }
    
        public decimal Id { get; set; }
        public Nullable<int> IdMovimiento { get; set; }
        public string Comentario { get; set; }
        public Nullable<long> IdUsuario { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
    
        public virtual Movimiento Movimiento { get; set; }
        public virtual Usuario Usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EntradaProducto> EntradaProducto { get; set; }
    }
}
