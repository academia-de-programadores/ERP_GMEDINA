﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_GMEDINA.Models
{
    public class Helpers
    {
            ERP_GMEDINAEntities db = new ERP_GMEDINAEntities();
        public bool GetUserLogin()
        {
            bool state = false;
            int user = 0;
            try
            {
                user = (int)HttpContext.Current.Session["UserLogin"];
                if (user != 0)
                    state = true;
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                state = false;
            }
            return state;
        }

        public List<tbUsuario> getUserInformation()
        {
            int user = 0;
            List<tbUsuario> UsuarioList = new List<tbUsuario>();
            try
            {
                user = (int)HttpContext.Current.Session["UserLogin"];
                if (user != 0)
                {
                    UsuarioList = db.tbUsuario.Where(s => s.usu_Id == user).ToList();
                }
                return UsuarioList;
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                return UsuarioList;
            }
        }

        public int GetUser()
        {
            int user = 0;
            try
            {
                user = (int)HttpContext.Current.Session["UserLogin"];
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }
            return user;
        }

        public bool Sesiones(string sPantalla)
        {
            int UserID = 0;
            bool Retorno = false;
            byte Sesion = 0;

            try
            {
                UserID = (int)HttpContext.Current.Session["UserLogin"];
                Sesion = (byte)HttpContext.Current.Session["UserLoginSesion"];
                if (Sesion > 1)
                    Retorno = true;
                //else
                //{
                //    var list = (IEnumerable<SDP_Acce_GetUserRols_Result>)HttpContext.Current.Session["UserLoginRols"];
                //    var BuscarList = list.Where(x => x.obj_Referencia == sPantalla);
                //    int Conteo = BuscarList.Count();
                //    if (Conteo > 0)
                //        Retorno = true;
                //}
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                Retorno = false;
            }
            return Retorno;
        }

        public bool GetUserRols(string sPantalla)
        {
            int UserID = 0;
            bool EsAdmin = false;
            bool Retorno = false;

            try
            {
                UserID = (int)HttpContext.Current.Session["UserLogin"];
                EsAdmin = (bool)HttpContext.Current.Session["UserLoginEsAdmin"];
                if (EsAdmin)
                {
                    Retorno = true;
                }
                else
                {
                    var list = (IEnumerable<SDP_Acce_GetUserRols_Result>)HttpContext.Current.Session["UserLoginRols"];
                    var BuscarList = list.Where(x => x.obj_Referencia == sPantalla);
                    int Conteo = BuscarList.Count();
                    if (Conteo > 0)
                        Retorno = true;
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                Retorno = false;
            }
            return Retorno;

        }

        public bool GetRol()
        {
            bool state = false;
            bool EsAdmin = false;
            int Rol = 0;
            try
            {
                Rol = (int)HttpContext.Current.Session["UserRol"];
                EsAdmin = (bool)HttpContext.Current.Session["UserLoginEsAdmin"];
                if (EsAdmin)
                    state = true;
                else
                {
                    if (Rol != 0)
                        state = true;
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                state = false;
            }
            return state;
        }

        public DateTime DatetimeNow()
        {
            DateTime dt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-6)).DateTime;
            return dt;
        }
    }
}