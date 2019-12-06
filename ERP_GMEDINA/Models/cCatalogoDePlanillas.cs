using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    [MetadataType(typeof(cCatalogoDePlanillas))]
    public partial class tbCatalogoDePlanillas
    {
    }

    [MetadataType(typeof(cUsuario))]
    public partial class tbUsuario
    {
    }

    public class cCatalogoDePlanillas
    {
        [Display(Name = "Id Planilla")]
        public int cpla_IdPlanilla { get; set; }

        [Display(Name = "Descripción")]
        public string cpla_DescripcionPlanilla { get; set; }


        [Display(Name = "Frecuencia en días")]
        public int cpla_FrecuenciaEnDias { get; set; }


        [Display(Name = "Usuario Crea")]
        public int cpla_UsuarioCrea { get; set; }


        [Display(Name = "Fecha Crea")]
        public System.DateTime cpla_FechaCrea { get; set; }


        [Display(Name = "Usuario Modifica")]
        public Nullable<int> cpla_UsuarioModifica { get; set; }


        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> cpla_FechaModifica { get; set; }


        [Display(Name = "Es activo")]
        public bool cpla_Activo { get; set; }
    }

    public class cUsuario
    {
        [Display(Name = "Id Usuario")]
        public int usu_Id { get; set; }


        [Display(Name = "Nombre Usuario")]
        public string usu_NombreUsuario { get; set; }


        [Display(Name = "Contraseña")]
        public byte[] usu_Password { get; set; }


        [Display(Name = "Nombre")]
        public string usu_Nombres { get; set; }


        [Display(Name = "Apellido")]
        public string usu_Apellidos { get; set; }


        [Display(Name = "Correo")]
        public string usu_Correos { get; set; }


        [Display(Name = "Activo")]
        public Nullable<bool> usu_EsActivo { get; set; }


        [Display(Name = "Razon de inactividad")]
        public string usu_RazonInactivo { get; set; }


        [Display(Name = "Administrador")]
        public Nullable<bool> usu_EsAdministrador { get; set; }

    }
}