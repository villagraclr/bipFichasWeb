using log4net;
using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Core.Providers;
using MDS.Dto;
using MDS.Svc;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

namespace SistemasBIPS.Models
{
    public class FormulariosModels
    {
        private static ISistemasBIPSSvc bips = null;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private IProviderConstante constantes = null;

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public FormulariosModels()
        {
            bips = (ISistemasBIPSSvc)Activator.CreateInstance(typeof(SistemasBIPSSvc));
            constantes = (IProviderConstante)Activator.CreateInstance(typeof(ProviderConstante));
        }

        /// <summary>
        /// Obtiene menu para un formulario determinado
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public Task<IList<TablaMenuFormulariosDto>> getMenuFiltro(TablaMenuFormulariosFiltroDto filtros)
        {
            IList<TablaMenuFormulariosDto> menu = new List<TablaMenuFormulariosDto>();
            try
            {
                menu = bips.BuscarMenuFormularios(new ContextoDto(), filtros).Dtos;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(menu);
        }

        private async Task<IList<PreguntasFormulariosDto>> getPreguntasFormularios(int idTipoFormulario, string idUsuario, int idFormulario)
        {
            IList<PreguntasFormulariosDto> objPreguntas = new List<PreguntasFormulariosDto>();
            try
            {
                ViewDto<PreguntasFormulariosDto> preguntas = new ViewDto<PreguntasFormulariosDto>();
                IList<TablaExcepcionesPreguntasDto> excepPregLectura = new List<TablaExcepcionesPreguntasDto>();
                excepPregLectura = await getExcepcionesPreguntas(new TablaExcepcionesPreguntasFiltroDto(){ IdUsuario = idUsuario, IdFormulario = idFormulario, TipoExcepcion = decimal.Parse(constantes.GetValue("GuardadoExcep")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                preguntas = bips.BuscarPreguntasFormularios(new ContextoDto(), new TablaPreguntasFormulariosFiltroDto() { IdTipoFormulario = idTipoFormulario, IdEstado = decimal.Parse(constantes.GetValue("Activo")) }, excepPregLectura);
                if (preguntas.HasElements())
                {                    
                    objPreguntas = preguntas.Dtos.ToList();
                    IList<TablaExcepcionesPreguntasDto> pregInvisibles = new List<TablaExcepcionesPreguntasDto>();
                    pregInvisibles = await getExcepcionesPreguntas(new TablaExcepcionesPreguntasFiltroDto() { IdUsuario = idUsuario, IdFormulario = idFormulario, TipoExcepcion = decimal.Parse(constantes.GetValue("ExcepPregVisible")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (pregInvisibles.Count > 0) {
                        foreach (var item in pregInvisibles) {
                            var quitar = objPreguntas.SingleOrDefault(p => p.idPregunta == item.IdPregunta);
                            objPreguntas.Remove(quitar);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return objPreguntas;
        }

        private async Task<IList<PreguntasFormulariosDto>> getPreguntasFormularios(int idTipoFormulario, string idUsuario)
        {
            IList<PreguntasFormulariosDto> objPreguntas = new List<PreguntasFormulariosDto>();
            try
            {
                ViewDto<PreguntasFormulariosDto> preguntas = new ViewDto<PreguntasFormulariosDto>();
                IList<TablaExcepcionesPreguntasDto> excepPregLectura = new List<TablaExcepcionesPreguntasDto>();
                excepPregLectura = await getExcepcionesPreguntas(new TablaExcepcionesPreguntasFiltroDto() { IdUsuario = idUsuario, IdTipoFormulario = idTipoFormulario, TipoExcepcion = decimal.Parse(constantes.GetValue("GuardadoExcep")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                preguntas = bips.BuscarPreguntasFormularios(new ContextoDto(), new TablaPreguntasFormulariosFiltroDto() { IdTipoFormulario = idTipoFormulario, IdEstado = decimal.Parse(constantes.GetValue("Activo")) }, excepPregLectura);
                if (preguntas.HasElements())
                {
                    objPreguntas = preguntas.Dtos.ToList();
                    IList<TablaExcepcionesPreguntasDto> pregInvisibles = new List<TablaExcepcionesPreguntasDto>();
                    pregInvisibles = await getExcepcionesPreguntas(new TablaExcepcionesPreguntasFiltroDto() { IdUsuario = idUsuario, IdTipoFormulario = idTipoFormulario, TipoExcepcion = decimal.Parse(constantes.GetValue("ExcepPregVisible")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (pregInvisibles.Count > 0)
                    {
                        foreach (var item in pregInvisibles)
                        {
                            var quitar = objPreguntas.SingleOrDefault(p => p.idPregunta == item.IdPregunta);
                            objPreguntas.Remove(quitar);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return objPreguntas;
        }

        public async Task<IList<TablaFuncionesDependenciasDto>> getFuncionesFormularios(string idUsuario, int idFormulario)
        {
            IList<TablaFuncionesDependenciasDto> objFunciones = new List<TablaFuncionesDependenciasDto>();
            try
            {
                int idTipoFormulario = int.Parse(bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idFormulario }).Dtos.FirstOrDefault().IdTipoFormulario.ToString());
                ViewDto<TablaFuncionesDependenciasDto> funciones = new ViewDto<TablaFuncionesDependenciasDto>();
                funciones = bips.BuscarFuncionesDependencias(new ContextoDto(), new TablaFuncionesDependenciasFiltroDto() { TipoFormulario = idTipoFormulario, IdEstado = decimal.Parse(constantes.GetValue("Activo")), TipoFuncion = decimal.Parse(constantes.GetValue("FuncionObligatoria")) });
                if (funciones.HasElements()) {
                    //Busco plantilla
                    ViewDto<TablaProgramasDto> programa = new ViewDto<TablaProgramasDto>();
                    programa = bips.BuscarPermisosFormularios(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idFormulario, IdUser = idUsuario, Estado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (programa.HasElements()) {
                        IList<TablaExcepcionesMenuDto> listaExcepMenu = new List<TablaExcepcionesMenuDto>();
                        listaExcepMenu = await getExcepcionesMenu(new TablaExcepcionesMenuFiltroDto() { IdExcepcionPlantilla = programa.Dtos.SingleOrDefault().IdExcepcion, Estado = decimal.Parse(constantes.GetValue("Activo")) });
                        if (listaExcepMenu.Count > 0)
                            foreach (var menu in listaExcepMenu)
                                funciones.Dtos.RemoveAll(p => p.IdMenu == menu.IdMenu);
                    }
                    //Busca menu restringidos por perfil
                    if (funciones.HasElements())
                    {
                        TablaUsuariosDto usuario = await getDatosUsuario(idUsuario);
                        ViewDto<TablaParametrosDto> data = new ViewDto<TablaParametrosDto>();
                        data = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("RestriccionMenu")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                        if (data.HasElements())
                        {
                            List<int> perfilesMenu = new List<int>();
                            foreach (var m in data.Dtos)
                            {
                                if (m.Valor != null)
                                    perfilesMenu.Add(int.Parse(m.Valor.ToString()));
                            }
                            if (!perfilesMenu.Contains(int.Parse(usuario.IdPerfil.ToString())))
                            {
                                foreach (var m in data.Dtos)
                                    if (m.Valor2 != null)
                                        funciones.Dtos.RemoveAll(p => p.IdMenu == m.Valor2);
                            }
                        }
                    }
                }
                if (funciones.HasElements()){
                    funciones.Dtos.ForEach(funcion => { if (funcion.ValorFuncion != null) { funcion.Datos = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = funcion.ValorFuncion, Estado = decimal.Parse(constantes.GetValue("Activo")) }).Dtos; } });

                    objFunciones = funciones.Dtos;
                    //objFunciones = await agregaFuncionesFormularios(funciones);
                }
            }
            catch (Exception ex) {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return objFunciones;
        }

        public async Task<IList<TablaFuncionesDependenciasDto>> getFuncionesFormulariosJSON(string idUsuario, int tipoFormulario)
        {
            IList<TablaFuncionesDependenciasDto> objFunciones = new List<TablaFuncionesDependenciasDto>();
            try
            {                
                ViewDto<TablaFuncionesDependenciasDto> funciones = new ViewDto<TablaFuncionesDependenciasDto>();
                funciones = bips.BuscarFuncionesDependencias(new ContextoDto(), new TablaFuncionesDependenciasFiltroDto() { TipoFormulario = tipoFormulario, IdEstado = decimal.Parse(constantes.GetValue("Activo")), TipoFuncion = decimal.Parse(constantes.GetValue("FuncionObligatoria")) });
                if (funciones.HasElements())
                {
                    //Busco plantilla
                    /*ViewDto<TablaProgramasDto> programa = new ViewDto<TablaProgramasDto>();
                    programa = bips.BuscarPermisosFormularios(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idFormulario, IdUser = idUsuario, Estado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (programa.HasElements())
                    {
                        IList<TablaExcepcionesMenuDto> listaExcepMenu = new List<TablaExcepcionesMenuDto>();
                        listaExcepMenu = await getExcepcionesMenu(new TablaExcepcionesMenuFiltroDto() { IdExcepcionPlantilla = programa.Dtos.SingleOrDefault().IdExcepcion, Estado = decimal.Parse(constantes.GetValue("Activo")) });
                        if (listaExcepMenu.Count > 0)
                            foreach (var menu in listaExcepMenu)
                                funciones.Dtos.RemoveAll(p => p.IdMenu == menu.IdMenu);
                    }*/
                    //Busca menu restringidos por perfil
                    if (funciones.HasElements())
                    {
                        TablaUsuariosDto usuario = await getDatosUsuario(idUsuario);
                        ViewDto<TablaParametrosDto> data = new ViewDto<TablaParametrosDto>();
                        data = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("RestriccionMenu")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                        if (data.HasElements())
                        {
                            List<int> perfilesMenu = new List<int>();
                            foreach (var m in data.Dtos)
                            {
                                if (m.Valor != null)
                                    perfilesMenu.Add(int.Parse(m.Valor.ToString()));
                            }
                            if (!perfilesMenu.Contains(int.Parse(usuario.IdPerfil.ToString())))
                            {
                                foreach (var m in data.Dtos)
                                    if (m.Valor2 != null)
                                        funciones.Dtos.RemoveAll(p => p.IdMenu == m.Valor2);
                            }
                        }
                    }
                }
                if (funciones.HasElements())
                {
                    funciones.Dtos.ForEach(funcion => { if (funcion.ValorFuncion != null) { funcion.Datos = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = funcion.ValorFuncion, Estado = decimal.Parse(constantes.GetValue("Activo")) }).Dtos; } });
                    objFunciones = funciones.Dtos;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return objFunciones;
        }

        private Task<IList<PreguntasFunciones>> agregaFuncionesFormularios(ViewDto<TablaFuncionesDependenciasDto> funciones)
        {
            IList<PreguntasFunciones> objFunciones = new List<PreguntasFunciones>();
            try {
                if (funciones.HasElements()) {
                    foreach (var funcion in funciones.Dtos) {
                        IList<TablaParametrosDto> datos = new List<TablaParametrosDto>();
                        if (funcion.ValorFuncion != null) {
                            datos = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = funcion.ValorFuncion, Estado = decimal.Parse(constantes.GetValue("Activo")) }).Dtos;
                        }
                        objFunciones.Add(new PreguntasFunciones() {
                            id = funcion.IdFuncionDependencia.ToString(),
                            idFuncion = funcion.IdFuncion.ToString(),
                            idEvento = funcion.IdEvento.ToString(),
                            categoriaEvento = funcion.IdCategoriaEvento.ToString(),
                            valorEvento = funcion.ValorEvento.ToString(),
                            valor2Evento = funcion.Valor2Evento.ToString(),
                            idPregunta = funcion.IdPregunta.ToString(),
                            tipoPregunta = funcion.TipoPregunta.ToString(),
                            valorPregunta = funcion.ValorPregunta.ToString(),
                            categoriaPregunta = funcion.CategoriaPregunta.ToString(),
                            idPreguntaDependiente = funcion.IdPreguntaDependiente.ToString(),
                            tipoPreguntaDependiente = funcion.TipoPreguntaDependiente.ToString(),
                            valorPreguntaDependiente = funcion.ValorPreguntaDependiente.ToString(),
                            categoriaPreguntaDependiente = funcion.CategoriaPreguntaDependiente.ToString(),
                            datos = datos
                        });
                    }
                }
            }
            catch (Exception ex) {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(objFunciones);
        }

        public Task<IList<TablaRespuestasDto>> getRespuestasFiltro(TablaRespuestasFiltroDto filtros)
        {
            IList<TablaRespuestasDto> respuestas = new List<TablaRespuestasDto>();
            try
            {
                respuestas = bips.BuscarRespuestas(new ContextoDto(), filtros).Dtos;
                if (respuestas == null || respuestas.Count == 0)
                    respuestas = new List<TablaRespuestasDto>();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(respuestas);
        }

        public Task<IList<TablaConsultasDto>> getConsultasFiltro(int idPrograma, int menu)
        {
            IList<TablaConsultasDto> comentarios = new List<TablaConsultasDto>();
            try
            {
                ViewDto<TablaConsultasDto> consultas = new ViewDto<TablaConsultasDto>();
                consultas = bips.BuscarConsultas(new ContextoDto(), new TablaConsultasDto() { IdPrograma = idPrograma, IdMenu = menu });
                if (!consultas.Sucess())
                    throw new Exception("Error al buscar consultas");
                if (consultas.HasElements())
                    comentarios = consultas.Dtos.OrderByDescending(p=>p.IdConsulta).ToList();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(comentarios);
        }

        private async Task<CabeceraFormulario> getCabecera(int idFormulario, string mensajeModo)
        {
            CabeceraFormulario cabecera = new CabeceraFormulario();
            try
            {
                ViewDto<TablaProgramasDto> datos = new ViewDto<TablaProgramasDto>();
                datos = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idFormulario });
                if (datos.HasElements())
                {
                    cabecera.nombre = datos.Dtos.FirstOrDefault().Nombre;
                    cabecera.tipo = datos.Dtos.FirstOrDefault().Tipo;
                    cabecera.ano = int.Parse(datos.Dtos.FirstOrDefault().Ano.ToString());
                    cabecera.modo = mensajeModo;
                    cabecera.idTipo = int.Parse(datos.Dtos.FirstOrDefault().IdTipoFormulario.ToString());
                    cabecera.version = await getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaVersionProgramas")), idFormulario);
                    cabecera.fechaEnvio = await getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaEnvioEvaluacion")), idFormulario);
                    cabecera.fechaModificacion = await getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaFecUltimaModif")), idFormulario);
                    //Datos usuario última modificación
                    string usuarioModif = await getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaUsuUltimaModif")), idFormulario);
                    TablaUsuariosDto nombreUsuario = new TablaUsuariosDto();
                    if (!String.IsNullOrEmpty(usuarioModif))
                        nombreUsuario = await getDatosUsuario(usuarioModif);
                    cabecera.usuarioModificacion = nombreUsuario.Nombre;
                    cabecera.idEtapa = datos.Dtos.FirstOrDefault().IdEtapa.ToString();                    
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return (cabecera);
        }

        public Task<TablaUsuariosDto> getDatosUsuario(string Usuario)
        {
            TablaUsuariosDto dato = new TablaUsuariosDto();
            try
            {
                ViewDto<TablaUsuariosDto> usuario = new ViewDto<TablaUsuariosDto>();
                usuario = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto { Id = Usuario });
                if (usuario.HasElements())
                    dato = usuario.Dtos.SingleOrDefault();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            return Task.FromResult(dato);
        }

        public Task<string> getRespuestasEvaluacion(int idPregunta, int idFormulario)
        {
            string dato = null;
            try {
                ViewDto<TablaRespuestasDto> respuesta = new ViewDto<TablaRespuestasDto>();
                respuesta = bips.BuscarRespuestas(new ContextoDto(), new TablaRespuestasFiltroDto() { IdFormulario = idFormulario, IdPregunta = idPregunta });
                if (respuesta.HasElements())
                    dato = respuesta.Dtos.FirstOrDefault().Respuesta.ToString();
            }
            catch (Exception ex) {
                log.Error(ex.Message, ex);
            }
            return Task.FromResult(dato);
        }

        private async Task<AccesoFormulario> getAcceso(string idUsuario, string idSesion, int idFormulario, string nombreUsuario)
        {
            AccesoFormulario accesoForm = new AccesoFormulario();
            try {
                //TODO: Se valida si usuario conectado tiene permiso de guardado sobre el formulario
                ViewDto<TablaLogFormulariosDto> buscarLogForm;
                LogUsuarioModels logUsuario = new LogUsuarioModels();
                bool logFormularios = true;
                if (await tienePermisoGuardado(idUsuario, idFormulario)) {
                    accesoForm = await registraAccesoFormulario(idUsuario, idFormulario, nombreUsuario, idSesion);
                } else {
                    buscarLogForm = new ViewDto<TablaLogFormulariosDto>();
                    buscarLogForm = bips.BuscarLogFormularios(new ContextoDto(), new TablaLogFormulariosFiltroDto()
                    {
                        EstadoAcceso = decimal.Parse(constantes.GetValue("Activo")),
                        IdFormulario = idFormulario,
                        IdUsuario = idUsuario,
                        IdSesion = idSesion,
                        TipoAcceso = decimal.Parse(constantes.GetValue("AccesoFormLecturaSinPerm"))
                    });
                    if (!buscarLogForm.HasElements())
                    {
                        logFormularios = await logUsuario.registraLogFormularios(new TablaLogFormulariosDto()
                        {
                            IdFormulario = idFormulario,
                            IdSesion = idSesion,
                            IdUsuario = idUsuario,
                            TipoAcceso = decimal.Parse(constantes.GetValue("AccesoFormLecturaSinPerm")),
                            EstadoAcceso = decimal.Parse(constantes.GetValue("Activo"))
                        });
                    }
                    accesoForm.nombreUsuario = nombreUsuario;
                    accesoForm.tipoAcceso = int.Parse(constantes.GetValue("AccesoFormLecturaSinPerm"));
                    accesoForm.mensaje = "Sin permiso para guardar datos";
                }
            }
            catch (Exception ex) {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return accesoForm;
        }

        public async Task<AccesoFormulario> registraAccesoFormulario(string idUsuario, int idFormulario, string nombreUsuario, string idSesion)
        {
            AccesoFormulario data = new AccesoFormulario();
            try {
                LogUsuarioModels logUsuario = new LogUsuarioModels();
                bool logFormularios = true;
                ViewDto<TablaLogFormulariosDto> buscarLogForm = new ViewDto<TablaLogFormulariosDto>();
                buscarLogForm = bips.BuscarLogFormularios(new ContextoDto(), new TablaLogFormulariosFiltroDto() {
                    EstadoAcceso = decimal.Parse(constantes.GetValue("Activo")),
                    IdFormulario = idFormulario
                });
                if (buscarLogForm.HasElements()) {
                    if (buscarLogForm.Dtos.Count(p => p.TipoAcceso == decimal.Parse(constantes.GetValue("AccesoFormGuardado"))) > 0) {
                        if (buscarLogForm.Dtos.Count(p => p.IdUsuario == idUsuario && p.TipoAcceso == decimal.Parse(constantes.GetValue("AccesoFormGuardado"))) == 0) {
                            if (buscarLogForm.Dtos.Count(p => p.IdUsuario == idUsuario && p.TipoAcceso == decimal.Parse(constantes.GetValue("AccesoFormGuardado"))) == 0) {
                                if (buscarLogForm.Dtos.Count(p => p.IdUsuario == idUsuario && p.TipoAcceso == decimal.Parse(constantes.GetValue("AccesoFormLectura"))) == 0) {
                                    logFormularios = await logUsuario.registraLogFormularios(new TablaLogFormulariosDto() {
                                        IdFormulario = idFormulario,
                                        IdSesion = idSesion,
                                        IdUsuario = idUsuario,
                                        TipoAcceso = decimal.Parse(constantes.GetValue("AccesoFormLectura")),
                                        EstadoAcceso = decimal.Parse(constantes.GetValue("Activo"))
                                    });
                                }
                                //Ingreso tipo de acceso actual al formulario para el usuario logeado
                                data.tipoAcceso = int.Parse(constantes.GetValue("AccesoFormLectura"));
                                data.nombreUsuario = await HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().GetEmailAsync(buscarLogForm.Dtos.FirstOrDefault(p => p.TipoAcceso == decimal.Parse(constantes.GetValue("AccesoFormGuardado"))).IdUsuario);
                                data.mensaje = string.Format("Sólo Lectura (en uso por: {0})", data.nombreUsuario);
                            }
                        } else {
                            data.tipoAcceso = int.Parse(constantes.GetValue("AccesoFormGuardado"));
                            data.nombreUsuario = nombreUsuario;
                            data.mensaje = "Edición";
                        }
                    } else {
                        logFormularios = await logUsuario.registraLogFormularios(new TablaLogFormulariosDto() {
                            IdFormulario = idFormulario,
                            IdSesion = idSesion,
                            IdUsuario = idUsuario,
                            TipoAcceso = decimal.Parse(constantes.GetValue("AccesoFormGuardado")),
                            EstadoAcceso = decimal.Parse(constantes.GetValue("Activo"))
                        });
                        data.tipoAcceso = int.Parse(constantes.GetValue("AccesoFormGuardado"));
                        data.nombreUsuario = nombreUsuario;
                        data.mensaje = "Edición";
                    }
                } else {
                    logFormularios = await logUsuario.registraLogFormularios(new TablaLogFormulariosDto() {
                        IdFormulario = idFormulario,
                        IdSesion = idSesion,
                        IdUsuario = idUsuario,
                        TipoAcceso = decimal.Parse(constantes.GetValue("AccesoFormGuardado")),
                        EstadoAcceso = decimal.Parse(constantes.GetValue("Activo"))
                    });
                    data.tipoAcceso = int.Parse(constantes.GetValue("AccesoFormGuardado"));
                    data.nombreUsuario = nombreUsuario;
                    data.mensaje = "Edición";
                }
                if (!logFormularios)
                    log.Error("Error al registrar log de formularios de sesion");
            }
            catch (Exception ex) {
                log.Error(ex.Message, ex);
            }
            return data;
        }

        public Task<bool> tienePermisoGuardado(string idUsuario, int idFormulario)
        {
            bool tienePermiso = false;
            try {
                ViewDto<TablaProgramasDto> formulario = new ViewDto<TablaProgramasDto>();
                formulario = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() {
                    IdPrograma = idFormulario,
                    Estado = decimal.Parse(constantes.GetValue("Activo"))
                }, EnumAccionRealizar.Buscar);
                if (formulario.HasElements()) {
                    ViewDto<TablaExcepcionesPreguntasDto> permiso = new ViewDto<TablaExcepcionesPreguntasDto>();
                    permiso = bips.BuscarExcepcionesPreguntas(new ContextoDto(), new TablaExcepcionesPreguntasFiltroDto()
                    {
                        IdFormulario = idFormulario,
                        IdUsuario = idUsuario,
                        Estado = decimal.Parse(constantes.GetValue("Activo"))
                    });
                    if (permiso.HasElements()) {
                        if (permiso.Dtos.FirstOrDefault().IdTipoFormulario == formulario.Dtos.FirstOrDefault().IdTipoFormulario)
                            tienePermiso = true;
                    }
                }
            } catch (Exception ex) {
                tienePermiso = false;
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(tienePermiso);
        }

        public Task<Boolean> calculaExcepcionesFormulario(string idUsuario, int idFormulario)
        {
            Boolean excepcion = false;
            try {
                bool permisoGuardado = false;
                ViewDto<TablaUsuariosDto> perfil = new ViewDto<TablaUsuariosDto>();
                perfil = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = idUsuario });
                if (perfil.HasElements()) {
                    ViewDto<TablaPerfilesDto> permisos = new ViewDto<TablaPerfilesDto>();
                    permisos = bips.BuscarPermisosPerfiles(new ContextoDto(), new TablaPerfilesFiltroDto() { IdPerfil = perfil.Dtos.SingleOrDefault().IdPerfil });
                    if (permisos.HasElements())
                        permisoGuardado = permisos.Dtos.Count(p => p.IdPermiso == decimal.Parse(constantes.GetValue("PermisoGuardadoFormularios"))) > 0 ? true : false;

                    ViewDto<TablaExcepcionesPermisosDto> listaExcepciones = new ViewDto<TablaExcepcionesPermisosDto>();
                    listaExcepciones = bips.BuscarExcepcionesPermisos(new ContextoDto(), new TablaExcepcionesPermisosFiltroDto() { IdPermiso = decimal.Parse(constantes.GetValue("PermisoGuardadoFormularios")), IdUsuario = idUsuario, IdFormulario = idFormulario, Estado = decimal.Parse(constantes.GetValue("Activo")) });
                    bool tieneExcepcion = listaExcepciones.HasElements() ? true : false;
                    excepcion = (permisoGuardado && tieneExcepcion) || (!permisoGuardado && !tieneExcepcion) ? true : false;
                }
            } catch (Exception ex) {
                excepcion = false;
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(excepcion);
        }

        public async Task<FormulariosViewModels> getFormulariosFiltro(int idFormulario, int tab, string tabForm, string idUsuario, string idSesion, string nombreUsuario)
        {
            FormulariosViewModels formulario = new FormulariosViewModels();
            try
            {
                int idTipoFormulario = int.Parse(bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idFormulario }).Dtos.FirstOrDefault().IdTipoFormulario.ToString());
                formulario.acceso = await getAcceso(idUsuario, idSesion, idFormulario, nombreUsuario);
                formulario.cabecera = await getCabecera(idFormulario, formulario.acceso.mensaje);
                formulario.menuPadres = await getMenuFormulario(idTipoFormulario, int.Parse(constantes.GetValue("NivelPadre")), idUsuario, idFormulario, formulario.cabecera.idEtapa);
                formulario.menuHijos = await getMenuFormulario(idTipoFormulario, int.Parse(constantes.GetValue("NivelHijo")), idUsuario, idFormulario, formulario.cabecera.idEtapa);
                formulario.preguntas = await getPreguntasFormularios(idTipoFormulario, idUsuario, idFormulario);
                formulario.respuestas = await getRespuestasFiltro(new TablaRespuestasFiltroDto() { IdFormulario = idFormulario });
                //formulario.funciones = await getFuncionesFormularios(idTipoFormulario, idUsuario, idFormulario);                
                formulario.tab = tab;
                formulario.tabForm = tabForm;
                //formulario.excepcionesMenu = await getExcepcionesMenu(new TablaExcepcionesMenuFiltroDto() { IdUsuario = idUsuario, IdFormulario = idFormulario, Estado = decimal.Parse(constantes.GetValue("Activo")) });
                formulario.excepcionesPreguntas = await getExcepcionesPreguntas(new TablaExcepcionesPreguntasFiltroDto() { IdUsuario = idUsuario, IdFormulario = idFormulario, Estado = decimal.Parse(constantes.GetValue("Activo")) });
                formulario.programasEvaluar = await getProgramasEvaluar();
                formulario.textoEnviarEvaluar = await getTextoEvaluacion(idTipoFormulario);
                formulario.enviaObservaciones = await getPermisoEnviarObservaciones(idUsuario);
                formulario.menuSinRevision = await getMenuSinRevision(idUsuario);
                formulario.enviaEvaluar = await getPermisoEvaluar(idUsuario, idTipoFormulario);
                formulario.textoEvaluar = await getTextoEvaluar(idTipoFormulario);
                formulario.preguntasEvaluacion = await getPreguntasEvaluacion(formulario.cabecera.idEtapa);
                formulario.idProgramaBota = idFormulario;
                if (formulario.cabecera.idEtapa == constantes.GetValue("EtapaEvaluacion"))
                    bips.RegistrarProgramas(new ContextoDto(), new TablaProgramasDto() { IdPrograma = idFormulario }, EnumAccionRealizar.EjecutarCalculoEficiencia);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return formulario;
        }

        public Task<int> getTipoPrograma(int idFormulario)
        {
            return Task.FromResult(int.Parse(bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idFormulario }).Dtos.FirstOrDefault().IdTipoFormulario.ToString()));
        }

        public async Task<FormulariosViewModels> getFormularios(int idFormulario, int tab, string tabForm, string idUsuario, string idSesion, string nombreUsuario)
        {
            FormulariosViewModels formulario = new FormulariosViewModels();
            try {                
                ViewDto<TablaProgramasDto> datosPrograma = new ViewDto<TablaProgramasDto>();
                datosPrograma = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idFormulario });
                //int idTipoFormulario = int.Parse(datosPrograma.Dtos.FirstOrDefault().IdTipoFormulario.ToString());
                formulario.acceso = await getAcceso(idUsuario, idSesion, idFormulario, nombreUsuario);
                formulario.cabecera = await getCabecera(idFormulario, formulario.acceso.mensaje);
                formulario.idProgramaBota = idFormulario;
                formulario.tab = tab;
                formulario.tabForm = tabForm;
                formulario.tipoPrograma = int.Parse(datosPrograma.Dtos.FirstOrDefault().IdTipoFormulario.ToString());//await getTipoPrograma(idFormulario);
                formulario.respuestas = await getRespuestasFiltro(new TablaRespuestasFiltroDto() { IdFormulario = idFormulario });
                formulario.programasEvaluar = await getProgramasEvaluar();
                formulario.plantillaBase = await getPermisoBase(idUsuario, formulario.tipoPrograma);
                formulario.permisoAbreCampos = await getPermisoAbrirCamposEditar(idUsuario);
                formulario.rolUsuario = await getRolUsuario(idUsuario);
                formulario.textoEnviarEvaluar = await getTextoEvaluacion(formulario.tipoPrograma);
                formulario.coordinadores = await getListaParametros(new TablaParametrosFiltroDto() { IdCategoria = int.Parse(constantes.GetValue("CoordinadoresMinisteriales")), Valor = formulario.rolUsuario, Estado = decimal.Parse(constantes.GetValue("Activo")) });//getRespuestasFiltro(new TablaRespuestasFiltroDto() { IdFormulario = idFormulario, IdPregunta = int.Parse(constantes.GetValue("EmailSectorialistas")) });
                formulario.datosUsuario = await getDatosUsuario(idUsuario);
                formulario.informesEvaluacion = await getListaParametros(new TablaParametrosFiltroDto() { IdCategoria = int.Parse(constantes.GetValue("LinksInformesEval")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                formulario.idBips = int.Parse(datosPrograma.Dtos.FirstOrDefault().IdBips.ToString());
                formulario.versiones = await getVersiones(formulario.idBips, formulario.cabecera.ano, formulario.tipoPrograma);
                formulario.contrapartes = await getListaParametros(new TablaParametrosFiltroDto() { IdCategoria = int.Parse(constantes.GetValue("PerfilesEnviarRevision")), Valor = formulario.rolUsuario, Estado = decimal.Parse(constantes.GetValue("Activo")) });//getRespuestasFiltro(new TablaRespuestasFiltroDto() { IdFormulario = idFormulario, IdPregunta = int.Parse(constantes.GetValue("EmailContrapartes")) });
                formulario.permisoVerInformes = await getVerInformes(formulario.tipoPrograma, formulario.rolUsuario);
                formulario.validaciones = await getListaParametros(new TablaParametrosFiltroDto() { IdCategoria = int.Parse(constantes.GetValue("ValidacionesForm")), Valor2 = formulario.tipoPrograma, Estado = decimal.Parse(constantes.GetValue("Activo")) });
                formulario.tipoFormulario = await getTipoFormulario(formulario.tipoPrograma);
                formulario.comentarios = await getComentarios(idFormulario);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return formulario;
        }

        public Task<List<TablaConsultasDto>> getComentarios(int idPrograma)
        {
            List<TablaConsultasDto> retorno = new List<TablaConsultasDto>();
            try
            {
                ViewDto<TablaConsultasDto> data = new ViewDto<TablaConsultasDto>();
                data = bips.BuscarConsultas(new ContextoDto(), new TablaConsultasDto() { IdPrograma = idPrograma, Estado = decimal.Parse(constantes.GetValue("Activo")) });
                if (data.HasElements())
                    retorno = data.Dtos.OrderBy(p=>p.Fecha).ToList();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(retorno);
        }

        public Task<int> getTipoFormulario(int tipoPrograma)
        {
            int tipo = 0;
            try {
                ViewDto<TablaParametrosDto> tipoFormulario = new ViewDto<TablaParametrosDto>();
                tipoFormulario = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdParametro = tipoPrograma });
                if (tipoFormulario.HasElements())
                    tipo = int.Parse(tipoFormulario.Dtos.FirstOrDefault().Valor.ToString());
            }
            catch(Exception ex) {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(tipo);
        }

        public Task<Boolean> getVerInformes(int tipoPrograma, int rolUsuario)
        {
            Boolean permiso = false;
            try {
                ViewDto<TablaParametrosDto> data = new ViewDto<TablaParametrosDto>();
                data = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("PermisoVerInformes")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                if (data.HasElements())
                    if (data.Dtos.Count(p => p.Valor == rolUsuario && p.Valor2 == tipoPrograma) > 0)
                        permiso = true;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(permiso);
        }

        public Task<Dictionary<int, int>> getVersiones(int idBips, int ano, int tipo)
        {
            Dictionary<int, int> versiones = new Dictionary<int, int>();
            try {
                ViewDto<TablaProgramasDto> buscarVersiones = new ViewDto<TablaProgramasDto>();
                buscarVersiones  = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdBips = idBips, Ano = ano, TipoFormulario = tipo });
                if (buscarVersiones.HasElements())
                {
                    Dictionary<int, int> verProg = new Dictionary<int, int>();
                    int i = 1;
                    foreach(var item in buscarVersiones.Dtos.Where(p=>p.IdEstado != int.Parse(constantes.GetValue("Inactivo"))).OrderBy(p => p.IdPrograma))
                    {
                        versiones.Add(int.Parse(item.IdPrograma.ToString()), i);
                        i++;
                    }
                }                    
            } catch (Exception ex) {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(versiones);
        }

        public async Task<int> getRolUsuario(string idUsuario)
        {
            int rol = 0;
            try {
                TablaUsuariosDto usuario = await getDatosUsuario(idUsuario);
                rol = int.Parse(usuario.IdPerfil.ToString());
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return rol;
        } 

        public async Task<NewFormulariosViewModels> getFormularioJS(string idUsuario, int idTipoFormulario)
        {
            NewFormulariosViewModels dataFormulario = new NewFormulariosViewModels();
            try {
                //int idTipoFormulario = int.Parse(bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idFormulario }).Dtos.FirstOrDefault().IdTipoFormulario.ToString());
                //ViewDto<TablaProgramasDto> datos = new ViewDto<TablaProgramasDto>();
                //datos = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idFormulario });
                dataFormulario.menu = await getMenu(idTipoFormulario, idUsuario);
            }
            catch (Exception ex){
                log.Error(ex.Message, ex);
                throw ex;
            }
            return dataFormulario;
        }

        public async Task<IList<Menu>> getMenu(int idTipoFormulario, string idUsuario)
        {
            IList<Menu> menu = new List<Menu>();
            try {
                IList<TablaMenuFormulariosDto> padres = await getMenuFormulario(idTipoFormulario, int.Parse(constantes.GetValue("NivelPadre")), idUsuario);
                IList<PreguntasFormulariosDto> preguntas = await getPreguntasFormularios(idTipoFormulario, idUsuario);
                if (padres.Count > 0)
                {
                    IList<TablaMenuFormulariosDto> hijos = await getMenuFormulario(idTipoFormulario, int.Parse(constantes.GetValue("NivelHijo")), idUsuario);
                    foreach(var padre in padres)
                    {
                        Menu item = new Menu();
                        item.menuPadre = padre;
                        if (hijos.Count > 0)
                        {
                            if (hijos.Count(p=>p.IdPadre==padre.IdTipoMenu) > 0)
                            {
                                IList<PreguntasMenu> preguntasMenu = new List<PreguntasMenu>();
                                foreach (var hijo in hijos.Where(p => p.IdPadre == padre.IdTipoMenu).ToList())
                                {
                                    PreguntasMenu preguntaMenu = new PreguntasMenu();
                                    preguntaMenu.menuHijo = hijo;
                                    if (preguntas.Count > 0){
                                        if (preguntas.Count(p => p.menu == hijo.IdTipoMenu) > 0){
                                            preguntaMenu.preguntas = preguntas.Where(p => p.menu == hijo.IdTipoMenu).ToList();
                                        }
                                    }
                                    preguntasMenu.Add(preguntaMenu);
                                }
                                item.menuHijo = preguntasMenu;
                            }                                 
                        }
                        menu.Add(item);
                    }                    
                }                
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return menu;
        }

        public Task<List<TablaParametrosDto>> getPreguntasEvaluacion(string idEtapa)
        {
            List<TablaParametrosDto> data = new List<TablaParametrosDto>();
            try {
                if (idEtapa == constantes.GetValue("EtapaEvaluacion"))
                {
                    ViewDto<TablaParametrosDto> preguntas = new ViewDto<TablaParametrosDto>();
                    preguntas = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("PreguntasEvaluacion")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (preguntas.HasElements())
                        data = preguntas.Dtos;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(data);
        }

        public Task<String> getTextoEvaluar(int idTipoFormulario)
        {
            String texto = string.Empty;
            try
            {
                ViewDto<TablaParametrosDto> data = new ViewDto<TablaParametrosDto>();
                data = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("TextoEvaluar")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                if (data.HasElements())
                    if (data.Dtos.Count(p => p.Valor == idTipoFormulario) > 0)
                        texto = data.Dtos.FirstOrDefault(p => p.Valor == idTipoFormulario).Descripcion;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(texto);
        }

        public async Task<Boolean> getPermisoEvaluar(string idUsuario, int idTipoFormulario)
        {
            Boolean acceso = false;
            try
            {
                TablaUsuariosDto usuario = await getDatosUsuario(idUsuario);
                ViewDto<TablaParametrosDto> data = new ViewDto<TablaParametrosDto>();
                data = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("EnviaEvaluar")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                if (data.HasElements())
                    if (data.Dtos.Count(p => p.Valor == usuario.IdPerfil) > 0){
                        ViewDto<TablaParametrosDto> formularios = new ViewDto<TablaParametrosDto>();
                        formularios = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("FormulariosEvaluar")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                        if (formularios.HasElements()){
                            if (formularios.Dtos.Count(p => p.Valor == idTipoFormulario) > 0){
                                acceso = true;
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return (acceso);
        }

        public async Task<IList<TablaExcepcionesPlantillasFormDto>> getPermisoBase(string idUsuario, int idTipoFormulario)
        {
            IList<TablaExcepcionesPlantillasFormDto> retorno = new List<TablaExcepcionesPlantillasFormDto>();
            try
            {
                ViewDto<TablaParametrosDto> data = new ViewDto<TablaParametrosDto>();
                data = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("PlantillasBaseMonitoreos")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                if (data.HasElements()){
                    if (data.Dtos.Count(p => p.IdParametro != p.IdCategoria && p.Valor2 == idTipoFormulario) > 0)
                        retorno = await getExcepcionesPlantillas(new TablaExcepcionesPlantillasFormDto() { IdExcepcionPlantilla = data.Dtos.Where(p=>p.IdParametro != p.IdCategoria && p.Valor2 == idTipoFormulario).SingleOrDefault().Valor, Estado = decimal.Parse(constantes.GetValue("Activo")) });
                }                    
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return (retorno);
        }

        public Task<List<TablaParametrosDto>> getMenuSinRevision(string idUsuario)
        {
            List<TablaParametrosDto> retorno = new List<TablaParametrosDto>();
            try
            {
                ViewDto<TablaParametrosDto> data = new ViewDto<TablaParametrosDto>();
                data = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("MenuSinRevision")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                if (data.HasElements())
                    retorno = data.Dtos;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(retorno);
        }

        public async Task<Boolean> getPermisoAbrirCamposEditar(string idUsuario)
        {
            Boolean retorno = false;
            try
            {
                ViewDto<TablaParametrosDto> data = new ViewDto<TablaParametrosDto>();
                data = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("RolesAsignaCamposAbiertos")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                TablaUsuariosDto usuario = await getDatosUsuario(idUsuario);
                if (data.HasElements())
                    retorno = (data.Dtos.Count(p=>p.Valor == usuario.IdPerfil) > 0 ? true : false);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return (retorno);
        }

        public async Task<Boolean> getPermisoEnviarObservaciones(string idUsuario)
        {
            Boolean acceso = false;
            try {
                TablaUsuariosDto usuario = await getDatosUsuario(idUsuario);
                ViewDto<TablaParametrosDto> data = new ViewDto<TablaParametrosDto>();
                data = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("EnviaObservaciones")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                if (data.HasElements())
                    if (data.Dtos.Count(p => p.Valor == usuario.IdPerfil) > 0)
                        acceso = true;
            }
            catch(Exception ex){
                log.Error(ex.Message, ex);
                throw ex;
            }
            return (acceso);
        }

        public Task<String> getTextoEvaluacion(int idTipoFormulario)
        {
            String texto = "Al solicitar la evaluación de este programa no se podrán hacer más cambios al formulario. Para que la solicitud sea válida, <strong>esta debe ser enviada por el Coordinador/a Ministerial en la primera versión y por la Contraparte Técnica desde la segunda en adelante</strong>.<br />Al presionar 'Cancelar', no enviará el programa a evaluación y podrá seguir cargando o modificando la información.<br />Al presionar 'Aceptar', enviará el programa a evaluación.<br />¿Está seguro que desea enviar el programa a Evaluación Ex Ante?";
            try
            {
                ViewDto<TablaParametrosDto> data = new ViewDto<TablaParametrosDto>();
                data = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto(){ IdCategoria = decimal.Parse(constantes.GetValue("IdTextoEnvioEvaluar")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                if (data.HasElements())
                    if (data.Dtos.Count(p=>p.Valor == idTipoFormulario) > 0)
                        texto = data.Dtos.FirstOrDefault(p=>p.Valor == idTipoFormulario).Descripcion;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(texto);
        } 

        public Task<IList<Nullable<Decimal>>> getProgramasEvaluar()
        {
            IList<Nullable<Decimal>> programas = new List<Nullable<Decimal>>();
            try {
                ViewDto<TablaParametrosDto> buscar = new ViewDto<TablaParametrosDto>();
                buscar = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() {
                    IdCategoria = decimal.Parse(constantes.GetValue("ProgramasEvaluar")),
                    Estado = decimal.Parse(constantes.GetValue("Activo"))
                });
                if (buscar.HasElements())
                    foreach (var item in buscar.Dtos)
                        if (item.IdParametro != item.IdCategoria)
                            programas.Add(item.Valor);
            }
            catch(Exception ex) {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(programas);
        }

        public async Task<IList<TablaMenuFormulariosDto>> getMenuFormulario(int idTipoFormulario, int nivel, string idUsuario)
        {
            IList<TablaMenuFormulariosDto> listaMenuFormulario = new List<TablaMenuFormulariosDto>();
            try
            {
                listaMenuFormulario = await getMenuFiltro(new TablaMenuFormulariosFiltroDto() { IdTipoFormulario = idTipoFormulario, Nivel = nivel, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                if (listaMenuFormulario.Count > 0)
                {
                    //Busco plantilla
                    /*ViewDto<TablaProgramasDto> programa = new ViewDto<TablaProgramasDto>();
                    programa = bips.BuscarPermisosFormularios(new ContextoDto(), new TablaProgramasFiltroDto() { IdUser = idUsuario, TipoFormulario = idTipoFormulario, Estado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (programa.HasElements())
                    {
                        IList<TablaExcepcionesMenuDto> listaExcepMenu = new List<TablaExcepcionesMenuDto>();
                        listaExcepMenu = await getExcepcionesMenu(new TablaExcepcionesMenuFiltroDto() { IdExcepcionPlantilla = programa.Dtos.SingleOrDefault().IdExcepcion, Estado = decimal.Parse(constantes.GetValue("Activo")) });
                        if (listaExcepMenu.Count > 0)
                        {
                            foreach (var item in listaExcepMenu)
                            {
                                var excepcion = listaMenuFormulario.SingleOrDefault(p => p.IdMenu == item.IdMenu);
                                listaMenuFormulario.Remove(excepcion);
                            }
                        }
                    }*/
                    //Busco permiso para evaluar usuario
                    bool permisoUsuario = await getPermisoEvaluar(idUsuario, idTipoFormulario);
                    //Busco menu condicionantes a etapas
                    if (listaMenuFormulario.Count > 0)
                    {
                        if (!permisoUsuario)
                        {
                            var menuCondicionante = listaMenuFormulario.SingleOrDefault(p => p.IdTipoMenu == decimal.Parse(constantes.GetValue("MenuEvaluacion")));
                            listaMenuFormulario.Remove(menuCondicionante);
                        }
                    }
                    //Busca menu restringidos por perfil
                    if (listaMenuFormulario.Count > 0)
                    {
                        TablaUsuariosDto usuario = await getDatosUsuario(idUsuario);
                        ViewDto<TablaParametrosDto> data = new ViewDto<TablaParametrosDto>();
                        data = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("RestriccionMenu")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                        if (data.HasElements())
                        {
                            List<int> perfilesMenu = new List<int>();
                            foreach (var m in data.Dtos)
                            {
                                if (m.Valor != null)
                                    perfilesMenu.Add(int.Parse(m.Valor.ToString()));
                            }
                            if (!perfilesMenu.Contains(int.Parse(usuario.IdPerfil.ToString())))
                            {
                                foreach (var m in data.Dtos)
                                {
                                    if (m.Valor2 != null)
                                    {
                                        var excep = listaMenuFormulario.SingleOrDefault(p => p.IdMenu == m.Valor2);
                                        listaMenuFormulario.Remove(excep);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return listaMenuFormulario;
        }

        public async Task<IList<TablaMenuFormulariosDto>> getMenuFormulario(int idTipoFormulario, int nivel, string idUsuario, int idFormulario, string idEtapa)
        {
            IList<TablaMenuFormulariosDto> listaMenuFormulario = new List<TablaMenuFormulariosDto>();
            try{
                listaMenuFormulario = await getMenuFiltro(new TablaMenuFormulariosFiltroDto() { IdTipoFormulario = idTipoFormulario, Nivel = nivel, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                if (listaMenuFormulario.Count > 0){
                    //Busco plantilla
                    ViewDto<TablaProgramasDto> programa = new ViewDto<TablaProgramasDto>();
                    programa = bips.BuscarPermisosFormularios(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idFormulario, IdUser = idUsuario, Estado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (programa.HasElements()){
                        IList<TablaExcepcionesMenuDto> listaExcepMenu = new List<TablaExcepcionesMenuDto>();
                        listaExcepMenu = await getExcepcionesMenu(new TablaExcepcionesMenuFiltroDto() { IdExcepcionPlantilla = programa.Dtos.SingleOrDefault().IdExcepcion, Estado = decimal.Parse(constantes.GetValue("Activo")) });
                        if (listaExcepMenu.Count > 0)
                        {
                            foreach (var item in listaExcepMenu)
                            {
                                var excepcion = listaMenuFormulario.SingleOrDefault(p => p.IdMenu == item.IdMenu);
                                listaMenuFormulario.Remove(excepcion);
                            }
                        }
                    }
                    //Busco permiso para evaluar usuario
                    bool permisoUsuario = await getPermisoEvaluar(idUsuario, idTipoFormulario);
                    //Busco menu condicionantes a etapas
                    if (listaMenuFormulario.Count > 0) {
                        if (idEtapa != constantes.GetValue("EtapaEvaluacion") || !permisoUsuario)
                        {
                            var menuCondicionante = listaMenuFormulario.SingleOrDefault(p => p.IdTipoMenu == decimal.Parse(constantes.GetValue("MenuEvaluacion")));
                            listaMenuFormulario.Remove(menuCondicionante);
                        }
                    }
                    //Busca menu restringidos por perfil
                    if (listaMenuFormulario.Count > 0) {
                        TablaUsuariosDto usuario = await getDatosUsuario(idUsuario);
                        ViewDto<TablaParametrosDto> data = new ViewDto<TablaParametrosDto>();
                        data = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("RestriccionMenu")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                        if (data.HasElements()){
                            List<int> perfilesMenu = new List<int>();
                            foreach(var m in data.Dtos){
                                if (m.Valor != null)
                                    perfilesMenu.Add(int.Parse(m.Valor.ToString()));                                  
                            }
                            if (!perfilesMenu.Contains(int.Parse(usuario.IdPerfil.ToString()))){
                                foreach (var m in data.Dtos) {
                                    if (m.Valor2 != null){
                                        var excep = listaMenuFormulario.SingleOrDefault(p => p.IdMenu == m.Valor2);
                                        listaMenuFormulario.Remove(excep);
                                    }                                    
                                }
                            }                            
                        }                            
                    }
                }                    
            }catch (Exception ex){
                log.Error(ex.Message, ex);
                throw ex;
            }
            return listaMenuFormulario;
        }

        public Task<IList<TablaExcepcionesPlantillasFormDto>> getExcepcionesPlantillas(TablaExcepcionesPlantillasFormDto filtros)
        {
            IList<TablaExcepcionesPlantillasFormDto> listaExcepMenu = new List<TablaExcepcionesPlantillasFormDto>();
            try
            {
                ViewDto<TablaExcepcionesPlantillasFormDto> data = new ViewDto<TablaExcepcionesPlantillasFormDto>();
                data = bips.BuscarExcepcionesPlantilla(new ContextoDto(), filtros);
                if (data.HasElements())
                    listaExcepMenu = data.Dtos;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(listaExcepMenu);
        }

        public Task<IList<TablaExcepcionesPreguntasDto>> getExcepcionesPreguntas(TablaExcepcionesPreguntasFiltroDto filtros)
        {
            IList<TablaExcepcionesPreguntasDto> listaExcepMenu = new List<TablaExcepcionesPreguntasDto>();
            try{
                ViewDto<TablaExcepcionesPreguntasDto> data = new ViewDto<TablaExcepcionesPreguntasDto>();
                data = bips.BuscarExcepcionesPreguntas(new ContextoDto(), filtros);
                if (data.HasElements())
                    listaExcepMenu = data.Dtos;
            }catch (Exception ex){
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(listaExcepMenu);
        }

        public Task<IList<TablaExcepcionesMenuDto>> getExcepcionesMenu(TablaExcepcionesMenuFiltroDto filtros)
        {
            IList<TablaExcepcionesMenuDto> listaExcepMenu = new List<TablaExcepcionesMenuDto>();
            try {
                ViewDto<TablaExcepcionesMenuDto> data = new ViewDto<TablaExcepcionesMenuDto>();
                data = bips.BuscarExcepcionesMenu(new ContextoDto(), filtros);
                if (data.HasElements())
                    listaExcepMenu = data.Dtos;
            }
            catch (Exception ex){
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(listaExcepMenu);
        }

        public async Task<String> guardaRespuestas(List<Respuestas> respuestas, int idFormulario, string idUsuario)
        {
            String estado = "ok";
            try
            {                
                if (respuestas.Count > 0)
                {
                    bool guardaDatos = false;
                    IList<TablaExcepcionesPreguntasDto> excepPreguntas = new List<TablaExcepcionesPreguntasDto>();
                    excepPreguntas = await getExcepcionesPreguntas(new TablaExcepcionesPreguntasFiltroDto() { IdUsuario = idUsuario, IdFormulario = idFormulario, TipoExcepcion = decimal.Parse(constantes.GetValue("GuardadoExcep")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (excepPreguntas.Count > 0) {
                        ViewDto<TablaRespuestasDto> guardado;
                        ViewDto<TablaRespuestasDto> borrado;
                        //Respaldo respuestas
                        ViewDto<TablaRespuestasDto> respaldo = new ViewDto<TablaRespuestasDto>();
                        respaldo = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormResp")) }, EnumAccionRealizar.Insertar);
                        if (!respaldo.Sucess())
                            throw new Exception("error guardado (respaldo)");
                        foreach (var p in excepPreguntas){
                            if (respuestas.Count(i => i.idPregunta == p.IdPregunta.ToString()) > 0) {
                                var dataGrupo = respuestas.Where(i => i.idPregunta == p.IdPregunta.ToString()).Select(i => i);
                                if (dataGrupo.Count() > 0) {
                                    foreach (var data in dataGrupo)
                                    {
                                        //Borrado
                                        borrado = new ViewDto<TablaRespuestasDto>();
                                        borrado = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = decimal.Parse(data.idPregunta), IdTab = (String.IsNullOrEmpty(data.idTab.Trim()) ? (Nullable<Decimal>)null : decimal.Parse(data.idTab)), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                                        if (!borrado.Sucess())
                                            throw new Exception("error guardado (borrado)");
                                        //Guardado                                
                                        if (!String.IsNullOrEmpty(data.respuesta))
                                        {
                                            guardado = new ViewDto<TablaRespuestasDto>();
                                            guardado = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = decimal.Parse(data.idPregunta), Respuesta = data.respuesta.Trim(), IdTab = (String.IsNullOrEmpty(data.idTab.Trim()) ? (Nullable<Decimal>)null : decimal.Parse(data.idTab)), TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                                            if (!guardado.Sucess())
                                                throw new Exception("error guardado");
                                        }
                                    }
                                    guardaDatos = true;
                                }
                            }
                        }
                    }
                    //Busco roles que no deben registrar guardado
                    TablaUsuariosDto usuario = await getDatosUsuario(idUsuario);
                    ViewDto<TablaParametrosDto> perfilNoGuardar = new ViewDto<TablaParametrosDto>();
                    perfilNoGuardar = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("NoGuardaModifi")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (perfilNoGuardar.HasElements())
                        if (perfilNoGuardar.Dtos.Count(p => p.Valor == usuario.IdPerfil) > 0)
                            guardaDatos = false;

                    if (guardaDatos)
                    {
                        ViewDto<TablaRespuestasDto> ultimoRegistro = new ViewDto<TablaRespuestasDto>();
                        ultimoRegistro = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaFecUltimaModif")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                        if (!ultimoRegistro.Sucess())
                            throw new Exception("error guardado (borrado último registro)");
                        ultimoRegistro = new ViewDto<TablaRespuestasDto>();
                        ultimoRegistro = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaUsuUltimaModif")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                        if (!ultimoRegistro.Sucess())
                            throw new Exception("error guardado (borrado usuario último registro)");
                        ultimoRegistro = new ViewDto<TablaRespuestasDto>();
                        ultimoRegistro = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaFecUltimaModif")), Respuesta = DateTime.Now, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                        if (!ultimoRegistro.Sucess())
                            throw new Exception("error guardado (fecha último registro)");
                        ultimoRegistro = new ViewDto<TablaRespuestasDto>();
                        ultimoRegistro = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaUsuUltimaModif")), Respuesta = idUsuario, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                        if (!ultimoRegistro.Sucess())
                            throw new Exception("error guardado (usuario último registro)");
                    }
                }
            }
            catch (Exception ex)
            {
                estado = ex.Message;
                log.Error(ex.Message, ex);
            }
            return estado;
        }

        public async Task<Boolean> guardarFormulario(FormulariosViewModels datos, int idFormulario, Dictionary<string,HttpPostedFileBase> listaArchivos, string rutaArchivos, string idUsuario)
        {
            Boolean estado = true;
            try
            {         
                if (datos.preguntas.Count > 0)
                {
                    Int16 guardadoDatos = 0;
                    IList<TablaExcepcionesPreguntasDto> excepPreguntas = new List<TablaExcepcionesPreguntasDto>();
                    excepPreguntas = await getExcepcionesPreguntas(new TablaExcepcionesPreguntasFiltroDto() { IdUsuario = idUsuario, IdFormulario = idFormulario, TipoExcepcion = decimal.Parse(constantes.GetValue("GuardadoExcep")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (excepPreguntas.Count > 0){
                        ViewDto<TablaRespuestasDto> guardado;
                        ViewDto<TablaRespuestasDto> borrado;                        
                        //Respaldo respuestas
                        ViewDto<TablaRespuestasDto> respaldo = new ViewDto<TablaRespuestasDto>();
                        respaldo = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormResp")) }, EnumAccionRealizar.Insertar);
                        if (!respaldo.Sucess())
                            throw new Exception("error guardado (respaldo)");
                        foreach (var p in excepPreguntas){
                            if (datos.preguntas.Count(i=>i.id == p.IdPregunta) > 0){
                                var dataGrupo = datos.preguntas.Where(i => i.id == p.IdPregunta).Select(i=>i);
                                if (dataGrupo.Count() > 0){
                                    foreach(var data in dataGrupo){
                                        //Borrado
                                        borrado = new ViewDto<TablaRespuestasDto>();
                                        borrado = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto()
                                        {
                                            IdFormulario = idFormulario,
                                            IdPregunta = data.id,
                                            IdTab = data.idTab,
                                            TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv"))
                                        }, EnumAccionRealizar.Eliminar);
                                        if (!borrado.Sucess())
                                            throw new Exception("error guardado (borrado)");
                                        //Guardado                                
                                        if (!String.IsNullOrEmpty(data.respuesta)){
                                            guardado = new ViewDto<TablaRespuestasDto>();
                                            guardado = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto()
                                            {
                                                IdFormulario = idFormulario,
                                                IdPregunta = data.id,
                                                Respuesta = data.respuesta,
                                                IdTab = data.idTab,
                                                TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal"))
                                            }, EnumAccionRealizar.Insertar);
                                            if (!guardado.Sucess())
                                                throw new Exception("error guardado");
                                        }                                        
                                    }
                                    guardadoDatos = 1;
                                }
                            }
                        }                        
                        /*23-11-2020: Comentado por que no se guardan archivos
                        if (listaArchivos.Count > 0){
                            foreach (var archivo in listaArchivos){
                                if (archivo.Value.ContentLength > 0){
                                    int indice = int.Parse(archivo.Key.Split('[').ElementAt(1).Split(']').FirstOrDefault());
                                    int idTab = int.Parse(datos.preguntas.ElementAt(indice).idTab.ToString());
                                    int idPreg = int.Parse(datos.preguntas.ElementAt(indice).id.ToString());
                                    string rutaArchivo = System.IO.Path.Combine(rutaArchivos, System.IO.Path.GetFileName(string.Format("{0}_{1}_{2}.pdf", idPreg, idFormulario, idTab)));
                                    if (System.IO.File.Exists(rutaArchivo))
                                        System.IO.File.Delete(rutaArchivo);
                                    archivo.Value.SaveAs(rutaArchivo);
                                    datos.preguntas.ElementAt(indice).respuesta = string.Format("{0}_{1}_{2}.pdf", idPreg, idFormulario, idTab);
                                }
                            }
                        }*/
                    }
                    //Busco roles que no deben registrar guardado
                    TablaUsuariosDto usuario = await getDatosUsuario(idUsuario);
                    ViewDto<TablaParametrosDto> perfilNoGuardar = new ViewDto<TablaParametrosDto>();
                    perfilNoGuardar = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("NoGuardaModifi")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (perfilNoGuardar.HasElements()) {
                        if (perfilNoGuardar.Dtos.Count(p => p.Valor == usuario.IdPerfil) > 0)
                            guardadoDatos = 0;
                    }

                    if (guardadoDatos == 1){
                        ViewDto<TablaRespuestasDto> ultimoRegistro = new ViewDto<TablaRespuestasDto>();
                        ultimoRegistro = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaFecUltimaModif")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                        if (!ultimoRegistro.Sucess())
                            throw new Exception("error guardado (borrado último registro)");
                        ultimoRegistro = new ViewDto<TablaRespuestasDto>();
                        ultimoRegistro = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaUsuUltimaModif")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                        if (!ultimoRegistro.Sucess())
                            throw new Exception("error guardado (borrado usuario último registro)");
                        ultimoRegistro = new ViewDto<TablaRespuestasDto>();
                        ultimoRegistro = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaFecUltimaModif")), Respuesta = DateTime.Now, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                        if (!ultimoRegistro.Sucess())
                            throw new Exception("error guardado (fecha último registro)");
                        ultimoRegistro = new ViewDto<TablaRespuestasDto>();
                        ultimoRegistro = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaUsuUltimaModif")), Respuesta = idUsuario, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                        if (!ultimoRegistro.Sucess())
                            throw new Exception("error guardado (usuario último registro)");
                    }
                }
            }
            catch(Exception ex)
            {
                estado = false;
                log.Error(ex.Message, ex);                
                throw ex;
            }
            return estado;
        }

        public Task<IList<TablaParametrosDto>> getListaParametros(TablaParametrosFiltroDto filtros)
        {
            IList<TablaParametrosDto> tipos = new List<TablaParametrosDto>();
            try{
                ViewDto<TablaParametrosDto> data = new ViewDto<TablaParametrosDto>();
                data = bips.BuscarParametros(new ContextoDto(), filtros);
                if (data.HasElements())
                    tipos = data.Dtos.OrderBy(p=>p.Orden).ToList();
            }
            catch(Exception ex){
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(tipos);
        }

        public async Task<IList<int>> getAnosFormularios(int anoInicio, int anoTermino = 0, string idUserConectado = "")
        {
            IList<int> anos = new List<int>();
            try
            {
                if (anoTermino > 0){
                    for (int i = (DateTime.Now.Year + anoTermino); i > DateTime.Now.Year; i--)
                        anos.Add(i);
                }
                bool anosAnteriores = false;
                if (!String.IsNullOrEmpty(idUserConectado)){
                    UsuariosModels usuario = new UsuariosModels();
                    List<TablaUsuariosDto> buscaUsuario = new List<TablaUsuariosDto>();
                    buscaUsuario = await usuario.getUsuariosFiltro(new TablaUsuariosFiltroDto() { Id = idUserConectado, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (buscaUsuario.Count > 0){
                        if (buscaUsuario.SingleOrDefault().IdPerfil == decimal.Parse(constantes.GetValue("PerfilAdmin")))
                            anosAnteriores = true;
                    }                    
                }
                if (anosAnteriores){
                    for (int i = DateTime.Now.Year; i >= anoInicio; i--)
                        anos.Add(i);
                }                
            }
            catch(Exception ex){
                log.Error(ex.Message, ex);
                throw ex;
            }
            return (anos);
        }

        public Task<Boolean> guardaNuevosFormularios(List<string> data)
        {
            Boolean estado = true;
            try {
                ViewDto<TablaProgramasDto> insert = new ViewDto<TablaProgramasDto>();
                TablaProgramasDto programa = new TablaProgramasDto() {
                    Ano = decimal.Parse(data[0]),
                    Nombre = data[1],
                    IdMinisterio = new TablaParametrosDto() { IdParametro = decimal.Parse(data[2]) },
                    IdServicio = new TablaParametrosDto() { IdParametro = decimal.Parse(data[3]) },
                    IdTipoFormulario = decimal.Parse(data[4]),
                    Estado = new TablaParametrosDto() { IdParametro = decimal.Parse(constantes.GetValue("Activo")) }
                };
                insert = bips.RegistrarProgramas(new ContextoDto(), programa, EnumAccionRealizar.Insertar);
                if (insert.Sucess()){
                    ViewDto<TablaRespuestasDto> insert2 = new ViewDto<TablaRespuestasDto>();
                    for (int i = 0; i < 3; i++){
                        insert2 = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto()
                        {
                            IdFormulario = insert.Dtos.SingleOrDefault().IdPrograma,
                            IdPregunta = i == 0 ? decimal.Parse(constantes.GetValue("PreguntaNombre")) : i == 1 ? decimal.Parse(constantes.GetValue("PreguntaMinisterio")) : decimal.Parse(constantes.GetValue("PreguntaServicio")),
                            Respuesta = i == 0 ? data[1] : i == 1 ? data[2] : data[3]
                        }, EnumAccionRealizar.Insertar);
                        if (insert2.HasError())
                            throw new Exception("Error insert respuestas");
                    }
                }
                else{
                    throw new Exception("Error insert programa");
                }
            } catch(Exception ex){
                estado = false;
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(estado);
        }

        public async Task<Boolean> validaAccesoFormulario(int idFormulario, string idUsuario)
        {
            Boolean estado = false;
            try {
                ProgramasModels programas = new ProgramasModels();
                IList<TablaProgramasDto> objPrograma = new List<TablaProgramasDto>();
                objPrograma = await programas.getProgramasFiltro(new TablaProgramasFiltroDto() {
                    IdUser = idUsuario,
                    IdPrograma = idFormulario,
                    IdPlataforma = decimal.Parse(constantes.GetValue("PlataformaCargaBIPS")),
                    Estado = decimal.Parse(constantes.GetValue("Activo"))
                });
                if (objPrograma.Count > 0)
                    estado = true;
            }
            catch(Exception ex) {
                estado = false;
                log.Error(ex.Message, ex);
                throw ex;
            }
            return (estado);
        }

        public async Task<Boolean> enviarProgramaEvaluar(int idFormulario, string idUsuario)
        {
            Boolean estado = true;
            try {
                EvaluacionExAnteModels evaluacion = new EvaluacionExAnteModels();
                ViewDto<TablaUsuariosDto> usuario = new ViewDto<TablaUsuariosDto>();
                ViewDto<TablaParametrosDto> correoSectorialistas = new ViewDto<TablaParametrosDto>();
                ViewDto<TablaUsuariosDto> correoContrapartes = new ViewDto<TablaUsuariosDto>();
                usuario = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = idUsuario, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                //int tipo = await getTipoFormulario(idFormulario);
                ViewDto<TablaProgramasDto> tipoFormulario = new ViewDto<TablaProgramasDto>();
                tipoFormulario = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idFormulario });
                int tipo = int.Parse(tipoFormulario.Dtos.FirstOrDefault().IdTipoFormulario.ToString());
                if (usuario.HasElements()){
                    string mailEvaluacion = string.Empty;
                    //Datos programa
                    ViewDto<TablaProgramasDto> programa = new ViewDto<TablaProgramasDto>();
                    programa = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idFormulario, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Buscar);
                    ViewDto<TablaParametrosDto> seguimiento = new ViewDto<TablaParametrosDto>();
                    if (tipo == int.Parse(constantes.GetValue("TipoPerfilGore")) || tipo == int.Parse(constantes.GetValue("TipoProgramaGore")))
                    {
                        mailEvaluacion += constantes.GetValue("EmailExAnteGore") + ",";

                        //Definido reunión 17-01-25, cambio de sectorialistas a contrapartes tecnicas     
                        correoContrapartes = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { IdGore = programa.Dtos.FirstOrDefault().IdServicio.IdParametro, IdPerfilGore = decimal.Parse(constantes.GetValue("PerfilContraparteGore")), IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                        if (correoContrapartes.HasElements())
                        {
                            foreach (var contraparte in correoContrapartes.Dtos)
                            {
                                if (contraparte.Email != usuario.Dtos.SingleOrDefault().Email)
                                    mailEvaluacion += contraparte.Email + ",";
                            }
                        }
                    }
                    else{
                        //Busco lista de formularios de seguimiento                        
                        seguimiento = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("FormulariosEvalSeguimiento")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                        decimal? categoriaCorreosEval = null;
                        if (programa.HasElements())
                        {
                            if (seguimiento.HasElements())
                            {
                                if (seguimiento.Dtos.Count(p => p.Valor == programa.Dtos.SingleOrDefault().IdTipoFormulario) > 0)
                                {
                                    categoriaCorreosEval = decimal.Parse(constantes.GetValue("CorreosEvalSeguimiento"));
                                }
                                else {
                                    categoriaCorreosEval = decimal.Parse(constantes.GetValue("listaMailEvalExAnte"));
                                }
                            }
                        }
                        //Busco datos usuario
                        ViewDto<TablaParametrosDto> casillaEvaluacion = new ViewDto<TablaParametrosDto>();
                        casillaEvaluacion = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = categoriaCorreosEval, Estado = decimal.Parse(constantes.GetValue("Activo")) });
                        if (casillaEvaluacion.HasElements())
                        {
                            foreach (var item in casillaEvaluacion.Dtos)
                            {
                                if (item.IdParametro != item.IdCategoria)
                                {
                                    mailEvaluacion += item.Descripcion + ",";
                                }
                            }
                        }
                        //Busco correos sectorialistas                        
                        correoSectorialistas = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("CorreoSectorialistas")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                        if (correoSectorialistas.HasElements())
                        {
                            foreach (var item in correoSectorialistas.Dtos)
                            {
                                if (item.Valor == programa.Dtos.FirstOrDefault().IdServicio.IdParametro)
                                {
                                    mailEvaluacion += item.Descripcion + ",";
                                }
                            }
                        }
                        //Busco correos contrapartes tecnicas
                        ViewDto<TablaUsuariosDto> contrapartes = new ViewDto<TablaUsuariosDto>();
                        contrapartes = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { IdServicio = programa.Dtos.FirstOrDefault().IdServicio.IdParametro, IdPerfil = decimal.Parse(constantes.GetValue("PerfilContraparte")), IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                        if (contrapartes.HasElements())
                        {
                            foreach (var contraparte in contrapartes.Dtos)
                            {
                                if (contraparte.Email != usuario.Dtos.SingleOrDefault().Email)
                                    mailEvaluacion += contraparte.Email + ",";
                            }
                        }
                        //Busco correos asociados al permiso del programa
                        ViewDto<TablaProgramasDto> permisos = new ViewDto<TablaProgramasDto>();
                        permisos = bips.BuscarPermisosFormularios(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idFormulario, Estado = decimal.Parse(constantes.GetValue("Activo")) });
                        if (permisos.HasElements())
                        {
                            ViewDto<TablaUsuariosDto> permisoUser = new ViewDto<TablaUsuariosDto>();
                            List<String> listaCorreos = !String.IsNullOrEmpty(mailEvaluacion) ? mailEvaluacion.Split(',').ToList() : new List<string>();
                            foreach (var user in permisos.Dtos)
                            {
                                permisoUser = new ViewDto<TablaUsuariosDto>();
                                permisoUser = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = user.IdUser, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                                if (permisoUser.HasElements())
                                {
                                    if (permisoUser.Dtos.FirstOrDefault().Email != usuario.Dtos.SingleOrDefault().Email && listaCorreos.Count(p => p == permisoUser.Dtos.FirstOrDefault().Email) == 0)
                                    {
                                        mailEvaluacion += permisoUser.Dtos.FirstOrDefault().Email + ",";
                                    }
                                }
                            }
                        }
                    }
                    if (programa.HasElements()){
                        //Enviar mail
                        string asunto = string.Empty;
                        string msj1 = string.Empty;
                        string msj2 = string.Empty;
                        DatosEmail dataMail = new DatosEmail();
                        dataMail.de = usuario.Dtos.SingleOrDefault().Email;
                        dataMail.para = mailEvaluacion + usuario.Dtos.SingleOrDefault().Email;                        
                        string nombreProg = programa.Dtos.SingleOrDefault().Nombre;
                        string servicio = usuario.Dtos.SingleOrDefault().Servicio;
                        string ministerioProg = programa.Dtos.SingleOrDefault().Ministerio;
                        string nombreUsuario = usuario.Dtos.SingleOrDefault().Nombre;
                        string servicioProg = programa.Dtos.SingleOrDefault().Servicio;
                        string ministerio = usuario.Dtos.SingleOrDefault().Ministerio;
                        string tipoProg = programa.Dtos.SingleOrDefault().Tipo;
                        string correoUsuario = usuario.Dtos.SingleOrDefault().Email;
                        string version = await getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaVersionProgramas")), idFormulario);
                        string versionFinal = (String.IsNullOrEmpty(version) ? string.Empty : (int.Parse(version) >= 1 ? "versión " + version : string.Empty));
                        if (tipo == int.Parse(constantes.GetValue("TipoPerfilGore"))) {
                            dataMail.imagenDipres = true;
                            asunto = "Solicitud de evaluación";
                            msj1 = "Evaluación Ex Ante:<br/> Informamos que el perfil <b>{0} {1}</b> ({2}) se encuentra listo para ser evaluado.";
                            msj2 = "Saludos cordiales.";
                            dataMail.mensaje = string.Format(new CuerpoEmail().emailHtmlEvaluacion, string.Format(msj1, nombreProg, versionFinal, ministerioProg), string.Format(msj2, nombreUsuario, ministerio, servicio, correoUsuario));
                        }
                        else{
                            if (seguimiento.HasElements()){
                                if (seguimiento.Dtos.Count(p => p.Valor == programa.Dtos.SingleOrDefault().IdTipoFormulario) > 0)
                                {
                                    dataMail.imagenDipres = false;
                                    dataMail.imagen = true;
                                    asunto = string.Format("Ficha para revisión – {0} {1} – {2} – Pre-Monitoreo 2024", nombreProg, servicioProg, ministerioProg);
                                    msj1 = string.Format("Estimado/a<br/><br/>Le comunicamos que en el marco del proceso de Pre-Monitoreo 2024, el programa {0}, perteneciente a {1} – {2}, ha sido enviado por la institución responsable para revisión.", nombreProg, servicioProg, ministerioProg);
                                    msj2 = "Saludos cordiales.";
                                    dataMail.mensaje = string.Format(new CuerpoEmail().emailHtmlSES, msj1, msj2);
                                }
                                else {
                                    dataMail.imagen = true;
                                    asunto = "Solicitud de evaluación";
                                    msj1 = "Evaluación Ex Ante:<br/> Informamos que el programa <b>{0} {1}</b> ({2} - {3}) se encuentra listo para ser evaluado como programa <b>{4}</b>.";
                                    msj2 = "Saluda Atentamente, <br/>{0}<br/>{1}<br/>{2}<br/>{3}";
                                    dataMail.mensaje = string.Format(new CuerpoEmail().emailHtmlEvaluacion, string.Format(msj1, nombreProg, versionFinal, ministerioProg, servicioProg, tipoProg), string.Format(msj2, nombreUsuario, ministerio, servicio, correoUsuario));
                                }
                            }
                        }
                        dataMail.asunto = asunto;
                        //-------------------------------------------------------------------------------
                        //DESCOMENTAR AL TERMINAR PRUEBAS
                        //Boolean estadoUsuario = evaluacion.enviaMail(dataMail);
                        //if (!estadoUsuario)
                        //    throw new Exception("error envio mail solicitud de evaluación");
                        //-------------------------------------------------------------------------------

                        //Guardar fecha envio evaluacion
                        ViewDto<TablaRespuestasDto> borradoFechaEval = new ViewDto<TablaRespuestasDto>();
                        borradoFechaEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaEnvioEvaluacion")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                        if (!borradoFechaEval.Sucess())
                            throw new Exception("error registro fecha envio solicitud evaluacion (borrado)");
                        ViewDto<TablaRespuestasDto> registraFechaEval = new ViewDto<TablaRespuestasDto>();
                        registraFechaEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaEnvioEvaluacion")), Respuesta = DateTime.Now, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                        if (!registraFechaEval.Sucess())
                            throw new Exception("error registro fecha envio solicitud evaluacion (registro)");

                        //Guardar usuario envio evaluacion
                        ViewDto<TablaRespuestasDto> borradoUsuarioEval = new ViewDto<TablaRespuestasDto>();
                        borradoUsuarioEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaUsuarioEnvioEvaluacion")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                        if (!borradoUsuarioEval.Sucess())
                            throw new Exception("error registro usuario envio solicitud evaluacion (borrado)");
                        ViewDto<TablaRespuestasDto> registraUsuarioEval = new ViewDto<TablaRespuestasDto>();
                        registraUsuarioEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaUsuarioEnvioEvaluacion")), Respuesta = idUsuario, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                        if (!registraUsuarioEval.Sucess())
                            throw new Exception("error registro usuario envio solicitud evaluacion (registro)");

                        //Quitar permisos primera parte del formulario
                        ViewDto<TablaExcepcionesPermisosDto> quitaPermisos = new ViewDto<TablaExcepcionesPermisosDto>();
                        quitaPermisos = bips.RegistrarExcepcionesFormularios(new ContextoDto(), new TablaExcepcionesPermisosDto() { IdFormulario = idFormulario, Estado = decimal.Parse(constantes.GetValue("EstadoInactivoTemporal"))}, EnumAccionRealizar.Eliminar);
                        if (quitaPermisos.HasError())
                            throw new Exception("Error al quitar permiso (envio evaluacion)");

                        //Asigna permiso modulo evaluacion si no es gore
                        if (tipo != int.Parse(constantes.GetValue("TipoPerfilGore")) && tipo != int.Parse(constantes.GetValue("TipoProgramaGore")))
                        {
                            UsuariosModels usuarios = new UsuariosModels();
                            string permisoModEval = string.Empty;
                            string idSectorialista = string.Empty;
                            string idContraparte = string.Empty;
                            if (correoSectorialistas.HasElements())
                            {
                                ViewDto<TablaUsuariosDto> sectorialista = new ViewDto<TablaUsuariosDto>();
                                sectorialista = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Email = correoSectorialistas.Dtos.Where(p => p.IdParametro != p.IdCategoria && p.Valor == programa.Dtos.FirstOrDefault().IdServicio.IdParametro).FirstOrDefault().Descripcion, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                                if (sectorialista.HasElements())
                                    idSectorialista = sectorialista.Dtos.FirstOrDefault().Id;
                            }
                            else if (correoContrapartes.HasElements())
                            {
                                ViewDto<TablaUsuariosDto> contraparte = new ViewDto<TablaUsuariosDto>();
                                contraparte = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Email = correoContrapartes.Dtos.Where(p => p.IdGore == programa.Dtos.FirstOrDefault().IdServicio.IdParametro).FirstOrDefault().Email, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                                if (contraparte.HasElements())
                                    idContraparte = contraparte.Dtos.FirstOrDefault().Id;
                            }
                            string idUsuarioPerModEval = idSectorialista != "" ? idSectorialista : idContraparte;
                            TablaExcepcionesPermisosDto perModEval = new TablaExcepcionesPermisosDto() { IdFormulario = idFormulario, IdUsuario = idUsuarioPerModEval, IdPermiso = decimal.Parse(constantes.GetValue("PermisoNoComparable")) };
                            permisoModEval = await usuarios.eliminaPermisosUsuarios(new List<TablaExcepcionesPermisosDto>() { perModEval });
                            if (permisoModEval != "ok")
                                throw new Exception("error eliminar permiso de evaluacion");
                            permisoModEval = await usuarios.registraPermisosUsuarios(new List<TablaExcepcionesPermisosDto>() { new TablaExcepcionesPermisosDto() { IdFormulario = idFormulario, IdUsuario = idUsuarioPerModEval, IdPermiso = decimal.Parse(constantes.GetValue("PermisoNoComparable")), Estado = decimal.Parse(constantes.GetValue("Activo")) } });
                            if (permisoModEval != "ok")
                                throw new Exception("error crear permiso de evaluacion");
                        }

                        //Cambio a etapa de solicitud de evaluacion
                        ViewDto<TablaProgramasDto> actualizaEtapa = new ViewDto<TablaProgramasDto>();
                        TablaProgramasDto datosProgAct = new TablaProgramasDto();
                        datosProgAct.IdPrograma = idFormulario;
                        datosProgAct.Estado.IdParametro = decimal.Parse(constantes.GetValue("Activo"));
                        if (tipo == int.Parse(constantes.GetValue("TipoPerfilGore")) || tipo == int.Parse(constantes.GetValue("TipoProgramaGore")))
                        {
                            datosProgAct.Etapa.IdParametro = decimal.Parse(constantes.GetValue("EtapaEvaluacion"));
                            if (tipo == int.Parse(constantes.GetValue("TipoProgramaGore")))
                            {
                                FormulariosModels formulario = new FormulariosModels();
                                //Elimina permiso formuladores
                                string listaFormuladores = await formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaFormuladoresProgramaGore")), idFormulario);
                                string[] formuladores = listaFormuladores.Split(',');

                                foreach (var formulador in formuladores)
                                {
                                    ViewDto<TablaExcepcionesPermisosDto> permisoFormulador = new ViewDto<TablaExcepcionesPermisosDto>();
                                    permisoFormulador = bips.RegistrarExcepcionesFormularios(new ContextoDto(), new TablaExcepcionesPermisosDto() { IdUsuario = formulador, IdFormulario = idFormulario, IdPermiso = decimal.Parse(constantes.GetValue("PermisoProgramaGore")), Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Insertar);
                                    if (permisoFormulador.HasError())
                                        throw new Exception(permisoFormulador.Error.Detalle);
                                }
                            }
                        }
                        else
                        {
                            datosProgAct.Etapa.IdParametro = decimal.Parse(constantes.GetValue("EtapaSolciEvalExAnte"));
                        }
                        actualizaEtapa = bips.RegistrarProgramas(new ContextoDto(), datosProgAct, EnumAccionRealizar.Actualizar);
                        if (actualizaEtapa.HasError())
                            throw new Exception("Error al actualizar etapa (envio evaluacion)");
                        //Guardar usuario de envio de evaluacion
                        ViewDto<TablaRespuestasDto> usuarioEnvioEval = new ViewDto<TablaRespuestasDto>();
                        usuarioEnvioEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = decimal.Parse(constantes.GetValue("PregUsuarioEnvEval")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                        if (!usuarioEnvioEval.Sucess())
                            throw new Exception("error registro usuario solicitud evaluacion (borrado)");
                        usuarioEnvioEval = new ViewDto<TablaRespuestasDto>();
                        usuarioEnvioEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = decimal.Parse(constantes.GetValue("PregUsuarioEnvEval")), Respuesta = idUsuario, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                        if (!usuarioEnvioEval.Sucess())
                            throw new Exception("error registro usuario solicitud evaluacion (registro)");
                    }
                }                                
            }
            catch(Exception ex) {
                estado = false;
                log.Error(ex.Message, ex);
                throw ex;
            }
            return (estado);
        }

        public async Task<String> enviarObservaciones(FormulariosViewModels datos, int idFormulario, string idUsuario)
        {
            string estado = string.Empty;
            try{
                if (datos.observaciones.Count > 0){
                    ViewDto<TablaRespuestasDto> guardado;
                    ViewDto<TablaRespuestasDto> borrado;
                    foreach (var obs in datos.observaciones){
                        //Borrado
                        borrado = new ViewDto<TablaRespuestasDto>();
                        borrado = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto(){ IdFormulario = idFormulario, IdPregunta = obs.IdPregunta, IdTab = obs.IdTab, TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                        if (!borrado.Sucess())
                            throw new Exception("error guardado observaciones (borrado)");
                        //Guardado                                
                        if (!String.IsNullOrEmpty(obs.Respuesta)){
                            guardado = new ViewDto<TablaRespuestasDto>();
                            guardado = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto(){ IdFormulario = idFormulario, IdPregunta = obs.IdPregunta, Respuesta = obs.Respuesta, IdTab = obs.IdTab, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                            if (!guardado.Sucess())
                                throw new Exception("error guardado");
                        }
                    }
                    //Cierro programa
                    EvaluacionExAnteModels evaluacion = new EvaluacionExAnteModels();
                    string cierre = await evaluacion.cierraEvaluacion(idFormulario, idUsuario);
                    if (cierre != "ok")
                        throw new Exception("error cierre programa");
                    //Creo nueva iteracion
                    string version = await getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaVersionProgramas")), idFormulario);
                    decimal? iteracion = await nuevaIteracionSeguimiento(idFormulario, (String.IsNullOrEmpty(version) ? 0 : int.Parse(version)), idUsuario);
                    if (iteracion == null)
                        throw new Exception("error nueva iteracion");
                    //Datos programa
                    ViewDto<TablaProgramasDto> programa = new ViewDto<TablaProgramasDto>();
                    programa = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = iteracion, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Buscar);
                    //Mando mail
                    ViewDto<TablaProgramasDto> usuariosPermisos = new ViewDto<TablaProgramasDto>();
                    usuariosPermisos = bips.BuscarPermisosFormularios(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = iteracion, Estado = decimal.Parse(constantes.GetValue("Activo")), TipoFormulario = programa.Dtos.SingleOrDefault().IdTipoFormulario });
                    ViewDto<TablaExcepcionesPlantillasDto> plantillas = new ViewDto<TablaExcepcionesPlantillasDto>();
                    plantillas = bips.BuscarPlantillasFormularios(new ContextoDto(), new TablaExcepcionesPlantillasFiltroDto() { Estado = decimal.Parse(constantes.GetValue("Activo")), TipoFormulario = programa.Dtos.SingleOrDefault().IdTipoFormulario });
                    string mailObservaciones = string.Empty;
                    if (usuariosPermisos.HasElements()) {
                        if (plantillas.HasElements()){
                            ViewDto<TablaUsuariosDto> usuario = new ViewDto<TablaUsuariosDto>();
                            foreach (var mail in usuariosPermisos.Dtos){
                                if (plantillas.Dtos.Count(p=>p.IdExcepcionPlantilla==mail.IdExcepcion) > 0){
                                    //busco mail de usuario
                                    usuario = new ViewDto<TablaUsuariosDto>();
                                    usuario = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = mail.IdUser, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                                    if (usuario.HasElements())
                                        mailObservaciones += usuario.Dtos.FirstOrDefault().Email + ",";
                                }
                            }
                        }
                        //Enviar mail
                        string nombreProg = programa.Dtos.SingleOrDefault().Nombre;
                        string ministerioProg = programa.Dtos.SingleOrDefault().Ministerio;
                        string servicioProg = programa.Dtos.SingleOrDefault().Servicio;
                        string asunto = string.Format("Ficha con observaciones - {0} {1}-{2} - Monitoreo o seguimiento oferta pública cierre 2021", nombreProg, servicioProg, ministerioProg);
                        string msj1 = string.Format("Estimado/a<br /><br />Le comunicamos que, en el marco del Monitoreo o Seguimiento de la oferta pública cierre 2021, el programa {0}, perteneciente a {1} – {2}, ha sido revisado y <b>se encuentra con observaciones</b>. Los campos que han sido observados se encuentran detallados en el formulario localizado en la plataforma.<br /><br />A partir de este momento la plataforma estará activa para el ingreso de la información necesaria. En caso de requerir asistencia técnica, comuníquese con su sectorialista del Departamento de Monitoreo de Programas Sociales de la SES o Subdepartamento de Seguimiento de la oferta programática de DIPRES, según corresponda.", nombreProg, servicioProg, ministerioProg);
                        string msj2 = "Saludos cordiales.<br />";
                        DatosEmail dataMail = new DatosEmail();
                        dataMail.de = constantes.GetValue("EmailMonitoreo");
                        dataMail.para = mailObservaciones.TrimEnd(',');
                        dataMail.asunto = asunto;
                        dataMail.imagenDipres = true;
                        dataMail.mensaje = string.Format(new CuerpoEmail().emailHtmlSeguimiento, msj1, msj2);
                        Boolean estadoUsuario = evaluacion.enviaMail(dataMail);
                        if (!estadoUsuario)
                            throw new Exception("error envio mail solicitud de evaluación");
                        estado = programa.Dtos.FirstOrDefault().IdEncriptado;
                    }
                }
            }
            catch (Exception ex){
                estado = string.Empty;
                log.Error(ex.Message, ex);
                throw ex;
            }
            return (estado);
        }

        public Task<String> guardaObservaciones(FormulariosViewModels datos, int idFormulario, string idUsuario)
        {
            string estado = "ok";
            try
            {
                if (datos.observaciones.Count > 0){
                    ViewDto<TablaRespuestasDto> guardado;
                    ViewDto<TablaRespuestasDto> borrado;
                    foreach (var obs in datos.observaciones)
                    {
                        //Borrado
                        borrado = new ViewDto<TablaRespuestasDto>();
                        borrado = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = obs.IdPregunta, IdTab = obs.IdTab, TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                        if (!borrado.Sucess())
                            throw new Exception("error guardado observaciones (borrado)");
                        //Guardado                                
                        if (!String.IsNullOrEmpty(obs.Respuesta))
                        {
                            guardado = new ViewDto<TablaRespuestasDto>();
                            guardado = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = obs.IdPregunta, Respuesta = obs.Respuesta, IdTab = obs.IdTab, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                            if (!guardado.Sucess())
                                throw new Exception("error guardado");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                estado = string.Empty;
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(estado);
        }

        public Task<decimal?> nuevaIteracionSeguimiento(int idPrograma, int version, string idUsuario)
        {
            decimal? registro = null;
            try
            {
                //Crea nueva iteracion
                ViewDto<TablaProgramasDto> creaIteracion = new ViewDto<TablaProgramasDto>();
                creaIteracion = bips.RegistrarProgramas(new ContextoDto(), new TablaProgramasDto() { IdPrograma = idPrograma, IdBips = version }, EnumAccionRealizar.EliminarUserGrupo);
                if (creaIteracion.Sucess())
                {
                    //Registro usuario que creo nueva iteracion
                    ViewDto<TablaRespuestasDto> registraUserCreaIteracion = new ViewDto<TablaRespuestasDto>();
                    registraUserCreaIteracion = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = creaIteracion.Dtos.SingleOrDefault().IdPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaUserCreaIteracion")), Respuesta = idUsuario, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                    if (!registraUserCreaIteracion.Sucess())
                        throw new Exception("error registro fecha de creacion de iteracion (registro)");
                    //Registro fecha de creacion de nueva iteracion
                    ViewDto<TablaRespuestasDto> registraFechaCreaIteracion = new ViewDto<TablaRespuestasDto>();
                    registraFechaCreaIteracion = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = creaIteracion.Dtos.SingleOrDefault().IdPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaFechaCreaIteracion")), Respuesta = DateTime.Now, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                    if (!registraFechaCreaIteracion.Sucess())
                        throw new Exception("error registro fecha de creacion de iteracion (registro)");
                    registro = creaIteracion.Dtos.SingleOrDefault().IdPrograma;
                }
            }
            catch (Exception ex)
            {
                registro = null;
                log.Error(ex.Message, ex);
            }
            return Task.FromResult(registro);
        }

        public Task<String> sinObservaciones(FormulariosViewModels datos, int idFormulario, string idUsuario)
        {
            string estado = "ok";
            try
            {
                //Guardo datos
                if (datos.observaciones.Count > 0) {
                    ViewDto<TablaRespuestasDto> guardado;
                    ViewDto<TablaRespuestasDto> borrado;
                    foreach (var obs in datos.observaciones)
                    {
                        //Borrado
                        borrado = new ViewDto<TablaRespuestasDto>();
                        borrado = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = obs.IdPregunta, IdTab = obs.IdTab, TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                        if (!borrado.Sucess())
                            throw new Exception("error guardado sin observaciones (borrado)");
                        //Guardado                                
                        if (!String.IsNullOrEmpty(obs.Respuesta))
                        {
                            guardado = new ViewDto<TablaRespuestasDto>();
                            guardado = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = obs.IdPregunta, Respuesta = obs.Respuesta, IdTab = obs.IdTab, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                            if (!guardado.Sucess())
                                throw new Exception("error guardado sin observaciones");
                        }
                    }
                }
                //Cambio a etapa a cierre de evaluación
                ViewDto<TablaProgramasDto> actualizaEtapa = new ViewDto<TablaProgramasDto>();
                TablaProgramasDto datosProgAct = new TablaProgramasDto();
                datosProgAct.IdPrograma = idFormulario;
                datosProgAct.Estado.IdParametro = decimal.Parse(constantes.GetValue("Activo"));
                datosProgAct.Etapa.IdParametro = decimal.Parse(constantes.GetValue("EtapaCierreSinObs"));
                actualizaEtapa = bips.RegistrarProgramas(new ContextoDto(), datosProgAct, EnumAccionRealizar.Actualizar);
                if (actualizaEtapa.HasError())
                    throw new Exception("Error al actualizar etapa (cierre sin observaciones)");
                //Datos programa
                ViewDto<TablaProgramasDto> programa = new ViewDto<TablaProgramasDto>();
                programa = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idFormulario, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Buscar);
                //Mando mail
                ViewDto<TablaProgramasDto> usuariosPermisos = new ViewDto<TablaProgramasDto>();
                usuariosPermisos = bips.BuscarPermisosFormularios(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idFormulario, Estado = decimal.Parse(constantes.GetValue("EstadoInactivoTemporal")), TipoFormulario = programa.Dtos.SingleOrDefault().IdTipoFormulario });
                ViewDto<TablaExcepcionesPlantillasDto> plantillas = new ViewDto<TablaExcepcionesPlantillasDto>();
                plantillas = bips.BuscarPlantillasFormularios(new ContextoDto(), new TablaExcepcionesPlantillasFiltroDto() { Estado = decimal.Parse(constantes.GetValue("Activo")), TipoFormulario = programa.Dtos.SingleOrDefault().IdTipoFormulario });
                string mailObservaciones = string.Empty;
                if (usuariosPermisos.HasElements())
                {
                    if (plantillas.HasElements())
                    {
                        ViewDto<TablaUsuariosDto> usuario = new ViewDto<TablaUsuariosDto>();
                        foreach (var mail in usuariosPermisos.Dtos)
                        {
                            if (plantillas.Dtos.Count(p => p.IdExcepcionPlantilla == mail.IdExcepcion) > 0)
                            {
                                //busco mail de usuario
                                usuario = new ViewDto<TablaUsuariosDto>();
                                usuario = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = mail.IdUser, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                                if (usuario.HasElements())
                                    mailObservaciones += usuario.Dtos.FirstOrDefault().Email + ",";
                            }
                        }
                    }
                    //Enviar mail
                    string nombreProg = programa.Dtos.SingleOrDefault().Nombre;
                    string ministerioProg = programa.Dtos.SingleOrDefault().Ministerio;
                    string servicioProg = programa.Dtos.SingleOrDefault().Servicio;
                    string asunto = string.Format("Ficha cerrada sin observaciones - {0} {1} - {2} - Cierre proceso Monitoreo o seguimiento oferta pública cierre 2021", nombreProg, servicioProg, ministerioProg);
                    string msj1 = string.Format("Estimado/a<br /><br />Le comunicamos que, en el marco del Monitoreo o Seguimiento de la oferta pública cierre 2021, el programa {0}, perteneciente a {1} – {2}, ha sido revisado y <b>se encuentra sin observaciones.</b><br /><br />A partir de este momento, ya no se pueden realizar nuevas modificaciones en la información ingresada. Dado lo anterior, el programa queda a la espera de la fase de evaluación de la oferta pública cierre 2021.<br />", nombreProg, servicioProg, ministerioProg);
                    string msj2 = "Saludos cordiales,<br />";
                    DatosEmail dataMail = new DatosEmail();
                    dataMail.de = constantes.GetValue("EmailMonitoreo");
                    dataMail.para = mailObservaciones.TrimEnd(',');
                    dataMail.asunto = asunto;
                    dataMail.imagenDipres = true;
                    dataMail.mensaje = string.Format(new CuerpoEmail().emailHtmlSeguimiento, msj1, msj2);
                    EvaluacionExAnteModels evaluacion = new EvaluacionExAnteModels();
                    Boolean estadoUsuario = evaluacion.enviaMail(dataMail);
                    if (!estadoUsuario)
                        throw new Exception("error envio mail sin observaciones");
                }
                //Quitar permisos
                ViewDto<TablaExcepcionesPermisosDto> quitaPermisos = new ViewDto<TablaExcepcionesPermisosDto>();
                quitaPermisos = bips.RegistrarExcepcionesFormularios(new ContextoDto(), new TablaExcepcionesPermisosDto() { IdFormulario = idFormulario, Estado = decimal.Parse(constantes.GetValue("EstadoInactivoTemporal")) }, EnumAccionRealizar.Eliminar);
                if (quitaPermisos.HasError())
                    throw new Exception("Error al quitar permiso (cierre sin observaciones)");
            }
            catch (Exception ex)
            {
                estado = string.Empty;
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(estado);
        }

        public async Task<String> cierreConObservaciones(FormulariosViewModels datos, int idFormulario, string idUsuario)
        {
            string estado = "ok";
            try
            {
                if (datos.observaciones.Count > 0)
                {
                    //Guarda observaciones
                    ViewDto<TablaRespuestasDto> guardado;
                    ViewDto<TablaRespuestasDto> borrado;
                    foreach (var obs in datos.observaciones)
                    {
                        //Borrado
                        borrado = new ViewDto<TablaRespuestasDto>();
                        borrado = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = obs.IdPregunta, IdTab = obs.IdTab, TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                        if (!borrado.Sucess())
                            throw new Exception("error guardado observaciones (borrado)");
                        //Guardado                                
                        if (!String.IsNullOrEmpty(obs.Respuesta))
                        {
                            guardado = new ViewDto<TablaRespuestasDto>();
                            guardado = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = obs.IdPregunta, Respuesta = obs.Respuesta, IdTab = obs.IdTab, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                            if (!guardado.Sucess())
                                throw new Exception("error guardado");
                        }
                    }
                    //Cierro programa con observaciones
                    Boolean cierre = await cambioEtapa(idFormulario, decimal.Parse(constantes.GetValue("EtapaCierreConObs")));
                    if (!cierre)
                        throw new Exception("error al cambiar etapa (Cierre con observaciones)");
                    //Registro usuario que cerro programa con observaciones
                    Boolean usuarioCierre = await registraRespuesta(idFormulario, int.Parse(constantes.GetValue("PreguntaUsuarioCierreConObs")), idUsuario);
                    if (!usuarioCierre)
                        throw new Exception("error al registrar usuario de cierre de programa con obervaciones");
                    //Registro fecha de cierre de programa con observaciones
                    Boolean fechaCierre = await registraRespuesta(idFormulario, int.Parse(constantes.GetValue("PreguntaFechaCierreConObs")), DateTime.Now);
                    if (!fechaCierre)
                        throw new Exception("error al registrar fecha de cierre de programa con observaciones");
                    //Datos programa
                    ViewDto<TablaProgramasDto> programa = new ViewDto<TablaProgramasDto>();
                    programa = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idFormulario, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Buscar);
                    //Mando mail
                    ViewDto<TablaProgramasDto> usuariosPermisos = new ViewDto<TablaProgramasDto>();
                    usuariosPermisos = bips.BuscarPermisosFormularios(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idFormulario, Estado = decimal.Parse(constantes.GetValue("EstadoInactivoTemporal")), TipoFormulario = programa.Dtos.SingleOrDefault().IdTipoFormulario });
                    ViewDto<TablaExcepcionesPlantillasDto> plantillas = new ViewDto<TablaExcepcionesPlantillasDto>();
                    plantillas = bips.BuscarPlantillasFormularios(new ContextoDto(), new TablaExcepcionesPlantillasFiltroDto() { Estado = decimal.Parse(constantes.GetValue("Activo")), TipoFormulario = programa.Dtos.SingleOrDefault().IdTipoFormulario });
                    string mailObservaciones = string.Empty;
                    if (usuariosPermisos.HasElements())
                    {
                        if (plantillas.HasElements())
                        {
                            ViewDto<TablaUsuariosDto> usuario = new ViewDto<TablaUsuariosDto>();
                            foreach (var mail in usuariosPermisos.Dtos)
                            {
                                if (plantillas.Dtos.Count(p => p.IdExcepcionPlantilla == mail.IdExcepcion) > 0)
                                {
                                    //busco mail de usuario
                                    usuario = new ViewDto<TablaUsuariosDto>();
                                    usuario = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = mail.IdUser, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                                    if (usuario.HasElements())
                                        mailObservaciones += usuario.Dtos.FirstOrDefault().Email + ",";
                                }
                            }
                        }
                        //Enviar mail
                        string nombreProg = programa.Dtos.SingleOrDefault().Nombre;
                        string ministerioProg = programa.Dtos.SingleOrDefault().Ministerio;
                        string servicioProg = programa.Dtos.SingleOrDefault().Servicio;
                        string asunto = string.Format("Ficha cerrada con observaciones - {0} {1} - {2} - Monitoreo o seguimiento oferta pública cierre 2021", nombreProg, servicioProg, ministerioProg);
                        string msj1 = string.Format("Estimado/a<br /><br />Le comunicamos que, en el marco del Monitoreo o Seguimiento de la oferta pública cierre 2021, el programa {0}, perteneciente a {1} – {2}, ha sido revisado y <b>se encuentra con observaciones. Dado los plazos del proceso, a partir de este momento no se pueden realizar nuevas modificaciones a la información ingresada.</b><br /><br />Dado lo anterior, el programa queda a la espera de la fase de evaluación de la oferta pública cierre 2021.<br />", nombreProg, servicioProg, ministerioProg);
                        string msj2 = "Saludos cordiales,<br />";
                        DatosEmail dataMail = new DatosEmail();
                        dataMail.de = constantes.GetValue("EmailMonitoreo");
                        dataMail.para = mailObservaciones.TrimEnd(',');
                        dataMail.asunto = asunto;
                        dataMail.imagenDipres = true;
                        dataMail.mensaje = string.Format(new CuerpoEmail().emailHtmlSeguimiento, msj1, msj2);
                        EvaluacionExAnteModels evaluacion = new EvaluacionExAnteModels();
                        Boolean estadoUsuario = evaluacion.enviaMail(dataMail);
                        if (!estadoUsuario)
                            throw new Exception("error envio mail sin observaciones");
                    }
                    //Quitar permisos
                    ViewDto<TablaExcepcionesPermisosDto> quitaPermisos = new ViewDto<TablaExcepcionesPermisosDto>();
                    quitaPermisos = bips.RegistrarExcepcionesFormularios(new ContextoDto(), new TablaExcepcionesPermisosDto() { IdFormulario = idFormulario, Estado = decimal.Parse(constantes.GetValue("Inactivo")) }, EnumAccionRealizar.Eliminar);
                    if (quitaPermisos.HasError())
                        throw new Exception("Error al quitar permiso (cierre con observaciones)");
                }
            }
            catch (Exception ex)
            {
                estado = string.Empty;
                log.Error(ex.Message, ex);
                throw ex;
            }
            return (estado);
        }

        public Task<Boolean> cambioEtapa(int idFormulario, decimal etapa)
        {
            Boolean estado = true;
            try
            {
                ViewDto<TablaProgramasDto> actualizaEtapa = new ViewDto<TablaProgramasDto>();
                TablaProgramasDto datosProgAct = new TablaProgramasDto();
                datosProgAct.IdPrograma = idFormulario;
                datosProgAct.Estado.IdParametro = decimal.Parse(constantes.GetValue("Activo"));
                datosProgAct.Etapa.IdParametro = etapa;
                actualizaEtapa = bips.RegistrarProgramas(new ContextoDto(), datosProgAct, EnumAccionRealizar.Actualizar);
                if (actualizaEtapa.HasError())
                    throw new Exception("Error al actualizar etapa");
            }
            catch (Exception ex)
            {
                estado = false;
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(estado);
        }

        public Task<Boolean> registraRespuesta(int idFormulario, int idPregunta, object respuesta)
        {
            Boolean estado = true;
            try {
                //Borra respuesta anterior (si existe)
                ViewDto<TablaRespuestasDto> borrado = new ViewDto<TablaRespuestasDto>();
                borrado = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = idPregunta, TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                if (!borrado.Sucess())
                    throw new Exception("error borrado respuesta");
                ViewDto<TablaRespuestasDto> registro = new ViewDto<TablaRespuestasDto>();
                registro = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = idPregunta, Respuesta = respuesta, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                if (!registro.Sucess())
                    throw new Exception("error registro respuesta");
            }
            catch(Exception ex)
            {
                estado = false;
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(estado);
        }

        public Task<String> etapaEvaluacion(int idFormulario, string idUsuario)
        {
            string estado = "ok";
            try
            {
                //Cambio a etapa a cierre de evaluación
                ViewDto<TablaProgramasDto> actualizaEtapa = new ViewDto<TablaProgramasDto>();
                TablaProgramasDto datosProgAct = new TablaProgramasDto();
                datosProgAct.IdPrograma = idFormulario;
                datosProgAct.Estado.IdParametro = decimal.Parse(constantes.GetValue("Activo"));
                datosProgAct.Etapa.IdParametro = decimal.Parse(constantes.GetValue("EtapaEvaluacion"));
                actualizaEtapa = bips.RegistrarProgramas(new ContextoDto(), datosProgAct, EnumAccionRealizar.Actualizar);
                if (actualizaEtapa.HasError())
                    throw new Exception("Error al actualizar etapa (etapa evaluacion)");
                //Quitar permisos
                ViewDto<TablaExcepcionesPermisosDto> quitaPermisos = new ViewDto<TablaExcepcionesPermisosDto>();
                quitaPermisos = bips.RegistrarExcepcionesFormularios(new ContextoDto(), new TablaExcepcionesPermisosDto() { IdFormulario = idFormulario, Estado = decimal.Parse(constantes.GetValue("EstadoInactivoTemporal")) }, EnumAccionRealizar.Eliminar);
                if (quitaPermisos.HasError())
                    throw new Exception("Error al quitar permiso (etapa evaluacion)");
                //Guardar fecha cambio a etapa evaluacion
                ViewDto<TablaRespuestasDto> respEtapaEval = new ViewDto<TablaRespuestasDto>();
                respEtapaEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = decimal.Parse(constantes.GetValue("FechaEtapaEvaluacion")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                if (!respEtapaEval.Sucess())
                    throw new Exception("error registro fecha etapa evaluacion (borrado)");
                respEtapaEval = new ViewDto<TablaRespuestasDto>();
                respEtapaEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = decimal.Parse(constantes.GetValue("FechaEtapaEvaluacion")), Respuesta = DateTime.Now, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                if (!respEtapaEval.Sucess())
                    throw new Exception("error registro fecha etapa evaluacion (registro)");
                //Guardar usario cambio a etapa evaluacion
                respEtapaEval = new ViewDto<TablaRespuestasDto>();
                respEtapaEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = decimal.Parse(constantes.GetValue("UsuarioEtapaEvaluacion")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                if (!respEtapaEval.Sucess())
                    throw new Exception("error registro usuario etapa evaluacion (borrado)");
                respEtapaEval = new ViewDto<TablaRespuestasDto>();
                respEtapaEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = decimal.Parse(constantes.GetValue("UsuarioEtapaEvaluacion")), Respuesta = idUsuario, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                if (!respEtapaEval.Sucess())
                    throw new Exception("error registro usuario etapa evaluacion (registro)");
            }
            catch (Exception ex)
            {
                estado = string.Empty;
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(estado);
        }

        public Task<String> guardaEvaluacion(int idFormulario, List<PreguntasObservaciones> data, string idUsuario)
        {
            string estado = "ok";
            try {
                if (data.Count > 0)
                {
                    ViewDto<TablaRespuestasDto> respuesta;
                    foreach (var resp in data)
                    {
                        //Borrado
                        respuesta = new ViewDto<TablaRespuestasDto>();
                        respuesta = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = resp.IdPregunta, IdTab = resp.IdTab, TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                        if (!respuesta.Sucess())
                            throw new Exception("error guardado evaluacion (borrado)");
                        //Guardado                                
                        if (!String.IsNullOrEmpty(resp.Respuesta))
                        {
                            respuesta = new ViewDto<TablaRespuestasDto>();
                            respuesta = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = resp.IdPregunta, Respuesta = resp.Respuesta, IdTab = resp.IdTab, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                            if (!respuesta.Sucess())
                                throw new Exception("error guardado evaluacion (Pregunta: " + resp.IdPregunta + ")");
                        }
                    }
                }
                else
                {
                    throw new Exception("Sin datos para guardar evaluacion");
                }
            }
            catch (Exception ex)
            {
                estado = string.Empty;
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(estado);
        }

        public async Task<String> iterarEvaluar(int idFormulario, string idUsuario, List<int> pregIterar, Int16 opcion, int tipoPrograma)
        {
            string estado = "ok";
            try {
                EvaluacionExAnteModels evaluacion = new EvaluacionExAnteModels();
                string mailEvaluacion = string.Empty;
                if (opcion == 1)
                {
                    //Cambio a etapa de evaluación
                    Boolean etapaEval = await cambioEtapa(idFormulario, decimal.Parse(constantes.GetValue("EtapaCierreEvalExAnte")));
                    if (!etapaEval)
                        throw new Exception("error al cambiar etapa (etapa de cierre)");
                    //Creo nueva iteracion
                    string version = await getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaVersionProgramas")), idFormulario);
                    decimal? iteracion = await nuevaIteracionSeguimiento(idFormulario, (String.IsNullOrEmpty(version) ? 0 : int.Parse(version)), idUsuario);
                    if (iteracion == null)
                        throw new Exception("error nueva iteracion");
                    //Creo permiso personalizado
                    if (pregIterar.Count > 0)
                    {
                        //Creo nueva plantilla
                        ViewDto<TablaExcepcionesPlantillasDto> plantilla = new ViewDto<TablaExcepcionesPlantillasDto>();
                        plantilla = bips.RegistrarExcepciones(new ContextoDto(), new TablaExcepcionesPlantillasDto() { Nombre = "plantilla_" + iteracion, Descripcion = "Permiso creado por el usuario: " + idUsuario, TipoFormulario = tipoPrograma, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Insertar);
                        if (plantilla.HasElements())
                        {
                            //Considero preguntas anidadas (padres)
                            ViewDto<TablaParametrosDto> pregAnidadasPadres = new ViewDto<TablaParametrosDto>();
                            pregAnidadasPadres = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("PregAnidadasPadres")), Valor2 = tipoPrograma, Estado = decimal.Parse(constantes.GetValue("Activo")) });
                            ViewDto<TablaParametrosDto> pregAnidadasHijos = new ViewDto<TablaParametrosDto>();
                            pregAnidadasHijos = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("PregAnidadasHijos")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                            ViewDto<TablaParametrosDto> pregAnidadasTotales = new ViewDto<TablaParametrosDto>();
                            pregAnidadasTotales = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("PregAnidadasTotales")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                            ViewDto<TablaExcepcionesPlantillasFormDto> data;
                            ViewDto<TablaExcepcionesPlantillasFormDto> insertTotales;
                            foreach (var item in pregIterar)
                            {
                                data = new ViewDto<TablaExcepcionesPlantillasFormDto>();
                                data = bips.RegistrarExcepcionesForm(new ContextoDto(), new TablaExcepcionesPlantillasFormDto() { IdExcepcionPlantilla = plantilla.Dtos.FirstOrDefault().IdExcepcionPlantilla, IdPregunta = item, TipoExcepcion = decimal.Parse(constantes.GetValue("GuardadoExcep")), Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Insertar);
                                if (!data.Sucess())
                                    throw new Exception("Error al registrar nueva pregunta en la plantilla de excepciones");
                                if (pregAnidadasPadres.HasElements())
                                {
                                    if (pregAnidadasPadres.Dtos.Count(p=>p.Valor == item) > 0)
                                    {
                                        //Busco preguntas anidadas (hijos)
                                        if (pregAnidadasHijos.HasElements())
                                        {
                                            ViewDto<TablaExcepcionesPlantillasFormDto> insertHijos;
                                            foreach (var h in pregAnidadasHijos.Dtos.Where(p=>p.Valor2 == item).ToList())
                                            {
                                                insertHijos = new ViewDto<TablaExcepcionesPlantillasFormDto>();
                                                insertHijos = bips.RegistrarExcepcionesForm(new ContextoDto(), new TablaExcepcionesPlantillasFormDto() { IdExcepcionPlantilla = plantilla.Dtos.FirstOrDefault().IdExcepcionPlantilla, IdPregunta = h.Valor, TipoExcepcion = decimal.Parse(constantes.GetValue("GuardadoExcep")), Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Insertar);
                                                if (!insertHijos.Sucess())
                                                    throw new Exception("Error al registrar nueva pregunta en la plantilla de excepciones (hijos)");
                                            }
                                        }
                                    }
                                }
                                //Agrego totales
                                if (pregAnidadasTotales.HasElements())
                                {
                                    if (pregAnidadasTotales.Dtos.Count(p=>p.Valor == item) > 0)
                                    {
                                        decimal? ultimoValor = 0;
                                        foreach(var x in pregAnidadasTotales.Dtos.Where(p=>p.Valor == item).ToList())
                                        {
                                            if (x.Valor2 != item && ultimoValor != x.Valor2)
                                            {
                                                insertTotales = new ViewDto<TablaExcepcionesPlantillasFormDto>();
                                                insertTotales = bips.RegistrarExcepcionesForm(new ContextoDto(), new TablaExcepcionesPlantillasFormDto() { IdExcepcionPlantilla = plantilla.Dtos.FirstOrDefault().IdExcepcionPlantilla, IdPregunta = x.Valor2, TipoExcepcion = decimal.Parse(constantes.GetValue("GuardadoExcep")), Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Insertar);
                                                if (!insertTotales.Sucess())
                                                    throw new Exception("Error al registrar totales en la plantilla de excepciones");
                                                ultimoValor = x.Valor2;
                                            }
                                        }                                        
                                    }
                                }
                            }
                        }
                        //Asigno permisos a usuarios del programa
                        UsuariosModels usuarios = new UsuariosModels();
                        List<TablaProgramasDto> listaProgramas = new List<TablaProgramasDto>();
                        //Busco usuarios con permisos activos
                        listaProgramas = await usuarios.getPermisosFormularios(new TablaProgramasFiltroDto() { IdPrograma = idFormulario, Estado = decimal.Parse(constantes.GetValue("EstadoInactivoTemporal")) });
                        if (listaProgramas.Count > 0){
                            ViewDto<TablaExcepcionesPermisosDto> permiso;
                            foreach(var prog in listaProgramas)
                            {
                                permiso = new ViewDto<TablaExcepcionesPermisosDto>();
                                permiso = bips.RegistrarExcepcionesFormularios(new ContextoDto(), new TablaExcepcionesPermisosDto() { IdFormulario = iteracion, IdPermiso = plantilla.Dtos.FirstOrDefault().IdExcepcionPlantilla, IdUsuario = prog.IdUser, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Insertar);
                                if (!permiso.Sucess())
                                    throw new Exception("Error al registrar nuevo permiso");
                            }
                        }
                    }
                    //Datos programa
                    ViewDto<TablaProgramasDto> programa = new ViewDto<TablaProgramasDto>();
                    programa = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = iteracion, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Buscar);
                    //Busco correo parametros
                    ViewDto<TablaParametrosDto> casillaEvaluacion = new ViewDto<TablaParametrosDto>();
                    casillaEvaluacion = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("CorreosEvalSeguimiento")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (casillaEvaluacion.HasElements())
                    {
                        foreach (var item in casillaEvaluacion.Dtos)
                        {
                            if (item.IdParametro != item.IdCategoria)
                            {
                                mailEvaluacion += item.Descripcion + ",";
                            }
                        }
                    }
                    //Busco correos sectorialistas
                    ViewDto<TablaParametrosDto> correoSectorialistas = new ViewDto<TablaParametrosDto>();
                    correoSectorialistas = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("CorreoSectorialistas")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (correoSectorialistas.HasElements())
                    {
                        foreach (var item in correoSectorialistas.Dtos)
                        {
                            if (item.Valor == programa.Dtos.FirstOrDefault().IdServicio.IdParametro)
                            {
                                mailEvaluacion += item.Descripcion + ",";
                            }
                        }
                    }
                    //Busco correos contrapartes tecnicas
                    ViewDto<TablaUsuariosDto> contrapartes = new ViewDto<TablaUsuariosDto>();
                    contrapartes = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { IdServicio = programa.Dtos.FirstOrDefault().IdServicio.IdParametro, IdPerfil = decimal.Parse(constantes.GetValue("PerfilContraparte")), IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (contrapartes.HasElements())
                    {
                        foreach (var contraparte in contrapartes.Dtos)
                        {
                            mailEvaluacion += contraparte.Email + ",";
                        }
                    }
                    //Busco otros correos
                    ViewDto<TablaProgramasDto> permisos = new ViewDto<TablaProgramasDto>();
                    permisos = bips.BuscarPermisosFormularios(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = iteracion, Estado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (permisos.HasElements())
                    {
                        ViewDto<TablaUsuariosDto> permisoUser = new ViewDto<TablaUsuariosDto>();
                        List<String> listaCorreos = !String.IsNullOrEmpty(mailEvaluacion) ? mailEvaluacion.Split(',').ToList() : new List<string>();
                        foreach (var user in permisos.Dtos)
                        {
                            permisoUser = new ViewDto<TablaUsuariosDto>();
                            permisoUser = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = user.IdUser, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                            if (permisoUser.HasElements())
                            {
                                if (listaCorreos.Count(p => p == permisoUser.Dtos.FirstOrDefault().Email) == 0)
                                {
                                    mailEvaluacion += permisoUser.Dtos.FirstOrDefault().Email + ",";
                                }
                            }
                        }
                    }
                    //Enviar mail                    
                    string nombreProg = programa.Dtos.SingleOrDefault().Nombre;
                    string ministerioProg = programa.Dtos.SingleOrDefault().Ministerio;
                    string servicioProg = programa.Dtos.SingleOrDefault().Servicio;
                    string asunto = string.Format("Ficha con observaciones - {0} {1}-{2} - Pre-Monitoreo 2024", nombreProg, servicioProg, ministerioProg);
                    string msj1 = string.Format("Estimado/a<br /><br />Le comunicamos que, en el marco del Proceso de Pre-Monitoreo 2024, el programa {0}, perteneciente a {1} – {2}, ha sido revisado y <b>se encuentra con observaciones</b>. Los campos que han sido observados se encuentran detallados en el formulario localizado en la plataforma.<br /><br />A partir de este momento la plataforma estará activa para el ingreso de la información necesaria. En caso de requerir asistencia técnica, comuníquese con su sectorialista del Departamento de Monitoreo de Programas Sociales de la SES.", nombreProg, servicioProg, ministerioProg);
                    string msj2 = "Saludos cordiales.<br />";
                    DatosEmail dataMail = new DatosEmail();
                    dataMail.de = constantes.GetValue("EmailSesDipres");
                    dataMail.para = mailEvaluacion.TrimEnd(',');
                    dataMail.asunto = asunto;
                    dataMail.imagenDipres = false;
                    dataMail.imagen = true;
                    dataMail.mensaje = string.Format(new CuerpoEmail().emailHtmlSES, msj1, msj2);
                    Boolean estadoUsuario = evaluacion.enviaMail(dataMail);
                    if (!estadoUsuario)
                        throw new Exception("error envio mail iteracion");
                }
                else
                {
                    //Cambio a etapa de evaluación
                    Boolean etapaEval = await cambioEtapa(idFormulario, decimal.Parse(constantes.GetValue("CierrePreMonitoreo")));
                    if (!etapaEval)
                        throw new Exception("error al cambiar etapa (etapa de evaluación)");
                    //Registro usuario de cierre de evaluacion
                    ViewDto<TablaRespuestasDto> borradoUsuarioCierreEval = new ViewDto<TablaRespuestasDto>();
                    borradoUsuarioCierreEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaUsuarioCierreEval")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                    if (!borradoUsuarioCierreEval.Sucess())
                        throw new Exception("error registro usuario que cerro evaluacion (borrado)");
                    ViewDto<TablaRespuestasDto> registraUsuarioCierreEval = new ViewDto<TablaRespuestasDto>();
                    registraUsuarioCierreEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaUsuarioCierreEval")), Respuesta = idUsuario, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                    if (!registraUsuarioCierreEval.Sucess())
                        throw new Exception("error registro usuario que cerro evaluacion (registro)");
                    //Registro fecha de cierre de evaluacion
                    ViewDto<TablaRespuestasDto> borradoFechaCierreEval = new ViewDto<TablaRespuestasDto>();
                    borradoFechaCierreEval = new ViewDto<TablaRespuestasDto>();
                    borradoFechaCierreEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaFechaCierreEval")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                    if (!borradoFechaCierreEval.Sucess())
                        throw new Exception("error registro fecha de cierre evaluacion (borrado)");
                    ViewDto<TablaRespuestasDto> registraFechaCierreEval = new ViewDto<TablaRespuestasDto>();
                    registraFechaCierreEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaFechaCierreEval")), Respuesta = DateTime.Now, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                    if (!registraFechaCierreEval.Sucess())
                        throw new Exception("error registro fecha de cierre evaluacion (registro)");
                    //Datos programa
                    ViewDto<TablaProgramasDto> programa = new ViewDto<TablaProgramasDto>();
                    programa = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idFormulario, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Buscar);
                    //Busco correo parametros
                    ViewDto<TablaParametrosDto> casillaEvaluacion = new ViewDto<TablaParametrosDto>();
                    casillaEvaluacion = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("CorreosEvalSeguimiento")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (casillaEvaluacion.HasElements())
                    {
                        foreach (var item in casillaEvaluacion.Dtos)
                        {
                            if (item.IdParametro != item.IdCategoria)
                            {
                                mailEvaluacion += item.Descripcion + ",";
                            }
                        }
                    }
                    //Busco correos sectorialistas
                    ViewDto<TablaParametrosDto> correoSectorialistas = new ViewDto<TablaParametrosDto>();
                    correoSectorialistas = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("CorreoSectorialistas")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (correoSectorialistas.HasElements())
                    {
                        foreach (var item in correoSectorialistas.Dtos)
                        {
                            if (item.Valor == programa.Dtos.FirstOrDefault().IdServicio.IdParametro)
                            {
                                mailEvaluacion += item.Descripcion + ",";
                            }
                        }
                    }
                    //Busco correos contrapartes tecnicas
                    ViewDto<TablaUsuariosDto> contrapartes = new ViewDto<TablaUsuariosDto>();
                    contrapartes = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { IdServicio = programa.Dtos.FirstOrDefault().IdServicio.IdParametro, IdPerfil = decimal.Parse(constantes.GetValue("PerfilContraparte")), IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (contrapartes.HasElements())
                    {
                        foreach (var contraparte in contrapartes.Dtos)
                        {
                            mailEvaluacion += contraparte.Email + ",";
                        }
                    }
                    //Busco otros correos
                    ViewDto<TablaProgramasDto> permisos = new ViewDto<TablaProgramasDto>();
                    permisos = bips.BuscarPermisosFormularios(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idFormulario, Estado = decimal.Parse(constantes.GetValue("EstadoInactivoTemporal")) });
                    if (permisos.HasElements())
                    {
                        ViewDto<TablaUsuariosDto> permisoUser = new ViewDto<TablaUsuariosDto>();
                        List<String> listaCorreos = !String.IsNullOrEmpty(mailEvaluacion) ? mailEvaluacion.Split(',').ToList() : new List<string>();
                        foreach (var user in permisos.Dtos)
                        {
                            permisoUser = new ViewDto<TablaUsuariosDto>();
                            permisoUser = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = user.IdUser, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                            if (permisoUser.HasElements())
                            {
                                if (listaCorreos.Count(p => p == permisoUser.Dtos.FirstOrDefault().Email) == 0)
                                {
                                    mailEvaluacion += permisoUser.Dtos.FirstOrDefault().Email + ",";
                                }
                            }
                        }
                    }
                    //Enviar mail                    
                    string nombreProg = programa.Dtos.SingleOrDefault().Nombre;
                    string ministerioProg = programa.Dtos.SingleOrDefault().Ministerio;
                    string servicioProg = programa.Dtos.SingleOrDefault().Servicio;
                    string asunto = string.Format("Cierre Pre-Monitoreo 2024 - {0} {1}-{2}", nombreProg, servicioProg, ministerioProg);
                    string msj1 = string.Format("Estimado/a<br /><br />Le comunicamos que, en el marco del Proceso de Pre-Monitoreo 2024, el programa {0}, perteneciente a {1} – {2}, ha finalizado su proceso de iteración y <b>pasará a etapa 'Cierre Pre-Monitoreo'</b>.<br /><br />Para resolver cualquier duda, comuníquese con su sectorialista del Departamento de Monitoreo de Programas Sociales de la SES.", nombreProg, servicioProg, ministerioProg);
                    string msj2 = "Saludos cordiales.<br />";
                    DatosEmail dataMail = new DatosEmail();
                    dataMail.de = constantes.GetValue("EmailSesDipres");
                    dataMail.para = mailEvaluacion.TrimEnd(',');
                    dataMail.asunto = asunto;
                    dataMail.imagenDipres = false;
                    dataMail.imagen = true;
                    dataMail.mensaje = string.Format(new CuerpoEmail().emailHtmlSES, msj1, msj2);
                    Boolean estadoUsuario = evaluacion.enviaMail(dataMail);
                    if (!estadoUsuario)
                        throw new Exception("error envio mail iteracion");
                }
            }
            catch(Exception ex)
            {
                estado = string.Empty;
                log.Error(ex.Message, ex);
                throw ex;
            }
            return estado;
        }

        public Task<TablaConsultasDto> ingresarComentarios(TablaConsultasDto datos, string idUsuario)
        {
            TablaConsultasDto estado = new TablaConsultasDto();
            try {
                ViewDto<TablaConsultasDto> consulta = new ViewDto<TablaConsultasDto>();
                datos.IdUsuario = idUsuario;
                datos.Fecha = DateTime.Now;
                datos.Estado = decimal.Parse(constantes.GetValue("Activo"));
                consulta = bips.RegistrarConsultas(new ContextoDto(), datos, (datos.IdConsulta == null ? EnumAccionRealizar.Insertar : EnumAccionRealizar.Actualizar));
                if (consulta.Sucess()){
                    ViewDto<TablaMenuFormulariosDto> menuPadre = new ViewDto<TablaMenuFormulariosDto>();
                    menuPadre = bips.BuscarMenuFormularios(new ContextoDto(), new TablaMenuFormulariosFiltroDto() { IdMenu = datos.IdMenu });
                    if (menuPadre.HasElements())
                        datos.MenuPadre = menuPadre.Dtos.FirstOrDefault().TipoMenu;
                    ViewDto<TablaMenuFormulariosDto> menuHijo = new ViewDto<TablaMenuFormulariosDto>();
                    menuHijo = bips.BuscarMenuFormularios(new ContextoDto(), new TablaMenuFormulariosFiltroDto() { IdMenu = datos.IdMenuHijo });
                    if (menuHijo.HasElements())
                        datos.MenuHijo = menuHijo.Dtos.FirstOrDefault().TipoMenu;
                    ViewDto<TablaPreguntasFormulariosDto> pregunta = new ViewDto<TablaPreguntasFormulariosDto>();
                    pregunta = bips.BuscarPreguntas(new ContextoDto(), new TablaPreguntasFormulariosFiltroDto() { IdPregunta = datos.IdPregunta });
                    if (pregunta.HasElements())
                        datos.Pregunta = pregunta.Dtos.FirstOrDefault().Pregunta;
                    EvaluacionExAnteModels evaluacion = new EvaluacionExAnteModels();
                    string mailEvaluacion = string.Empty;
                    //Datos programa
                    ViewDto<TablaProgramasDto> programa = new ViewDto<TablaProgramasDto>();
                    programa = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = datos.IdPrograma, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Buscar);
                    //Busco datos usuarios
                    ViewDto<TablaUsuariosDto> usuario = new ViewDto<TablaUsuariosDto>();
                    usuario = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = idUsuario, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (usuario.HasElements())
                        mailEvaluacion += usuario.Dtos.FirstOrDefault().Email + ",";
                    //Busco correos sectorialistas
                    ViewDto<TablaParametrosDto> correoSectorialistas = new ViewDto<TablaParametrosDto>();
                    correoSectorialistas = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("CorreoSectorialistas")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (correoSectorialistas.HasElements())
                        foreach (var item in correoSectorialistas.Dtos.Where(p=>p.Descripcion != usuario.Dtos.FirstOrDefault().Email))
                            if (item.Valor == programa.Dtos.FirstOrDefault().IdServicio.IdParametro)
                                mailEvaluacion += item.Descripcion + ",";
                    //Busco otros correos
                    ViewDto<TablaProgramasDto> permisos = new ViewDto<TablaProgramasDto>();
                    permisos = bips.BuscarPermisosFormularios(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = datos.IdPrograma });
                    if (permisos.HasElements())
                    {
                        ViewDto<TablaUsuariosDto> permisoUser = new ViewDto<TablaUsuariosDto>();
                        List<String> listaCorreos = !String.IsNullOrEmpty(mailEvaluacion) ? mailEvaluacion.Split(',').ToList() : new List<string>();
                        foreach (var user in permisos.Dtos)
                        {
                            permisoUser = new ViewDto<TablaUsuariosDto>();
                            permisoUser = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = user.IdUser, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                            if (permisoUser.HasElements())
                                if (listaCorreos.Count(p => p == permisoUser.Dtos.FirstOrDefault().Email) == 0)
                                    mailEvaluacion += permisoUser.Dtos.FirstOrDefault().Email + ",";
                        }
                    }
                    //Envia correo                    
                    string nombreProg = programa.Dtos.SingleOrDefault().Nombre;
                    string ministerioProg = programa.Dtos.SingleOrDefault().Ministerio;
                    string servicioProg = programa.Dtos.SingleOrDefault().Servicio;
                    string asunto = string.Format("Nuevo comentario - {0}", nombreProg);
                    string msj1 = string.Format("Estimado/a<br /><br />Le comunicamos que se ha agregado un nuevo comentario al programa <b>{0}</b> en la pregunta: <br /><br /><ul><li type='disc'><b>{1}</b></li></ul>Para revisar el comentario ingrese a la plataforma.", nombreProg, datos.Pregunta);
                    string msj2 = "Saludos cordiales.<br />";
                    DatosEmail dataMail = new DatosEmail();
                    dataMail.de = constantes.GetValue("EmailSesDipres");
                    dataMail.para = mailEvaluacion.TrimEnd(',');
                    dataMail.asunto = asunto;
                    dataMail.imagenDipres = true;
                    dataMail.mensaje = string.Format(new CuerpoEmail().emailHtmlSeguimiento, msj1, msj2);
                    Boolean estadoUsuario = evaluacion.enviaMail(dataMail);
                    if (!estadoUsuario)
                        throw new Exception("error envio mail nuevo comentario");
                }
                else{
                    throw new Exception("Error al guardar consulta");
                }
                estado = datos;
            }
            catch (Exception ex)
            {
                estado = new TablaConsultasDto();
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(estado);
        }

        public Task<String> ingresarRespuesta(int idConsulta, string idUsuario, string respuesta)
        {
            string estado = "ok";
            try
            {
                ViewDto<TablaRespuestasConsultasDto> consulta = new ViewDto<TablaRespuestasConsultasDto>();
                consulta = bips.RegistrarRespuestasConsultas(new ContextoDto(), new TablaRespuestasConsultasDto() { IdConsulta = idConsulta, IdUsuario = idUsuario, Respuesta = respuesta, Fecha = DateTime.Now, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Insertar);
                if (!consulta.Sucess())
                    throw new Exception("Error al guardar respuesta");
            }
            catch (Exception ex)
            {
                estado = string.Empty;
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(estado);
        }

        public Task<IList<TablaRespuestasConsultasDto>> getRespuestasConsultasFiltro(int idConsulta)
        {
            IList<TablaRespuestasConsultasDto> comentarios = new List<TablaRespuestasConsultasDto>();
            try
            {
                ViewDto<TablaRespuestasConsultasDto> consultas = new ViewDto<TablaRespuestasConsultasDto>();
                consultas = bips.BuscarRespuestasConsultas(new ContextoDto(), new TablaRespuestasConsultasDto() { IdConsulta = idConsulta, Estado = decimal.Parse(constantes.GetValue("Activo")) });
                if (!consultas.Sucess())
                    throw new Exception("Error al buscar respuestas");
                if (consultas.HasElements())
                    comentarios = consultas.Dtos.OrderBy(p=>p.IdConsulta).OrderBy(p=> p.Fecha).ToList();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(comentarios);
        }

        public async Task<String> enviaEvaluacion(int idFormulario, int tipo, string idUsuario)
        {
            string estado = "ok";
            try
            {
                EvaluacionExAnteModels evaluacion = new EvaluacionExAnteModels();
                UsuariosModels usuarios = new UsuariosModels();
                //Datos programa
                ViewDto<TablaProgramasDto> programa = new ViewDto<TablaProgramasDto>();
                programa = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idFormulario, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Buscar);                
                if (programa.HasElements())
                {
                    //Mail por defecto
                    string mailEvaluacion = constantes.GetValue("EmailExAnte") + ",";
                    //Evaluadores Ex-Ante
                    string evaluador1 = await getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaEvaluador1")), idFormulario);
                    string evaluador2 = await getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaEvaluador2")), idFormulario);
                    List<TablaUsuariosDto> evalUsuario1 = new List<TablaUsuariosDto>();
                    List<TablaUsuariosDto> evalUsuario2 = new List<TablaUsuariosDto>();
                    if (!String.IsNullOrEmpty(evaluador1))
                    {
                        evalUsuario1 = await usuarios.getUsuariosFiltro(new TablaUsuariosFiltroDto() { Id = evaluador1 });
                        if (evalUsuario1.Count > 0)
                        {
                            if (evalUsuario1.FirstOrDefault().Id != idUsuario)
                                mailEvaluacion += evalUsuario1.FirstOrDefault().Email + ",";
                        }
                    }                        
                    if (!String.IsNullOrEmpty(evaluador2))
                    {
                        evalUsuario2 = await usuarios.getUsuariosFiltro(new TablaUsuariosFiltroDto() { Id = evaluador2 });
                        if (evalUsuario2.Count > 0)
                        {
                            if (evalUsuario2.FirstOrDefault().Id != idUsuario)
                                mailEvaluacion += evalUsuario2.FirstOrDefault().Email + ",";
                        }
                    }                        
                    //var tipoOferta = await getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaTipoOferta")), idFormulario);
                    if (tipo == 1)
                    {
                        //Busco correos jefaturas
                        ViewDto<TablaParametrosDto> correoJefatura = new ViewDto<TablaParametrosDto>();
                        correoJefatura = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("CorreosJefaturas")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                        if (correoJefatura.HasElements())
                        {
                            foreach (var correo in correoJefatura.Dtos.Where(p=>p.IdParametro!=p.IdCategoria))
                            {
                                /*if (correo.Valor.ToString() == tipoOferta)
                                {*/
                                mailEvaluacion += correo.Descripcion + ",";
                                //}
                            }
                        }
                    }
                    else
                    {
                        //Busco correos sectorialistas
                        ViewDto<TablaParametrosDto> correoSectorialistas = new ViewDto<TablaParametrosDto>();
                        correoSectorialistas = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("CorreoSectorialistas")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                        if (correoSectorialistas.HasElements())
                        {
                            foreach (var item in correoSectorialistas.Dtos)
                            {
                                if (item.Valor == programa.Dtos.FirstOrDefault().IdServicio.IdParametro)
                                {
                                    mailEvaluacion += item.Descripcion + ",";
                                }
                            }
                        }
                    }                    
                    //Usuario de envio
                    var evaluador = await usuarios.getUsuariosFiltro(new TablaUsuariosFiltroDto() { Id = idUsuario });                    
                    //Link informe evaluacion
                    ViewDto<TablaParametrosDto> linkInfoEval = new ViewDto<TablaParametrosDto>();
                    linkInfoEval = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("InformesEvaluacion")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                    string linkInformeEvaluacion = string.Empty;
                    if (linkInfoEval.HasElements())
                        linkInformeEvaluacion = String.Format(linkInfoEval.Dtos.FirstOrDefault(p => p.IdParametro != p.IdCategoria).Descripcion, idFormulario, programa.Dtos.FirstOrDefault().Ano);                    
                    //Envio de mail
                    DatosEmail dataMail = new DatosEmail();
                    dataMail.de = evaluador.FirstOrDefault().Email;
                    dataMail.para = mailEvaluacion + evaluador.FirstOrDefault().Email;
                    dataMail.asunto = "Programa evaluado" + (tipo == 1 ? " (Jefaturas)" : " (Sectorialista)");
                    dataMail.imagen = true;
                    string msj1 = string.Empty;
                    if (tipo == 1)
                    {
                        msj1 = "Estimadas Jefaturas:<br/><br/> El programa <b>{0} {1}</b> ({2} - {3}) se encuentra evaluado por <b>{4}</b>. Por favor, dar el visto bueno para su envío. Se recuerda que <b>tiene un plazo de 2 días hábiles para revisar</b> la evaluación y emitir comentarios (si los tiene).<br/><br/> Acceso al Informe de Evaluación: <a href =\"{5}\">Aquí</a>";
                    }
                    else
                    {
                        msj1 = "Estimado/a sectorialista:<br/><br/> El programa <b>{0} {1}</b> ({2} - {3}) se encuentra evaluado por <b>{4}</b>. Por favor, dar el visto bueno para su envío. Se recuerda que <b>tiene un plazo de 1 día hábil para revisar</b> la evaluación y emitir comentarios (si los tiene).<br/><br/> Acceso al Informe de Evaluación: <a href =\"{5}\">Aquí</a>";
                    }
                    string msj2 = "Saluda Atentamente, <br/> Evaluador Ex-Ante<br/>{0}<br/>{1}<br/>{2}<br/>{3}";
                    string nombreProg = programa.Dtos.SingleOrDefault().Nombre;
                    string version = await getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaVersionProgramas")), idFormulario);
                    string versionFinal = (String.IsNullOrEmpty(version) ? string.Empty : (int.Parse(version) >= 1 ? "versión " + version : string.Empty));
                    string ministerioProg = programa.Dtos.SingleOrDefault().Ministerio;
                    string servicioProg = programa.Dtos.SingleOrDefault().Servicio;
                    string nombreUsuario = evaluador.FirstOrDefault().Nombre;
                    string ministerio = evaluador.FirstOrDefault().Ministerio;
                    string servicio = evaluador.FirstOrDefault().Servicio;
                    string correoUsuario = evaluador.FirstOrDefault().Email;
                    dataMail.mensaje = string.Format(new CuerpoEmail().emailHtmlEvaluacion, string.Format(msj1, nombreProg, versionFinal, ministerioProg, servicioProg, nombreUsuario, linkInformeEvaluacion), string.Format(msj2, nombreUsuario, ministerio, servicio, correoUsuario));
                    //Boolean estadoUsuario = evaluacion.enviaMail(dataMail);
                    //if (!estadoUsuario)
                    //    throw new Exception("error envio mail de evaluacion a sectorialistas");
                    //Registro fecha de envio informe evaluacion
                    ViewDto<TablaRespuestasDto> registraFechaEval = new ViewDto<TablaRespuestasDto>();
                    if (tipo == 1)
                    {
                        ViewDto<TablaRespuestasDto> buscarFechasAnteriores = new ViewDto<TablaRespuestasDto>();
                        buscarFechasAnteriores = bips.BuscarRespuestas(new ContextoDto(), new TablaRespuestasFiltroDto() { IdFormulario = idFormulario, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaFechaEnvioInforEval")) });
                        int countFechasAnteriores = (buscarFechasAnteriores.HasElements() ? buscarFechasAnteriores.Dtos.Count() + 1 : 1);                        
                        registraFechaEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdTab = countFechasAnteriores, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaFechaEnvioInforEval")), Respuesta = DateTime.Now, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                        if (!registraFechaEval.Sucess())
                            throw new Exception("error registro fecha envio informe evaluacion (registro)");
                    }
                    else
                    {
                        ViewDto<TablaRespuestasDto> borradoFechaEval = new ViewDto<TablaRespuestasDto>();
                        borradoFechaEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = decimal.Parse(constantes.GetValue("FechaEnvioSectorialista")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                        if (!borradoFechaEval.Sucess())
                            throw new Exception("error registro fecha envio informe evaluacion (borrado)");
                        registraFechaEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = decimal.Parse(constantes.GetValue("FechaEnvioSectorialista")), Respuesta = DateTime.Now, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                        if (!registraFechaEval.Sucess())
                            throw new Exception("error registro fecha envio informe evaluacion (registro)");
                    }                    
                    //Quitar permisos
                    ViewDto<TablaExcepcionesPermisosDto> quitaPermisos = new ViewDto<TablaExcepcionesPermisosDto>();
                    quitaPermisos = bips.RegistrarExcepcionesFormularios(new ContextoDto(), new TablaExcepcionesPermisosDto() { IdFormulario = idFormulario, IdUsuario = idUsuario, Estado = decimal.Parse(constantes.GetValue("EstadoInactivoTemporal")) }, EnumAccionRealizar.Eliminar);
                    if (quitaPermisos.HasError())
                        throw new Exception("Error al quitar permiso (modulo evaluacion) evaluador 1");
                    //Cambio a etapa
                    if (tipo == 1)
                    {
                        /*Boolean etapaEval = await cambioEtapa(idFormulario, decimal.Parse(constantes.GetValue("EtapaProgramaEvaluado")));
                        if (!etapaEval)
                            throw new Exception("error al cambiar etapa (etapa de programa evaluado)");*/
                        ViewDto<TablaRespuestasDto> borradoEtapaRevisionJefe = new ViewDto<TablaRespuestasDto>();
                        borradoEtapaRevisionJefe = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = decimal.Parse(constantes.GetValue("EtapaRevisionJefe")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                        if (!borradoEtapaRevisionJefe.Sucess())
                            throw new Exception("error registro etapa revision jefatura (borrado)");
                        ViewDto<TablaRespuestasDto> registroEtapaRevJeje = new ViewDto<TablaRespuestasDto>();
                        registroEtapaRevJeje = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = decimal.Parse(constantes.GetValue("EtapaRevisionJefe")), Respuesta = DateTime.Now, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                        if (!registroEtapaRevJeje.Sucess())
                            throw new Exception("error registro etapa revision jefatura (registro)");
                    }
                    else
                    {
                        decimal idpregunta = (tipo == 1 ? decimal.Parse(constantes.GetValue("PreguntaFechaEnvioInforEval")) : decimal.Parse(constantes.GetValue("FechaEnvioSectorialista")));
                        ViewDto<TablaRespuestasDto> borradoEtapaSect = new ViewDto<TablaRespuestasDto>();
                        borradoEtapaSect = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = idpregunta, TipoDelete = decimal.Parse(constantes.GetValue("EtapaRevisionSect")) }, EnumAccionRealizar.Eliminar);
                        if (!borradoEtapaSect.Sucess())
                            throw new Exception("error registro etapa envio sectorialista (borrado)");
                        ViewDto<TablaRespuestasDto> registroEtapaSect = new ViewDto<TablaRespuestasDto>();
                        registroEtapaSect = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idFormulario, IdPregunta = idpregunta, Respuesta = DateTime.Now, TipoInsert = decimal.Parse(constantes.GetValue("EtapaRevisionSect")) }, EnumAccionRealizar.Insertar);
                        if (!registroEtapaSect.Sucess())
                            throw new Exception("error registro etapa envio sectorialista (registro)");
                    }
                }
            }
            catch (Exception ex)
            {
                estado = string.Empty;
                log.Error(ex.Message, ex);
                throw ex;
            }
            return (estado);
        }

        public Task<String> BorrarComentario(int idConsulta)
        {
            string estado = "ok";
            try
            {
                ViewDto<TablaConsultasDto> consulta = new ViewDto<TablaConsultasDto>();
                consulta = bips.RegistrarConsultas(new ContextoDto(), new TablaConsultasDto() { IdConsulta = idConsulta, Estado = decimal.Parse(constantes.GetValue("Inactivo")) }, EnumAccionRealizar.Eliminar);
                if (!consulta.Sucess())
                    throw new Exception("Error al eliminar comentario");
            }
            catch (Exception ex)
            {
                estado = string.Empty;
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(estado);
        }
    }
}
