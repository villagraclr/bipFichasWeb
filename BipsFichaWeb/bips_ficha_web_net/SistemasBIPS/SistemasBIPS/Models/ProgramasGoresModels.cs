using log4net;
using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Core.Providers;
using MDS.Dto;
using MDS.Svc;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

namespace SistemasBIPS.Models
{
    public class ProgramasGoresModels
    {
        private static ISistemasBIPSSvc bips = null;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private IProviderConstante constantes = null;

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public ProgramasGoresModels()
        {
            bips = (ISistemasBIPSSvc)Activator.CreateInstance(typeof(SistemasBIPSSvc));
            constantes = (IProviderConstante)Activator.CreateInstance(typeof(ProviderConstante));
        }

        /// <summary>
        /// Obtiene lista de formularios para un usuario determinado según filtro
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public Task<IList<TablaProgramasDto>> getProgramasFiltro(TablaProgramasFiltroDto filtros)
        {
            IList<TablaProgramasDto> programas = new List<TablaProgramasDto>();
            try
            {
                ViewDto<TablaProgramasDto> data = new ViewDto<TablaProgramasDto>();
                data = bips.BuscarFormulariosUsuarios(new ContextoDto(), filtros);
                if (data.HasElements())
                    programas = data.Dtos;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(programas);
        }

        /// <summary>
        /// Obtiene lista de formularios según filtro años y/o ministerios/servicios
        /// </summary>
        /// <param name="filtroAnos"></param>
        /// <param name="filtroMinisterios"></param>
        /// <returns></returns>
        public async Task<List<TablaProgramasDto>> getProgramasFiltro(string filtroAnos, string filtroGores, string idUsuario, int tipo)
        {
            List<TablaProgramasDto> objProgramas = new List<TablaProgramasDto>();
            try
            {
                IList<TablaProgramasDto> objPrograma = new List<TablaProgramasDto>();
                if (!string.IsNullOrEmpty(filtroAnos) || !string.IsNullOrEmpty(filtroGores))
                {
                    Dictionary<String, Object> anosLista = !string.IsNullOrEmpty(filtroAnos) ? JsonConvert.DeserializeObject<Dictionary<String, Object>>(filtroAnos) : new Dictionary<String, Object>();
                    Dictionary<String, Object> ministeriosLista = !string.IsNullOrEmpty(filtroGores) ? JsonConvert.DeserializeObject<Dictionary<String, Object>>(filtroGores) : new Dictionary<String, Object>();
                    //Filtro años
                    if (anosLista.Count > 0)
                    {
                        JsonTextReader ano = new JsonTextReader(new StringReader(anosLista.Values.First().ToString()));
                        while (ano.Read())
                        {
                            if (ano.Value != null)
                            {
                                //Filtro ministerios, años
                                if (ministeriosLista.Count > 0)
                                {
                                    foreach (var item in ministeriosLista)
                                    {
                                        JsonTextReader ministerio = new JsonTextReader(new StringReader(item.Value.ToString()));
                                        Boolean salir = false;
                                        while (ministerio.Read())
                                        {
                                            if (ministerio.Value != null)
                                            {
                                                objPrograma = await getProgramasFiltro(new TablaProgramasFiltroDto() { Ano = ano.Value.ToString() == "0" ? (decimal?)null : decimal.Parse(ano.Value.ToString()), IdServicio = ministerio.Value.ToString() == "0" ? (decimal?)null : decimal.Parse(ministerio.Value.ToString()), IdUser = idUsuario, IdPlataforma = decimal.Parse(constantes.GetValue("PlataformaCargaBIPS")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                                                if (objPrograma != null)
                                                    objProgramas.AddRange(objPrograma);

                                                if (ministerio.Value.ToString() == "0")
                                                {
                                                    salir = true;
                                                    break;
                                                }
                                            }
                                        }
                                        if (salir)
                                            break;
                                    }
                                }
                                else
                                {
                                    //Filtro años
                                    objPrograma = await getProgramasFiltro(new TablaProgramasFiltroDto() { Ano = ano.Value.ToString() == "0" ? (decimal?)null : decimal.Parse(ano.Value.ToString()), IdUser = idUsuario, IdPlataforma = decimal.Parse(constantes.GetValue("PlataformaCargaBIPS")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                                    if (objPrograma != null)
                                        objProgramas.AddRange(objPrograma);
                                }
                                if (ano.Value.ToString() == "0")
                                    break;
                            }
                        }
                    }
                    else
                    {
                        if (ministeriosLista.Count > 0)
                        {
                            foreach (var item in ministeriosLista)
                            {
                                JsonTextReader ministerio = new JsonTextReader(new StringReader(item.Value.ToString()));
                                Boolean salir = false;
                                while (ministerio.Read())
                                {
                                    if (ministerio.Value != null)
                                    {
                                        //Filtro ministerios
                                        objPrograma = await getProgramasFiltro(new TablaProgramasFiltroDto() { IdServicio = decimal.Parse(ministerio.Value.ToString()) == 0 ? (decimal?)null : decimal.Parse(ministerio.Value.ToString()), IdUser = idUsuario, IdPlataforma = decimal.Parse(constantes.GetValue("PlataformaCargaBIPS")) });
                                        if (objPrograma != null)
                                            objProgramas.AddRange(objPrograma);
                                    }
                                    if (ministerio.Value.ToString() == "0")
                                    {
                                        salir = true;
                                        break;
                                    }
                                }
                                if (salir)
                                    break;
                            }
                        }
                    }
                }
                if (objProgramas.Count > 0)
                    objProgramas = objProgramas.DistinctBy(p => p.IdPrograma).ToList();
                if (objProgramas.Count > 0)
                    objProgramas.RemoveAll(p => p.TipoGeneral != tipo);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return objProgramas;
        }

        /// <summary>
        /// Obtiene lista de formularios según filtro usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public async Task<List<TablaProgramasDto>> getProgramasFiltro(string idUsuario, int tipo)
        {
            List<TablaProgramasDto> objProgramas = new List<TablaProgramasDto>();
            try
            {
                IList<TablaProgramasDto> objPrograma = new List<TablaProgramasDto>();
                IList<int> anos = new List<int>();
                anos = await getAnos();
                if (anos.Count > 0)
                {
                    objPrograma = await getProgramasFiltro(new TablaProgramasFiltroDto() { Ano = anos.Max(), IdUser = idUsuario, TipoFormulario = tipo, IdPlataforma = decimal.Parse(constantes.GetValue("PlataformaCargaBIPS")), Estado = decimal.Parse(constantes.GetValue("Activo")), TipoGrupo = decimal.Parse(constantes.GetValue("FormulariosPropios")) });
                    if (objPrograma != null)
                    {
                        if (objPrograma.Count > 0)
                            objProgramas.AddRange(objPrograma);

                        if (objProgramas.Count > 0)
                            objProgramas = objProgramas.DistinctBy(p => p.IdPrograma).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return objProgramas;
        }

        /// <summary>
        /// Obtiene lista de años según programas existentes en la BD
        /// </summary>
        /// <returns></returns>
        public Task<IList<int>> getAnos()
        {
            IList<int> listaAnos = new List<int>();
            ViewDto<TablaProgramasDto> anos = new ViewDto<TablaProgramasDto>();
            try
            {
                anos = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto(), EnumAccionRealizar.BuscarAnosGores);
                if (anos.HasElements())
                    anos.Dtos.ForEach(p => { listaAnos.Add(int.Parse(p.Ano.ToString())); });
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(listaAnos);
        }

        /// <summary>
        /// Obtiene listado de gobiernos regionales
        /// </summary>
        /// <returns></returns>
        public Task<List<TablaParametrosDto>> getListaGores()
        {
            List<TablaParametrosDto> gores = new List<TablaParametrosDto>();
            try
            {
                ViewDto<TablaParametrosDto> listadoGores = new ViewDto<TablaParametrosDto>();
                listadoGores = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("ListadoGores")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                if (listadoGores.HasElements())
                    gores = listadoGores.Dtos.Where(p => p.IdParametro != p.IdCategoria).ToList();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(gores);
        }

        public async Task<List<string>> registraNuevoPerfil(String nombre, String idUsuario, int gore)
        {
            List<string> registro = new List<string>();
            try
            {
                FormulariosModels formulario = new FormulariosModels();
                TablaUsuariosDto usuario = await formulario.getDatosUsuario(idUsuario);
                TablaProgramasDto data = new TablaProgramasDto();
                data.IdMinisterio.IdParametro = gore;
                data.IdServicio.IdParametro = gore;
                data.Estado.IdParametro = decimal.Parse(constantes.GetValue("Activo"));
                data.Nombre = nombre;
                data.IdTipoFormulario = decimal.Parse(constantes.GetValue("TipoPerfilGore"));
                data.Ano = DateTime.Now.Year;
                ViewDto<TablaProgramasDto> reg = new ViewDto<TablaProgramasDto>();
                reg = bips.RegistrarProgramas(new ContextoDto(), data, EnumAccionRealizar.Insertar);
                if (reg.Sucess())
                {
                    if (reg.Dtos.SingleOrDefault().IdPrograma > 0)
                    {
                        registro.Add("ok");
                        //Respuesta nombre gore
                        ViewDto<TablaRespuestasDto> regResp = new ViewDto<TablaRespuestasDto>();
                        regResp = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdPregunta = decimal.Parse(constantes.GetValue("PreguntaNombreGores")), IdFormulario = reg.Dtos.SingleOrDefault().IdPrograma, Respuesta = data.Nombre, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                        if (regResp.HasError())
                            throw new Exception("Error al registrar respuestas (nombre perfil)");
                        //Respuesta gore
                        regResp = new ViewDto<TablaRespuestasDto>();
                        regResp = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdPregunta = decimal.Parse(constantes.GetValue("PreguntaGore")), IdFormulario = reg.Dtos.SingleOrDefault().IdPrograma, Respuesta = data.IdMinisterio.IdParametro, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                        if (regResp.HasError())
                            throw new Exception("Error al registrar respuestas (gore)");
                        //Respuesta version
                        regResp = new ViewDto<TablaRespuestasDto>();
                        regResp = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdPregunta = decimal.Parse(constantes.GetValue("PreguntaVersionProgramas")), IdFormulario = reg.Dtos.SingleOrDefault().IdPrograma, Respuesta = int.Parse(constantes.GetValue("VersionInicialProgramas")), TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                        if (regResp.HasError())
                            throw new Exception("Error al registrar respuestas (version)");
                        //Registra grupo todos gore por defecto
                        ViewDto<TablaFormulariosGruposDto> regGrupo = new ViewDto<TablaFormulariosGruposDto>();
                        regGrupo = new ViewDto<TablaFormulariosGruposDto>();
                        regGrupo = bips.RegistrarFormulariosGrupos(new ContextoDto(), new TablaFormulariosGruposDto() { IdGrupoFormulario = decimal.Parse(constantes.GetValue("GrupoTodosGORE")), IdFormulario = reg.Dtos.SingleOrDefault().IdPrograma, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Insertar);
                        if (regGrupo.HasError())
                            throw new Exception("Error al registrar formulario en el grupo gore");
                        //Registra grupo gore por region
                        NuevoFormulariosViewModels viewModelNuevoFormulario = new NuevoFormulariosViewModels();
                        FormulariosMantenedorModels formularioMantenedor = new FormulariosMantenedorModels();
                        viewModelNuevoFormulario.listaGrupos = await formularioMantenedor.getGruposFormularios(new TablaGruposFormulariosFiltroDto() { Estado = decimal.Parse(constantes.GetValue("Activo")) });
                        var listaGruposFiltrada = viewModelNuevoFormulario.listaGrupos
                        .Where(grupo => { int descripcionInt; return int.TryParse(grupo.Descripcion, out descripcionInt) && descripcionInt == gore; }).ToList();
                        int idGrupoGoreRegion = int.Parse(listaGruposFiltrada.FirstOrDefault().IdGrupoFormulario.ToString());
                        regGrupo = new ViewDto<TablaFormulariosGruposDto>();
                        regGrupo = bips.RegistrarFormulariosGrupos(new ContextoDto(), new TablaFormulariosGruposDto() { IdGrupoFormulario = idGrupoGoreRegion, IdFormulario = reg.Dtos.SingleOrDefault().IdPrograma, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Insertar);
                        if (regGrupo.HasError())
                            throw new Exception("Error al registrar formulario en el grupo todos");
                        //Asigna permiso usuario
                        ViewDto<TablaExcepcionesPermisosDto> regPermiso = new ViewDto<TablaExcepcionesPermisosDto>();
                        regPermiso = bips.RegistrarExcepcionesFormularios(new ContextoDto(), new TablaExcepcionesPermisosDto() { IdUsuario = idUsuario, IdFormulario = reg.Dtos.SingleOrDefault().IdPrograma, IdPermiso = decimal.Parse(constantes.GetValue("PermisoPerfilGore")), Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Insertar);
                        if (regPermiso.HasError())
                            throw new Exception(regPermiso.Error.Detalle);
                        registro.Add(reg.Dtos.SingleOrDefault().IdEncriptado);
                    }
                    else
                    {
                        registro.Add("Error al crear formulario");
                    }
                }
                else if (reg.HasError())
                {
                    registro.Add(reg.Error.Detalle);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return (registro);
        }

        public Task<List<TablaProgramasDto>> getEvaluacionPerfil(String idUsuario)
        {
            List<TablaProgramasDto> data = new List<TablaProgramasDto>();
            try
            {
                decimal tipoFormulario = decimal.Parse(constantes.GetValue("TipoPerfilGore"));
                ViewDto<TablaProgramasDto> buscarProgramas = new ViewDto<TablaProgramasDto>();
                buscarProgramas = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { Ano = DateTime.Now.Year, TipoFormulario = tipoFormulario }, EnumAccionRealizar.BuscarExAntePerfil);
                if (buscarProgramas.HasElements())
                    data = buscarProgramas.Dtos;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(data);
        }

        public Task<List<TablaProgramasDto>> getEvaluacionPrograma(String idUsuario)
        {
            List<TablaProgramasDto> data = new List<TablaProgramasDto>();
            try
            {
                decimal tipoFormulario = decimal.Parse(constantes.GetValue("TipoProgramaGore"));
                ViewDto<TablaProgramasDto> buscarProgramas = new ViewDto<TablaProgramasDto>();
                buscarProgramas = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { Ano = DateTime.Now.Year, TipoFormulario = tipoFormulario }, EnumAccionRealizar.BuscarExAntePerfil);
                if (buscarProgramas.HasElements())
                    data = buscarProgramas.Dtos;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(data);
        }

        public async Task<string> asignaEvaluadoresPerfil(int idPrograma, string idEvaluador, string idEvaluadorAnterior, string idUsuario)
        {
            string registro = "ok";
            try
            {
                //Busco correos de envio
                string mailEvaluacion = constantes.GetValue("EmailExAnteGore");
                //Enviar mail a evaluador asignado
                ViewDto<TablaUsuariosDto> usuario = new ViewDto<TablaUsuariosDto>();
                usuario = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = idUsuario, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                ViewDto<TablaUsuariosDto> evaluador = new ViewDto<TablaUsuariosDto>();
                evaluador = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = idEvaluador, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                ViewDto<TablaProgramasDto> programa = new ViewDto<TablaProgramasDto>();
                programa = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idPrograma, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Buscar);
                DatosEmail dataMail = new DatosEmail();
                dataMail.de = usuario.Dtos.SingleOrDefault().Email;
                dataMail.para = mailEvaluacion + "," + evaluador.Dtos.SingleOrDefault().Email;
                dataMail.asunto = "Asignación Perfil GORE";
                dataMail.imagen = true;
                string msj1 = "Estimado Evaluador:<br/> Se le ha asignado el perfil <b>{0} {1}</b> (Gobierno regional de {2}) para ser evaluado. Se recuerda que tiene un plazo de 10 días hábiles para evaluar y enviar su informe.";
                string msj2 = "Saluda Atentamente, <br/> Coordinador Evaluación Ex-Ante <br/>{0}<br/>{1}<br/>{2}";
                string nombreUsuario = usuario.Dtos.SingleOrDefault().Nombre;
                string nombreProg = programa.Dtos.SingleOrDefault().Nombre;
                string ministerioProg = programa.Dtos.SingleOrDefault().Ministerio;
                string ministerio = usuario.Dtos.SingleOrDefault().Ministerio;
                string correoUsuario = usuario.Dtos.SingleOrDefault().Email;
                FormulariosModels formulario = new FormulariosModels();
                string version = await formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaVersionProgramas")), idPrograma);
                string versionFinal = (String.IsNullOrEmpty(version) ? string.Empty : (int.Parse(version) >= 1 ? "versión " + version : string.Empty));
                dataMail.mensaje = string.Format(new CuerpoEmail().emailHtmlEvaluacion, string.Format(msj1, nombreProg, versionFinal, ministerioProg), string.Format(msj2, nombreUsuario, ministerio, correoUsuario));
                EvaluacionExAnteModels exAnteCentral = new EvaluacionExAnteModels();
                Boolean estadoUsuario = exAnteCentral.enviaMail(dataMail);
                if (!estadoUsuario)
                    throw new Exception("error envio mail asignacion de evaluador");

                //Registro fecha envio de mail de asignacion (Elimina fecha de 2 evaluadores y solo agrega 1)
                ViewDto<TablaRespuestasDto> borradoFechaEval1 = new ViewDto<TablaRespuestasDto>();
                borradoFechaEval1 = new ViewDto<TablaRespuestasDto>();
                borradoFechaEval1 = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaFechaRegEval1")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                if (!borradoFechaEval1.Sucess())
                    throw new Exception("error registro fecha asignacion de evaluador 1 (borrado)");
                ViewDto<TablaRespuestasDto> borradoFechaEval2 = new ViewDto<TablaRespuestasDto>();
                borradoFechaEval2 = new ViewDto<TablaRespuestasDto>();
                borradoFechaEval2 = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaFechaRegEval2")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                if (!borradoFechaEval2.Sucess())
                    throw new Exception("error registro fecha asignacion de evaluador 2 (borrado)");
                ViewDto<TablaRespuestasDto> registraFechaEval1 = new ViewDto<TablaRespuestasDto>();
                registraFechaEval1 = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaFechaRegEval1")), Respuesta = DateTime.Now, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                if (!registraFechaEval1.Sucess())
                    throw new Exception("error registro fecha asignacion de evaluador (registro)");
                //Registro usuario envio de mail de asignacion (Elimina usuario de 2 evaluadores y solo agrega 1)
                ViewDto<TablaRespuestasDto> borradoUsuarioEval1 = new ViewDto<TablaRespuestasDto>();
                borradoUsuarioEval1 = new ViewDto<TablaRespuestasDto>();
                borradoUsuarioEval1 = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaUsuarioRegEva1")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                if (!borradoFechaEval1.Sucess())
                    throw new Exception("error registro usuario asignacion de evaluador (borrado)");
                ViewDto<TablaRespuestasDto> borradoUsuarioEval2 = new ViewDto<TablaRespuestasDto>();
                borradoUsuarioEval2 = new ViewDto<TablaRespuestasDto>();
                borradoUsuarioEval2 = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaUsuarioRegEva2")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                if (!borradoFechaEval2.Sucess())
                    throw new Exception("error registro usuario asignacion de evaluador (borrado)");
                ViewDto<TablaRespuestasDto> registraUsuarioEval2 = new ViewDto<TablaRespuestasDto>();
                registraUsuarioEval2 = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaUsuarioRegEva1")), Respuesta = idUsuario, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                if (!registraUsuarioEval2.Sucess())
                    throw new Exception("error registro usuario asignacion de evaluador (registro)");

                //Asigno evaluador
                ViewDto<TablaRespuestasDto> borradoEvaluador = new ViewDto<TablaRespuestasDto>();
                borradoEvaluador = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaEvaluador1")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                if (!borradoEvaluador.Sucess())
                    throw new Exception("error borrado asignacion de evaluador");
                ViewDto<TablaRespuestasDto> registraEvaluador = new ViewDto<TablaRespuestasDto>();
                registraEvaluador = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaEvaluador1")), Respuesta = idEvaluador, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                if (!registraEvaluador.Sucess())
                    throw new Exception("error registro asignacion de evaluador");

                //Asigna permiso modulo evaluacion
                UsuariosModels usuarios = new UsuariosModels();
                string permisoModEval = string.Empty;
                TablaExcepcionesPermisosDto perModEval = new TablaExcepcionesPermisosDto() { IdFormulario = idPrograma, IdUsuario = idEvaluadorAnterior, IdPermiso = decimal.Parse(constantes.GetValue("PermisoModEvalPerfilGore")) };
                if (idEvaluadorAnterior != "")
                {
                    permisoModEval = await usuarios.eliminaPermisosUsuarios(new List<TablaExcepcionesPermisosDto>() { perModEval });
                    if (permisoModEval != "ok")
                        throw new Exception("error eliminar permiso de evaluacion");
                }
                permisoModEval = await usuarios.registraPermisosUsuarios(new List<TablaExcepcionesPermisosDto>() { new TablaExcepcionesPermisosDto() { IdFormulario = idPrograma, IdUsuario = idEvaluador, IdPermiso = decimal.Parse(constantes.GetValue("PermisoModEvalPerfilGore")), Estado = decimal.Parse(constantes.GetValue("Activo")) } });
                if (permisoModEval != "ok")
                    throw new Exception("error crear permiso de evaluacion");
            }
            catch (Exception ex)
            {
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return (registro);
        }

        public async Task<string> asignaTipoOferta(int idPrograma, string tipoOferta, string[] evaluadorDipres, string[] idEvaluadorAnterior, string idUsuario)
        {
            string registro = "ok";
            try
            {
                //Elimina asignacion para el evaluador 1
                ViewDto<TablaRespuestasDto> borradoEvaluador1 = new ViewDto<TablaRespuestasDto>();
                borradoEvaluador1 = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaEvaluador1")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                if (!borradoEvaluador1.Sucess())
                    throw new Exception("error borrado asignacion de evaluador");
                //Elimina asignacion para el evaluador 2 (Adicional DIPRES)
                ViewDto<TablaRespuestasDto> borradoEvaluador2 = new ViewDto<TablaRespuestasDto>();
                borradoEvaluador2 = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaEvaluador2")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                if (!borradoEvaluador2.Sucess())
                    throw new Exception("error borrado asignacion de evaluador");

                //Asignación de permiso módulo de evaluación
                UsuariosModels usuarios = new UsuariosModels();
                string permisoAdmiModEval = string.Empty;

                //13001 Social
                if (tipoOferta == "13001")
                {
                    //Sujeto a revision si eliminar el registro de evaluador al no haber ninguno asignado
                    ViewDto<TablaRespuestasDto> borradoFechaEval1 = new ViewDto<TablaRespuestasDto>();
                    borradoFechaEval1 = new ViewDto<TablaRespuestasDto>();
                    borradoFechaEval1 = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaFechaRegEval1")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                    if (!borradoFechaEval1.Sucess())
                        throw new Exception("error registro fecha asignacion de evaluador 2 (borrado)");
                    ViewDto<TablaRespuestasDto> borradoUsuarioEval1 = new ViewDto<TablaRespuestasDto>();
                    borradoUsuarioEval1 = new ViewDto<TablaRespuestasDto>();
                    borradoUsuarioEval1 = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaUsuarioRegEva1")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                    if (!borradoUsuarioEval1.Sucess())
                        throw new Exception("error registro usuario asignacion de evaluador 2 (borrado)");

                    //Elimina registro de fecha y usuario 2
                    ViewDto<TablaRespuestasDto> borradoFechaEval2 = new ViewDto<TablaRespuestasDto>();
                    borradoFechaEval2 = new ViewDto<TablaRespuestasDto>();
                    borradoFechaEval2 = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaFechaRegEval2")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                    if (!borradoFechaEval2.Sucess())
                        throw new Exception("error registro fecha asignacion de evaluador 2 (borrado)");
                    ViewDto<TablaRespuestasDto> borradoUsuarioEval2 = new ViewDto<TablaRespuestasDto>();
                    borradoUsuarioEval2 = new ViewDto<TablaRespuestasDto>();
                    borradoUsuarioEval2 = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaUsuarioRegEva2")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                    if (!borradoFechaEval2.Sucess())
                        throw new Exception("error registro usuario asignacion de evaluador 2 (borrado)");

                    //Elimina permisos de evaluación a GORE, deja el campo vacío para volver a seleccionar
                    if (idEvaluadorAnterior.Length > 0)
                    {
                        for (int i = 0; i < idEvaluadorAnterior.Length; i++)
                        {
                            TablaExcepcionesPermisosDto perAdmiModEval = new TablaExcepcionesPermisosDto() { IdFormulario = idPrograma, IdUsuario = idEvaluadorAnterior[i], IdPermiso = decimal.Parse(constantes.GetValue("PermisoModEvalPerfilGore")) };
                            permisoAdmiModEval = await usuarios.eliminaPermisosUsuarios(new List<TablaExcepcionesPermisosDto>() { perAdmiModEval });
                            if (permisoAdmiModEval != "ok")
                                throw new Exception("error eliminar permiso de evaluacion");
                        }
                    }
                }
                else
                {
                    //Verificar que haya un evaluador al cual quitar el permiso
                    if (idEvaluadorAnterior != null)
                    {
                        if (idEvaluadorAnterior.Length > 0)
                        {
                            TablaExcepcionesPermisosDto perAdmiModEval = new TablaExcepcionesPermisosDto() { IdFormulario = idPrograma, IdUsuario = idEvaluadorAnterior[0], IdPermiso = decimal.Parse(constantes.GetValue("PermisoModEvalPerfilGore")) };
                            permisoAdmiModEval = await usuarios.eliminaPermisosUsuarios(new List<TablaExcepcionesPermisosDto>() { perAdmiModEval });
                            if (permisoAdmiModEval != "ok")
                                throw new Exception("error eliminar permiso de evaluacion");
                        }
                    }

                    string[] evaluadores = evaluadorDipres;
                    for (int i = 0; i < evaluadores.Length; i++)
                    {
                        //Registra pregunta asignación evaluador
                        ViewDto<TablaRespuestasDto> registraEvaluador = new ViewDto<TablaRespuestasDto>();
                        registraEvaluador = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaEvaluador" + (i + 1))), Respuesta = evaluadores[i], TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                        if (!registraEvaluador.Sucess())
                            throw new Exception("error registro asignacion de evaluador " + (i + 1));

                        //Asigna permiso evaluacion
                        TablaExcepcionesPermisosDto perAdmiModEval = new TablaExcepcionesPermisosDto() { IdFormulario = idPrograma, IdUsuario = evaluadores[i], IdPermiso = decimal.Parse(constantes.GetValue("PermisoModEvalPerfilGore")) };
                        permisoAdmiModEval = await usuarios.registraPermisosUsuarios(new List<TablaExcepcionesPermisosDto>() { new TablaExcepcionesPermisosDto() { IdFormulario = idPrograma, IdUsuario = evaluadores[i], IdPermiso = decimal.Parse(constantes.GetValue("PermisoModEvalPerfilGore")), Estado = decimal.Parse(constantes.GetValue("Activo")) } });
                        if (permisoAdmiModEval != "ok")
                            throw new Exception("error crear permiso de evaluacion");

                        //Registro fecha envio de mail de asignacion
                        ViewDto<TablaRespuestasDto> borradoFechaEval = new ViewDto<TablaRespuestasDto>();
                        borradoFechaEval = new ViewDto<TablaRespuestasDto>();
                        borradoFechaEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaFechaRegEval" + (i + 1))), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                        if (!borradoFechaEval.Sucess())
                            throw new Exception("error registro fecha asignacion de evaluador (borrado)");
                        ViewDto<TablaRespuestasDto> registraFechaEval = new ViewDto<TablaRespuestasDto>();
                        registraFechaEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaFechaRegEval" + (i + 1))), Respuesta = DateTime.Now, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                        if (!registraFechaEval.Sucess())
                            throw new Exception("error registro fecha asignacion de evaluador (registro)");
                        //Registro usuario envio de mail de asignacion
                        ViewDto<TablaRespuestasDto> borradoUsuarioEval = new ViewDto<TablaRespuestasDto>();
                        borradoUsuarioEval = new ViewDto<TablaRespuestasDto>();
                        borradoUsuarioEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaUsuarioRegEva" + (i + 1))), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                        if (!borradoFechaEval.Sucess())
                            throw new Exception("error registro usuario asignacion de evaluador (borrado)");
                        ViewDto<TablaRespuestasDto> registraUsuarioEval = new ViewDto<TablaRespuestasDto>();
                        registraUsuarioEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaUsuarioRegEva" + (i + 1))), Respuesta = idUsuario, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                        if (!registraFechaEval.Sucess())
                            throw new Exception("error registro usuario asignacion de evaluador (registro)");
                    }

                }

                //Asigna tipo oferta
                ViewDto<TablaRespuestasDto> borradoTipoDeOferta = new ViewDto<TablaRespuestasDto>();
                borradoTipoDeOferta = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaTipoOferta")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                if (!borradoTipoDeOferta.Sucess())
                    throw new Exception("error borrado asignacion de tipo de oferta");
                ViewDto<TablaRespuestasDto> registraTipoDeOferta = new ViewDto<TablaRespuestasDto>();
                registraTipoDeOferta = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaTipoOferta")), Respuesta = tipoOferta, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                if (!registraTipoDeOferta.Sucess())
                    throw new Exception("error registro asignacion de tipo de oferta");

                //Busco correos de envio
                string mailEvaluacion = constantes.GetValue("EmailExAnteGore");
                string mailDipres = constantes.GetValue("EmailDipresGore");
                //Enviar mail a evaluador asignado
                ViewDto<TablaUsuariosDto> usuario = new ViewDto<TablaUsuariosDto>();
                usuario = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = idUsuario, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                //ViewDto<TablaUsuariosDto> evaluador = new ViewDto<TablaUsuariosDto>();
                //evaluador = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = idEvaluador, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                ViewDto<TablaProgramasDto> programa = new ViewDto<TablaProgramasDto>();
                programa = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idPrograma, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Buscar);
                DatosEmail dataMail = new DatosEmail();
                dataMail.de = usuario.Dtos.SingleOrDefault().Email;
                dataMail.para = tipoOferta == "13001" ? mailEvaluacion : mailDipres;
                dataMail.asunto = "Asignación tipo de oferta Perfil GORE";
                dataMail.imagen = true;
                string msj1 = "Estimado Evaluador:<br/> Se ha asignado el tipo de oferta {0} del perfil <b>{1} {2}</b> (Gobierno regional de {3}).";
                string msj2 = "Saluda Atentamente, <br/> Coordinador Evaluación Ex-Ante <br/>{0}<br/>{1}<br/>{2}";
                string nombreUsuario = usuario.Dtos.SingleOrDefault().Nombre;
                string nombreProg = programa.Dtos.SingleOrDefault().Nombre;
                string ministerioProg = programa.Dtos.SingleOrDefault().Ministerio;
                string ministerio = usuario.Dtos.SingleOrDefault().Ministerio;
                string correoUsuario = usuario.Dtos.SingleOrDefault().Email;
                string tipoDeOferta = tipoOferta == "13001" ? "Social" : "No Social";
                FormulariosModels formulario = new FormulariosModels();
                string version = await formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaVersionProgramas")), idPrograma);
                string versionFinal = (String.IsNullOrEmpty(version) ? string.Empty : (int.Parse(version) >= 1 ? "versión " + version : string.Empty));
                dataMail.mensaje = string.Format(new CuerpoEmail().emailHtmlEvaluacion, string.Format(msj1, tipoDeOferta, nombreProg, versionFinal, ministerioProg), string.Format(msj2, nombreUsuario, ministerio, correoUsuario));
                EvaluacionExAnteModels exAnteCentral = new EvaluacionExAnteModels();
                Boolean estadoUsuario = exAnteCentral.enviaMail(dataMail);
                if (!estadoUsuario)
                    throw new Exception("error envio mail asignacion de evaluador");

                //Registro fecha envio de mail asignacion oferta
                ViewDto<TablaRespuestasDto> borradoFechaOferta = new ViewDto<TablaRespuestasDto>();
                borradoFechaOferta = new ViewDto<TablaRespuestasDto>();
                borradoFechaOferta = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaFechaRegOferta")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                if (!borradoFechaOferta.Sucess())
                    throw new Exception("error registro fecha asignacion de oferta (borrado)");
                ViewDto<TablaRespuestasDto> registraFechaOferta = new ViewDto<TablaRespuestasDto>();
                registraFechaOferta = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaFechaRegOferta")), Respuesta = DateTime.Now, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                if (!registraFechaOferta.Sucess())
                    throw new Exception("error registro fecha asignacion de oferta (registro)");

                //Registro usuario envio de mail asignacion oferta
                ViewDto<TablaRespuestasDto> borradoUsuarioOferta = new ViewDto<TablaRespuestasDto>();
                borradoUsuarioOferta = new ViewDto<TablaRespuestasDto>();
                borradoUsuarioOferta = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaUsuarioRegOferta")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                if (!borradoUsuarioOferta.Sucess())
                    throw new Exception("error registro usuario asignacion de oferta (borrado)");
                ViewDto<TablaRespuestasDto> registraUsuarioOferta = new ViewDto<TablaRespuestasDto>();
                registraUsuarioOferta = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaUsuarioRegOferta")), Respuesta = idUsuario, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                if (!registraUsuarioOferta.Sucess())
                    throw new Exception("error registro usuario asignacion de oferta (registro)");
            }
            catch (Exception ex)
            {
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return (registro);
        }

        public async Task<string> enviarAdmisibilidad(int idPrograma, string ingresoExAnte, string motivoNoIngreso, string otroMotivo, string idUsuario)
        {
            string registro = "ok";
            try
            {
                //Asigna ingreso a Ex Ante (Segunda pregunta)
                ViewDto<TablaRespuestasDto> borradoAsignacionExAnte = new ViewDto<TablaRespuestasDto>();
                borradoAsignacionExAnte = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaIngresoExAnte")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                if (!borradoAsignacionExAnte.Sucess())
                    throw new Exception("error borrado asignacion ingreso ExAnte");
                ViewDto<TablaRespuestasDto> registraAsignacionExAnte = new ViewDto<TablaRespuestasDto>();
                registraAsignacionExAnte = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaIngresoExAnte")), Respuesta = ingresoExAnte, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                if (!registraAsignacionExAnte.Sucess())
                    throw new Exception("error registro asignacion ingreso ExAnte");

                if (ingresoExAnte == "901")
                {
                    //Elimina permiso modulo evaluacion para evaluador 2 (Adicional DIPRES)
                    FormulariosModels formularioIteracion = new FormulariosModels();
                    string idEvaluador2 = await formularioIteracion.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaEvaluador2")), idPrograma);
                    if (idEvaluador2 != null)
                    {
                        UsuariosModels usuarios = new UsuariosModels();
                        string permisoModEval = string.Empty;
                        TablaExcepcionesPermisosDto perModEval = new TablaExcepcionesPermisosDto() { IdFormulario = idPrograma, IdUsuario = idEvaluador2, IdPermiso = decimal.Parse(constantes.GetValue("PermisoModEvalPerfilGore")) };
                        permisoModEval = await usuarios.eliminaPermisosUsuarios(new List<TablaExcepcionesPermisosDto>() { perModEval });
                        if (permisoModEval != "ok")
                            throw new Exception("error eliminar permiso de evaluacion");
                    }

                    //Enviar mail a Dipres con la consulta
                    ViewDto<TablaUsuariosDto> usuario = new ViewDto<TablaUsuariosDto>();
                    usuario = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = idUsuario, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                    string mailEvaluacion = constantes.GetValue("EmailExAnteGore");
                    string mailDipres = constantes.GetValue("EmailDipresGore");
                    ViewDto<TablaProgramasDto> programa = new ViewDto<TablaProgramasDto>();
                    programa = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idPrograma, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Buscar);
                    DatosEmail dataMail = new DatosEmail();
                    dataMail.de = mailEvaluacion;
                    dataMail.para = mailDipres;
                    dataMail.asunto = "Admisibilidad Perfil GORE";
                    dataMail.imagen = true;
                    string msj1 = "Estimado(a):<br/> De acuerdo a la evaluación de admisibilidad del perfil <b>{0}</b> hecha por SES este no corresponde que ingrese a Evaluación Ex ante por el siguiente motivo: <b>{1} {2}</b>.</p><p>Verificar los antecedentes y validar si corresponde su ingreso en la opción <b>Ingreso ExAnte del submenú Evaluador Perfil en Evaluadores</b></p>";
                    string msj2 = "Saludos cordiales. <br/> Coordinador Evaluación Ex-Ante <br/>{0}<br/>{1}";
                    string nombreUsuario = usuario.Dtos.SingleOrDefault().Nombre;
                    string nombreProg = programa.Dtos.SingleOrDefault().Nombre;
                    string ministerioProg = programa.Dtos.SingleOrDefault().Ministerio;
                    string ministerio = usuario.Dtos.SingleOrDefault().Ministerio;
                    string correoUsuario = usuario.Dtos.SingleOrDefault().Email;
                    string motivoNoIngresoExAnte = motivoNoIngreso;
                    string otroMotivoNoIngresoExAnte = motivoNoIngresoExAnte == "Otro" ? ": " + otroMotivo + "." : ".";
                    FormulariosModels formulario = new FormulariosModels();
                    string version = await formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaVersionProgramas")), idPrograma);
                    string versionFinal = (String.IsNullOrEmpty(version) ? string.Empty : (int.Parse(version) >= 1 ? "versión " + version : string.Empty));
                    dataMail.mensaje = string.Format(new CuerpoEmail().emailHtmlEvaluacion, string.Format(msj1, nombreProg, motivoNoIngresoExAnte, otroMotivoNoIngresoExAnte), string.Format(msj2, ministerio, mailEvaluacion));
                    EvaluacionExAnteModels exAnteCentral = new EvaluacionExAnteModels();
                    Boolean estadoUsuario = exAnteCentral.enviaMail(dataMail);
                    if (!estadoUsuario)
                        throw new Exception("error envio mail asignacion de evaluador");

                    //Registro fecha envio admisibilidad
                    ViewDto<TablaRespuestasDto> borradoFechaOferta = new ViewDto<TablaRespuestasDto>();
                    borradoFechaOferta = new ViewDto<TablaRespuestasDto>();
                    borradoFechaOferta = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaFechaRegAdmisibilidad")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                    if (!borradoFechaOferta.Sucess())
                        throw new Exception("error registro fecha envio admisibilidad (borrado)");
                    ViewDto<TablaRespuestasDto> registraFechaOferta = new ViewDto<TablaRespuestasDto>();
                    registraFechaOferta = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaFechaRegAdmisibilidad")), Respuesta = DateTime.Now, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                    if (!registraFechaOferta.Sucess())
                        throw new Exception("error registro fecha envio admisibilidad (registro)");

                    //Registro usuario envio admisibilidad
                    ViewDto<TablaRespuestasDto> borradoUsuarioOferta = new ViewDto<TablaRespuestasDto>();
                    borradoUsuarioOferta = new ViewDto<TablaRespuestasDto>();
                    borradoUsuarioOferta = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaUsuarioRegAdmisibilidad")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                    if (!borradoUsuarioOferta.Sucess())
                        throw new Exception("error registro usuario envio admisibilidad (borrado)");
                    ViewDto<TablaRespuestasDto> registraUsuarioOferta = new ViewDto<TablaRespuestasDto>();
                    registraUsuarioOferta = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaUsuarioRegAdmisibilidad")), Respuesta = idUsuario, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                    if (!registraUsuarioOferta.Sucess())
                        throw new Exception("error registro usuario envio admisibilidad (registro)");

                    //Cambio de etapa a Perfil en Consulta
                    ViewDto<TablaProgramasDto> actualizaEtapa = new ViewDto<TablaProgramasDto>();
                    TablaProgramasDto datosProgAct = new TablaProgramasDto();
                    datosProgAct.IdPrograma = idPrograma;
                    datosProgAct.Estado.IdParametro = decimal.Parse(constantes.GetValue("Activo"));
                    datosProgAct.Etapa.IdParametro = decimal.Parse(constantes.GetValue("EtapaPerfilEnConsulta"));
                    actualizaEtapa = bips.RegistrarProgramas(new ContextoDto(), datosProgAct, EnumAccionRealizar.Actualizar);
                    if (actualizaEtapa.HasError())
                        throw new Exception("Error al actualizar etapa (envio evaluacion)");
                }
            }
            catch (Exception ex)
            {
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return (registro);
        }

        public async Task<string> correspondeIngresoExAnte(int idPrograma, string idEvaluador, bool correspondeIngreso, string idUsuario, string rutaArchivos)
        {
            string registro = "ok";
            try
            {
                //Ingresa respuesta a pregunta 9725
                int respuestaIngreso = correspondeIngreso ? 902 : 901;

                //Asigna valor ingrese a Ex Ante (Segunda pregunta)
                ViewDto<TablaRespuestasDto> borradoAsignacionExAnte = new ViewDto<TablaRespuestasDto>();
                borradoAsignacionExAnte = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaIngresoExAnte")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                if (!borradoAsignacionExAnte.Sucess())
                    throw new Exception("error borrado asignacion ingreso ExAnte");
                ViewDto<TablaRespuestasDto> registraAsignacionExAnte = new ViewDto<TablaRespuestasDto>();
                registraAsignacionExAnte = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaIngresoExAnte")), Respuesta = respuestaIngreso, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                if (!registraAsignacionExAnte.Sucess())
                    throw new Exception("error registro asignacion ingreso ExAnte");

                //Registro fecha respuesta ingreso ExAnte
                ViewDto<TablaRespuestasDto> borradoFechaOferta = new ViewDto<TablaRespuestasDto>();
                borradoFechaOferta = new ViewDto<TablaRespuestasDto>();
                borradoFechaOferta = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaFechaIngresoExAnte")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                if (!borradoFechaOferta.Sucess())
                    throw new Exception("error registro fecha respuesta ingreso ExAnte (borrado)");
                ViewDto<TablaRespuestasDto> registraFechaOferta = new ViewDto<TablaRespuestasDto>();
                registraFechaOferta = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaFechaIngresoExAnte")), Respuesta = DateTime.Now, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                if (!registraFechaOferta.Sucess())
                    throw new Exception("error registro fecha respuesta ingreso ExAnte (registro)");

                //Registro usuario respuesta ingreso ExAnte
                ViewDto<TablaRespuestasDto> borradoUsuarioOferta = new ViewDto<TablaRespuestasDto>();
                borradoUsuarioOferta = new ViewDto<TablaRespuestasDto>();
                borradoUsuarioOferta = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaUsuarioIngresoExAnte")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                if (!borradoUsuarioOferta.Sucess())
                    throw new Exception("error registro usuario respuesta ingreso ExAnte (borrado)");
                ViewDto<TablaRespuestasDto> registraUsuarioOferta = new ViewDto<TablaRespuestasDto>();
                registraUsuarioOferta = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaUsuarioIngresoExAnte")), Respuesta = idUsuario, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                if (!registraUsuarioOferta.Sucess())
                    throw new Exception("error registro usuario respuesta ingreso ExAnte (registro)");

                if (correspondeIngreso)
                {
                    //Asigna permiso modulo evaluacion nuevamente al evaluador 1
                    UsuariosModels usuarios = new UsuariosModels();
                    string permisoModEval = string.Empty;
                    TablaExcepcionesPermisosDto perModEval = new TablaExcepcionesPermisosDto() { IdFormulario = idPrograma, IdUsuario = idEvaluador, IdPermiso = decimal.Parse(constantes.GetValue("PermisoModEvalPerfilGore")) };
                    permisoModEval = await usuarios.eliminaPermisosUsuarios(new List<TablaExcepcionesPermisosDto>() { perModEval });
                    if (permisoModEval != "ok")
                        throw new Exception("error eliminar permiso de evaluacion");
                    permisoModEval = await usuarios.registraPermisosUsuarios(new List<TablaExcepcionesPermisosDto>() { new TablaExcepcionesPermisosDto() { IdFormulario = idPrograma, IdUsuario = idEvaluador, IdPermiso = decimal.Parse(constantes.GetValue("PermisoModEvalPerfilGore")), Estado = decimal.Parse(constantes.GetValue("Activo")) } });
                    if (permisoModEval != "ok")
                        throw new Exception("error crear permiso de evaluacion");

                    //Asigna permiso modulo evaluacion nuevamente al evaluador 2 (Dipres)
                    FormulariosModels formulario = new FormulariosModels();
                    string idEvaluador2 = await formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaEvaluador2")), idPrograma);
                    if (idEvaluador2 != null)
                    {
                        perModEval = new TablaExcepcionesPermisosDto() { IdFormulario = idPrograma, IdUsuario = idEvaluador2, IdPermiso = decimal.Parse(constantes.GetValue("PermisoModEvalPerfilGore")) };
                        permisoModEval = await usuarios.eliminaPermisosUsuarios(new List<TablaExcepcionesPermisosDto>() { perModEval });
                        if (permisoModEval != "ok")
                            throw new Exception("error eliminar permiso de evaluacion");
                        permisoModEval = await usuarios.registraPermisosUsuarios(new List<TablaExcepcionesPermisosDto>() { new TablaExcepcionesPermisosDto() { IdFormulario = idPrograma, IdUsuario = idEvaluador2, IdPermiso = decimal.Parse(constantes.GetValue("PermisoModEvalPerfilGore")), Estado = decimal.Parse(constantes.GetValue("Activo")) } });
                        if (permisoModEval != "ok")
                            throw new Exception("error crear permiso de evaluacion");
                    }

                    //Cambio a etapa de solicitud de evaluacion
                    ViewDto<TablaProgramasDto> actualizaEtapa = new ViewDto<TablaProgramasDto>();
                    TablaProgramasDto datosProgAct = new TablaProgramasDto();
                    datosProgAct.IdPrograma = idPrograma;
                    datosProgAct.Estado.IdParametro = decimal.Parse(constantes.GetValue("Activo"));
                    datosProgAct.Etapa.IdParametro = decimal.Parse(constantes.GetValue("EtapaEvaluacion"));
                    actualizaEtapa = bips.RegistrarProgramas(new ContextoDto(), datosProgAct, EnumAccionRealizar.Actualizar);
                    if (actualizaEtapa.HasError())
                        throw new Exception("Error al actualizar etapa (envio evaluacion)");
                }
                else
                {
                    //Descarga archivo pdf desde url
                    int añoActual = DateTime.Now.Year;
                    var fileUrl = "http://10.10.14.161:83/Informes_BIPS_BO/admin/fichas/5/0/5/" + idPrograma + "/" + añoActual + "/344";
                    var fileName = "PRG" + añoActual + "_" + idPrograma + ".pdf";
                    using (var client = new WebClient())
                    {
                        client.DownloadFile(fileUrl, Path.Combine(rutaArchivos, Path.GetFileName(fileName)));
                    }
                    string rutaArchivo = string.Empty;
                    rutaArchivo = Path.Combine(rutaArchivos, fileName);

                    //Correo informando cierre
                    string emailContraparte = string.Empty;
                    ViewDto<TablaProgramasDto> programa = new ViewDto<TablaProgramasDto>();
                    programa = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idPrograma, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Buscar);
                    ViewDto<TablaUsuariosDto> correoContrapartes = new ViewDto<TablaUsuariosDto>();
                    correoContrapartes = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { IdGore = programa.Dtos.FirstOrDefault().IdServicio.IdParametro, IdPerfilGore = decimal.Parse(constantes.GetValue("PerfilContraparteGore")), IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                    ViewDto<TablaUsuariosDto> contraparte = new ViewDto<TablaUsuariosDto>();
                    contraparte = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Email = correoContrapartes.Dtos.Where(p => p.IdGore == programa.Dtos.FirstOrDefault().IdServicio.IdParametro).FirstOrDefault().Email, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (contraparte.HasElements())
                        emailContraparte = contraparte.Dtos.FirstOrDefault().Email;
                    ViewDto<TablaUsuariosDto> usuario = new ViewDto<TablaUsuariosDto>();
                    usuario = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = idUsuario, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                    string mailEvaluacion = constantes.GetValue("EmailExAnteGore");
                    DatosEmail dataMail = new DatosEmail();
                    dataMail.de = mailEvaluacion;
                    dataMail.para = emailContraparte;
                    dataMail.asunto = "Notificación Perfil GORE";
                    dataMail.imagen = true;
                    dataMail.adjunto = rutaArchivo;
                    string msj1 = "Estimado(a):<br/> De acuerdo con la consulta a DIPRES, este ha decidido que el perfil <b>{0}</b> no ingresará a ExAnte, por lo que se procederá a su cierre";
                    string msj2 = "Saludos cordiales. <br/> Coordinador Evaluación Ex-Ante <br/>{0}<br/>{1}";
                    string nombreUsuario = usuario.Dtos.SingleOrDefault().Nombre;
                    string nombreProg = programa.Dtos.SingleOrDefault().Nombre;
                    string ministerioProg = programa.Dtos.SingleOrDefault().Ministerio;
                    string ministerio = usuario.Dtos.SingleOrDefault().Ministerio;
                    string correoUsuario = usuario.Dtos.SingleOrDefault().Email;
                    FormulariosModels formulario = new FormulariosModels();
                    string version = await formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaVersionProgramas")), idPrograma);
                    string versionFinal = (String.IsNullOrEmpty(version) ? string.Empty : (int.Parse(version) >= 1 ? "versión " + version : string.Empty));
                    dataMail.mensaje = string.Format(new CuerpoEmail().emailHtmlEvaluacion, string.Format(msj1, nombreProg), string.Format(msj2, ministerio, mailEvaluacion));
                    EvaluacionExAnteModels exAnteCentral = new EvaluacionExAnteModels();
                    Boolean estadoUsuario = exAnteCentral.enviaMail(dataMail);
                    if (!estadoUsuario)
                        throw new Exception("error envio mail asignacion de evaluador");

                    //Cierre de etapa perfil gore
                    ViewDto<TablaProgramasDto> actualizaEtapa = new ViewDto<TablaProgramasDto>();
                    TablaProgramasDto datosProgAct = new TablaProgramasDto();
                    datosProgAct.IdPrograma = idPrograma;
                    datosProgAct.Estado.IdParametro = decimal.Parse(constantes.GetValue("Activo"));
                    datosProgAct.Etapa.IdParametro = decimal.Parse(constantes.GetValue("EtapaCierrePerfilGore"));
                    actualizaEtapa = bips.RegistrarProgramas(new ContextoDto(), datosProgAct, EnumAccionRealizar.Actualizar);
                    if (actualizaEtapa.HasError())
                        throw new Exception("Error al actualizar etapa (cierre perfil gore)");
                }
            }
            catch (Exception ex)
            {
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return (registro);
        }

        public async Task<string> enviarCalificacion(int idPrograma, int idBips, string calificacion, string idUsuario, string rutaArchivos)
        {
            string registro = "ok";
            try
            {
                //Cuerpo del correo según calificación
                string msj1 = "<p>Estimado(a):</p>Se ha realizado la calificación del perfil <b>{0}</b> con el siguiente resultado:<br/>";

                //Descarga archivo pdf desde url
                int añoActual = DateTime.Now.Year;
                var fileUrl = "http://10.10.14.161:83/Informes_BIPS_BO/admin/fichas/5/0/5/" + idPrograma + "/" + añoActual + "/344";
                var fileName = "PRG" + añoActual + "_" + idPrograma + ".pdf";
                using (var client = new WebClient())
                {
                    client.DownloadFile(fileUrl, Path.Combine(rutaArchivos, Path.GetFileName(fileName)));
                }
                string rutaArchivo = string.Empty;
                rutaArchivo = Path.Combine(rutaArchivos, fileName);

                string emailContraparte = string.Empty;
                ViewDto<TablaProgramasDto> programa = new ViewDto<TablaProgramasDto>();
                programa = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idPrograma, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Buscar);
                ViewDto<TablaUsuariosDto> correoContrapartes = new ViewDto<TablaUsuariosDto>();
                correoContrapartes = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { IdGore = programa.Dtos.FirstOrDefault().IdServicio.IdParametro, IdPerfilGore = decimal.Parse(constantes.GetValue("PerfilContraparteGore")), IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                ViewDto<TablaUsuariosDto> contraparte = new ViewDto<TablaUsuariosDto>();
                contraparte = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Email = correoContrapartes.Dtos.Where(p => p.IdGore == programa.Dtos.FirstOrDefault().IdServicio.IdParametro).FirstOrDefault().Email, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                if (contraparte.HasElements())
                    emailContraparte = contraparte.Dtos.FirstOrDefault().Email;

                ViewDto<TablaProgramasDto> actualizaEtapa = new ViewDto<TablaProgramasDto>();
                TablaProgramasDto datosProgAct = new TablaProgramasDto();
                string resultadoIteracion = null;

                switch (calificacion)
                {
                    case "29420":
                        //Cambio etapa a Perfil Calificado
                        datosProgAct.IdPrograma = idPrograma;
                        datosProgAct.Estado.IdParametro = decimal.Parse(constantes.GetValue("Activo"));
                        datosProgAct.Etapa.IdParametro = decimal.Parse(constantes.GetValue("EtapaPerfilCalificado"));
                        actualizaEtapa = bips.RegistrarProgramas(new ContextoDto(), datosProgAct, EnumAccionRealizar.Actualizar);
                        if (actualizaEtapa.HasError())
                            throw new Exception("Error al actualizar etapa (Perfil calificado)");

                        //Crea parte del correo informando el avance del perfil
                        msj1 += "<p><b>Es programa</b>, la plataforma procederá al cambio de etapa a <b>Perfil Calificado</b> para que la COG pueda crear el Programa GORE en la vista <b>Perfiles</b>.<br/></p>";

                        break;
                    case "29421":
                        resultadoIteracion = await iteracionPerfilGore(idPrograma, idBips, idUsuario, false, contraparte);
                        if (resultadoIteracion == null)
                            throw new Exception("error al iterar perfil");

                        //Crea parte del correo informando que vuelve al inicio
                        msj1 += "<p><b>Información insuficiente</b>, la plataforma procederá al cambio de etapa <b>En correción del Servicio</b> para que la COG realice el proceso nuevamente.<br/></p>";

                        break;
                    case "29422":
                        //Cierre de etapa perfil gore
                        datosProgAct.IdPrograma = idPrograma;
                        datosProgAct.Estado.IdParametro = decimal.Parse(constantes.GetValue("Activo"));
                        datosProgAct.Etapa.IdParametro = decimal.Parse(constantes.GetValue("EtapaCierrePerfilGore"));
                        actualizaEtapa = bips.RegistrarProgramas(new ContextoDto(), datosProgAct, EnumAccionRealizar.Actualizar);
                        if (actualizaEtapa.HasError())
                            throw new Exception("Error al actualizar etapa (cierre perfil gore)");

                        //Crea parte del correo informando el cierre del perfil
                        msj1 += "<p><b>No es programa</b>, la plataforma procederá al cambio de etapa a <b>Cierre Perfil</b>.<br/></p>";

                        break;
                    default:
                        break;
                }

                //Asigna calificacion
                ViewDto<TablaRespuestasDto> borradoAsignacionExAnte = new ViewDto<TablaRespuestasDto>();
                borradoAsignacionExAnte = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaCalificacionPerfilGore")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                if (!borradoAsignacionExAnte.Sucess())
                    throw new Exception("Error al borrar asignacion calificación");
                ViewDto<TablaRespuestasDto> registraAsignacionExAnte = new ViewDto<TablaRespuestasDto>();
                registraAsignacionExAnte = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaCalificacionPerfilGore")), Respuesta = calificacion, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                if (!registraAsignacionExAnte.Sucess())
                    throw new Exception("Error al registrar asignacion calificación");

                //Elimina permiso evaluación para evaluador 1
                UsuariosModels usuarios = new UsuariosModels();
                string permisoModEval = string.Empty;
                TablaExcepcionesPermisosDto perModEval = new TablaExcepcionesPermisosDto() { IdFormulario = idPrograma, IdUsuario = idUsuario, IdPermiso = decimal.Parse(constantes.GetValue("PermisoModEvalPerfilGore")) };
                permisoModEval = await usuarios.eliminaPermisosUsuarios(new List<TablaExcepcionesPermisosDto>() { perModEval });
                if (permisoModEval != "ok")
                    throw new Exception("Error al eliminar permiso de evaluación");

                //Elimina permiso modulo evaluacion para evaluador 2 (Adicional DIPRES)
                FormulariosModels formulario = new FormulariosModels();
                string idEvaluador2 = await formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaEvaluador2")), idPrograma);
                if (idEvaluador2 != null)
                {
                    perModEval = new TablaExcepcionesPermisosDto() { IdFormulario = idPrograma, IdUsuario = idEvaluador2, IdPermiso = decimal.Parse(constantes.GetValue("PermisoModEvalPerfilGore")) };
                    permisoModEval = await usuarios.eliminaPermisosUsuarios(new List<TablaExcepcionesPermisosDto>() { perModEval });
                    if (permisoModEval != "ok")
                        throw new Exception("Error al eliminar permiso de evaluación Dipres");
                }

                //Correo informando calificación (Para los 3 calificaciones)
                ViewDto<TablaUsuariosDto> usuario = new ViewDto<TablaUsuariosDto>();
                usuario = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = idUsuario, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                string mailEvaluacion = constantes.GetValue("EmailExAnteGore");
                DatosEmail dataMail = new DatosEmail();
                dataMail.de = mailEvaluacion;
                dataMail.para = emailContraparte;
                dataMail.asunto = "Calificación Perfil GORE";
                dataMail.imagen = true;
                string msj2 = "Saludos cordiales. <br/> Coordinador Evaluación Ex-Ante <br/>{0}<br/>{1}";
                string nombreUsuario = usuario.Dtos.SingleOrDefault().Nombre;
                string nombreProg = programa.Dtos.SingleOrDefault().Nombre;
                string ministerioProg = programa.Dtos.SingleOrDefault().Ministerio;
                string ministerio = usuario.Dtos.SingleOrDefault().Ministerio;
                string correoUsuario = usuario.Dtos.SingleOrDefault().Email;
                string version = await formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaVersionProgramas")), idPrograma);
                string versionFinal = (String.IsNullOrEmpty(version) ? string.Empty : (int.Parse(version) >= 1 ? "versión " + version : string.Empty));
                dataMail.mensaje = string.Format(new CuerpoEmail().emailHtmlEvaluacion, string.Format(msj1, nombreProg), string.Format(msj2, ministerio, mailEvaluacion));
                dataMail.adjunto = rutaArchivo;
                EvaluacionExAnteModels exAnteCentral = new EvaluacionExAnteModels();
                Boolean estadoUsuario = exAnteCentral.enviaMail(dataMail);
                if (!estadoUsuario)
                    throw new Exception("error envio mail asignacion de evaluador");
            }
            catch (Exception ex)
            {
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return (registro);
        }

        public Task<string> iteracionPerfilGore(int idPrograma, int idBips, string idUsuario, bool conviertePrograma, ViewDto<TablaUsuariosDto> contraparte)
        {
            string resultado = null;
            try
            {
                TablaParametrosDto tipoFormulario = new TablaParametrosDto();
                if (conviertePrograma) { tipoFormulario.IdParametro = int.Parse(constantes.GetValue("TipoProgramaGore")); }
                
                //ViewDto<TablaProgramasDto> tipoFormulario = new ViewDto<TablaProgramasDto>();
                //tipoFormulario = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idFormulario });
                //int tipo = int.Parse(tipoFormulario.Dtos.FirstOrDefault().IdTipoFormulario.ToString());

                FormulariosModels formulario = new FormulariosModels();
                //var versionActual = decimal.Parse(formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("AccesoFormGuardado")), idPrograma).Result);
                decimal nuevaVersion = conviertePrograma ? 0 : decimal.Parse(formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("AccesoFormGuardado")), idPrograma).Result);
                //Crea nueva iteracion
                ViewDto<TablaProgramasDto> creaIteracion = new ViewDto<TablaProgramasDto>();
                creaIteracion = bips.RegistrarProgramas(new ContextoDto(), new TablaProgramasDto() { IdPrograma = idPrograma, IdBips = idBips, VersionActual = nuevaVersion, TipoFormulario = tipoFormulario }, EnumAccionRealizar.EliminarUserGrupo);
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
                }

                //Realiza ajustes antes de volver a usar el flujo
                if (conviertePrograma)
                {
                    resultado = creaIteracion.Dtos.SingleOrDefault().IdPrograma.ToString();
                }
                else
                {
                    //Asigna permiso usuario
                    string idContraparte = string.Empty;
                    if (contraparte.HasElements())
                    {
                        idContraparte = contraparte.Dtos.FirstOrDefault().Id;
                        ViewDto<TablaExcepcionesPermisosDto> regPermiso = new ViewDto<TablaExcepcionesPermisosDto>();
                        regPermiso = bips.RegistrarExcepcionesFormularios(new ContextoDto(), new TablaExcepcionesPermisosDto() { IdUsuario = idContraparte, IdFormulario = creaIteracion.Dtos.SingleOrDefault().IdPrograma, IdPermiso = decimal.Parse(constantes.GetValue("PermisoPerfilGore")), Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Insertar);
                        if (regPermiso.HasError())
                            throw new Exception(regPermiso.Error.Detalle);
                    }

                    //Limpia preguntas evaluacion para repetir proceso
                    ViewDto<TablaRespuestasDto> borradoPreguntasEvaluacion = new ViewDto<TablaRespuestasDto>();
                    borradoPreguntasEvaluacion = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = creaIteracion.Dtos.SingleOrDefault().IdPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaTipoOferta")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                    if (!borradoPreguntasEvaluacion.Sucess())
                        throw new Exception("Error al limpiar pregunta evaluacion");
                    borradoPreguntasEvaluacion = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = creaIteracion.Dtos.SingleOrDefault().IdPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaAdmisibilidadIngreso")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                    if (!borradoPreguntasEvaluacion.Sucess())
                        throw new Exception("Error al limpiar pregunta evaluacion");
                    borradoPreguntasEvaluacion = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = creaIteracion.Dtos.SingleOrDefault().IdPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaAdmisibilidadMotivo")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                    if (!borradoPreguntasEvaluacion.Sucess())
                        throw new Exception("Error al limpiar pregunta evaluacion");
                    borradoPreguntasEvaluacion = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = creaIteracion.Dtos.SingleOrDefault().IdPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaAdmisibilidadOtro")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                    if (!borradoPreguntasEvaluacion.Sucess())
                        throw new Exception("Error al limpiar pregunta evaluacion");
                    borradoPreguntasEvaluacion = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = creaIteracion.Dtos.SingleOrDefault().IdPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaIngresoExAnte")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                    if (!borradoPreguntasEvaluacion.Sucess())
                        throw new Exception("Error al limpiar pregunta evaluacion");
                    borradoPreguntasEvaluacion = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = creaIteracion.Dtos.SingleOrDefault().IdPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaCalificacionPerfilGore")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                    if (!borradoPreguntasEvaluacion.Sucess())
                        throw new Exception("Error al limpiar pregunta evaluacion");

                    resultado = "0";
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            return Task.FromResult(resultado);
        }

        public async Task<string> crearPrograma(int idPrograma, int idBips, string idUsuario)
        {
            string resultado = null;
            try
            {
                string emailContraparte = string.Empty;
                ViewDto<TablaProgramasDto> programa = new ViewDto<TablaProgramasDto>();
                programa = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idPrograma, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Buscar);
                ViewDto<TablaUsuariosDto> correoContrapartes = new ViewDto<TablaUsuariosDto>();
                correoContrapartes = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { IdGore = programa.Dtos.FirstOrDefault().IdServicio.IdParametro, IdPerfilGore = decimal.Parse(constantes.GetValue("PerfilContraparteGore")), IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                ViewDto<TablaUsuariosDto> contraparte = new ViewDto<TablaUsuariosDto>();
                contraparte = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Email = correoContrapartes.Dtos.Where(p => p.IdGore == programa.Dtos.FirstOrDefault().IdServicio.IdParametro).FirstOrDefault().Email, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                if (contraparte.HasElements())
                    emailContraparte = contraparte.Dtos.FirstOrDefault().Email;

                string resultadoIteracion = string.Empty;
                resultadoIteracion = await iteracionPerfilGore(idPrograma, idBips, idUsuario, true, contraparte);
                if (resultadoIteracion == null)
                    throw new Exception("error al iterar perfil");

                //Asigna permiso formulario programa
                UsuariosModels usuarios = new UsuariosModels();
                string permisoModEval = string.Empty;
                TablaExcepcionesPermisosDto perModEval = new TablaExcepcionesPermisosDto() { IdFormulario = decimal.Parse(resultadoIteracion), IdUsuario = idUsuario, IdPermiso = decimal.Parse(constantes.GetValue("PermisoProgramaGore")) };
                permisoModEval = await usuarios.eliminaPermisosUsuarios(new List<TablaExcepcionesPermisosDto>() { perModEval });
                if (permisoModEval != "ok")
                    throw new Exception("error eliminar permiso de formulario programa");
                permisoModEval = await usuarios.registraPermisosUsuarios(new List<TablaExcepcionesPermisosDto>() { new TablaExcepcionesPermisosDto() { IdFormulario = decimal.Parse(resultadoIteracion), IdUsuario = idUsuario, IdPermiso = decimal.Parse(constantes.GetValue("PermisoProgramaGore")), Estado = decimal.Parse(constantes.GetValue("Activo")) } });
                if (permisoModEval != "ok")
                    throw new Exception("error crear permiso de formulario programa");

                resultado = "ok";
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            return (resultado);
        }

        public async Task<string> guardaFormuladores(string[] formuladores, int idPermiso, int idPrograma, string idUsuario)
        {
            string resultado = null;
            try
            {
                string respuestaFormuladores = string.Empty;
                for (int i = 0; i < formuladores.Length; i++)
                {
                    //Asigna permiso formulario programa
                    UsuariosModels usuarios = new UsuariosModels();
                    string permisoModEval = string.Empty;
                    TablaExcepcionesPermisosDto perModEval = new TablaExcepcionesPermisosDto() { IdFormulario = idPrograma, IdUsuario = formuladores[i], IdPermiso = idPermiso };
                    permisoModEval = await usuarios.eliminaPermisosUsuarios(new List<TablaExcepcionesPermisosDto>() { perModEval });
                    if (permisoModEval != "ok")
                        throw new Exception("error eliminar permiso de formulario programa");
                    permisoModEval = await usuarios.registraPermisosUsuarios(new List<TablaExcepcionesPermisosDto>() { new TablaExcepcionesPermisosDto() { IdFormulario = idPrograma, IdUsuario = formuladores[i], IdPermiso = idPermiso, Estado = decimal.Parse(constantes.GetValue("Activo")) } });
                    if (permisoModEval != "ok")
                        throw new Exception("error crear permiso de formulario programa");

                    respuestaFormuladores += formuladores[i] + ",";
                }

                //Guardar formuladores en pregunta
                ViewDto<TablaRespuestasDto> borradoAsignacionExAnte = new ViewDto<TablaRespuestasDto>();
                borradoAsignacionExAnte = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaFormuladoresProgramaGore")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                if (!borradoAsignacionExAnte.Sucess())
                    throw new Exception("Error al borrar asignacion formuladores");
                ViewDto<TablaRespuestasDto> registraAsignacionExAnte = new ViewDto<TablaRespuestasDto>();
                registraAsignacionExAnte = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaFormuladoresProgramaGore")), Respuesta = respuestaFormuladores, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                if (!registraAsignacionExAnte.Sucess())
                    throw new Exception("Error al registrar asignacion formuladores");

                resultado = "ok";
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                resultado = ex.Message;
            }
            return (resultado);
        }

        public async Task<string> asignaEvaluadoresPrograma(int idPrograma, string idEvaluador, string idEvaluadorAnterior, int numeroEvaluador, string idUsuario)
        {
            string registro = "ok";
            try
            {
                //Busco correos de envio
                string mailEvaluacion = constantes.GetValue("EmailExAnteGore");
                //Enviar mail a evaluador asignado
                ViewDto<TablaUsuariosDto> usuario = new ViewDto<TablaUsuariosDto>();
                usuario = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = idUsuario, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                ViewDto<TablaUsuariosDto> evaluador = new ViewDto<TablaUsuariosDto>();
                evaluador = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = idEvaluador, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                ViewDto<TablaProgramasDto> programa = new ViewDto<TablaProgramasDto>();
                programa = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idPrograma, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Buscar);
                DatosEmail dataMail = new DatosEmail();
                dataMail.de = mailEvaluacion;
                dataMail.para = evaluador.Dtos.SingleOrDefault().Email;
                dataMail.asunto = "Asignación Programa GORE";
                dataMail.imagen = true;
                string msj1 = "Estimado Evaluador:<br/> Se le ha asignado como <b>Evaluador {0}</b> el programa <b>{1} {2}</b> (Gobierno regional de {3}) para ser evaluado. Se recuerda que tiene un plazo de 10 días hábiles para evaluar y enviar su informe.";
                string msj2 = "Saluda Atentamente, <br/> Coordinador Evaluación Ex-Ante <br/>{0}<br/>{1}<br/>{2}";
                string nombreUsuario = usuario.Dtos.SingleOrDefault().Nombre;
                string nombreProg = programa.Dtos.SingleOrDefault().Nombre;
                string ministerioProg = programa.Dtos.SingleOrDefault().Ministerio;
                string ministerio = usuario.Dtos.SingleOrDefault().Ministerio;
                string correoUsuario = usuario.Dtos.SingleOrDefault().Email;
                FormulariosModels formulario = new FormulariosModels();
                string version = await formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaVersionProgramas")), idPrograma);
                string versionFinal = (String.IsNullOrEmpty(version) ? string.Empty : (int.Parse(version) >= 1 ? "versión " + version : string.Empty));
                dataMail.mensaje = string.Format(new CuerpoEmail().emailHtmlEvaluacion, string.Format(msj1, numeroEvaluador, nombreProg, versionFinal, ministerioProg), string.Format(msj2, nombreUsuario, ministerio, correoUsuario));
                EvaluacionExAnteModels exAnteCentral = new EvaluacionExAnteModels();
                Boolean estadoUsuario = exAnteCentral.enviaMail(dataMail);
                if (!estadoUsuario)
                    throw new Exception("error envio mail asignacion de evaluador");

                //Registro fecha envio de mail de asignacion
                ViewDto<TablaRespuestasDto> borradoFechaEval = new ViewDto<TablaRespuestasDto>();
                borradoFechaEval = new ViewDto<TablaRespuestasDto>();
                borradoFechaEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaFechaRegEval" + numeroEvaluador)), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                if (!borradoFechaEval.Sucess())
                    throw new Exception("error registro fecha asignacion de evaluador (borrado)");
                ViewDto<TablaRespuestasDto> registraFechaEval = new ViewDto<TablaRespuestasDto>();
                registraFechaEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaFechaRegEval" + numeroEvaluador)), Respuesta = DateTime.Now, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                if (!registraFechaEval.Sucess())
                    throw new Exception("error registro fecha asignacion de evaluador (registro)");
                //Registro usuario envio de mail de asignacion
                ViewDto<TablaRespuestasDto> borradoUsuarioEval = new ViewDto<TablaRespuestasDto>();
                borradoUsuarioEval = new ViewDto<TablaRespuestasDto>();
                borradoUsuarioEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaUsuarioRegEva" + numeroEvaluador)), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                if (!borradoUsuarioEval.Sucess())
                    throw new Exception("error registro usuario asignacion de evaluador (borrado)");
                ViewDto<TablaRespuestasDto> registraUsuarioEval = new ViewDto<TablaRespuestasDto>();
                registraUsuarioEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaUsuarioRegEva" + numeroEvaluador)), Respuesta = idUsuario, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                if (!registraUsuarioEval.Sucess())
                    throw new Exception("error registro usuario asignacion de evaluador (registro)");

                //Asigno evaluador (Definido si es evaluador 1 o 2)
                ViewDto<TablaRespuestasDto> borradoEvaluador = new ViewDto<TablaRespuestasDto>();
                borradoEvaluador = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaEvaluador" + numeroEvaluador)), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                if (!borradoEvaluador.Sucess())
                    throw new Exception("error borrado asignacion de evaluador");
                ViewDto<TablaRespuestasDto> registraEvaluador = new ViewDto<TablaRespuestasDto>();
                registraEvaluador = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaEvaluador" + numeroEvaluador)), Respuesta = idEvaluador, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                if (!registraEvaluador.Sucess())
                    throw new Exception("error registro asignacion de evaluador");

                //Asigna permiso modulo evaluacion
                UsuariosModels usuarios = new UsuariosModels();
                string permisoModEval = string.Empty;
                TablaExcepcionesPermisosDto perModEval = new TablaExcepcionesPermisosDto() { IdFormulario = idPrograma, IdUsuario = idEvaluadorAnterior, IdPermiso = decimal.Parse(constantes.GetValue("PermisoModEvalProgramaGore" + numeroEvaluador)) };
                if (idEvaluadorAnterior != "")
                {
                    permisoModEval = await usuarios.eliminaPermisosUsuarios(new List<TablaExcepcionesPermisosDto>() { perModEval });
                    if (permisoModEval != "ok")
                        throw new Exception("error eliminar permiso de evaluacion");
                }
                permisoModEval = await usuarios.registraPermisosUsuarios(new List<TablaExcepcionesPermisosDto>() { new TablaExcepcionesPermisosDto() { IdFormulario = idPrograma, IdUsuario = idEvaluador, IdPermiso = decimal.Parse(constantes.GetValue("PermisoModEvalProgramaGore" + numeroEvaluador)), Estado = decimal.Parse(constantes.GetValue("Activo")) } });
                if (permisoModEval != "ok")
                    throw new Exception("error crear permiso de evaluacion");
            }
            catch (Exception ex)
            {
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return (registro);
        }

        public async Task<string> enviarEvaluacionJefatura(int idPrograma, string calificacion, string idUsuario, string rutaArchivos)
        {
            string registro = "ok";
            try
            {
                ViewDto<TablaRespuestasDto> borradoAsignacionExAnte = new ViewDto<TablaRespuestasDto>();
                borradoAsignacionExAnte = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaCalificacionProgramaGore")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                if (!borradoAsignacionExAnte.Sucess())
                    throw new Exception("Error registro calificacion programa (registro");
                ViewDto<TablaRespuestasDto> registraCalificacionPrograma = new ViewDto<TablaRespuestasDto>();
                registraCalificacionPrograma = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaCalificacionProgramaGore")), Respuesta = calificacion, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                if (!registraCalificacionPrograma.Sucess())
                    throw new Exception("error registro calificacion programa (registro)");

                FormulariosModels formulario = new FormulariosModels();
                UsuariosModels usuarios = new UsuariosModels();
                string permisoModEval = string.Empty;

                //Descarga archivo pdf desde url
                int añoActual = DateTime.Now.Year;
                var fileUrl = "http://10.10.14.161:83/Informes_BIPS_BO/admin/fichas/6/0/6/" + idPrograma + "/" + añoActual + "/348";
                var fileName = "PRG" + añoActual + "_" + idPrograma + ".pdf";
                using (var client = new WebClient())
                {
                    client.DownloadFile(fileUrl, Path.Combine(rutaArchivos, Path.GetFileName(fileName)));
                }
                string rutaArchivo = string.Empty;
                rutaArchivo = Path.Combine(rutaArchivos, fileName);

                string emailJefatura = string.Empty;
                ViewDto<TablaProgramasDto> programa = new ViewDto<TablaProgramasDto>();
                programa = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idPrograma, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Buscar);
                ViewDto<TablaUsuariosDto> correoContrapartes = new ViewDto<TablaUsuariosDto>();
                correoContrapartes = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { IdGore = programa.Dtos.FirstOrDefault().IdServicio.IdParametro, IdPerfilGore = decimal.Parse(constantes.GetValue("PerfilContraparteGore")), IdEstado = decimal.Parse(constantes.GetValue("Activo")) });

                //Busco correos jefaturas
                ViewDto<TablaParametrosDto> correoJefatura = new ViewDto<TablaParametrosDto>();
                correoJefatura = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("CorreosJefaturas")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                if (correoJefatura.HasElements())
                {
                    foreach (var correo in correoJefatura.Dtos.Where(p => p.IdParametro != p.IdCategoria))
                    {
                        emailJefatura += correo.Descripcion;
                    }
                }

                ViewDto<TablaUsuariosDto> usuario = new ViewDto<TablaUsuariosDto>();
                usuario = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = idUsuario, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                string mailEvaluacion = constantes.GetValue("EmailExAnteGore");
                DatosEmail dataMail = new DatosEmail();
                dataMail.de = mailEvaluacion;
                dataMail.para = emailJefatura;
                dataMail.asunto = "Revisión evaluación Programa GORE";
                dataMail.imagen = true;
                string msj1 = "<p>Estimados(as):</p>Los evaluadores del programa <b>{0}</b> envían la evaluación para su revisión, se adjunta reporte.<br/>";
                string msj2 = "Saludos cordiales. <br/> Coordinador Evaluación Ex-Ante <br/>{0}<br/>{1}";
                string nombreUsuario = usuario.Dtos.SingleOrDefault().Nombre;
                string nombreProg = programa.Dtos.SingleOrDefault().Nombre;
                string ministerioProg = programa.Dtos.SingleOrDefault().Ministerio;
                string ministerio = usuario.Dtos.SingleOrDefault().Ministerio;
                string correoUsuario = usuario.Dtos.SingleOrDefault().Email;
                string version = await formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaVersionProgramas")), idPrograma);
                string versionFinal = (String.IsNullOrEmpty(version) ? string.Empty : (int.Parse(version) >= 1 ? "versión " + version : string.Empty));
                dataMail.mensaje = string.Format(new CuerpoEmail().emailHtmlEvaluacion, string.Format(msj1, nombreProg), string.Format(msj2, ministerio, mailEvaluacion));
                dataMail.adjunto = rutaArchivo;
                EvaluacionExAnteModels exAnteCentral = new EvaluacionExAnteModels();
                Boolean estadoUsuario = exAnteCentral.enviaMail(dataMail);
                if (!estadoUsuario)
                    throw new Exception("error envio mail asignacion de evaluador");

                //Cambio Etapa revisión Jefatura
                ViewDto<TablaProgramasDto> actualizaEtapa = new ViewDto<TablaProgramasDto>();
                TablaProgramasDto datosProgAct = new TablaProgramasDto();
                datosProgAct.IdPrograma = idPrograma;
                datosProgAct.Estado.IdParametro = decimal.Parse(constantes.GetValue("Activo"));
                datosProgAct.Etapa.IdParametro = decimal.Parse(constantes.GetValue("EtapaRevisionJefe"));
                actualizaEtapa = bips.RegistrarProgramas(new ContextoDto(), datosProgAct, EnumAccionRealizar.Actualizar);
                if (actualizaEtapa.HasError())
                    throw new Exception("Error al actualizar etapa (Etapa revisión Jefe)");

                //Elimina permiso evaluación para evaluador 1
                string idEvaluador1 = await formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaEvaluador1")), idPrograma);
                TablaExcepcionesPermisosDto perModEval = new TablaExcepcionesPermisosDto() { IdFormulario = idPrograma, IdUsuario = idEvaluador1, IdPermiso = decimal.Parse(constantes.GetValue("PermisoModEvalProgramaGore1")) };
                permisoModEval = await usuarios.eliminaPermisosUsuarios(new List<TablaExcepcionesPermisosDto>() { perModEval });
                if (permisoModEval != "ok")
                    throw new Exception("Error al eliminar permiso de evaluación 1");

                //Elimina permiso evaluación para evaluador 2
                string idEvaluador2 = await formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaEvaluador2")), idPrograma);
                perModEval = new TablaExcepcionesPermisosDto() { IdFormulario = idPrograma, IdUsuario = idEvaluador2, IdPermiso = decimal.Parse(constantes.GetValue("PermisoModEvalProgramaGore2")) };
                permisoModEval = await usuarios.eliminaPermisosUsuarios(new List<TablaExcepcionesPermisosDto>() { perModEval });
                if (permisoModEval != "ok")
                    throw new Exception("Error al eliminar permiso de evaluación 2");
            }
            catch (Exception ex)
            {
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return (registro);
        }

        public async Task<string> enviarComentariosJefatura(int idPrograma, HttpPostedFileBase comentarios, string idUsuario, string rutaArchivos)
        {
            string registro = "ok";
            try
            {
                FormulariosModels formulario = new FormulariosModels();
                UsuariosModels usuarios = new UsuariosModels();
                string permisoModEval = string.Empty;

                var fileName = comentarios.FileName;
                string rutaArchivo = string.Empty;
                rutaArchivo = Path.Combine(rutaArchivos, fileName);
                comentarios.SaveAs(rutaArchivo);

                string idEvaluador1 = await formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaEvaluador1")), idPrograma);
                string idEvaluador2 = await formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaEvaluador2")), idPrograma);

                ViewDto<TablaUsuariosDto> evaluador1 = new ViewDto<TablaUsuariosDto>();
                evaluador1 = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = idEvaluador1, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                ViewDto<TablaUsuariosDto> evaluador2 = new ViewDto<TablaUsuariosDto>();
                evaluador2 = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = idEvaluador2, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });

                ViewDto<TablaProgramasDto> programa = new ViewDto<TablaProgramasDto>();
                programa = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idPrograma, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Buscar);

                ViewDto<TablaUsuariosDto> usuario = new ViewDto<TablaUsuariosDto>();
                usuario = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = idUsuario, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                string mailEvaluacion = constantes.GetValue("EmailExAnteGore");
                DatosEmail dataMail = new DatosEmail();
                dataMail.de = mailEvaluacion;
                dataMail.para = evaluador1.Dtos.SingleOrDefault().Email + "," + evaluador2.Dtos.SingleOrDefault().Email;
                dataMail.asunto = "Comentarios Jefatura Programa GORE";
                dataMail.imagen = true;
                string msj1 = "Estimado/a<br/> Se ha asignado el permiso del módulo de evaluación del Programa GORE <b>{0}</b> por revisión de Jefatura, se adjunta reporte con comentarios.";
                string msj2 = "Saludos cordiales. <br/> Coordinador Evaluación Ex-Ante <br/>{0}<br/>{1}";
                string nombreUsuario = usuario.Dtos.SingleOrDefault().Nombre;
                string nombreProg = programa.Dtos.SingleOrDefault().Nombre;
                string ministerioProg = programa.Dtos.SingleOrDefault().Ministerio;
                string ministerio = usuario.Dtos.SingleOrDefault().Ministerio;
                string correoUsuario = usuario.Dtos.SingleOrDefault().Email;
                string version = await formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaVersionProgramas")), idPrograma);
                string versionFinal = (String.IsNullOrEmpty(version) ? string.Empty : (int.Parse(version) >= 1 ? "versión " + version : string.Empty));
                dataMail.mensaje = string.Format(new CuerpoEmail().emailHtmlEvaluacion, string.Format(msj1, nombreProg), string.Format(msj2, ministerio, mailEvaluacion));
                dataMail.adjunto = rutaArchivo;
                EvaluacionExAnteModels exAnteCentral = new EvaluacionExAnteModels();
                Boolean estadoUsuario = exAnteCentral.enviaMail(dataMail);
                if (!estadoUsuario)
                    throw new Exception("error envio mail asignacion de evaluador");

                //Asigna permiso evaluación para evaluador 1
                permisoModEval = await usuarios.registraPermisosUsuarios(new List<TablaExcepcionesPermisosDto>() { new TablaExcepcionesPermisosDto() { IdFormulario = idPrograma, IdUsuario = idEvaluador1, IdPermiso = decimal.Parse(constantes.GetValue("PermisoModEvalProgramaGore1")), Estado = decimal.Parse(constantes.GetValue("Activo")) } });
                if (permisoModEval != "ok")
                    throw new Exception("error crear permiso de evaluacion 1");

                //Asigna permiso evaluación para evaluador 2
                permisoModEval = await usuarios.registraPermisosUsuarios(new List<TablaExcepcionesPermisosDto>() { new TablaExcepcionesPermisosDto() { IdFormulario = idPrograma, IdUsuario = idEvaluador2, IdPermiso = decimal.Parse(constantes.GetValue("PermisoModEvalProgramaGore2")), Estado = decimal.Parse(constantes.GetValue("Activo")) } });
                if (permisoModEval != "ok")
                    throw new Exception("error crear permiso de evaluacion 2");

                //Cambio etapa a En Evaluación
                ViewDto<TablaProgramasDto> actualizaEtapa = new ViewDto<TablaProgramasDto>();
                TablaProgramasDto datosProgAct = new TablaProgramasDto();
                datosProgAct.IdPrograma = idPrograma;
                datosProgAct.Estado.IdParametro = decimal.Parse(constantes.GetValue("Activo"));
                datosProgAct.Etapa.IdParametro = decimal.Parse(constantes.GetValue("EtapaEvaluacion"));
                actualizaEtapa = bips.RegistrarProgramas(new ContextoDto(), datosProgAct, EnumAccionRealizar.Actualizar);
                if (actualizaEtapa.HasError())
                    throw new Exception("Error al actualizar etapa (En evaluacion)");
            }
            catch (Exception)
            {
                throw;
            }
            return registro;
        }

        public async Task<string> enviarCalificacionPrograma(int idPrograma, int idBips, string idUsuario, string rutaArchivos)
        {
            string registro = "ok";
            try
            {
                FormulariosModels formulario = new FormulariosModels();
                string calificacion = await formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaCalificacionProgramaGore")), idPrograma);
                //VALIDACIÓN EVALUACIÓN PROGRAMA GORE
                //30520 - Falta información
                //30521 - Objetado técnicamente
                //30522 - Recomendado favorablemente

                //Cuerpo del correo según calificación
                string msj1 = "<p>Estimado(a):</p>Se ha realizado la calificación del programa <b>{0}</b> con el siguiente resultado:<br/>";

                //Descarga archivo pdf desde url
                int añoActual = DateTime.Now.Year;
                var fileUrl = "http://10.10.14.161:83/Informes_BIPS_BO/admin/fichas/6/0/6/" + idPrograma + "/" + añoActual + "/348";
                var fileName = "PRG" + añoActual + "_" + idPrograma + ".pdf";
                using (var client = new WebClient())
                {
                    client.DownloadFile(fileUrl, Path.Combine(rutaArchivos, Path.GetFileName(fileName)));
                }
                string rutaArchivo = string.Empty;
                rutaArchivo = Path.Combine(rutaArchivos, fileName);

                string emailContraparte = string.Empty;
                ViewDto<TablaProgramasDto> programa = new ViewDto<TablaProgramasDto>();
                programa = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idPrograma, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Buscar);
                ViewDto<TablaUsuariosDto> correoContrapartes = new ViewDto<TablaUsuariosDto>();
                correoContrapartes = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { IdGore = programa.Dtos.FirstOrDefault().IdServicio.IdParametro, IdPerfilGore = decimal.Parse(constantes.GetValue("PerfilContraparteGore")), IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                ViewDto<TablaUsuariosDto> contraparte = new ViewDto<TablaUsuariosDto>();
                contraparte = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Email = correoContrapartes.Dtos.Where(p => p.IdGore == programa.Dtos.FirstOrDefault().IdServicio.IdParametro && p.Id == idUsuario).FirstOrDefault().Email, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                if (contraparte.HasElements())
                    emailContraparte = contraparte.Dtos.FirstOrDefault().Email;

                string resultadoIteracion = null;

                switch (calificacion)
                {
                    case "30520":
                        resultadoIteracion = await iteracionProgramaGore(idPrograma, idBips, idUsuario, contraparte);
                        if (resultadoIteracion != null)
                            throw new Exception("error al iterar programa");

                        //Crea parte del correo informando el avance del perfil
                        msj1 += "<p><b>Falta Información</b>, la plataforma procederá al cambio de etapa a <b>En corrección servicio</b> para que la COG realice corrija el formulario del Programa.<br/></p>";

                        break;
                    case "30521":
                        resultadoIteracion = await iteracionProgramaGore(idPrograma, idBips, idUsuario, contraparte);
                        if (resultadoIteracion != null)
                            throw new Exception("error al iterar programa");

                        //Crea parte del correo informando que vuelve al inicio
                        msj1 += "<p><b>Objetado técnicamente</b>, la plataforma procederá al cambio de etapa a <b>En corrección servicio</b> para que la COG realice corrija el formulario del Programa.<br/></p>";

                        break;
                    case "30522":
                        //Finalización flujo programa
                        ViewDto<TablaProgramasDto> actualizaEtapa = new ViewDto<TablaProgramasDto>();
                        TablaProgramasDto datosProgAct = new TablaProgramasDto();
                        datosProgAct.IdPrograma = idPrograma;
                        datosProgAct.Estado.IdParametro = decimal.Parse(constantes.GetValue("Activo"));
                        datosProgAct.Etapa.IdParametro = decimal.Parse(constantes.GetValue("EtapaProgramaCalificado"));
                        actualizaEtapa = bips.RegistrarProgramas(new ContextoDto(), datosProgAct, EnumAccionRealizar.Actualizar);
                        if (actualizaEtapa.HasError())
                            throw new Exception("Error al actualizar etapa (calificacion programa gore)");

                        //Crea parte del correo informando el cierre del perfil
                        msj1 += "<p><b>Recomendado favorablemente</b>, se adjunta reporte del Programa GORE.<br/></p>";

                        break;
                    default:
                        break;
                }

                //Correo informando calificación (Para las 3 calificaciones)
                ViewDto<TablaUsuariosDto> usuario = new ViewDto<TablaUsuariosDto>();
                usuario = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = idUsuario, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                string mailEvaluacion = constantes.GetValue("EmailExAnteGore");
                DatosEmail dataMail = new DatosEmail();
                dataMail.de = mailEvaluacion;
                dataMail.para = emailContraparte;
                dataMail.asunto = "Calificación Programa GORE";
                dataMail.imagen = true;
                string msj2 = "Saludos cordiales. <br/> Coordinador Evaluación Ex-Ante <br/>{0}<br/>{1}";
                string nombreUsuario = usuario.Dtos.SingleOrDefault().Nombre;
                string nombreProg = programa.Dtos.SingleOrDefault().Nombre;
                string ministerioProg = programa.Dtos.SingleOrDefault().Ministerio;
                string ministerio = usuario.Dtos.SingleOrDefault().Ministerio;
                string correoUsuario = usuario.Dtos.SingleOrDefault().Email;
                string version = await formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaVersionProgramas")), idPrograma);
                string versionFinal = (String.IsNullOrEmpty(version) ? string.Empty : (int.Parse(version) >= 1 ? "versión " + version : string.Empty));
                dataMail.mensaje = string.Format(new CuerpoEmail().emailHtmlEvaluacion, string.Format(msj1, nombreProg), string.Format(msj2, ministerio, mailEvaluacion));
                dataMail.adjunto = rutaArchivo;
                EvaluacionExAnteModels exAnteCentral = new EvaluacionExAnteModels();
                Boolean estadoUsuario = exAnteCentral.enviaMail(dataMail);
                if (!estadoUsuario)
                    throw new Exception("error envio mail asignacion de evaluador");
            }
            catch (Exception ex)
            {
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return (registro);
        }
        public async Task<string> iteracionProgramaGore(int idPrograma, int idBips, string idUsuario, ViewDto<TablaUsuariosDto> contraparte)
        {
            string resultado = null;
            try
            {
                TablaParametrosDto tipoFormulario = new TablaParametrosDto();
                //if (conviertePrograma) { tipoFormulario.IdParametro = int.Parse(constantes.GetValue("TipoProgramaGore")); }

                FormulariosModels formulario = new FormulariosModels();
                decimal nuevaVersion = decimal.Parse(formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaVersionProgramas")), idPrograma).Result);
                //Crea nueva iteracion
                ViewDto<TablaProgramasDto> creaIteracion = new ViewDto<TablaProgramasDto>();
                creaIteracion = bips.RegistrarProgramas(new ContextoDto(), new TablaProgramasDto() { IdPrograma = idPrograma, IdBips = idBips, VersionActual = nuevaVersion }, EnumAccionRealizar.EliminarUserGrupo);
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
                }

                //Asigna permiso contraparte
                string idContraparte = string.Empty;
                if (contraparte.HasElements())
                {
                    idContraparte = contraparte.Dtos.FirstOrDefault().Id;
                    ViewDto<TablaExcepcionesPermisosDto> regPermiso = new ViewDto<TablaExcepcionesPermisosDto>();
                    regPermiso = bips.RegistrarExcepcionesFormularios(new ContextoDto(), new TablaExcepcionesPermisosDto() { IdUsuario = idContraparte, IdFormulario = creaIteracion.Dtos.SingleOrDefault().IdPrograma, IdPermiso = decimal.Parse(constantes.GetValue("PermisoProgramaGore")), Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Insertar);
                    if (regPermiso.HasError())
                        throw new Exception(regPermiso.Error.Detalle);
                }

                //Asigna permiso formuladores
                string listaFormuladores = await formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaFormuladoresProgramaGore")), idPrograma);
                string[] formuladores = listaFormuladores.Split(',');

                foreach (var formulador in formuladores)
                {
                    ViewDto<TablaExcepcionesPermisosDto> permisoFormulador = new ViewDto<TablaExcepcionesPermisosDto>();
                    permisoFormulador = bips.RegistrarExcepcionesFormularios(new ContextoDto(), new TablaExcepcionesPermisosDto() { IdUsuario = formulador, IdFormulario = creaIteracion.Dtos.SingleOrDefault().IdPrograma, IdPermiso = decimal.Parse(constantes.GetValue("PermisoProgramaGore")), Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Insertar);
                    if (permisoFormulador.HasError())
                        throw new Exception(permisoFormulador.Error.Detalle);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                resultado = ex.Message;
            }
            return resultado;
        }
    }
}