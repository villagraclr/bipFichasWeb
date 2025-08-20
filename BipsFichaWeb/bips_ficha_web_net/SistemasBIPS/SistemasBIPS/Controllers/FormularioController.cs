using log4net;
using MDS.Core.Providers;
using MDS.Dto;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using SistemasBIPS.Hubs;
using SistemasBIPS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SistemasBIPS.Controllers
{
    public class FormularioController : Controller
    {
        private FormulariosModels formularios = null;
        private IProviderConstante constantes = null;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public FormularioController()
        {
            this.formularios = new FormulariosModels();
            this.constantes = (IProviderConstante)Activator.CreateInstance(typeof(ProviderConstante));
        }

        [Authorize]
        public async Task<ActionResult> Index(string _id = null, string _tab = null, string _tabForm = null)
        {
            string id = EncriptaDatos.RijndaelSimple.Desencriptar(_id);            
            FormulariosViewModels viewModelFormulario = new FormulariosViewModels();
            var sesionID = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Sid).Value;
            if (await formularios.validaAccesoFormulario(int.Parse(id), User.Identity.GetUserId())){
                /*viewModelFormulario = await formularios.getFormulariosFiltro(int.Parse(id),
                                                                        (_tab == null ? 0 : int.Parse(EncriptaDatos.RijndaelSimple.Desencriptar(_tab))),
                                                                        (String.IsNullOrEmpty(_tabForm) ? string.Empty : EncriptaDatos.RijndaelSimple.Desencriptar(_tabForm)),
                                                                        User.Identity.GetUserId(),
                                                                        sesionID,
                                                                        User.Identity.Name);
                return View(viewModelFormulario);
                */
                viewModelFormulario = await formularios.getFormularios(int.Parse(id),
                                                                        (_tab == null ? 0 : int.Parse(EncriptaDatos.RijndaelSimple.Desencriptar(_tab))),
                                                                        (String.IsNullOrEmpty(_tabForm) ? string.Empty : EncriptaDatos.RijndaelSimple.Desencriptar(_tabForm)),
                                                                        User.Identity.GetUserId(),sesionID,User.Identity.Name);
                return View("Index2", viewModelFormulario);
                
            }
            else{
                return View("AccesoRestringido", new AccesoRestringido() { Vista = "Formulario" });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> GuardaDatos(string id, string data)
        {
            string estado = string.Empty;
            try
            {
                int idFormulario = int.Parse(EncriptaDatos.RijndaelSimple.Desencriptar(Server.UrlDecode(id)));
                List<Respuestas> respuestas = JsonConvert.DeserializeObject<List<Respuestas>>(data);                
                estado = await formularios.guardaRespuestas(respuestas, idFormulario, User.Identity.GetUserId());
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Formulario(string _id, string _tab, string _tabForm, string _enviar, string _enviarObs, FormulariosViewModels model)
        {            
            /*Dictionary<string, HttpPostedFileBase> listaArchivos = new Dictionary<string, HttpPostedFileBase>();
            if (this.HttpContext.Request.Files.Count > 0){
                foreach(string nombreArchivo in this.HttpContext.Request.Files){
                    HttpPostedFileBase archivo = this.HttpContext.Request.Files.Get(nombreArchivo);
                    listaArchivos.Add(nombreArchivo,archivo);                    
                }
            }*/
            int idFormulario = int.Parse(EncriptaDatos.RijndaelSimple.Desencriptar(Server.UrlDecode(_id)));
            if (_enviarObs == "1")
            {
                if (!String.IsNullOrEmpty(await formularios.enviarObservaciones(model, idFormulario, User.Identity.GetUserId())))
                    return RedirectToAction("Programas", "Programa", new { returnUrl = string.Empty });
                else
                    return View("Error", "Iterar con observaciones");
            }
            else if (_enviarObs == "2")
            {
                if (!String.IsNullOrEmpty(await formularios.sinObservaciones(model, idFormulario, User.Identity.GetUserId())))
                    return RedirectToAction("Programas", "Programa", new { returnUrl = string.Empty });
                else
                    return View("Error", "Cierre sin observaciones");
            }
            else if (_enviarObs == "3")
            {
                if (!String.IsNullOrEmpty(await formularios.guardaObservaciones(model, idFormulario, User.Identity.GetUserId())))
                    return RedirectToAction("Index", "Formulario", new { _id = Server.UrlDecode(_id), _tab = EncriptaDatos.RijndaelSimple.Encriptar(_tab), _tabForm = EncriptaDatos.RijndaelSimple.Encriptar(_tabForm) });
                else
                    return View("Error", "Guardar observaciones");
            }
            else if (_enviarObs == "4")
            {
                if (!String.IsNullOrEmpty(await formularios.cierreConObservaciones(model, idFormulario, User.Identity.GetUserId())))
                    return RedirectToAction("Programas", "Programa", new { returnUrl = string.Empty });
                else
                    return View("Error", "Cierre con observaciones");
            }
            else if (_enviarObs == "5")
            {
                if (await formularios.guardarFormulario(model, idFormulario, new Dictionary<string, HttpPostedFileBase>(), Server.MapPath(constantes.GetValue("CarpetaArchivosSubidos")), User.Identity.GetUserId()))
                    return RedirectToAction("Programas", "Programa", new { returnUrl = string.Empty });
                else
                    return View("Error", "Liberar programa");
            }
            else {
                bool estado = await formularios.guardarFormulario(model, idFormulario, new Dictionary<string, HttpPostedFileBase>(), Server.MapPath(constantes.GetValue("CarpetaArchivosSubidos")), User.Identity.GetUserId());
                if (_enviar != null)
                {
                    if (_enviar == "1"){
                        bool enviar = await formularios.enviarProgramaEvaluar(idFormulario, User.Identity.GetUserId());
                    }
                }
                return RedirectToAction("Index", "Formulario", new { _id = Server.UrlDecode(_id), _tab = EncriptaDatos.RijndaelSimple.Encriptar(_tab), _tabForm = EncriptaDatos.RijndaelSimple.Encriptar(_tabForm) });
            }
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Salir(string _idSalida = null)
        {
            LogUsuarioModels logUsuario = new LogUsuarioModels();
            int idFormulario = int.Parse(EncriptaDatos.RijndaelSimple.Desencriptar(Server.UrlDecode(_idSalida)));
            var sesionID = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Sid).Value;
            //TODO: MDC se liberan formularios tomados por usuario en esta sesion
            bool liberaForm = await logUsuario.actualizaLogFormularios(new TablaLogFormulariosDto() { IdFormulario = idFormulario, IdSesion = sesionID, IdUsuario = User.Identity.GetUserId(), EstadoAcceso = decimal.Parse(constantes.GetValue("Inactivo")), TipoUpdate = decimal.Parse(constantes.GetValue("UpdateLiberaUnForm")) });
            if (!liberaForm)
                log.Error("Error al liberar formulario de sesion");
            return RedirectToAction("Programas", "Programa", new { returnUrl = string.Empty });
        }        

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> EtapaEvaluacion(string _id)
        {
            string estado = string.Empty;
            try
            {
                int idFormulario = int.Parse(EncriptaDatos.RijndaelSimple.Desencriptar(Server.UrlDecode(_id)));
                estado = await formularios.etapaEvaluacion(idFormulario, User.Identity.GetUserId());
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GuardaEvaluacion(string id, List<PreguntasObservaciones> data)
        {
            string estado = string.Empty;
            try
            {
                int idFormulario = int.Parse(EncriptaDatos.RijndaelSimple.Desencriptar(Server.UrlDecode(id)));
                estado = await formularios.guardaEvaluacion(idFormulario, data, User.Identity.GetUserId());
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> GetFunciones (string _id)
        {
            int idFormulario = int.Parse(EncriptaDatos.RijndaelSimple.Desencriptar(Server.UrlDecode(_id)));
            IList<TablaFuncionesDependenciasDto> objFunciones = new List<TablaFuncionesDependenciasDto>();
            try
            {
                objFunciones = await formularios.getFuncionesFormularios(User.Identity.GetUserId(), idFormulario);
                int tipoPrograma = await formularios.getTipoPrograma(idFormulario);
                string rutaArchivo = Path.Combine(Server.MapPath(constantes.GetValue("CarpetaArchivosSubidos")), Path.GetFileName(string.Format("funciones_{0}.json", tipoPrograma)));
                if (System.IO.File.Exists(rutaArchivo))
                    System.IO.File.Delete(rutaArchivo);
                System.IO.File.WriteAllText(rutaArchivo, JsonConvert.SerializeObject(objFunciones, Formatting.None));
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
            return Content(JsonConvert.SerializeObject(objFunciones, Formatting.None));
        }
       
        [Authorize]
        public async Task<ActionResult> Post(int _id)
        {
            string estado = "ok";
            var hubContext = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<NotificacionesHub>();
            try{
                hubContext.Clients.All.liberar(_id);
                LogUsuarioModels logUsuario = new LogUsuarioModels();
                bool liberaForm = await logUsuario.actualizaLogFormularios(new TablaLogFormulariosDto() { IdFormulario = _id, TipoAcceso = decimal.Parse(constantes.GetValue("AccesoFormGuardado")), EstadoAcceso = decimal.Parse(constantes.GetValue("Inactivo")), TipoUpdate = decimal.Parse(constantes.GetValue("UpdateLiberaFormSignal")) });
                if (!liberaForm)
                    log.Error("Error al liberar formulario");
            }
            catch (Exception ex){
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<ActionResult> LiberaPrograma(int _id)
        {
            string estado = "ok";
            try {
                LogUsuarioModels logUsuario = new LogUsuarioModels();
                bool liberaForm = await logUsuario.actualizaLogFormularios(new TablaLogFormulariosDto() { IdFormulario = _id, TipoAcceso = decimal.Parse(constantes.GetValue("AccesoFormGuardado")), EstadoAcceso = decimal.Parse(constantes.GetValue("Inactivo")), TipoUpdate = decimal.Parse(constantes.GetValue("UpdateLiberaFormSignal")) });
                if (!liberaForm)
                    log.Error("Error al liberar formulario");
            }
            catch (Exception ex){
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> SalirPrograma(string _id)
        {
            string estado = "ok";
            try
            {
                LogUsuarioModels logUsuario = new LogUsuarioModels();
                int idFormulario = int.Parse(EncriptaDatos.RijndaelSimple.Desencriptar(Server.UrlDecode(_id)));
                bool liberaForm = await logUsuario.actualizaLogFormularios(new TablaLogFormulariosDto() { IdFormulario = idFormulario, TipoAcceso = decimal.Parse(constantes.GetValue("AccesoFormGuardado")), EstadoAcceso = decimal.Parse(constantes.GetValue("Inactivo")), TipoUpdate = decimal.Parse(constantes.GetValue("UpdateLiberaFormSignal")) });
                if (!liberaForm)
                    log.Error("Error al liberar formulario");
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreaFormulario(string _id)
        {
            NewFormulariosViewModels dataFormulario = new NewFormulariosViewModels ();
            try
            {
                int idFormulario = int.Parse(EncriptaDatos.RijndaelSimple.Desencriptar(Server.UrlDecode(_id)));
                int tipoPrograma = await formularios.getTipoPrograma(idFormulario);
                dataFormulario = await formularios.getFormularioJS(User.Identity.GetUserId(), tipoPrograma);
                
                string rutaArchivo = Path.Combine(Server.MapPath(constantes.GetValue("CarpetaArchivosSubidos")), Path.GetFileName(string.Format("formulario_{0}.json", tipoPrograma)));
                if (System.IO.File.Exists(rutaArchivo))
                    System.IO.File.Delete(rutaArchivo);
                System.IO.File.WriteAllText(rutaArchivo, JsonConvert.SerializeObject(dataFormulario, Formatting.None));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return View("Error", ex);
            }
            //return Json(dataFormulario, JsonRequestBehavior.AllowGet);
            return Content(JsonConvert.SerializeObject(dataFormulario, Formatting.None));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> BuscaRespuestas(string _id)
        {
            IList<TablaRespuestasDto> respuestas = new List<TablaRespuestasDto>();
            try
            {
                int idFormulario = int.Parse(EncriptaDatos.RijndaelSimple.Desencriptar(Server.UrlDecode(_id)));
                respuestas = await formularios.getRespuestasFiltro(new TablaRespuestasFiltroDto() { IdFormulario = idFormulario });
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return View("Error", ex);
            }
            return Content(JsonConvert.SerializeObject(respuestas, Formatting.None));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> BuscaExcepciones(string _id)
        {
            IList<TablaExcepcionesPreguntasDto> excepciones = new List<TablaExcepcionesPreguntasDto>();
            try
            {
                int idFormulario = int.Parse(EncriptaDatos.RijndaelSimple.Desencriptar(Server.UrlDecode(_id)));
                excepciones = await formularios.getExcepcionesPreguntas(new TablaExcepcionesPreguntasFiltroDto() { IdUsuario = User.Identity.GetUserId(), IdFormulario = idFormulario, Estado = decimal.Parse(constantes.GetValue("Activo")) });
                /*int tipoPrograma = await formularios.getTipoPrograma(idFormulario);
                string rutaArchivo = Path.Combine(Server.MapPath(constantes.GetValue("CarpetaArchivosSubidos")), Path.GetFileName(string.Format("excepciones_{0}.json", tipoPrograma)));
                if (System.IO.File.Exists(rutaArchivo))
                    System.IO.File.Delete(rutaArchivo);
                System.IO.File.WriteAllText(rutaArchivo, JsonConvert.SerializeObject(excepciones, Formatting.None));*/
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return View("Error", ex);
            }
            return Content(JsonConvert.SerializeObject(excepciones, Formatting.None));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> IterarEvaluar(string _id, List<int> pregIterar, Int16 opcion, int tipoPrograma)
        {
            string estado = "ok";
            try {
                int idFormulario = int.Parse(EncriptaDatos.RijndaelSimple.Desencriptar(Server.UrlDecode(_id)));
                estado = await formularios.iterarEvaluar(idFormulario, User.Identity.GetUserId(), pregIterar, opcion, tipoPrograma);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> revisionAnalista(string _id)
        {
            bool estado = false;
            try
            {
                int idFormulario = int.Parse(EncriptaDatos.RijndaelSimple.Desencriptar(Server.UrlDecode(_id)));
                estado = await formularios.enviarProgramaEvaluar(idFormulario, User.Identity.GetUserId());
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
        public async Task<ActionResult> ingresaComentario(string _id, int? idMenuP, int? idMenuH, int? idPreg, int? idT, int? idConsulta, string comentario)
        {
            TablaConsultasDto estado = new TablaConsultasDto();
            try {
                TablaConsultasDto data = new TablaConsultasDto();
                data.IdPrograma = int.Parse(EncriptaDatos.RijndaelSimple.Desencriptar(Server.UrlDecode(_id)));
                data.IdMenu = idMenuP;
                data.IdMenuHijo = idMenuH;
                data.IdPregunta = idPreg;
                data.IdTab = idT;
                data.IdUsuario = User.Identity.GetUserId();
                data.Consulta = comentario;
                data.IdConsulta = idConsulta;
                estado = await formularios.ingresarComentarios(data, User.Identity.GetUserId());
            }
            catch (Exception ex){
                log.Error(ex.Message, ex);
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> BuscaComentarios(string _id, int menu)
        {
            IList<TablaConsultasDto> comentarios = new List<TablaConsultasDto>();
            try
            {
                int idFormulario = int.Parse(EncriptaDatos.RijndaelSimple.Desencriptar(Server.UrlDecode(_id)));
                comentarios = await formularios.getConsultasFiltro(idFormulario, menu);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return View("Error", ex);
            }
            return Content(JsonConvert.SerializeObject(comentarios, Formatting.None));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> ingresaRespuesta(int idConsulta, string respuesta)
        {
            string estado = "ok";
            try
            {                
                estado = await formularios.ingresarRespuesta(idConsulta, User.Identity.GetUserId(), respuesta);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> BuscaRespuestasComentarios(int idConsulta)
        {
            IList<TablaRespuestasConsultasDto> respuestas = new List<TablaRespuestasConsultasDto>();
            try
            {                
                respuestas = await formularios.getRespuestasConsultasFiltro(idConsulta);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return View("Error", ex);
            }
            return Content(JsonConvert.SerializeObject(respuestas, Formatting.None));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> EnvioEvaluacion(string _id, int tipo)
        {
            string estado = "ok";
            try
            {
                int idFormulario = int.Parse(EncriptaDatos.RijndaelSimple.Desencriptar(Server.UrlDecode(_id)));
                estado = await formularios.enviaEvaluacion(idFormulario, tipo, User.Identity.GetUserId());
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return View("Error", ex);
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> BorrarComentario(int idConsulta)
        {
            string estado = "ok";
            try
            {
                estado = await formularios.BorrarComentario(idConsulta);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> GetComentarios(string _id)
        {
            IList<TablaConsultasDto> comentarios = new List<TablaConsultasDto>();
            try
            {
                int idFormulario = int.Parse(EncriptaDatos.RijndaelSimple.Desencriptar(Server.UrlDecode(_id)));
                comentarios = await formularios.getComentarios(idFormulario);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return View("Error", ex);
            }
            return Content(JsonConvert.SerializeObject(comentarios, Formatting.None));
        }
    }
}