using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    internal partial class ProductoMetadata{
        [Display(Name ="Codigo Producto")]
        public long Id { get; set; }
        [Display(Name = "Nombre Producto")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        [Display(Name ="Cantidad Minima")]
        public Nullable<int> cantidad_minima { get; set; }
        [Display(Name = "Cantidad Maxima")]
        public Nullable<long> cantidad_maxima { get; set; }
        public Nullable<decimal> Costo { get; set; }
        [Display(Name ="Total Existencias")]
        public Nullable<long> Total { get; set; }
        public Nullable<int> Categoria { get; set; }
        public byte[] imagen { get; set; }
        public byte[] n { get; set; }

        public virtual Categoria Categoria1 { get; set; }
        
        public virtual ICollection<EntradaProducto> EntradaProducto { get; set; }
        
        public virtual ICollection<ProductoSeccion> ProductoSeccion { get; set; }
      
        public virtual ICollection<SalidaProducto> SalidaProducto { get; set; }
      
        public virtual ICollection<Proveedor> Proveedor { get; set; }
    }
    internal partial class ProveedorMetadata
    {
        [Display(Name ="Identificador")]
        public int Id { get; set; }
        [Display(Name = "Nombre Organización")]
        public string NombreOrganizacion { get; set; }
        public string Direccion { get; set; }
        public string Pais { get; set; }

        
        public virtual ICollection<Contacto> Contacto { get; set; }
       
        public virtual ICollection<Producto> Producto { get; set; }
    }
}
