using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cSucursales))]
    public partial class tbSucursales
    {

    }
    public class cSucursales
    {

        [Display(Name = "Número")]
        public int suc_Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\"es requerido")]
        [Display(Name = "Empresa ")]
        public int empr_Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\"es requerido")]
        [MaxLength(4, ErrorMessage = "Excedió el número máximo de carácteres.")]
        [Display(Name = "Código Municipio ")]
        public string mun_Codigo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\"es requerido")]
        [Display(Name = "Bodega ")]
        public int bod_Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\"es requerido")]
        [Display(Name = "pemi_Id ")]
        public int pemi_Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\"es requerido")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de carácteres.")]
        [Display(Name = "Descripción ")]
        public string suc_Descripcion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\"es requerido")]
        [MaxLength(50, ErrorMessage = "Excedió el número máximo de carácteres.")]
        [Display(Name = "Correo Electrónico ")]
        public string suc_Correo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\"es requerido")]
        [MaxLength(100, ErrorMessage = "Excedió el número máximo de carácteres.")]
        [Display(Name = "Dirección ")]
        public string suc_Direccion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\"es requerido")]
        [MaxLength(9, ErrorMessage = "Excedió el número máximo de carácteres.")]
        [Display(Name = "Teléfono ")]
        public string suc_Telefono { get; set; }

        [Display(Name = "Estado ")]
        public bool suc_Estado { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo \"{0}\"es requerido")]
        [MaxLength(100, ErrorMessage = "Excedió el número máximo de carácteres.")]
        [Display(Name = "Razón Inactivo")]
        public string suc_RazonInactivo { get; set; }

        [Display(Name = "Usuario Crea")]
        public int suc_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public System.DateTime suc_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> suc_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> suc_FechaModifica { get; set; }
 
    }
}