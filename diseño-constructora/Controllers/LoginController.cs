using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using diseño_constructora.Helpers;
using RepositorioGenerico;

namespace diseño_constructora.Controllers
{
    public class LoginController : Controller
    {

        // GET: Login
        [HttpGet]
        public ActionResult Entrar(string returnUrl)
        {
            ActionResult Result;
            ViewBag.ReturnUrl = returnUrl;
                Result = View();
            
            return Result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Modelos.LoginViewModel data, string returnUrl)
        {
            ActionResult Result;
            string clave = Seguridad.Encriptar(data.USUARIOS_CONTRASEÑA);
            Modelos.Repositorio<Modelos.cd_usuarios> Usuario = new Modelos.Repositorio<Modelos.cd_usuarios>();
            var User = Usuario.Retrieve(
                    x => x.USUARIOS_USUARIO == data.USUARIOS_USUARIO
                    && x.USUARIOS_CONTRASEÑA == clave
                );

            if(User != null)
            {
                Result = SignInUser(User, data.Remember, returnUrl);
            }
            else
            {
                Result = View(data);
            }
            return Result;
        }

        private ActionResult SignInUser(cd_usuarios user, bool remember, string returnUrl)
        {
            ActionResult Result;
            List<Claim> Claims = new List<Claim>();
            Claims.Add(new Claim(ClaimTypes.NameIdentifier, user.USUARIOS_ID.ToString()));
            Claims.Add(new Claim(ClaimTypes.Name, user.USUARIOS_NOMBRE));
            Claims.Add(new Claim("FullName", $"{ user.USUARIOS_NOMBRE} {user.USUARIOS_USUARIO}"));

            //if (user.USUARIOS_ROL != null && user.cd_roles.ROLES_NOMBRES.Any())
            //{
            //    Claims.AddRange(user.cd_roles.ROLES_NOMBRES.Select(
            //            x => new Claim(ClaimTypes.Role, x.ToString())
            //        ));
            //}

            var Identity = new ClaimsIdentity(Claims, DefaultAuthenticationTypes.ApplicationCookie);
            IAuthenticationManager authenticationManager =
                HttpContext.GetOwinContext().Authentication;

            authenticationManager.SignIn(
                new AuthenticationProperties()
                { IsPersistent = remember }, Identity);

            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Url.Action("MostrarAdministradores", "Menu");
            }

            Result = Redirect(returnUrl);
            return Result;

        }

        public ActionResult LogOff()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Entrar", "Login");
        }
    }
}