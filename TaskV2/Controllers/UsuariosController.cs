using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using TaskV2.Models;

namespace TaskV2.Controllers
{

    public class UsuariosController : ApiController
    {

        CRUDTaskDB db = new CRUDTaskDB();


        [Authorize]

        public IHttpActionResult GetAll() 
        {
            
            var usuario = from d in db.Usuarios
                          where d.Correo == User.Identity.Name
                          select new {Nombre = d.Nombre,Apellido = d.Apellido,Email = d.Correo } ;
            
            
            return Json(usuario);
        
        }
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult InsertarUsuario(string fisrtname, string lastname, string email, string pass) 
        {
            Usuarios usuarios = new Usuarios();
           
            try
            {
                if (ModelState.IsValid)
                {
                    usuarios.Nombre = fisrtname;
                    usuarios.Apellido = lastname;
                    usuarios.Correo = email;
                  



                    usuarios.Contrasena = CrypPass(pass);
                    db.Usuarios.Add(usuarios);

                    db.SaveChanges();




                    return Ok("Usuario registrado mi patron Ricardo");

                }
            }
            catch (Exception)
            {

                return BadRequest(ModelState);

            }
            return Ok();
        }

        [HttpPut]
        [Authorize]

        public IHttpActionResult ModificarUsuario(int? id,string nombre,string apellido,string correo,string pass ) 
        {




            Usuarios usuarios = new Usuarios();

            usuarios.IdUsuario = id;
            usuarios.Nombre = nombre;
            usuarios.Apellido = apellido;
            usuarios.Correo = correo;
            usuarios.Contrasena = pass;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuarios.IdUsuario)
            {
                return BadRequest();
            }

            db.Entry(usuarios).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {

                return StatusCode(HttpStatusCode.NotFound);
            }


            return Ok();
        
        
        }
        [HttpDelete]
        [ResponseType(typeof(Usuarios))]
        [Authorize]

        public IHttpActionResult EliminarUsuario(int id)
        {
            Usuarios usuarios =  db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return NotFound();
            }

            db.Usuarios.Remove(usuarios);
            db.SaveChanges();

            return Ok(usuarios);
        }


        public string CrypPass(string pass)
        {

            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(pass));
                return Convert.ToBase64String(data);
            }
        
        
        }

    }
}
