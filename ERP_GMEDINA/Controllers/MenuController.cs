﻿using ERP_GMEDINA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_GMEDINA.Controllers
{
    public class MenuController : Controller
    {
        private ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
        // GET: Menu
        public ActionResult Index(int idmenu)
        {
            if(idmenu != 0)
            {
                tbUsuario sesionUsuario = db.tbUsuario.Where(x => x.usu_Id == 1).FirstOrDefault();
                Session["sesionUsuario"] = sesionUsuario;
                Session["sesionIdMenu"] = idmenu;
                return View();
            }
            else
            {
                return RedirectToAction("MenuPrincipal");
            }
        }

        public ActionResult MenuPrincipal()
        {
            tbUsuario sesionUsuario = db.tbUsuario.Where(x => x.usu_Id == 1).FirstOrDefault();
            Session["sesionUsuario"] = sesionUsuario;

            return View();
        }


    }
}
