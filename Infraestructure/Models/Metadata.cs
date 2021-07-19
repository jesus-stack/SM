
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    internal partial class ProductoMetadata{
        [Display(Name ="Código")]
        public long Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50,ErrorMessage ="El {0} no debe de sobrepasar 50 caraceteres")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Nombre { get; set; }
        [Display(Name = "Descripción")]
        [MaxLength(70, ErrorMessage = "El {0} no debe de sobrepasar 70 caraceteres")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Descripcion { get; set; }
        [Display(Name ="Cantidad Mínima")]
        
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public Nullable<int> cantidad_minima { get; set; }
        [Display(Name = "Cantidad Máxima")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
       
        public Nullable<long> cantidad_maxima { get; set; }
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public Nullable<decimal> Costo { get; set; }
        [Display(Name ="Total Existencias")]
        public Nullable<long> Total { get; set; }
        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "Debe seleccionar una categoria")]
        public Nullable<int> Categoria { get; set; }
        [DataType(DataType.ImageUrl,ErrorMessage ="solo se pueden seleccionar imagenes")]
        public byte[] imagen { get; set; }
        public Nullable<bool> Estado { get; set; }

        [Display(Name = "Categoría")]
        public virtual Categoria Categoria1 { get; set; }
        
        public virtual ICollection<EntradaProducto> EntradaProducto { get; set; }
        
        public virtual ICollection<ProductoSeccion> ProductoSeccion { get; set; }
      
        public virtual ICollection<SalidaProducto> SalidaProducto { get; set; }
      
        public virtual ICollection<Proveedor> Proveedor { get; set; }
    }
    internal partial class ProveedorMetadata
    {
        [Display(Name = "Identificador")]
        public int Id { get; set; }
        [Display(Name = "Nombre Organización")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string NombreOrganizacion { get; set; }
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Pais { get; set; }
        public Nullable<bool> Estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contacto> Contacto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Producto> Producto { get; set; }
      
      
    }

   internal partial class EntradaMetadata
    {
        
        [Display(Name ="N° Entrada")]
        public long Id { get; set; }
        public Nullable<int> IdMovimiento { get; set; }
        [Required(ErrorMessage = "{0} es un dato requerrido")]
        public string Comentario { get; set; }
        public Nullable<long> IdUsuario { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        [Display(Name ="Movimiento")]
        public virtual Movimiento Movimiento { get; set; }
        public virtual Usuario Usuario { get; set; }
       
        public virtual ICollection<EntradaProducto> EntradaProducto { get; set; }
    }
    internal partial class SalidaMetadata{

        [Display(Name = "N° Salida")]
        public long Id { get; set; }
        [Display(Name ="Tipo Movimiento")]
        [Required(ErrorMessage ="{0} es un dato requerrido")]
        public Nullable<int> IdMovimiento { get; set; }
        [Required(ErrorMessage = "{0} es un dato requerrido")]
        public string comentario { get; set; }
        public Nullable<long> IdUsuario { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        [Required(ErrorMessage = "{0} es un dato requerrido")]
        [Display(Name = "Movimiento")]
        public virtual Movimiento Movimiento { get; set; }
        public virtual Usuario Usuario { get; set; }
       
        public virtual ICollection<SalidaProducto> SalidaProducto { get; set; }
    }
    internal partial class ProductoSeccionMetadata
    {
        public int IdSeccion { get; set; }
        public long IdProducto { get; set; }
        public long Lote { get; set; }
        [Required(ErrorMessage ="{0} es un dato requerido")]
        public Nullable<long> Cantidad { get; set; }
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Display(Name ="Fecha de vencimiento")]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        [DataType(DataType.Date, ErrorMessage ="Formato de fecha no valido")]
        public Nullable<System.DateTime> FechaVencimiento { get; set; }

        public virtual Producto Producto { get; set; }
        public virtual Seccion Seccion { get; set; }

       
    }
    public partial class UsuarioMetadata
    {    [Display(Name ="Codigo de Usuario")]
    [Required(ErrorMessage ="{0} Es un dato requerido")]
        public long Id { get; set; }
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "{0} Es un dato requerido")]
        public string contrasena { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> TipoUsuario { get; set; }
        public Nullable<bool> Estado { get; set; }
        public byte[] Imagen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Entrada> Entrada { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Salida> Salida { get; set; }
        public virtual TipoUsuario TipoUsuario1 { get; set; }
    }
}
