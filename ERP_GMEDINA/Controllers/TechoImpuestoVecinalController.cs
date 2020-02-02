using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_GMEDINA.Models;

namespace ERP_GMEDINA.Controllers
{
    public class TechoImpuestoVecinalController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();

        #region GET: AcumuladosISR
        public ActionResult Index()
        {
            return View(db.tbTechoImpuestoVecinal.ToList());
        }
        #endregion
    }
}
