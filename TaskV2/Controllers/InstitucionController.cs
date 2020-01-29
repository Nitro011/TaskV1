using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskV2.Models;

namespace TaskV2.Controllers
{

    [Authorize]
    public class InstitucionController : ApiController
    {


        CRUDTaskDB db = new CRUDTaskDB();

        public IHttpActionResult GetAll() 
        {
            var idUSer = (from f in db.Usuarios
                          where f.Correo == User.Identity.Name
                          select f.IdUsuario).First();


            var GetConsultar = from g in db.Identidad
                                where g.IdUsuarios == idUSer
                               select new { NombreEmpresa = g.NombreIndentidad, Usuario = g.IdUsuarios, Email = g.CorreoIdentidad };

            return Json(GetConsultar);
        
        }

        public IHttpActionResult InsertarInstitucion(string NombreIdentidad,string CorreoIdentidad) 
        {
            Identidad identidad = new Identidad();


            var idd= (from r in db.Usuarios
                      where r.Correo == User.Identity.Name
                      select r.IdUsuario).First();

            var valid = (from d in db.Identidad
                         where d.IdUsuarios == idd
                         select d.NombreIndentidad).Count();


            if (valid == 0)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        identidad.IdUsuarios = idd;
                                               
                                               
                                               
                        identidad.NombreIndentidad = NombreIdentidad;
                        identidad.CorreoIdentidad = CorreoIdentidad;

                        db.Identidad.Add(identidad);
                        db.SaveChanges();

                        return Json("Identidad registrado");

                    }
                }
                catch (Exception ex)
                {

                    return Json(ex.Message);
                }

            }
            else
            {
                return Json("Esta identidad ya tiene un usuario");
            }



            return Ok();


        }


     
        




    }
}
