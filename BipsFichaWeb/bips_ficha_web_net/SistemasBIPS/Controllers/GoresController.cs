using log4net;
using MDS.Core.Providers;
using MDS.Dto;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using SistemasBIPS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SistemasBIPS.Controllers
{
    public class GoresController : Controller
    {
        #region Variables globales
        private static readonly ILog log = LogManager.GetLogger((typeof(ProgramaController)));
        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }
        private ProgramasGoresModels programas = null;
        private FormulariosModels formularios = null;
        private IProviderConstante constantes = null;
        #endregion

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public GoresController()
        {
            this.programas = new ProgramasGoresModels();
            this.formularios = new FormulariosModels();
            this.constantes = (IProviderConstante)Activator.CreateInstance(typeof(ProviderConstante));
        }

        [Authorize]
        public async Task<ActionResult> Index()
        {
            GoresViewModels viewModelGores = new GoresViewModels();
            EvaluacionExAnteModels evaluaciones = new EvaluacionExAnteModels();
            UsuariosModels usuarios = new UsuariosModels();
            ProgramasModels ministeriosServicios = new ProgramasModels();
            if (await usuarios.getPerfilesPermisos(int.Parse(constantes.GetValue("PermisoUsuarios")), User.Identity.GetUserId()))
            {
                viewModelGores.usuarioViewModel.listaPerfiles = await usuarios.getPerfiles(User.Identity.GetUserId());
                viewModelGores.usuarioViewModel.listaMinistServ = await ministeriosServicios.getListaMinistServ();
                viewModelGores.usuarioViewModel.dataUsuario = await usuarios.getDatosUsuarios(User.Identity.GetUserId());
                viewModelGores.usuarioViewModel.plantillas = await usuarios.getPlantillasFormularios(new TablaExcepcionesPlantillasFiltroDto() { Estado = decimal.Parse(constantes.GetValue("Activo")) }, User.Identity.GetUserId());
                viewModelGores.usuarioViewModel.tipos = await formularios.getListaParametros(new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("TiposFormularios")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
            }

            viewModelGores.listaEvaluadores = await evaluaciones.getEvaluadores(int.Parse(constantes.GetValue("PerfilFormuladoresGore")));
            viewModelGores.listaGores = await programas.getListaGores();
            viewModelGores.listaAnos = await programas.getAnos();
            viewModelGores.usuarioGore = await formularios.getDatosUsuario(User.Identity.GetUserId());
            viewModelGores.etapaPerfilCalificado = decimal.Parse(constantes.GetValue("EtapaPerfilCalificado"));
            //viewModelGores.listaFormulariosTomados = await programas.getFormulariosTomados(User.Identity.GetUserId());
            //viewModelGores.listaRutasFichas = await programas.getListaRutasFichas();
            //viewModelGores.listaTiposProgramas = await programas.getTiposProgramas();
            //viewModelGores.listaAnosFichas = await programas.getAnosFichas();
            //viewModelGores.permisoLibera = await programas.getPermisoLibera(User.Identity.GetUserId());
            //viewModelGores.permisoInfoEval = await programas.getPermisoInformeEval(User.Identity.GetUserId());
            return View(viewModelGores);
        }

        [Authorize]
        public async Task<ActionResult> Programas()
        {
            GoresViewModels viewModelGores = new GoresViewModels();
            viewModelGores.listaGores = await programas.getListaGores();
            viewModelGores.listaAnos = await programas.getAnos();
            //viewModelGores.listaFormulariosTomados = await programas.getFormulariosTomados(User.Identity.GetUserId());
            //viewModelGores.listaRutasFichas = await programas.getListaRutasFichas();
            //viewModelGores.listaTiposProgramas = await programas.getTiposProgramas();
            //viewModelGores.listaAnosFichas = await programas.getAnosFichas();
            //viewModelGores.permisoLibera = await programas.getPermisoLibera(User.Identity.GetUserId());
            //viewModelGores.permisoInfoEval = await programas.getPermisoInformeEval(User.Identity.GetUserId());
            return View(viewModelGores);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GetPerfilesGores(string filtroAnos, string filtroGores)
        {
            List<TablaProgramasDto> objProgramas = new List<TablaProgramasDto>();
            try
            {
                LogUsuarioModels logUsuario = new LogUsuarioModels();
                var sesionID = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Sid).Value;
                bool logSesion = await logUsuario.registraLogSesion(new TablaLogSesionDto() { IdSesion = sesionID, IdUsuario = User.Identity.GetUserId(), EstadoSesion = decimal.Parse(constantes.GetValue("EstadoOnline")) });
                if (!logSesion)
                    log.Error("Error al registrar log de inicio de sesion");
                programas = new ProgramasGoresModels();
                if (!string.IsNullOrEmpty(filtroAnos) || !string.IsNullOrEmpty(filtroGores))
                    objProgramas = await programas.getProgramasFiltro(filtroAnos, filtroGores, User.Identity.GetUserId(), int.Parse(constantes.GetValue("TipoPerfilGore")));
                else
                    objProgramas = await programas.getProgramasFiltro(User.Identity.GetUserId(), int.Parse(constantes.GetValue("TipoPerfilGore")));
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
            return Content(JsonConvert.SerializeObject(objProgramas, Formatting.None));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GetProgramasGores(string filtroAnos, string filtroMinisterios, string filtroFormularios)
        {
            List<TablaProgramasDto> objProgramas = new List<TablaProgramasDto>();
            try
            {
                LogUsuarioModels logUsuario = new LogUsuarioModels();
                var sesionID = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Sid).Value;
                bool logSesion = await logUsuario.registraLogSesion(new TablaLogSesionDto() { IdSesion = sesionID, IdUsuario = User.Identity.GetUserId(), EstadoSesion = decimal.Parse(constantes.GetValue("EstadoOnline")) });
                if (!logSesion)
                    log.Error("Error al registrar log de inicio de sesion");
                programas = new ProgramasGoresModels();
                if (!string.IsNullOrEmpty(filtroAnos) || !string.IsNullOrEmpty(filtroMinisterios) || !string.IsNullOrEmpty(filtroFormularios))
                    objProgramas = await programas.getProgramasFiltro(filtroAnos, filtroMinisterios, User.Identity.GetUserId(), int.Parse(constantes.GetValue("TipoProgramaGore")));
                else
                    objProgramas = await programas.getProgramasFiltro(User.Identity.GetUserId(), int.Parse(constantes.GetValue("TipoProgramaGore")));
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
            return Content(JsonConvert.SerializeObject(objProgramas, Formatting.None));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GuardaNuevoPerfil(String nombre, int gore)
        {
            List<string> estado = new List<string>();
            try
            {
                ProgramasGoresModels formularios = new ProgramasGoresModels();
                UsuariosModels usuarios = new UsuariosModels();
                if (await usuarios.getPerfilesPermisos(int.Parse(constantes.GetValue("CreaPerfilesGores")), User.Identity.GetUserId())){
                    estado = await formularios.registraNuevoPerfil(nombre, User.Identity.GetUserId(), gore);
                }else
                    estado.Add("Sin permiso para realizar esta acción");
            }
            catch (Exception ex)
            {
                estado.Add(ex.Message);
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<ActionResult> EvaluadorPerfil()
        {
            EvaluacionExAnteViewModels viewModelEvalPerfil = new EvaluacionExAnteViewModels();
            viewModelEvalPerfil.etapaCierre = decimal.Parse(constantes.GetValue("EtapaCierreEvalExAnte"));
            viewModelEvalPerfil.etapaPerfilEnConsulta = decimal.Parse(constantes.GetValue("EtapaPerfilEnConsulta"));
            viewModelEvalPerfil.etapaCierrePerfilGore = decimal.Parse(constantes.GetValue("EtapaCierrePerfilGore"));
            EvaluacionExAnteModels evaluaciones = new EvaluacionExAnteModels();
            viewModelEvalPerfil.listaEvaluadores = await evaluaciones.getEvaluadores(int.Parse(constantes.GetValue("EvaluadoresPerfilGore")));
            return View(viewModelEvalPerfil);
        }

        [Authorize]
        public async Task<ActionResult> EvaluadorPrograma()
        {
            EvaluacionExAnteViewModels viewModelEvalPrograma = new EvaluacionExAnteViewModels();
            viewModelEvalPrograma.etapaCierre = decimal.Parse(constantes.GetValue("EtapaCierreEvalExAnte"));
            viewModelEvalPrograma.etapaRevisionJefe = decimal.Parse(constantes.GetValue("EtapaRevisionJefe"));
            viewModelEvalPrograma.etapaProgramaCalificado = decimal.Parse(constantes.GetValue("EtapaProgramaCalificado"));
            EvaluacionExAnteModels evaluaciones = new EvaluacionExAnteModels();
            viewModelEvalPrograma.listaEvaluadores = await evaluaciones.getEvaluadores(int.Parse(constantes.GetValue("EvaluadoresPerfilGore")));
            return View(viewModelEvalPrograma);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GetEvaluacionesPerfil()
        {
            List<TablaProgramasDto> objEvaluacionPerfil = new List<TablaProgramasDto>();
            try
            {
                ProgramasGoresModels evaluaciones = new ProgramasGoresModels();
                objEvaluacionPerfil = await evaluaciones.getEvaluacionPerfil(User.Identity.GetUserId());
                objEvaluacionPerfil = objEvaluacionPerfil.Where(e => e.IdEstado == 2).ToList();
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
            return Content(JsonConvert.SerializeObject(objEvaluacionPerfil, Formatting.None));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GetEvaluacionesPrograma()
        {
            List<TablaProgramasDto> objEvaluacionPrograma = new List<TablaProgramasDto>();
            try
            {
                ProgramasGoresModels evaluaciones = new ProgramasGoresModels();
                objEvaluacionPrograma = await evaluaciones.getEvaluacionPrograma(User.Identity.GetUserId());
                objEvaluacionPrograma = objEvaluacionPrograma.Where(e => e.IdEstado == 2).ToList();
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
            return Content(JsonConvert.SerializeObject(objEvaluacionPrograma, Formatting.None));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AsignaEvaluadorPerfil(int idPrograma, string idEvaluador, string idEvaluadorAnterior)
        {
            string estado = string.Empty;
            try
            {
                ProgramasGoresModels evaluaciones = new ProgramasGoresModels();
                estado = await evaluaciones.asignaEvaluadoresPerfil(idPrograma, idEvaluador, idEvaluadorAnterior, User.Identity.GetUserId());
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AsignaTipoOferta(int idPrograma, string tipoOferta, string[] evaluadorDipres, string[] idEvaluadorAnterior)
        {
            string estado = string.Empty;
            try
            {
                ProgramasGoresModels evaluaciones = new ProgramasGoresModels();
                estado = await evaluaciones.asignaTipoOferta(idPrograma, tipoOferta, evaluadorDipres, idEvaluadorAnterior, User.Identity.GetUserId());
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EnviarAdmisibilidad(string _id, string ingresoExAnte, string motivoNoIngreso, string otroMotivo)
        {
            string estado = string.Empty;
            try
            {
                int idPrograma = int.Parse(EncriptaDatos.RijndaelSimple.Desencriptar(Server.UrlDecode(_id)));
                ProgramasGoresModels evaluaciones = new ProgramasGoresModels();
                estado = await evaluaciones.enviarAdmisibilidad(idPrograma, ingresoExAnte, motivoNoIngreso, otroMotivo, User.Identity.GetUserId());
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> correspondeIngresoExAnte(int idPrograma, string idEvaluador, bool correspondeIngreso)
        {
            string estado = string.Empty;
            try
            {
                string rutaArchivos = Server.MapPath(constantes.GetValue("CarpetaArchivosSubidos"));
                ProgramasGoresModels evaluaciones = new ProgramasGoresModels();
                estado = await evaluaciones.correspondeIngresoExAnte(idPrograma, idEvaluador, correspondeIngreso, User.Identity.GetUserId(), rutaArchivos);
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> enviarCalificacion(string _id, int idBips, string calificacion)
        {
            string estado = string.Empty;
            try
            {
                int idPrograma = int.Parse(EncriptaDatos.RijndaelSimple.Desencriptar(Server.UrlDecode(_id)));
                string rutaArchivos = Server.MapPath(constantes.GetValue("CarpetaArchivosSubidos"));
                ProgramasGoresModels evaluaciones = new ProgramasGoresModels();
                estado = await evaluaciones.enviarCalificacion(idPrograma, idBips, calificacion, User.Identity.GetUserId(), rutaArchivos);
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> crearPrograma(int idPrograma, int idBips)
        {
            string estado = string.Empty;
            try
            {
                //string rutaArchivos = Server.MapPath(constantes.GetValue("CarpetaArchivosSubidos"));
                ProgramasGoresModels evaluaciones = new ProgramasGoresModels();
                estado = await evaluaciones.crearPrograma(idPrograma, idBips, User.Identity.GetUserId());
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GuardaFormuladores(string[]idFormuladores, int idPermiso, int idPrograma)
        {
            string estado = string.Empty;
            try
            {
                UsuariosModels usuarios = new UsuariosModels();
                if (await usuarios.getPerfilesPermisos(int.Parse(constantes.GetValue("PermisoAsignarPermisosUsers")), User.Identity.GetUserId()))
                {
                    ProgramasGoresModels evaluaciones = new ProgramasGoresModels();
                    estado = await evaluaciones.guardaFormuladores(idFormuladores, idPermiso, idPrograma, User.Identity.GetUserId());
                }
                else
                {
                    estado = "Sin permiso para realizar esta acción";
                }
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AsignaEvaluadoresPrograma(int idPrograma, string idEvaluador, string idEvaluadorAnterior, int numeroEvaluador)
        {
            string estado = string.Empty;
            try
            {
                ProgramasGoresModels evaluaciones = new ProgramasGoresModels();
                estado = await evaluaciones.asignaEvaluadoresPrograma(idPrograma, idEvaluador, idEvaluadorAnterior, numeroEvaluador, User.Identity.GetUserId());
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EnviarEvaluacionJefatura(string _id, string calificacion)
        {
            string estado = string.Empty;
            try
            {
                int idPrograma = int.Parse(EncriptaDatos.RijndaelSimple.Desencriptar(Server.UrlDecode(_id)));
                string rutaArchivos = Server.MapPath(constantes.GetValue("CarpetaArchivosSubidos"));
                ProgramasGoresModels evaluaciones = new ProgramasGoresModels();
                estado = await evaluaciones.enviarEvaluacionJefatura(idPrograma, calificacion, User.Identity.GetUserId(), rutaArchivos);
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EnviarComentariosJefatura(string idPrograma, HttpPostedFileBase comentarios)
        {
            string estado = string.Empty;
            try
            {
                //int idPrograma = int.Parse(EncriptaDatos.RijndaelSimple.Desencriptar(Server.UrlDecode(_id)));
                string rutaArchivos = Server.MapPath(constantes.GetValue("CarpetaArchivosSubidos"));
                ProgramasGoresModels evaluaciones = new ProgramasGoresModels();
                estado = await evaluaciones.enviarComentariosJefatura(int.Parse(idPrograma), comentarios, User.Identity.GetUserId(), rutaArchivos);
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EnviarCalificacionPrograma(string idPrograma, string idBips)
        {
            string estado = string.Empty;
            try
            {
                string rutaArchivos = Server.MapPath(constantes.GetValue("CarpetaArchivosSubidos"));
                ProgramasGoresModels evaluaciones = new ProgramasGoresModels();
                estado = await evaluaciones.enviarCalificacionPrograma(int.Parse(idPrograma), int.Parse(idBips), User.Identity.GetUserId(), rutaArchivos);
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

    }
}