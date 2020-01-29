using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web.Http;
using TaskV2.Models;


namespace TaskV2.Controllers
{

 

    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        CRUDTaskDB dba = new CRUDTaskDB();
        static string key { get; set; } = "A!9HHhi%XjjYY4YP2@Nob009X";


        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return Ok(true);
        }

        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok($" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}");
        }

        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(string userna,string passwor)
        {
            LoginRequest login = new LoginRequest();

            login.Username = userna;
            login.Password = CrypPass(passwor);


            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            //TODO: Validate credentials Correctly, this code is only for demo !!
            var validar = (from e in dba.Usuarios
                          where e.Contrasena == login.Password && e.Correo == login.Username
                          select e).Count();

            bool isCredentialValid = (validar ==1);
          
            if (isCredentialValid)
            {
                var token = TokenGenerator.GenerateTokenJwt(login.Username);

                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
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
