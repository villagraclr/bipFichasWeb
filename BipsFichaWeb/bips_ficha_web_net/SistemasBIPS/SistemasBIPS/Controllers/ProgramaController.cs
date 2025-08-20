using log4net;
using MDS.Core.Providers;
using MDS.Dto;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using SistemasBIPS.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SistemasBIPS.Controllers
{
    public class ProgramaController : Controller
    {
        #region Variables globales
        private static readonly ILog log = LogManager.GetLogger((typeof(ProgramaController)));
        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }
        private ProgramasModels programas = null;
        private IProviderConstante constantes = null;
        #endregion

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public ProgramaController()
        {
            this.programas = new ProgramasModels();
            this.constantes = (IProviderConstante)Activator.CreateInstance(typeof(ProviderConstante));
        }

        [Authorize]
        public async Task<ActionResult> Programas()
        {
            ProgramasViewModels viewModelProgramas = new ProgramasViewModels();
            viewModelProgramas.listaMinServ = await programas.getListaMinistServ();
            viewModelProgramas.listaAnos = await programas.getAnos();
            viewModelProgramas.listaFormulariosTomados = await programas.getFormulariosTomados(User.Identity.GetUserId());
            viewModelProgramas.listaRutasFichas = await programas.getListaRutasFichas();
            viewModelProgramas.listaTiposProgramas = await programas.getTiposProgramas();
            viewModelProgramas.listaAnosFichas = await programas.getAnosFichas();
            viewModelProgramas.permisoLibera = await programas.getPermisoLibera(User.Identity.GetUserId());
            viewModelProgramas.permisoInfoEval = await programas.getPermisoInformeEval(User.Identity.GetUserId());
            return View(viewModelProgramas);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GetProgramas(string filtroAnos, string filtroMinisterios, string filtroFormularios)
        {
            List<TablaProgramasDto> objProgramas = new List<TablaProgramasDto>();
            try
            {
                LogUsuarioModels logUsuario = new LogUsuarioModels();
                var sesionID = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Sid).Value;
                bool logSesion = await logUsuario.registraLogSesion(new TablaLogSesionDto() { IdSesion = sesionID, IdUsuario = User.Identity.GetUserId(), EstadoSesion = decimal.Parse(constantes.GetValue("EstadoOnline")) });
                if (!logSesion)
                    log.Error("Error al registrar log de inicio de sesion");
                programas = new ProgramasModels();
                if (!string.IsNullOrEmpty(filtroAnos) || !string.IsNullOrEmpty(filtroMinisterios) || !string.IsNullOrEmpty(filtroFormularios))
                    objProgramas = await programas.getProgramasFiltro(filtroAnos, filtroMinisterios, filtroFormularios, User.Identity.GetUserId());
                else
                    objProgramas = await programas.getProgramasFiltro(User.Identity.GetUserId());
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
            return Content(JsonConvert.SerializeObject(objProgramas, Formatting.None));
        }        
    }
}