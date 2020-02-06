using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
	public class VM_ModelState
	{
		public object ListaPantallas { get; set; }
		public int CantidadRoles { get; set; }
		//VALIDAR SI LA SESION NO ESTA EXPIRADA
		public bool SesionIniciada { get; set; }
		public bool ContraseniaExpirada { get; set; }
		public bool EsAdmin { get; set; }
	}
}