using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelos;
using System.Data.SqlClient;
using System.Threading;
using Microsoft.AspNet.Identity;

namespace diseño_constructora.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        //Modelos.constructora_diseñoEntities contexto = new Modelos.constructora_diseñoEntities();
        // GET: Menu

        //metodo para el menu principal de la pagina
        [AllowAnonymous]
        public ActionResult Menu_principal()
        {
            return View();
        }


        //metodo para ver los administradores
        
        public ActionResult MostrarAdministradores()
        {
            using(Repositorio<cd_usuarios> obj = new Repositorio<cd_usuarios>())
            {
                obj.Exception += Obj_Exception;
                ViewBag.data = obj.Filter(x => true);
            }
            return View();

        }


        
        //vista del formulario para crear un nuevo administrador
        [HttpGet]
        public ActionResult Inicio()
        {
            using (Repositorio<cd_roles> obj = new Repositorio<cd_roles>())
            {
                obj.Exception += Obj_Exception;
                ViewBag.data = obj.Filter(x => true);
            }
            return View();
        }

        
        [HttpPost]
        public ActionResult Crear(string USUARIOS_NOMBRE, string USUARIOS_USUARIO, string USUARIOS_CONTRASEÑA, int USUARIOS_ROL)
        {
            Thread.Sleep(2000);
            using(Repositorio<cd_usuarios> obj = new Repositorio<cd_usuarios>())
            {
                obj.Exception += Obj_Exception;
                obj.Create(new Modelos.cd_usuarios()
                {
                    USUARIOS_NOMBRE = USUARIOS_NOMBRE,
                    USUARIOS_USUARIO = USUARIOS_USUARIO,
                    USUARIOS_CONTRASEÑA = Seguridad.Encriptar(USUARIOS_CONTRASEÑA),
                    USUARIOS_ROL = USUARIOS_ROL,
                });
            }
            return RedirectToAction("Inicio", "Menu");
        }

        
        [HttpPost]
        public ActionResult CrearAjax(string USUARIOS_NOMBRE, string USUARIOS_USUARIO, string USUARIOS_CONTRASEÑA, int USUARIOS_ROL)
        {
            Thread.Sleep(5000);
            bool result = false;
            string mensaje = "Error al crear el registro";
            using (Repositorio<cd_usuarios> obj = new Repositorio<cd_usuarios>())
            {
                obj.Exception += Obj_Exception;
                var usuario = obj.Create(new Modelos.cd_usuarios()
                                {
                                    USUARIOS_NOMBRE = USUARIOS_NOMBRE,
                                    USUARIOS_USUARIO = USUARIOS_USUARIO,
                                    USUARIOS_CONTRASEÑA = Seguridad.Encriptar(USUARIOS_CONTRASEÑA),
                                    USUARIOS_ROL = USUARIOS_ROL,
                                });
                if (usuario != null)
                {
                    result = true;
                    mensaje = "Usuario creado con exito";
                }
            }
            return Json(new { result = result, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


        //Funcion que contiene la vista de editar
        [HttpGet]
        public ActionResult EditarAdministrador(int? id)
        {
            using (Repositorio<cd_roles> obj = new Repositorio<cd_roles>())
            {
                ViewBag.roles = obj.Filter(x => true);
            }
            //ViewBag.valor = id_usuario;
            if (id != null)
            {
                //ViewBag.valor = "esta entrando aca";
                using(Repositorio<cd_usuarios> obj = new Repositorio<cd_usuarios>())
                {
                    var modelo = obj.Retrieve(x => x.USUARIOS_ID == id);
                    if(modelo != null)
                    {
                        return View(modelo);
                    }
                }
            }
            else
            {
                ViewBag.valor = "No existe";
            }
            return View();
        }

        //funcion para editar un administrador
        [HttpPost]
        public ActionResult EditarAjax(cd_usuarios usuario)
        {
            Thread.Sleep(2000);
            bool result = false;
            string mensaje = "Error al actualizar el registro";
            using(Repositorio<cd_usuarios> obj = new Repositorio<cd_usuarios>())
            {
                obj.Exception += Obj_Exception;
                obj.Update(usuario);
                result = true;
                mensaje = "Registro actualizado!";
            }
            return Json(new { result = result, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        //funcion para eliminar un administrador
        [HttpPost]
        public ActionResult EliminarAdministrador(int? id_user)
        {
            Thread.Sleep(2000);
            bool result = false;
            string mensaje = "Error al borrar el registro";
            using(Repositorio<cd_usuarios> obj = new Repositorio<cd_usuarios>())
            {
                obj.Exception += Obj_Exception;
                obj.Delete(obj.Retrieve(x => x.USUARIOS_ID == id_user));
                result = true;
                mensaje = "Registro Borrado";
            }
            return Json(new { result = result, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Cargarimagenes()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AjaxProyectos(string PROYECTOS_NOMBRE, string PROYECTOS_DESCRIPCION, HttpPostedFileBase archivo)
        {
            Thread.Sleep(2000);
            bool result = false;
            string mensaje = "Error al insertar la imagen";
            if (archivo != null)
            {
                mensaje = "imagen encontrada con exito";
                result = true;
            }
            return Json(new { result = result, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }



        private void Obj_Exception(object sender, ExceptionEventArgs e)
        {
            if (e.EntityValidationErrors != null)
            {
                throw new DbEntityValidationException(e.Message, e.EntityValidationErrors, e.InnerException) { Source = e.Source };

            }
  

        }

        


    }
}