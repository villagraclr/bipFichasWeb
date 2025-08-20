using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SistemasBIPS.Models;
using System.Security.Claims;
using MDS.Dto;
using MDS.Core.Providers;
using System.Threading.Tasks;
using System;
using log4net;

namespace SistemasBIPS.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger((typeof(ProgramaController)));
        private IProviderConstante constantes = null;
        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public HomeController()
        {
            this.constantes = (IProviderConstante)Activator.CreateInstance(typeof(ProviderConstante));
        }

        [Authorize]
        public ActionResult Escritorio()
        {
            return View();
        }

        [Authorize]
        public ActionResult Mensajes()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> LogOff()
        {
            LogUsuarioModels logUsuario = new LogUsuarioModels();
            var sesionID = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Sid).Value;
            //TODO: MDC => se liberan formularios tomados por usuario en esta sesion
            bool liberaForm = await logUsuario.actualizaLogFormularios(new TablaLogFormulariosDto() { IdSesion = sesionID, IdUsuario = User.Identity.GetUserId(), EstadoAcceso = decimal.Parse(constantes.GetValue("Inactivo")), TipoUpdate = decimal.Parse(constantes.GetValue("UpdateLiberaTodosForm")) });
            if (!liberaForm)
                log.Error("Error al liberar formularios de sesion");
            //TODO: MDC => se libera sesion
            bool logSesion = await logUsuario.registraLogSesion(new TablaLogSesionDto() { IdSesion = sesionID, IdUsuario = User.Identity.GetUserId(), EstadoSesion = decimal.Parse(constantes.GetValue("EstadoOffline")) });
            if (!logSesion)
                log.Error("Error al registrar log de salida de sesion");
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);            
            return RedirectToAction("Login", "Login", new { returnUrl = string.Empty });
        }

        [AllowAnonymous]
        public async Task<PartialViewResult> _Menu()
        {
            UsuariosViewModels data = new UsuariosViewModels();
            try {
                if (User.Identity.IsAuthenticated){
                    UsuariosModels usuario = new UsuariosModels();
                    data.listaPermisosPerfiles = await usuario.getPerfilesPermisos(User.Identity.GetUserId());
                    data.dataUsuario = await usuario.getDatosUsuarios(User.Identity.GetUserId());
                    data.listaPerfilesEspeciales = await usuario.getPerfilesEspeciales(User.Identity.GetUserId());
                }                
            }
            catch(Exception ex) {
                log.Error(ex.Message, ex);
            }
            return PartialView(data);
        }
    }
}