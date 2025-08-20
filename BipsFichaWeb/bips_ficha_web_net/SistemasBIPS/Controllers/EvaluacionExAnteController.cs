using log4net;
using MDS.Core.Providers;
using MDS.Dto;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using SistemasBIPS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SistemasBIPS.Controllers
{
    public class EvaluacionExAnteController : Controller
    {
        private IProviderConstante constantes = null;
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(MantenedoresController));

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public EvaluacionExAnteController()
        {
            this.constantes = (IProviderConstante)Activator.CreateInstance(typeof(ProviderConstante));
        }

        // GET: EvaluacionExAnte
        [Authorize]
        public async Task<ActionResult> Index()
        {
            EvaluacionExAnteViewModels viewModelsEvalExAnte = new EvaluacionExAnteViewModels();
            EvaluacionExAnteModels evaluaciones = new EvaluacionExAnteModels();
            viewModelsEvalExAnte.listaEvaluadores = await evaluaciones.getEvaluadores(int.Parse(constantes.GetValue("PerfilEvaluadores")));
            viewModelsEvalExAnte.listaCalificaciones = await evaluaciones.getCalificaciones();
            viewModelsEvalExAnte.linkInformeDetalleExAnte = await evaluaciones.getDescripcionParametro(constantes.GetValue("linkInformeDetalleExAnte"));
            viewModelsEvalExAnte.linkInformeEvalExAnte = await evaluaciones.getDescripcionParametro(constantes.GetValue("linkInformeEvalExAnte"));
            viewModelsEvalExAnte.etapaCierre = decimal.Parse(constantes.GetValue("EtapaCierreEvalExAnte"));
            viewModelsEvalExAnte.perfilEvaluacion = await evaluaciones.getPerfilEvaluador(User.Identity.GetUserId());
            viewModelsEvalExAnte.perfilCoordinadorEval = decimal.Parse(constantes.GetValue("CoordinadorExAnte"));
            viewModelsEvalExAnte.perfilSoloLecturaEval = decimal.Parse(constantes.GetValue("EvalSoloLectExAnte"));
            viewModelsEvalExAnte.perfilUsuario = await evaluaciones.getPerfilUsuario(User.Identity.GetUserId());
            viewModelsEvalExAnte.perfilesNuevaIteracion = new List<decimal?>() { decimal.Parse(constantes.GetValue("RolAnalistaMonitoreo")), decimal.Parse(constantes.GetValue("RolAdministrador")) };
            viewModelsEvalExAnte.estadoIteracion = decimal.Parse(constantes.GetValue("EstadoInactivoIteracion"));
            viewModelsEvalExAnte.idTipoNuevos = await evaluaciones.getTipoFormulariosExAnte(int.Parse(constantes.GetValue("Nuevo")));
            viewModelsEvalExAnte.idTipoReformulados = await evaluaciones.getTipoFormulariosExAnte(int.Parse(constantes.GetValue("Reformulado")));
            viewModelsEvalExAnte.revisionJefaturas = await evaluaciones.getRevisionJefaturas(int.Parse(constantes.GetValue("RevisionExAnte")), User.Identity.GetUserId());
            viewModelsEvalExAnte.tipoJefatura = await evaluaciones.getJefaturaConectada(User.Identity.GetUserId());
            return View(viewModelsEvalExAnte);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> revisarEvaluacion(HttpPostedFileBase informePDF, int idPrograma, string tieneComentMonitoreo, string comentMonitoreo, string tieneComentEstudio, string comentEstudio, string evaluador1, string evaluador2)
        {
            string estado = string.Empty;
            try
            {
                List<String> data = new List<string>();
                data.Add(idPrograma.ToString());
                data.Add(tieneComentMonitoreo);
                data.Add(comentMonitoreo);
                data.Add(tieneComentEstudio);
                data.Add(comentEstudio);
                string rutaArchivos = Server.MapPath(constantes.GetValue("CarpetaArchivosSubidos"));
                EvaluacionExAnteModels evaluaciones = new EvaluacionExAnteModels();
                estado = await evaluaciones.revisionJefaturas(User.Identity.GetUserId(), data, informePDF, rutaArchivos, evaluador1, evaluador2);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GetFinalizarRevisionExAnte(int idPrograma, HttpPostedFileBase informePDF)
        {
            string estado = string.Empty;
            try
            {
                EvaluacionExAnteModels evaluaciones = new EvaluacionExAnteModels();
                estado = await evaluaciones.cierraEvaluacionExAnte(idPrograma, User.Identity.GetUserId(), Server.MapPath(constantes.GetValue("CarpetaArchivosSubidos")), informePDF);                
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GetEnviarComentariosSect(int idPrograma, HttpPostedFileBase informePDF, string evaluador1, string evaluador2, string comentarios)
        {
            string estado = string.Empty;
            try
            {
                EvaluacionExAnteModels evaluaciones = new EvaluacionExAnteModels();
                estado = await evaluaciones.envioComentariosSectorialista(idPrograma, User.Identity.GetUserId(), Server.MapPath(constantes.GetValue("CarpetaArchivosSubidos")), informePDF, evaluador1, evaluador2, comentarios);
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GetEnviarComentJefSect(int idPrograma, HttpPostedFileBase informePDF, string evaluador1, string evaluador2, int tipoJefatura, string comentarios)
        {
            string estado = string.Empty;
            try
            {
                EvaluacionExAnteModels evaluaciones = new EvaluacionExAnteModels();
                estado = await evaluaciones.envioComentariosJefSect(idPrograma, User.Identity.GetUserId(), Server.MapPath(constantes.GetValue("CarpetaArchivosSubidos")), informePDF, evaluador1, evaluador2, tipoJefatura, comentarios);
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GetEvaluacionesExAnte()
        {
            List<TablaProgramasDto> objEvaluacionesExAnte = new List<TablaProgramasDto>();
            try
            {
                EvaluacionExAnteModels evaluaciones = new EvaluacionExAnteModels();
                objEvaluacionesExAnte = await evaluaciones.getEvaluacionesExAnte(User.Identity.GetUserId());
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
            return Content(JsonConvert.SerializeObject(objEvaluacionesExAnte, Formatting.None));
        }        

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AsignaEvaluador(int idPrograma, string idEvaluador, int numEvaluador)
        {
            string estado = string.Empty;
            try
            {
                EvaluacionExAnteModels evaluaciones = new EvaluacionExAnteModels();
                estado = await evaluaciones.asignaEvaluadores(idPrograma, idEvaluador, numEvaluador, User.Identity.GetUserId());
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GuardaEvaluacion(PreguntaEvaluaciones data)
        {
            string estado = string.Empty;
            try
            {
                EvaluacionExAnteModels evaluaciones = new EvaluacionExAnteModels();
                estado = await evaluaciones.guardaEvaluacion(data);
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GetPreguntasEvaluaciones(int idPrograma)
        {
            PreguntaEvaluaciones data = new PreguntaEvaluaciones();
            try
            {
                EvaluacionExAnteModels evaluaciones = new EvaluacionExAnteModels();
                data = await evaluaciones.getPregEvaluaciones(idPrograma);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EnviaEvaluacion(int idPrograma, PreguntaEvaluaciones data)
        {
            string estado = string.Empty;
            try
            {
                EvaluacionExAnteModels evaluaciones = new EvaluacionExAnteModels();
                estado = await evaluaciones.guardaEvaluacion(data);
                if (estado == "ok")
                    estado = await evaluaciones.enviaEvaluacion(idPrograma, User.Identity.GetUserId());
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CierraEvaluacion(int idPrograma)
        {
            string estado = string.Empty;
            try
            {
                EvaluacionExAnteModels evaluaciones = new EvaluacionExAnteModels();
                estado = await evaluaciones.cierraEvaluacion(idPrograma, User.Identity.GetUserId());
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> NuevaIteracion(int idPrograma, string versionPrograma)
        {
            string estado = string.Empty;
            try
            {
                EvaluacionExAnteModels evaluaciones = new EvaluacionExAnteModels();
                int version = String.IsNullOrEmpty(versionPrograma) ? 0 : int.Parse(versionPrograma);
                estado = await evaluaciones.nuevaIteracion(idPrograma, version, User.Identity.GetUserId());
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GetComentariosJefaturas(int idPrograma)
        {
            ComentariosJefaturas data = new ComentariosJefaturas();
            try
            {
                EvaluacionExAnteModels evaluaciones = new EvaluacionExAnteModels();
                data = await evaluaciones.getComentarios(idPrograma);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // GET: EvaluacionExAnte
        [Authorize]
        public ActionResult Panel()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GetPanelExAnte()
        {
            List<TablaProgramasDto> objEvaluacionesExAnte = new List<TablaProgramasDto>();
            try
            {
                EvaluacionExAnteModels evaluaciones = new EvaluacionExAnteModels();
                objEvaluacionesExAnte = await evaluaciones.getPanelExAnte();
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
            return Content(JsonConvert.SerializeObject(objEvaluacionesExAnte, Formatting.None));
        }

        [Authorize]
        public async Task<ActionResult> Dashboard()
        {            
            try
            {
                DashboardViewModels viewModelDashboard = new DashboardViewModels();
                EvaluacionExAnteModels evaluaciones = new EvaluacionExAnteModels();
                FormulariosModels formularios = new FormulariosModels();
                viewModelDashboard.ministerios = await formularios.getListaParametros(new TablaParametrosFiltroDto() { IdCategoria = int.Parse(constantes.GetValue("Ministerios")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                viewModelDashboard.servicios = await formularios.getListaParametros(new TablaParametrosFiltroDto() { IdCategoria = int.Parse(constantes.GetValue("Servicios")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                viewModelDashboard.indicadores = await evaluaciones.getIndicadoresDashboard(User.Identity.GetUserId(), 0, "1");
                viewModelDashboard.tipoProgramas = await evaluaciones.getIndicTipoProgramas(User.Identity.GetUserId(), 0, "2");
                viewModelDashboard.califProgramas = await evaluaciones.getIndicTipoProgramas(User.Identity.GetUserId(), 0, "3");
                viewModelDashboard.estadoProgramas = await evaluaciones.getIndicTipoProgramas(User.Identity.GetUserId(), 0, "4");
                viewModelDashboard.programas = await evaluaciones.getListadoProgramas(2024, 360);
                viewModelDashboard.evaluadores = await evaluaciones.getEvaluadores(int.Parse(constantes.GetValue("PerfilEvaluadores")));
                viewModelDashboard.programasXiteracion = await evaluaciones.getProgramasXIteracion(2024, 360);

                return View(viewModelDashboard);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }            
        }
    }
}