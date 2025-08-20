using log4net;
using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Core.Providers;
using MDS.Core.Util;
using MDS.Dto;
using MDS.Svc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

namespace SistemasBIPS.Models
{
    public class EvaluacionExAnteModels
    {
        private static ISistemasBIPSSvc bips = null;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private IProviderConstante constantes = null;

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public EvaluacionExAnteModels()
        {
            bips = (ISistemasBIPSSvc)Activator.CreateInstance(typeof(SistemasBIPSSvc));
            constantes = (IProviderConstante)Activator.CreateInstance(typeof(ProviderConstante));
        }
        #endregion

        public Task<List<TablaProgramasDto>> getEvaluacionesExAnte(String idUsuario)
        {
            List<TablaProgramasDto> data = new List<TablaProgramasDto>();
            try
            {
                //Busco perfil adicional usuario
                ViewDto<TablaParametrosUsuariosDto> perfilUsuario = new ViewDto<TablaParametrosUsuariosDto>();
                perfilUsuario = bips.BuscarParametrosUsuarios(new ContextoDto(), new TablaParametrosUsuariosFiltroDto(){ IdUsuario = idUsuario, Ano = DateTime.Now.Year });
                //Busca perfil en tabla parametro
                if (perfilUsuario.HasElements()){
                    ViewDto<TablaParametrosDto> buscarPerfilParametro = new ViewDto<TablaParametrosDto>();
                    buscarPerfilParametro = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdParametro = perfilUsuario.Dtos.FirstOrDefault().IdParametro, Estado = decimal.Parse(constantes.GetValue("Activo"))});
                    if (buscarPerfilParametro.HasElements()){
                        ProgramasModels programas = new ProgramasModels();
                        ViewDto<TablaProgramasDto> buscarProgramas = new ViewDto<TablaProgramasDto>();
                        buscarProgramas = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { Ano = DateTime.Now.Year, Nombre = (buscarPerfilParametro.Dtos.SingleOrDefault().IdParametro == decimal.Parse(constantes.GetValue("EvaluadorExAnte")) ? idUsuario : null) }, EnumAccionRealizar.BuscarExAnte);
                        if (buscarProgramas.HasElements())
                            data = buscarProgramas.Dtos;
                    }
                }                                              
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(data);
        }

        public Task<List<TablaProgramasDto>> getPanelExAnte()
        {
            List<TablaProgramasDto> data = new List<TablaProgramasDto>();
            try
            {
                
                    ProgramasModels programas = new ProgramasModels();
                    ViewDto<TablaProgramasDto> buscarProgramas = new ViewDto<TablaProgramasDto>();
                    buscarProgramas = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { Ano = DateTime.Now.Year, TipoFormulario = 360 }, EnumAccionRealizar.BuscarPanelExAnte);
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

        public async Task<List<TablaEvaluacionesExAnteDto>> programaDtoAEvaluacionesDto(List<TablaProgramasDto> programas)
        {
            List<TablaEvaluacionesExAnteDto> data = new List<TablaEvaluacionesExAnteDto>();
            try {
                if (programas.Count > 0){
                    FormulariosModels formulario = new FormulariosModels();
                    foreach(var item in programas){
                        data.Add(new TablaEvaluacionesExAnteDto() {
                            IdPrograma = item.IdPrograma,
                            IdBips = item.IdBips,
                            IdMinisterio = item.IdMinisterio.IdParametro,
                            IdServicio = item.IdServicio.IdParametro,
                            Ministerio = item.Ministerio,
                            Servicio = item.Servicio,
                            IdTipo = item.IdTipoFormulario,
                            Tipo = item.Tipo,
                            Nombre = item.Nombre,
                            Calificacion = await getDescripcionParametro(await formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaCalificacion")), int.Parse(item.IdPrograma.ToString()))),
                            Version = await formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaVersionProgramas")), int.Parse(item.IdPrograma.ToString())),
                            IdEvaluador1 = await formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaEvaluador1")), int.Parse(item.IdPrograma.ToString())),
                            IdEvaluador2 = await formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaEvaluador2")), int.Parse(item.IdPrograma.ToString())),
                            IdEncriptado = EncriptaDato.RijndaelSimple.Encriptar(item.IdPrograma.ToString()),
                            Etapa = item.IdEtapa,
                            Ano = item.Ano,
                            Estado = item.IdEstado
                        });
                    }
                }
            }
            catch(Exception ex) {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return (data);
        }

        public Task<bool> getRevisionJefaturas(int idCategoria, string idUsuario)
        {
            bool data = false;
            try {
                ViewDto<TablaParametrosDto> parametro = new ViewDto<TablaParametrosDto>();
                parametro = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = idCategoria, Estado = decimal.Parse(constantes.GetValue("Activo")) });
                if (parametro.HasElements()) {
                    if (parametro.Dtos.Count(p => p.Descripcion == idUsuario) > 0)
                        data = true;
                }
            }
            catch(Exception ex) {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(data);
        }

        public Task<List<TablaParametrosDto>> getJefaturasExAnte(int idCategoria)
        {
            List<TablaParametrosDto> data = new List<TablaParametrosDto>();
            try {
                ViewDto<TablaParametrosDto> parametro = new ViewDto<TablaParametrosDto>();
                parametro = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = idCategoria, Estado = decimal.Parse(constantes.GetValue("Activo")) });
                if (parametro.HasElements())
                    data = parametro.Dtos.OrderBy(p=>p.Orden).ToList();
            }
            catch(Exception ex) {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(data);
        }

        public async Task<int> getJefaturaConectada(string idUsuario)
        {
            int data = 0;
            try
            {
                List<TablaParametrosDto> jefaturas = new List<TablaParametrosDto>();
                jefaturas = await getJefaturasExAnte(int.Parse(constantes.GetValue("RevisionExAnte")));
                if (jefaturas.Count(p=>p.Descripcion == idUsuario) > 0)
                    data = int.Parse(jefaturas.Where(p => p.Descripcion == idUsuario).SingleOrDefault().Valor.ToString());
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return (data);
        }

        public Task<ComentariosJefaturas> getComentarios(int idPrograma)
        {
            ComentariosJefaturas data = new ComentariosJefaturas();
            try
            {
                List<int> listaPregEvaluacion = new List<int>() {
                    int.Parse(constantes.GetValue("TieneComentMonitoreo")),
                    int.Parse(constantes.GetValue("ComentMonitoreo")),
                    int.Parse(constantes.GetValue("TieneComentEstudio")),
                    int.Parse(constantes.GetValue("ComentEstudio")),
                    int.Parse(constantes.GetValue("InformePDF"))
                };
                ViewDto<TablaRespuestasDto> buscaComentariosJefaturas = new ViewDto<TablaRespuestasDto>();
                foreach (var item in listaPregEvaluacion)
                {
                    buscaComentariosJefaturas = new ViewDto<TablaRespuestasDto>();
                    buscaComentariosJefaturas = bips.BuscarRespuestas(new ContextoDto(), new TablaRespuestasFiltroDto() { IdFormulario = idPrograma, IdPregunta = item });
                    if (buscaComentariosJefaturas.HasElements())
                    {
                        if (item == int.Parse(constantes.GetValue("TieneComentMonitoreo")))
                            data.tieneComentMonitoreo = buscaComentariosJefaturas.Dtos.FirstOrDefault().Respuesta.ToString();
                        else if (item == int.Parse(constantes.GetValue("ComentMonitoreo")))
                            data.comentMonitoreo = buscaComentariosJefaturas.Dtos.FirstOrDefault().Respuesta.ToString();
                        else if (item == int.Parse(constantes.GetValue("TieneComentEstudio")))
                            data.tieneComentEstudios = buscaComentariosJefaturas.Dtos.FirstOrDefault().Respuesta.ToString();
                        else if (item == int.Parse(constantes.GetValue("ComentEstudio")))
                            data.comentEstudios = buscaComentariosJefaturas.Dtos.FirstOrDefault().Respuesta.ToString();
                        else if (item == int.Parse(constantes.GetValue("InformePDF")))
                            data.nombrePDF = buscaComentariosJefaturas.Dtos.FirstOrDefault().Respuesta.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(data);
        }

        public async Task<String> revisionJefaturas(string idUsuario, List<String> datos, HttpPostedFileBase pdf, string rutaArchivos, string idEvaluador1, string idEvaluador2)
        {
            string data = "ok";
            //Random random = new Random();
            //const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            try {
                //Busca datos usuario conectado
                ViewDto<TablaUsuariosDto> usuarioConectado = new ViewDto<TablaUsuariosDto>();
                usuarioConectado = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = idUsuario });
                string rutaArchivo = string.Empty;
                //Busco datos del programa
                ViewDto<TablaProgramasDto> programa = new ViewDto<TablaProgramasDto>();
                programa = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = decimal.Parse(datos[0]), Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Buscar);
                //Guardo comentarios
                List<int> idComentarios = new List<int>() {
                    int.Parse(constantes.GetValue("TieneComentMonitoreo")),
                    int.Parse(constantes.GetValue("ComentMonitoreo")),
                    int.Parse(constantes.GetValue("TieneComentEstudio")),
                    int.Parse(constantes.GetValue("ComentEstudio"))
                };                
                if (datos.Count > 0){
                    int indiceComent = 1;
                    foreach (var dato in datos){
                        if (indiceComent > 1){
                            //borrado
                            ViewDto<TablaRespuestasDto> borrado = new ViewDto<TablaRespuestasDto>();
                            borrado = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = programa.Dtos.SingleOrDefault().IdPrograma, IdPregunta = idComentarios[indiceComent-2], TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                            if (!borrado.Sucess())
                                throw new Exception("error guardado (borrado)");
                            //guardado                                
                            if (!String.IsNullOrEmpty(dato))
                            {
                                ViewDto<TablaRespuestasDto> guardado = new ViewDto<TablaRespuestasDto>();
                                guardado = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = programa.Dtos.SingleOrDefault().IdPrograma, IdPregunta = idComentarios[indiceComent-2], Respuesta = dato, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                                if (!guardado.Sucess())
                                    throw new Exception("error guardado");
                            }
                        }   
                        indiceComent++;
                    }
                }                
                //Guardo informes en pdf
                if (pdf != null)
                {
                    if (pdf.ContentLength > 0)
                    {
                        string nombre = pdf.FileName.Split('.')[0];//new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
                        rutaArchivo = Path.Combine(rutaArchivos, Path.GetFileName(nombre + ".pdf"));
                        if (File.Exists(rutaArchivo))
                            File.Delete(rutaArchivo);
                        pdf.SaveAs(rutaArchivo);
                        //borrado
                        ViewDto<TablaRespuestasDto> borrado = new ViewDto<TablaRespuestasDto>();
                        borrado = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = programa.Dtos.SingleOrDefault().IdPrograma, IdPregunta = int.Parse(constantes.GetValue("InformePDF")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                        if (!borrado.Sucess())
                            throw new Exception("error guardado (borrado)");
                        //guardado
                        ViewDto<TablaRespuestasDto> guardado = new ViewDto<TablaRespuestasDto>();
                        guardado = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = programa.Dtos.SingleOrDefault().IdPrograma, IdPregunta = int.Parse(constantes.GetValue("InformePDF")), Respuesta = nombre, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                        if (!guardado.Sucess())
                            throw new Exception("error guardado");
                    }
                }                                                
                //Determino reglas de envio de mail, según observaciones
                bool tieneObs = ((datos[1] == "Si" || datos[3] == "Si") ? true : false);
                //Busco listado de jefaturas
                string mailJefaturas = string.Empty;
                string tipoJefatura = string.Empty;
                string comentariosMonitoreo = string.Empty;
                string comentariosEstudios = string.Empty;
                List<TablaParametrosDto> jefes = new List<TablaParametrosDto>();
                ViewDto<TablaUsuariosDto> jefaturas = new ViewDto<TablaUsuariosDto>();                
                jefes = await getJefaturasExAnte(int.Parse(constantes.GetValue("RevisionExAnte")));
                if (jefes.Count > 0){
                    foreach (var jefe in jefes){
                        jefaturas = new ViewDto<TablaUsuariosDto>();
                        jefaturas = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = jefe.Descripcion });
                        if (jefaturas.HasElements())
                            if (jefe.Descripcion != usuarioConectado.Dtos.SingleOrDefault().Id)
                                mailJefaturas += jefaturas.Dtos.SingleOrDefault().Email + ",";
                    }
                    //Busco si es jefatura de estudio conectada
                    //if (tieneObs){
                        //if (jefes.Where(p => p.Descripcion == idUsuario).SingleOrDefault().Valor == 2)
                        //{
                            ViewDto<TablaUsuariosDto> evaluador = new ViewDto<TablaUsuariosDto>();
                            if (!String.IsNullOrEmpty(idEvaluador1) || !String.IsNullOrEmpty(idEvaluador2))
                            {
                                if (idEvaluador1 != "-1")
                                {
                                    evaluador = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = idEvaluador1 });
                                    if (evaluador.HasElements())
                                        mailJefaturas += evaluador.Dtos.SingleOrDefault().Email + ",";
                                }
                                if (idEvaluador2 != "-1")
                                {
                                    evaluador = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = idEvaluador2 });
                                    if (evaluador.HasElements())
                                        mailJefaturas += evaluador.Dtos.SingleOrDefault().Email + ",";
                                }
                            }
                        //}
                    //}
                    //Tipo de jefatura conectada
                    tipoJefatura = (jefes.Where(p => p.Descripcion == idUsuario).SingleOrDefault().Valor == 1 ? "Monitoreo" : "Estudios");
                    //Obtengo comentarios según jefatura conectada
                    comentariosMonitoreo = (String.IsNullOrEmpty(datos[2]) ? string.Empty : datos[2]);
                    comentariosEstudios = (String.IsNullOrEmpty(datos[4]) ? string.Empty : datos[4]);
                    //Obtengo copias adicionales
                    List<TablaParametrosDto> copiasAdicionales = new List<TablaParametrosDto>();
                    copiasAdicionales = await getJefaturasExAnte(int.Parse(constantes.GetValue("OtrasCopiasComentExAnte")));
                    if (copiasAdicionales.Count > 0) {
                        foreach(var copia in copiasAdicionales.Where(p=>p.IdParametro != p.IdCategoria)){
                            if (!String.IsNullOrEmpty(copia.Descripcion))
                                mailJefaturas += copia.Descripcion + ",";
                        }
                    }
                }
                //Se define mensaje a enviar según si tiene o no comentarios
                string msj = string.Empty;
                if (tieneObs)
                    msj = "Estimada Jefatura:<br/><br/> Se informa que la evaluación del programa <b>{0} {1}</b> ({2} - {3}) se encuentra revisada por la Jefatura de {4} y se encuentra con observaciones a incorporar por la dupla evaluadora, las cuales podrá encontrar en el informe adjunto. Adicionalmente, podrá encontrar las observaciones más abajo en este correo.<br/><br/>Una vez revisado dicho Informe, debe incorporar las observaciones que considere pertinentes al Informe y cargar el documento a la plataforma, para que sea enviado a la dupla evaluadora.<br/><br/>Comentarios Jefatura Monitoreo:<br/><br/>{5}<br/><br/>Comentarios Jefatura Estudios:<br/><br/>{6}";
                else
                    msj = "Estimada Jefatura:<br/><br/> Se informa que la evaluación del programa <b>{0} {1}</b> ({2} - {3}) se encuentra revisada por la Jefatura de {4} y se encuentra sin observaciones a incorporar por la dupla evaluadora.<br/><br/>Una vez revisado dicho Informe, debe incorporar las observaciones que considere pertinentes al Informe y cargar el documento a la plataforma, para que sea enviado a la dupla evaluadora.";
                //Envio de mail
                DatosEmail dataMail = new DatosEmail();
                dataMail.de = usuarioConectado.Dtos.SingleOrDefault().Email;
                dataMail.para = mailJefaturas.Substring(0,mailJefaturas.Length-1);
                dataMail.asunto = "Revisión de informe Ex-Ante";
                dataMail.imagen = true;                
                string msj1 = msj;
                string msj2 = "Saluda Atentamente, <br/>{0}<br/>{1}<br/>{2}<br/>{3}";
                string nombreProg = programa.Dtos.SingleOrDefault().Nombre;
                string ministerioProg = programa.Dtos.SingleOrDefault().Ministerio;
                string servicioProg = programa.Dtos.SingleOrDefault().Servicio;
                string tipoProg = programa.Dtos.SingleOrDefault().Tipo;
                string nombreUsuario = usuarioConectado.Dtos.SingleOrDefault().Nombre;
                string ministerio = usuarioConectado.Dtos.SingleOrDefault().Ministerio;
                string servicio = usuarioConectado.Dtos.SingleOrDefault().Servicio;
                string correoUsuario = usuarioConectado.Dtos.SingleOrDefault().Email;
                FormulariosModels formulario = new FormulariosModels();
                string version = await formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaVersionProgramas")), int.Parse(datos[0]));
                string versionFinal = (String.IsNullOrEmpty(version) ? string.Empty : (int.Parse(version) >= 1 ? "versión " + version : string.Empty));
                dataMail.mensaje = string.Format(new CuerpoEmail().emailHtmlEvaluacion, string.Format(msj1, nombreProg, versionFinal, ministerioProg, servicioProg, tipoJefatura, comentariosMonitoreo, comentariosEstudios), string.Format(msj2, nombreUsuario, ministerio, servicio, correoUsuario));
                dataMail.adjunto = rutaArchivo;
                Boolean estadoUsuario = enviaMail(dataMail);
                if (!estadoUsuario)
                    throw new Exception("error envio mail asignacion de evaluador");
            }
            catch(Exception ex) {
                log.Error(ex.Message, ex);
                data = ex.Message;
                throw ex;
            }
            return (data);
        }

        public Task<String> getDescripcionParametro(string idParametro)
        {
            String data = string.Empty;
            try {
                if (!String.IsNullOrEmpty(idParametro))
                {
                    ViewDto<TablaParametrosDto> parametro = new ViewDto<TablaParametrosDto>();
                    parametro = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdParametro = decimal.Parse(idParametro), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (parametro.HasElements())
                        data = parametro.Dtos.SingleOrDefault().Descripcion;
                }                
            }
            catch(Exception ex) {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(data);
        }

        public Task<IList<TablaUsuariosDto>> getEvaluadores(int categoriaEval)
        {
            List<TablaUsuariosDto> data = new List<TablaUsuariosDto>();
            try {
                ViewDto<TablaParametrosDto> listaPerfilEvaluadores = new ViewDto<TablaParametrosDto>();
                listaPerfilEvaluadores = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = categoriaEval, Estado = decimal.Parse(constantes.GetValue("Activo"))});
                if (listaPerfilEvaluadores.HasElements()){
                    ViewDto<TablaUsuariosDto> evaluadores;
                    foreach (var perfil in listaPerfilEvaluadores.Dtos.OrderBy(p=>p.Orden)){
                        if (perfil.IdParametro != perfil.IdCategoria){
                            evaluadores = new ViewDto<TablaUsuariosDto>();
                            evaluadores = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Email = perfil.Descripcion, IdEstado = decimal.Parse(constantes.GetValue("Activo"))});
                            if (evaluadores.HasElements())
                                data.AddRange(evaluadores.Dtos);
                        }
                    }
                }
            }catch(Exception ex) {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult((IList<TablaUsuariosDto>)data);
        }

        public Task<IList<TablaParametrosDto>> getCalificaciones()
        {
            List<TablaParametrosDto> data = new List<TablaParametrosDto>();
            try
            {
                ViewDto<TablaParametrosDto> listaCalificaciones = new ViewDto<TablaParametrosDto>();
                listaCalificaciones = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto()
                {
                    IdCategoria = decimal.Parse(constantes.GetValue("Calificaciones")),
                    Estado = decimal.Parse(constantes.GetValue("Activo"))
                });
                if (listaCalificaciones.HasElements())
                    data = listaCalificaciones.Dtos;
                if (data.Count > 0)
                    data.RemoveAll(p=>p.IdParametro == p.IdCategoria);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult((IList<TablaParametrosDto>)data);
        }

        public Task<PreguntaEvaluaciones> getPregEvaluaciones(int idFormulario)
        {
            PreguntaEvaluaciones data = new PreguntaEvaluaciones();
            try {
                List<int> listaPregEvaluacion = new List<int>() {
                    int.Parse(constantes.GetValue("PreguntaCalificacion")),
                    int.Parse(constantes.GetValue("PreguntaComentSegpres")),
                    int.Parse(constantes.GetValue("PreguntaComentGeneral")),
                    int.Parse(constantes.GetValue("PreguntaAtingencia")),
                    int.Parse(constantes.GetValue("PreguntaCoherencia")),
                    int.Parse(constantes.GetValue("PreguntaConsistencia")),
                    int.Parse(constantes.GetValue("PreguntaAntecProg")),
                    int.Parse(constantes.GetValue("PreguntaDiagProg")),
                    int.Parse(constantes.GetValue("PreguntaObjProgPobl")),
                    int.Parse(constantes.GetValue("PreguntaEstragProg")),
                    int.Parse(constantes.GetValue("PreguntaIndic")),
                    int.Parse(constantes.GetValue("PreguntaGastos"))
                };
                ViewDto<TablaRespuestasDto> buscaPregEvaluacion = new ViewDto<TablaRespuestasDto>();
                foreach(var item in listaPregEvaluacion)
                {
                    buscaPregEvaluacion = new ViewDto<TablaRespuestasDto>();
                    buscaPregEvaluacion = bips.BuscarRespuestas(new ContextoDto(), new TablaRespuestasFiltroDto()
                    {
                        IdFormulario = idFormulario,
                        IdPregunta = item
                    });
                    if (buscaPregEvaluacion.HasElements())
                    {
                        if (item == int.Parse(constantes.GetValue("PreguntaCalificacion")))
                            data.calificacion = decimal.Parse(buscaPregEvaluacion.Dtos.FirstOrDefault().Respuesta.ToString());
                        else if (item == int.Parse(constantes.GetValue("PreguntaComentSegpres")))
                            data.comentSegpres = buscaPregEvaluacion.Dtos.FirstOrDefault().Respuesta.ToString();
                        else if (item == int.Parse(constantes.GetValue("PreguntaComentGeneral")))
                            data.comentGeneral = buscaPregEvaluacion.Dtos.FirstOrDefault().Respuesta.ToString();
                        else if (item == int.Parse(constantes.GetValue("PreguntaAtingencia")))
                            data.atingencia = buscaPregEvaluacion.Dtos.FirstOrDefault().Respuesta.ToString();
                        else if (item == int.Parse(constantes.GetValue("PreguntaCoherencia")))
                            data.coherencia = buscaPregEvaluacion.Dtos.FirstOrDefault().Respuesta.ToString();
                        else if (item == int.Parse(constantes.GetValue("PreguntaConsistencia")))
                            data.consistencia = buscaPregEvaluacion.Dtos.FirstOrDefault().Respuesta.ToString();
                        else if (item == int.Parse(constantes.GetValue("PreguntaAntecProg")))
                            data.antecPrograma = buscaPregEvaluacion.Dtos.FirstOrDefault().Respuesta.ToString();
                        else if (item == int.Parse(constantes.GetValue("PreguntaDiagProg")))
                            data.diagPrograma = buscaPregEvaluacion.Dtos.FirstOrDefault().Respuesta.ToString();
                        else if (item == int.Parse(constantes.GetValue("PreguntaObjProgPobl")))
                            data.objPoblPrograma = buscaPregEvaluacion.Dtos.FirstOrDefault().Respuesta.ToString();
                        else if (item == int.Parse(constantes.GetValue("PreguntaEstragProg")))
                            data.estrategiaPrograma = buscaPregEvaluacion.Dtos.FirstOrDefault().Respuesta.ToString();
                        else if (item == int.Parse(constantes.GetValue("PreguntaIndic")))
                            data.indicadoresPrograma = buscaPregEvaluacion.Dtos.FirstOrDefault().Respuesta.ToString();
                        else if (item == int.Parse(constantes.GetValue("PreguntaGastos")))
                            data.gastosPrograma = buscaPregEvaluacion.Dtos.FirstOrDefault().Respuesta.ToString();
                    }                        
                }                
            }
            catch (Exception ex) {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(data);
        }

        /// <summary>
        /// Metodo encargado del envio de mail
        /// </summary>
        /// <param name="de"></param>
        /// <param name="para"></param>
        /// <param name="cc"></param>
        /// <param name="asunto"></param>
        /// <param name="msg"></param>
        /// <returns name="Boolean"></returns>
        public Boolean enviaMail(DatosEmail dataMail)
        {
            Boolean mailEnviado = true;
            try
            {
                SmtpClient obj = new SmtpClient();
                MailMessage Mailmsg = new MailMessage();
                Mailmsg.From = new MailAddress(dataMail.de);
                Mailmsg.To.Add(dataMail.para);
                Mailmsg.Subject = dataMail.asunto;
                if (!String.IsNullOrEmpty(dataMail.cco))
                {
                    //MailAddressCollection addressBCC = new MailAddressCollection(dataMail.cco);
                    Mailmsg.Bcc.Add(dataMail.cco);
                }
                if (!String.IsNullOrEmpty(dataMail.cc))
                {
                    //MailAddressCollection EmailCC = new MailAddressCollection();
                    //EmailCC = Mailmsg.CC;
                    Mailmsg.CC.Add(dataMail.cc);
                }
                if (!dataMail.imagen && !dataMail.imagenDipres)
                {
                    Mailmsg.Body = dataMail.mensaje;
                    if (dataMail.cc != null)
                    {
                        MailAddressCollection EmailCC = new MailAddressCollection();
                        EmailCC = Mailmsg.CC;
                        EmailCC.Add(dataMail.cc);
                    }
                }
                else if (dataMail.imagenDipres)
                {
                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(dataMail.mensaje, null, "text/html");
                    LinkedResource logo = new LinkedResource(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content\\Images\\logoSES.png"));
                    logo.ContentId = "logoSES";
                    LinkedResource logoDipres = new LinkedResource(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content\\Images\\logo_DIPRES.png"));
                    logoDipres.ContentId = "logo_DIPRES";
                    htmlView.LinkedResources.Add(logo);
                    htmlView.LinkedResources.Add(logoDipres);
                    Mailmsg.AlternateViews.Add(htmlView);
                }
                else{
                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(dataMail.mensaje, null, "text/html");
                    LinkedResource logo = new LinkedResource(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content\\Images\\logoSES.png"));
                    logo.ContentId = "logoSES";
                    htmlView.LinkedResources.Add(logo);
                    Mailmsg.AlternateViews.Add(htmlView);
                }
                //Archivos adjuntos
                if (!String.IsNullOrEmpty(dataMail.adjunto))
                    Mailmsg.Attachments.Add(new Attachment(dataMail.adjunto));
                //servidor smtp
                obj.Host = constantes.GetValue("mailSmtp");
                obj.Port = int.Parse(constantes.GetValue("mailPuerto"));
                //Envia el correo en formato HTML
                Mailmsg.IsBodyHtml = true;
                //Envia el correo
                obj.Send(Mailmsg);
            }
            catch (Exception ex)
            {
                mailEnviado = false;
                log.Error(ex.Message, ex);
            }
            return mailEnviado;
        }

        public async Task<string> asignaEvaluadores(int idPrograma, string idEvaluador, int numEvaluador, string idUsuario)
        {
            string registro = "ok";
            try
            {
                //Asigno evaluador
                ViewDto<TablaRespuestasDto> borradoEvaluador = new ViewDto<TablaRespuestasDto>();
                borradoEvaluador = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto()
                {
                    IdFormulario = idPrograma,
                    IdPregunta = (numEvaluador == 1 ? decimal.Parse(constantes.GetValue("PreguntaEvaluador1")) : decimal.Parse(constantes.GetValue("PreguntaEvaluador2"))),
                    TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv"))
                }, EnumAccionRealizar.Eliminar);
                if (!borradoEvaluador.Sucess())
                    throw new Exception("error borrado asignacion de evaluador");
                ViewDto<TablaRespuestasDto> registraEvaluador = new ViewDto<TablaRespuestasDto>();
                registraEvaluador = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto()
                {
                    IdFormulario = idPrograma,
                    IdPregunta = (numEvaluador == 1 ? decimal.Parse(constantes.GetValue("PreguntaEvaluador1")) : decimal.Parse(constantes.GetValue("PreguntaEvaluador2"))),
                    Respuesta = idEvaluador,
                    TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal"))
                }, EnumAccionRealizar.Insertar);
                if (!registraEvaluador.Sucess())
                    throw new Exception("error registro asignacion de evaluador");
                //Busco correos de envio
                string mailEvaluacion = string.Empty;
                ViewDto<TablaParametrosDto> casillaEvaluacion = new ViewDto<TablaParametrosDto>();
                casillaEvaluacion = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto(){ IdCategoria = decimal.Parse(constantes.GetValue("listaMailAsigEval")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
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
                //Enviar mail a evaluador asignado
                ViewDto<TablaUsuariosDto> usuario = new ViewDto<TablaUsuariosDto>();
                usuario = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = idUsuario, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                ViewDto<TablaUsuariosDto> evaluador = new ViewDto<TablaUsuariosDto>();
                evaluador = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = idEvaluador, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                ViewDto<TablaProgramasDto> programa = new ViewDto<TablaProgramasDto>();
                programa = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idPrograma, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Buscar);
                DatosEmail dataMail = new DatosEmail();
                dataMail.de = usuario.Dtos.SingleOrDefault().Email;
                dataMail.para = mailEvaluacion + evaluador.Dtos.SingleOrDefault().Email;
                dataMail.asunto = "Programa para evaluar";
                dataMail.imagen = true;
                string msj1 = "Estimado Evaluador:<br/> Se le ha asignado el programa <b>{0} {1}</b> ({2} - {3}) para ser evaluado como <b>{4}</b>. Se recuerda que tiene un plazo de 2 días hábiles para evaluar y enviar su informe de evaluación al sectorialista.";
                string msj2 = "Saluda Atentamente, <br/> Coordinador Evaluación Ex-Ante <br/>{0}<br/>{1}<br/>{2}<br/>{3}";
                string nombreUsuario = usuario.Dtos.SingleOrDefault().Nombre;
                string nombreProg = programa.Dtos.SingleOrDefault().Nombre;
                string ministerioProg = programa.Dtos.SingleOrDefault().Ministerio;
                string servicioProg = programa.Dtos.SingleOrDefault().Servicio;
                string tipoProg = programa.Dtos.SingleOrDefault().Tipo;
                string ministerio = usuario.Dtos.SingleOrDefault().Ministerio;
                string servicio = usuario.Dtos.SingleOrDefault().Servicio;
                string correoUsuario = usuario.Dtos.SingleOrDefault().Email;
                FormulariosModels formulario = new FormulariosModels();
                string version = await formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaVersionProgramas")), idPrograma);
                string versionFinal = (String.IsNullOrEmpty(version) ? string.Empty : (int.Parse(version) >= 1 ? "versión " + version : string.Empty));
                dataMail.mensaje = string.Format(new CuerpoEmail().emailHtmlEvaluacion, string.Format(msj1, nombreProg, versionFinal, ministerioProg, servicioProg, tipoProg), string.Format(msj2, nombreUsuario, ministerio, servicio, correoUsuario));
                Boolean estadoUsuario = enviaMail(dataMail);
                if (!estadoUsuario)
                    throw new Exception("error envio mail asignacion de evaluador");
                //Registro fecha envio de mail de asignacion
                ViewDto<TablaRespuestasDto> borradoFechaEval = new ViewDto<TablaRespuestasDto>();
                borradoFechaEval = new ViewDto<TablaRespuestasDto>();
                borradoFechaEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto()
                {
                    IdFormulario = idPrograma,
                    IdPregunta = (numEvaluador == 1 ? decimal.Parse(constantes.GetValue("PreguntaFechaRegEval1")) : decimal.Parse(constantes.GetValue("PreguntaFechaRegEval2"))),
                    TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv"))
                }, EnumAccionRealizar.Eliminar);
                if (!borradoFechaEval.Sucess())
                    throw new Exception("error registro fecha asignacion de evaluador (borrado)");
                ViewDto<TablaRespuestasDto> registraFechaEval = new ViewDto<TablaRespuestasDto>();
                registraFechaEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto()
                {
                    IdFormulario = idPrograma,
                    IdPregunta = (numEvaluador == 1 ? decimal.Parse(constantes.GetValue("PreguntaFechaRegEval1")) : decimal.Parse(constantes.GetValue("PreguntaFechaRegEval2"))),
                    Respuesta = DateTime.Now,
                    TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal"))
                }, EnumAccionRealizar.Insertar);
                if (!registraFechaEval.Sucess())
                    throw new Exception("error registro fecha asignacion de evaluador (registro)");
                //Cambio de etapa a modulo de evaluacion
                ViewDto<TablaProgramasDto> actualizaEtapa = new ViewDto<TablaProgramasDto>();
                TablaProgramasDto datosProgAct = new TablaProgramasDto();
                datosProgAct.IdPrograma = idPrograma;
                datosProgAct.Estado.IdParametro = decimal.Parse(constantes.GetValue("Activo"));
                datosProgAct.Etapa.IdParametro = decimal.Parse(constantes.GetValue("EtapaEvaluacion"));
                actualizaEtapa = bips.RegistrarProgramas(new ContextoDto(), datosProgAct, EnumAccionRealizar.Actualizar);
                if (actualizaEtapa.HasError())
                    throw new Exception("Error al actualizar etapa (modulo evaluacion)");
                //Asigna permiso modulo evaluacion
                UsuariosModels usuarios = new UsuariosModels();
                string permisoModEval = string.Empty;
                TablaExcepcionesPermisosDto perModEval = new TablaExcepcionesPermisosDto() { IdFormulario = idPrograma, IdUsuario = idEvaluador, IdPermiso = decimal.Parse(constantes.GetValue("PermisoModuloEvaluacion")) };
                permisoModEval = await usuarios.eliminaPermisosUsuarios(new List<TablaExcepcionesPermisosDto>() { perModEval });
                if (permisoModEval != "ok")
                    throw new Exception("error eliminar permiso de evaluacion");
                permisoModEval = await usuarios.registraPermisosUsuarios(new List<TablaExcepcionesPermisosDto>() { new TablaExcepcionesPermisosDto() { IdFormulario = idPrograma, IdUsuario = idEvaluador, IdPermiso = decimal.Parse(constantes.GetValue("PermisoModuloEvaluacion")), Estado = decimal.Parse(constantes.GetValue("Activo")) } });
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

        public Task<string> guardaEvaluacion(PreguntaEvaluaciones data)
        {
            string registro = "ok";
            try {
                List<int> listaPregEvaluacion = new List<int>() {
                    int.Parse(constantes.GetValue("PreguntaCalificacion")),
                    int.Parse(constantes.GetValue("PreguntaComentSegpres")),
                    int.Parse(constantes.GetValue("PreguntaComentGeneral")),
                    int.Parse(constantes.GetValue("PreguntaAtingencia")),
                    int.Parse(constantes.GetValue("PreguntaCoherencia")),
                    int.Parse(constantes.GetValue("PreguntaConsistencia")),
                    int.Parse(constantes.GetValue("PreguntaAntecProg")),
                    int.Parse(constantes.GetValue("PreguntaDiagProg")),
                    int.Parse(constantes.GetValue("PreguntaObjProgPobl")),
                    int.Parse(constantes.GetValue("PreguntaEstragProg")),
                    int.Parse(constantes.GetValue("PreguntaIndic")),
                    int.Parse(constantes.GetValue("PreguntaGastos"))
                };
                ViewDto<TablaRespuestasDto> borradoEvaluacion = new ViewDto<TablaRespuestasDto>();
                foreach (var item in listaPregEvaluacion){
                    borradoEvaluacion = new ViewDto<TablaRespuestasDto>();
                    borradoEvaluacion = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto()
                    {
                        IdFormulario = data.idPrograma,
                        IdPregunta = item,
                        TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv"))
                    }, EnumAccionRealizar.Eliminar);
                    if (!borradoEvaluacion.Sucess())
                        throw new Exception("error borrado preguntas de evaluacion");
                }
                Dictionary<int, string> listaRespPregEval = new Dictionary<int, string>();
                listaRespPregEval.Add(int.Parse(constantes.GetValue("PreguntaCalificacion")), data.calificacion.ToString());
                listaRespPregEval.Add(int.Parse(constantes.GetValue("PreguntaComentSegpres")), data.comentSegpres);
                listaRespPregEval.Add(int.Parse(constantes.GetValue("PreguntaComentGeneral")), data.comentGeneral);
                listaRespPregEval.Add(int.Parse(constantes.GetValue("PreguntaAtingencia")), data.atingencia);
                listaRespPregEval.Add(int.Parse(constantes.GetValue("PreguntaCoherencia")), data.coherencia);
                listaRespPregEval.Add(int.Parse(constantes.GetValue("PreguntaConsistencia")), data.consistencia);
                listaRespPregEval.Add(int.Parse(constantes.GetValue("PreguntaAntecProg")), data.antecPrograma);
                listaRespPregEval.Add(int.Parse(constantes.GetValue("PreguntaDiagProg")), data.diagPrograma);
                listaRespPregEval.Add(int.Parse(constantes.GetValue("PreguntaObjProgPobl")), data.objPoblPrograma);
                listaRespPregEval.Add(int.Parse(constantes.GetValue("PreguntaEstragProg")), data.estrategiaPrograma);
                listaRespPregEval.Add(int.Parse(constantes.GetValue("PreguntaIndic")), data.indicadoresPrograma);
                listaRespPregEval.Add(int.Parse(constantes.GetValue("PreguntaGastos")), data.gastosPrograma);
                ViewDto<TablaRespuestasDto> registraPregEvaluador = new ViewDto<TablaRespuestasDto>();
                foreach (var item in listaRespPregEval){
                    if (!String.IsNullOrEmpty(item.Value) && item.Value != "-1"){
                        registraPregEvaluador = new ViewDto<TablaRespuestasDto>();
                        registraPregEvaluador = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto()
                        {
                            IdFormulario = data.idPrograma,
                            IdPregunta = item.Key,
                            Respuesta = item.Value,
                            TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal"))
                        }, EnumAccionRealizar.Insertar);
                        if (!registraPregEvaluador.Sucess())
                            throw new Exception("error registro preguntas de evaluacion");
                    }                    
                }
            }
            catch (Exception ex)
            {
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return Task.FromResult(registro);
        }

        public async Task<string> enviaEvaluacion(int idPrograma, string idUsuario)
        {
            string registro = "ok";
            try {
                //Usuario conectado
                ViewDto<TablaUsuariosDto> usuario = new ViewDto<TablaUsuariosDto>();
                usuario = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = idUsuario, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                if (usuario.HasElements())
                {
                    //Busco correos de envio
                    string mailEvaluacion = string.Empty;
                    ViewDto<TablaParametrosDto> casillaEvaluacion = new ViewDto<TablaParametrosDto>();
                    casillaEvaluacion = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("listaMailEnvioEval")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (casillaEvaluacion.HasElements()){
                        foreach (var item in casillaEvaluacion.Dtos){
                            if (item.IdParametro != item.IdCategoria){
                                mailEvaluacion += item.Descripcion + ",";
                            }
                        }
                    }
                    //Datos programa
                    ViewDto<TablaProgramasDto> programa = new ViewDto<TablaProgramasDto>();
                    programa = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idPrograma, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Buscar);
                    //Busco evaluadores
                    string eval1 = string.Empty, eval2 = string.Empty, mailEval1 = string.Empty, mailEval2 = string.Empty, mailEvaluadores = string.Empty;
                    ViewDto<TablaUsuariosDto> buscaUsuarioEval;
                    ViewDto<TablaRespuestasDto> buscaEvaluadores = new ViewDto<TablaRespuestasDto>();
                    buscaEvaluadores = bips.BuscarRespuestas(new ContextoDto(), new TablaRespuestasFiltroDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaEvaluador1")) });
                    if (buscaEvaluadores.HasElements()){
                        buscaUsuarioEval = new ViewDto<TablaUsuariosDto>();
                        buscaUsuarioEval = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = buscaEvaluadores.Dtos.SingleOrDefault().Respuesta.ToString(), IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                        if (buscaUsuarioEval.HasElements()){
                            eval1 = buscaUsuarioEval.Dtos.SingleOrDefault().Nombre;
                            mailEval1 = buscaUsuarioEval.Dtos.SingleOrDefault().Email;
                        }
                            
                    }
                    buscaEvaluadores = new ViewDto<TablaRespuestasDto>();
                    buscaEvaluadores = bips.BuscarRespuestas(new ContextoDto(), new TablaRespuestasFiltroDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaEvaluador2")) });
                    if (buscaEvaluadores.HasElements())
                    {
                        buscaUsuarioEval = new ViewDto<TablaUsuariosDto>();
                        buscaUsuarioEval = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = buscaEvaluadores.Dtos.SingleOrDefault().Respuesta.ToString(), IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                        if (buscaUsuarioEval.HasElements()){
                            eval2 = buscaUsuarioEval.Dtos.SingleOrDefault().Nombre;
                            mailEval2 = buscaUsuarioEval.Dtos.SingleOrDefault().Email;
                        }                            
                    }
                    mailEvaluadores = (String.IsNullOrEmpty(mailEval1) ? (String.IsNullOrEmpty(mailEval2) ? string.Empty : mailEval2) : (String.IsNullOrEmpty(mailEval2) ? mailEval1 : (mailEval1 + "," + mailEval2)));
                    //Busco link informe evaluacion ex-ante
                    string link = string.Empty;
                    ViewDto<TablaParametrosDto> buscaLinkInformeEval = new ViewDto<TablaParametrosDto>();
                    buscaLinkInformeEval = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdParametro = decimal.Parse(constantes.GetValue("linkInformeEvalExAnte")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (buscaLinkInformeEval.HasElements()){
                        var listaNuevos = getTipoFormulariosExAnte(int.Parse(constantes.GetValue("Nuevo")));
                        var listaReformulados = getTipoFormulariosExAnte(int.Parse(constantes.GetValue("Reformulado")));
                        string tipo = (listaNuevos.Result.Count(p => p == programa.Dtos.SingleOrDefault().IdTipoFormulario) > 0 ? "1" : (listaReformulados.Result.Count(p => p == programa.Dtos.SingleOrDefault().IdTipoFormulario) > 0 ? "2" : "0"));
                        link = buscaLinkInformeEval.Dtos.FirstOrDefault().Descripcion.Replace("{0}", tipo).Replace("{1}", idPrograma.ToString()).Replace("{2}", programa.Dtos.SingleOrDefault().Ano.ToString());
                    }                        
                    DatosEmail dataMail = new DatosEmail();
                    dataMail.de = usuario.Dtos.SingleOrDefault().Email;
                    dataMail.para = mailEvaluacion + mailEvaluadores;
                    dataMail.asunto = "Programa evaluado";
                    dataMail.imagen = true;
                    string msj1 = "Estimados/as:<br/> El programa <b>{0} {1}</b> ({2} - {3}) se encuentra evaluado por <b>{4}</b> y <b>{5}</b>. Por favor, dar el visto bueno para su envío. <br/><br/> Acceso al Informe de Evaluación: <a href =\"{6}\">Aquí</a>";
                    string msj2 = "Saluda Atentamente, <br/> Evaluador Ex-Ante <br/>{0}<br/>{1}<br/>{2}<br/>{3}";
                    string nombreUsuario = usuario.Dtos.SingleOrDefault().Nombre;
                    string nombreProg = programa.Dtos.SingleOrDefault().Nombre;
                    string ministerioProg = programa.Dtos.SingleOrDefault().Ministerio;
                    string servicioProg = programa.Dtos.SingleOrDefault().Servicio;
                    string tipoProg = programa.Dtos.SingleOrDefault().Tipo;
                    string ministerio = usuario.Dtos.SingleOrDefault().Ministerio;
                    string servicio = usuario.Dtos.SingleOrDefault().Servicio;
                    string correoUsuario = usuario.Dtos.SingleOrDefault().Email;
                    FormulariosModels formulario = new FormulariosModels();
                    string version = await formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaVersionProgramas")), idPrograma);
                    string versionFinal = (String.IsNullOrEmpty(version) ? string.Empty : (int.Parse(version) >= 1 ? "versión " + version : string.Empty));
                    dataMail.mensaje = string.Format(new CuerpoEmail().emailHtmlEvaluacion, string.Format(msj1, nombreProg, versionFinal, ministerioProg, servicioProg, eval1, eval2, link), string.Format(msj2, nombreUsuario, ministerio, servicio, correoUsuario));
                    Boolean estadoUsuario = enviaMail(dataMail);
                    if (!estadoUsuario)
                        throw new Exception("error envio mail envio de evaluacion");
                    //Registro fecha de envio informe evaluacion
                    ViewDto<TablaRespuestasDto> borradoFechaEval = new ViewDto<TablaRespuestasDto>();
                    borradoFechaEval = new ViewDto<TablaRespuestasDto>();
                    borradoFechaEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaFechaEnvioInforEval")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv"))}, EnumAccionRealizar.Eliminar);
                    if (!borradoFechaEval.Sucess())
                        throw new Exception("error registro fecha envio informe evaluacion (borrado)");
                    ViewDto<TablaRespuestasDto> registraFechaEval = new ViewDto<TablaRespuestasDto>();
                    registraFechaEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaFechaEnvioInforEval")), Respuesta = DateTime.Now, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                    if (!registraFechaEval.Sucess())
                        throw new Exception("error registro fecha envio informe evaluacion (registro)");
                }
            }
            catch(Exception ex) {
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return (registro);
        }

        public async Task<string> cierraEvaluacionExAnte(int idPrograma, string idUsuario, string rutaArchivos, HttpPostedFileBase pdf)
        {
            string registro = "ok";
            try {                
                //Guardo informes en pdf
                /*//Descarga archivo pdf desde url
                var fileUrl = "http://informes-monitoreo.ministeriodesarrollosocial.gob.cl/admin/fichas/2/0/1/" + idPrograma + "/2022/";
                var fileName = "PRG2022_2_" + idPrograma + ".pdf";
                using (var client = new WebClient())
                {
                    client.DownloadFile(fileUrl, Path.Combine(rutaArchivos, Path.GetFileName(fileName)));
                }*/
                string rutaArchivo = string.Empty;
                if (pdf.ContentLength > 0)
                {
                    string nombre = pdf.FileName.Split('.')[0];
                    rutaArchivo = Path.Combine(rutaArchivos, Path.GetFileName(nombre + ".pdf"));
                    if (File.Exists(rutaArchivo))
                        File.Delete(rutaArchivo);
                    pdf.SaveAs(rutaArchivo);
                }
                //Busco datos del programa
                FormulariosModels formulario = new FormulariosModels();
                ViewDto<TablaProgramasDto> programa = new ViewDto<TablaProgramasDto>();
                programa = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idPrograma, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Buscar);
                string mailEvaluacion = string.Empty;                
                //Busco coordinadores
                ViewDto<TablaParametrosDto> correoCoordinadores = new ViewDto<TablaParametrosDto>();
                correoCoordinadores = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("CoordinadoresMinisteriales")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                if (correoCoordinadores.HasElements())
                {
                    foreach (var item in correoCoordinadores.Dtos)
                    {
                        if (item.Valor2 == programa.Dtos.FirstOrDefault().IdMinisterio.IdParametro)
                            mailEvaluacion += item.Descripcion + ",";
                    }
                }
                //Busco contrapartes técnicas
                ViewDto<TablaUsuariosDto> contrapartes = new ViewDto<TablaUsuariosDto>();
                contrapartes = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { IdServicio = programa.Dtos.FirstOrDefault().IdServicio.IdParametro, IdPerfil = decimal.Parse(constantes.GetValue("PerfilContraparte")), IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                if (contrapartes.HasElements())
                {
                    foreach (var contraparte in contrapartes.Dtos)
                        mailEvaluacion += contraparte.Email + ",";
                }
                //Copias (CC)
                string mailCopias = string.Empty;
                //Busco sectorialista
                ViewDto<TablaParametrosDto> correoSectorialistas = new ViewDto<TablaParametrosDto>();
                correoSectorialistas = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("CorreoSectorialistas")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                if (correoSectorialistas.HasElements())
                {
                    foreach (var item in correoSectorialistas.Dtos)
                    {
                        if (item.Valor == programa.Dtos.FirstOrDefault().IdServicio.IdParametro)
                            mailCopias += item.Descripcion + ",";
                    }
                }
                //Busco otras copias
                List<TablaParametrosDto> otrasCopias = new List<TablaParametrosDto>();
                otrasCopias = await getJefaturasExAnte(int.Parse(constantes.GetValue("OtrasCopiasInforExAnte")));
                ViewDto<TablaUsuariosDto> copias = new ViewDto<TablaUsuariosDto>();
                if (otrasCopias.Count > 0)
                {
                    foreach (var copia in otrasCopias)
                    {
                        copias = new ViewDto<TablaUsuariosDto>();
                        copias = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = copia.Descripcion });
                        if (copias.HasElements())
                            mailCopias += copias.Dtos.SingleOrDefault().Email + ",";
                    }
                }
                //Busco copias ocultas
                string mailCopiasOcultas = string.Empty;
                ViewDto<TablaParametrosDto> correoCCO = new ViewDto<TablaParametrosDto>();
                correoCCO = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("CorreosOcultos")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                if (correoCCO.HasElements())
                {
                    foreach (var item in correoCCO.Dtos.Where(p=>p.IdParametro != p.IdCategoria).OrderBy(p=>p.Orden))
                        mailCopiasOcultas += item.Descripcion + ",";
                }
                //Busco evaluación
                string msj = string.Empty;
                string evaluacion = await formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaCalificacion")), idPrograma);
                if (!String.IsNullOrEmpty(evaluacion))
                {
                    ViewDto<TablaParametrosDto> buscaDescMail = new ViewDto<TablaParametrosDto>();
                    buscaDescMail = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("EmailCierreExAnte")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (evaluacion == "1063") //OT
                    {                        
                        if (buscaDescMail.HasElements()){
                            msj = buscaDescMail.Dtos.FirstOrDefault(p=>p.Valor==1).Descripcion;
                        }
                    }
                    else if (evaluacion == "1064") //RF
                    {
                        if (buscaDescMail.HasElements())
                        {
                            msj = buscaDescMail.Dtos.FirstOrDefault(p => p.Valor == 2).Descripcion;
                        }
                    }
                    else if (evaluacion == "1062")
                    {
                        if (buscaDescMail.HasElements())
                        {
                            msj = buscaDescMail.Dtos.FirstOrDefault(p => p.Valor == 3).Descripcion;
                        }
                    }
                }
                else
                {
                    throw new Exception("Programa sin evaluación");
                }
                //Envio mail a coordinadores y contrapartres tecnicas desde casilla Ex-Ante
                DatosEmail dataMail = new DatosEmail();
                dataMail.de = constantes.GetValue("EmailExAnte");
                dataMail.para = mailEvaluacion.Substring(0, mailEvaluacion.Length - 1);
                dataMail.cc = mailCopias.Substring(0, mailCopias.Length - 1);
                dataMail.cco = mailCopiasOcultas.Substring(0, mailCopiasOcultas.Length - 1);
                dataMail.asunto = "Informe de Recomendación";
                dataMail.imagen = true;
                string msj1 = msj;
                string msj2 = "Saludos cordiales, <br/>Evaluación Ex Ante<br/>División de Políticas Sociales<br/>Subsecretaría de Evaluación Social<br/>Ministerio de Desarrollo Social y Familia<br/>Correo: evaluacionexante@desarrollosocial.cl";
                string nombreProg = programa.Dtos.SingleOrDefault().Nombre;
                string ministerioProg = programa.Dtos.SingleOrDefault().Ministerio;
                string servicioProg = programa.Dtos.SingleOrDefault().Servicio;         
                string version = await formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaVersionProgramas")), idPrograma);
                string versionFinal = (String.IsNullOrEmpty(version) ? string.Empty : (int.Parse(version) >= 1 ? "versión " + version : string.Empty));
                dataMail.mensaje = string.Format(new CuerpoEmail().emailHtmlEvaluacion, string.Format(msj1, nombreProg, versionFinal, ministerioProg, servicioProg), msj2);
                dataMail.adjunto = rutaArchivo;
                Boolean estadoUsuario = enviaMail(dataMail);
                if (!estadoUsuario)
                    throw new Exception("error envio mail asignacion de evaluador");
                //Cierre de programa
                string cierrePrograma = await cierraEvaluacion(idPrograma, idUsuario);
                if (cierrePrograma != "ok")
                    throw new Exception("error al cerrar programa (Ex-Ante revision informes)");
            }
            catch (Exception ex)
            {
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return (registro);
        }

        public async Task<string> envioComentariosSectorialista(int idPrograma, string idUsuario, string rutaArchivos, HttpPostedFileBase pdf, string idEvaluador1, string idEvaluador2, string comentarios)
        {
            string registro = "ok";
            try
            {
                //Guardo informes en pdf
                /*//Descarga archivo pdf desde url
                var fileUrl = "http://informes-monitoreo.ministeriodesarrollosocial.gob.cl/admin/fichas/2/0/1/" + idPrograma + "/2022/";
                var fileName = "PRG2022_2_" + idPrograma + ".pdf";
                using (var client = new WebClient())
                {
                    client.DownloadFile(fileUrl, Path.Combine(rutaArchivos, Path.GetFileName(fileName)));
                }*/
                string rutaArchivo = string.Empty;                
                //Busco datos del programa
                FormulariosModels formulario = new FormulariosModels();
                ViewDto<TablaProgramasDto> programa = new ViewDto<TablaProgramasDto>();
                programa = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idPrograma, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Buscar);
                string mailJefaturas = constantes.GetValue("EmailExAnte") + ",";
                ViewDto<TablaUsuariosDto> evaluador = new ViewDto<TablaUsuariosDto>();
                if (!String.IsNullOrEmpty(idEvaluador1) || !String.IsNullOrEmpty(idEvaluador2))
                {
                    if (idEvaluador1 != "-1")
                    {
                        evaluador = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = idEvaluador1 });
                        if (evaluador.HasElements())
                            mailJefaturas += evaluador.Dtos.SingleOrDefault().Email + ",";
                    }
                    if (idEvaluador2 != "-1")
                    {
                        evaluador = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = idEvaluador2 });
                        if (evaluador.HasElements())
                            mailJefaturas += evaluador.Dtos.SingleOrDefault().Email + ",";
                    }
                }
                //Guardo informes en pdf
                if (pdf != null)
                {
                    if (pdf.ContentLength > 0)
                    {
                        string nombre = pdf.FileName.Split('.')[0];//new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
                        rutaArchivo = Path.Combine(rutaArchivos, Path.GetFileName(nombre + ".pdf"));
                        if (File.Exists(rutaArchivo))
                            File.Delete(rutaArchivo);
                        pdf.SaveAs(rutaArchivo);
                        //borrado
                        ViewDto<TablaRespuestasDto> borrado = new ViewDto<TablaRespuestasDto>();
                        borrado = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = programa.Dtos.SingleOrDefault().IdPrograma, IdPregunta = int.Parse(constantes.GetValue("NombreArchivoPDFSect")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                        if (!borrado.Sucess())
                            throw new Exception("error guardado (borrado)");
                        //guardado
                        ViewDto<TablaRespuestasDto> guardado = new ViewDto<TablaRespuestasDto>();
                        guardado = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = programa.Dtos.SingleOrDefault().IdPrograma, IdPregunta = int.Parse(constantes.GetValue("NombreArchivoPDFSect")), Respuesta = nombre, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                        if (!guardado.Sucess())
                            throw new Exception("error guardado");
                    }
                }
                //Busca datos usuario conectado
                ViewDto<TablaUsuariosDto> usuarioConectado = new ViewDto<TablaUsuariosDto>();
                usuarioConectado = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = idUsuario });
                //Se define mensaje a enviar según si tiene o no comentarios
                string msj = string.Empty;
                if (comentarios == "Si")
                    msj = "Estimados/as Evaluadores:<br/><br/> Se informa que la evaluación del programa <b>{0} {1}</b> ({2} - {3}) fue revisada por el/la sectorialista y se encuentra <b>con observaciones</b>. Se recuerda que tienen un <b>plazo de 1 día hábil para incorporar las observaciones y enviar</b> el programa a jefaturas.";
                else
                    msj = "Estimados/as Evaluadores:<br/><br/> Se informa que la evaluación del programa <b>{0} {1}</b> ({2} - {3}) fue revisada por el/la sectorialista y se encuentra <b>sin observaciones</b>. Se recuerda que tienen un <b>plazo de 1 día hábil para enviar</b> el programa a jefaturas.";
                //Envio de mail
                DatosEmail dataMail = new DatosEmail();
                dataMail.de = usuarioConectado.Dtos.SingleOrDefault().Email;
                dataMail.para = mailJefaturas.Substring(0, mailJefaturas.Length - 1) + "," + usuarioConectado.Dtos.SingleOrDefault().Email;
                dataMail.asunto = "Revisión de informe de evaluación Ex-Ante (sectorialista)";
                dataMail.imagen = true;
                string msj1 = msj;
                string msj2 = "Saluda Atentamente, <br/>{0}<br/>{1}<br/>{2}<br/>{3}";
                string nombreProg = programa.Dtos.SingleOrDefault().Nombre;
                string ministerioProg = programa.Dtos.SingleOrDefault().Ministerio;
                string servicioProg = programa.Dtos.SingleOrDefault().Servicio;
                string tipoProg = programa.Dtos.SingleOrDefault().Tipo;
                string nombreUsuario = usuarioConectado.Dtos.SingleOrDefault().Nombre;
                string ministerio = usuarioConectado.Dtos.SingleOrDefault().Ministerio;
                string servicio = usuarioConectado.Dtos.SingleOrDefault().Servicio;
                string correoUsuario = usuarioConectado.Dtos.SingleOrDefault().Email;
                string version = await formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaVersionProgramas")), idPrograma);
                string versionFinal = (String.IsNullOrEmpty(version) ? string.Empty : (int.Parse(version) >= 1 ? "versión " + version : string.Empty));
                dataMail.mensaje = string.Format(new CuerpoEmail().emailHtmlEvaluacion, string.Format(msj1, nombreProg, versionFinal, ministerioProg, servicioProg), string.Format(msj2, nombreUsuario, ministerio, servicio, correoUsuario));
                dataMail.adjunto = rutaArchivo;
                Boolean estadoUsuario = enviaMail(dataMail);
                if (!estadoUsuario)
                    throw new Exception("error envio mail de revision sectorialista");
                //Registro fecha de envio informe evaluacion
                ViewDto<TablaRespuestasDto> borradoFechaEval = new ViewDto<TablaRespuestasDto>();
                borradoFechaEval = new ViewDto<TablaRespuestasDto>();
                borradoFechaEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PregFecEnvioComentSect")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                if (!borradoFechaEval.Sucess())
                    throw new Exception("error registro fecha envio informe evaluacion sectorialista (borrado)");
                ViewDto<TablaRespuestasDto> registraFechaEval = new ViewDto<TablaRespuestasDto>();
                registraFechaEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PregFecEnvioComentSect")), Respuesta = DateTime.Now, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                if (!registraFechaEval.Sucess())
                    throw new Exception("error registro fecha envio informe evaluacion sectorialista (registro)");
                //Cambio a etapa de corrección dupla sectorialista
                ViewDto<TablaRespuestasDto> borradoEtapaSect = new ViewDto<TablaRespuestasDto>();
                borradoEtapaSect = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("EtapaCorrecDupla")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                if (!borradoEtapaSect.Sucess())
                    throw new Exception("error registro etapa envio sectorialista (borrado)");
                ViewDto<TablaRespuestasDto> registroEtapaSect = new ViewDto<TablaRespuestasDto>();
                registroEtapaSect = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("EtapaCorrecDupla")), Respuesta = DateTime.Now, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                if (!registroEtapaSect.Sucess())
                    throw new Exception("error registro etapa envio sectorialista (registro)");
                //Devuelvo permisos de evaluacion
                FormulariosModels formularioModels = new FormulariosModels();
                UsuariosModels usuarios = new UsuariosModels();
                string evaluador1 = await formularioModels.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaEvaluador1")), idPrograma);
                string evaluador2 = await formularioModels.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaEvaluador2")), idPrograma);
                List<TablaUsuariosDto> evalUsuario1 = new List<TablaUsuariosDto>();
                List<TablaUsuariosDto> evalUsuario2 = new List<TablaUsuariosDto>();
                if (!String.IsNullOrEmpty(evaluador1))
                {
                    evalUsuario1 = await usuarios.getUsuariosFiltro(new TablaUsuariosFiltroDto() { Id = evaluador1 });
                    if (evalUsuario1.Count > 0)
                    {                        
                        ViewDto<TablaExcepcionesPermisosDto> quitaPermisos = new ViewDto<TablaExcepcionesPermisosDto>();
                        quitaPermisos = bips.RegistrarExcepcionesFormularios(new ContextoDto(), new TablaExcepcionesPermisosDto() { IdFormulario = idPrograma, IdUsuario = evalUsuario1.FirstOrDefault().Id, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Eliminar);
                        if (quitaPermisos.HasError())
                            throw new Exception("Error al devolver permiso evaluacion (etapa en revision sectorialista)");
                    }
                }
                if (!String.IsNullOrEmpty(evaluador2))
                {
                    evalUsuario2 = await usuarios.getUsuariosFiltro(new TablaUsuariosFiltroDto() { Id = evaluador2 });
                    if (evalUsuario2.Count > 0)
                    {                        
                        ViewDto<TablaExcepcionesPermisosDto> quitaPermisos = new ViewDto<TablaExcepcionesPermisosDto>();
                        quitaPermisos = bips.RegistrarExcepcionesFormularios(new ContextoDto(), new TablaExcepcionesPermisosDto() { IdFormulario = idPrograma, IdUsuario = evalUsuario2.FirstOrDefault().Id, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Eliminar);
                        if (quitaPermisos.HasError())
                            throw new Exception("Error al devolver permiso evaluacion (etapa en revision sectorialista)");
                    }
                }
            }
            catch (Exception ex)
            {
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return (registro);
        }

        public async Task<string> envioComentariosJefSect(int idPrograma, string idUsuario, string rutaArchivos, HttpPostedFileBase pdf, string idEvaluador1, string idEvaluador2, int tipoJefatura, string comentarios)
        {
            string registro = "ok";
            try
            {                
                //Busco datos del programa
                FormulariosModels formulario = new FormulariosModels();
                ViewDto<TablaProgramasDto> programa = new ViewDto<TablaProgramasDto>();
                programa = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idPrograma, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Buscar);
                //Busca datos usuario conectado
                ViewDto<TablaUsuariosDto> usuarioConectado = new ViewDto<TablaUsuariosDto>();
                usuarioConectado = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = idUsuario });
                //Busco listado de jefaturas
                string mailJefaturas = string.Empty;
                List<TablaParametrosDto> jefes = new List<TablaParametrosDto>();
                ViewDto<TablaUsuariosDto> jefaturas = new ViewDto<TablaUsuariosDto>();
                jefes = await getJefaturasExAnte(int.Parse(constantes.GetValue("RevisionExAnte")));
                if (jefes.Count > 0)
                {
                    foreach (var jefe in jefes)
                    {
                        jefaturas = new ViewDto<TablaUsuariosDto>();
                        jefaturas = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = jefe.Descripcion });
                        if (jefaturas.HasElements())
                            if (jefe.Descripcion != usuarioConectado.Dtos.SingleOrDefault().Id)
                                mailJefaturas += jefaturas.Dtos.SingleOrDefault().Email + ",";
                    }                                       
                }                                
                //Busco evaluadores
                ViewDto<TablaUsuariosDto> evaluador = new ViewDto<TablaUsuariosDto>();
                if (!String.IsNullOrEmpty(idEvaluador1) || !String.IsNullOrEmpty(idEvaluador2))
                {
                    if (idEvaluador1 != "-1")
                    {
                        evaluador = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = idEvaluador1 });
                        if (evaluador.HasElements())
                            mailJefaturas += evaluador.Dtos.SingleOrDefault().Email + ",";
                    }
                    if (idEvaluador2 != "-1")
                    {
                        evaluador = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = idEvaluador2 });
                        if (evaluador.HasElements())
                            mailJefaturas += evaluador.Dtos.SingleOrDefault().Email + ",";
                    }
                }
                //Busco otras copias
                List<TablaParametrosDto> otrasCopias = new List<TablaParametrosDto>();
                otrasCopias = await getJefaturasExAnte(int.Parse(constantes.GetValue("OtrasCopiasComentExAnte")));
                if (otrasCopias.Count > 0)
                    foreach (var copia in otrasCopias.Where(p=>p.IdParametro != p.IdCategoria))
                        mailJefaturas += copia.Descripcion + ",";
                //Guardo informes en pdf
                string rutaArchivo = string.Empty;
                if (pdf != null)
                {
                    if (pdf.ContentLength > 0)
                    {
                        string nombre = pdf.FileName.Split('.')[0];
                        rutaArchivo = Path.Combine(rutaArchivos, Path.GetFileName(nombre + ".pdf"));
                        if (File.Exists(rutaArchivo))
                            File.Delete(rutaArchivo);
                        pdf.SaveAs(rutaArchivo);
                    }
                }               
                //Se define mensaje a enviar según si tiene o no comentarios
                string msj = string.Empty;
                if (!String.IsNullOrEmpty(comentarios) || pdf != null)
                    msj = "Estimados/as Evaluadores:<br/><br/> Se informa que la evaluación del programa <b>{0} {1}</b> ({2} - {3}) fue revisada por la Jefatura de {4} y se encuentra <b>con observaciones</b>. Se recuerda que <b>tienen un plazo de 1 día hábil para incorporar las observaciones y enviar</b> el programa nuevamente a jefaturas.<br/><br/>Comentarios adicionales Jefatura:<br/><br/>{5}";
                else
                    msj = "Estimados/as Evaluadores:<br/><br/> Se informa que la evaluación del programa <b>{0} {1}</b> ({2} - {3}) fue revisada por la Jefatura de {4} y se encuentra <b>sin observaciones.</b>{5}";                
                //Envio de mail
                DatosEmail dataMail = new DatosEmail();
                dataMail.de = usuarioConectado.Dtos.SingleOrDefault().Email;
                dataMail.para = mailJefaturas.Substring(0, mailJefaturas.Length - 1);
                dataMail.asunto = "Revisión de informe de evaluación Ex-Ante " + (tipoJefatura == 1 ? "(jefatura Monitoreo)" : "(jefatura Estudios)");
                dataMail.imagen = true;
                string msj1 = msj;
                string msj2 = "Saluda Atentamente, <br/>{0}<br/>{1}<br/>{2}<br/>{3}";
                string nombreProg = programa.Dtos.SingleOrDefault().Nombre;
                string ministerioProg = programa.Dtos.SingleOrDefault().Ministerio;
                string servicioProg = programa.Dtos.SingleOrDefault().Servicio;
                string tipoProg = programa.Dtos.SingleOrDefault().Tipo;
                string nombreUsuario = usuarioConectado.Dtos.SingleOrDefault().Nombre;
                string ministerio = usuarioConectado.Dtos.SingleOrDefault().Ministerio;
                string servicio = usuarioConectado.Dtos.SingleOrDefault().Servicio;
                string correoUsuario = usuarioConectado.Dtos.SingleOrDefault().Email;
                string version = await formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaVersionProgramas")), idPrograma);
                string versionFinal = (String.IsNullOrEmpty(version) ? string.Empty : (int.Parse(version) >= 1 ? "versión " + version : string.Empty));
                dataMail.mensaje = string.Format(new CuerpoEmail().emailHtmlEvaluacion, string.Format(msj1, nombreProg, versionFinal, ministerioProg, servicioProg, (tipoJefatura == 1 ? "Monitoreo" : "Estudios"), comentarios), string.Format(msj2, nombreUsuario, ministerio, servicio, correoUsuario));
                dataMail.adjunto = rutaArchivo;
                Boolean estadoUsuario = enviaMail(dataMail);
                if (!estadoUsuario)
                    throw new Exception("error envio mail de revision sectorialista");
                //Registro fecha de envio informe evaluacion
                ViewDto<TablaRespuestasDto> registraFechaEval = new ViewDto<TablaRespuestasDto>();
                ViewDto<TablaRespuestasDto> buscarFechasAnteriores = new ViewDto<TablaRespuestasDto>();
                buscarFechasAnteriores = bips.BuscarRespuestas(new ContextoDto(), new TablaRespuestasFiltroDto() { IdFormulario = idPrograma, IdPregunta = (tipoJefatura == 1 ? decimal.Parse(constantes.GetValue("PregFecEnvioComentJefSect1")) : decimal.Parse(constantes.GetValue("PregFecEnvioComentJefSect2"))) });
                int countFechasAnteriores = (buscarFechasAnteriores.HasElements() ? buscarFechasAnteriores.Dtos.Count() + 1 : 1);
                registraFechaEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdTab = countFechasAnteriores, IdPregunta = (tipoJefatura == 1 ? decimal.Parse(constantes.GetValue("PregFecEnvioComentJefSect1")) : decimal.Parse(constantes.GetValue("PregFecEnvioComentJefSect2"))), Respuesta = DateTime.Now, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                if (!registraFechaEval.Sucess())
                    throw new Exception("error registro fecha envio informe evaluacion (registro)");                
                if (!String.IsNullOrEmpty(comentarios) || pdf != null)
                {
                    //Asigna permiso modulo evaluacion
                    UsuariosModels usuarios = new UsuariosModels();
                    string permisoModEval = string.Empty;
                    //Evaluador 1
                    if (idEvaluador1 != "-1")
                    {
                        TablaExcepcionesPermisosDto perModEval = new TablaExcepcionesPermisosDto() { IdFormulario = idPrograma, IdUsuario = idEvaluador1, IdPermiso = decimal.Parse(constantes.GetValue("PermisoModuloEvaluacion")) };
                        permisoModEval = await usuarios.eliminaPermisosUsuarios(new List<TablaExcepcionesPermisosDto>() { perModEval });
                        if (permisoModEval != "ok")
                            throw new Exception("error eliminar permiso de evaluacion (evaluador 1)");
                        permisoModEval = await usuarios.registraPermisosUsuarios(new List<TablaExcepcionesPermisosDto>() { new TablaExcepcionesPermisosDto() { IdFormulario = idPrograma, IdUsuario = idEvaluador1, IdPermiso = decimal.Parse(constantes.GetValue("PermisoModuloEvaluacion")), Estado = decimal.Parse(constantes.GetValue("Activo")) } });
                        if (permisoModEval != "ok")
                            throw new Exception("error crear permiso de evaluacion (evaluador 1)");
                    }
                    //Evaluador 2
                    if (idEvaluador2 != "-1")
                    {
                        TablaExcepcionesPermisosDto perModEval = new TablaExcepcionesPermisosDto() { IdFormulario = idPrograma, IdUsuario = idEvaluador2, IdPermiso = decimal.Parse(constantes.GetValue("PermisoModuloEvaluacion")) };
                        permisoModEval = await usuarios.eliminaPermisosUsuarios(new List<TablaExcepcionesPermisosDto>() { perModEval });
                        if (permisoModEval != "ok")
                            throw new Exception("error eliminar permiso de evaluacion (evaluador 2)");
                        permisoModEval = await usuarios.registraPermisosUsuarios(new List<TablaExcepcionesPermisosDto>() { new TablaExcepcionesPermisosDto() { IdFormulario = idPrograma, IdUsuario = idEvaluador2, IdPermiso = decimal.Parse(constantes.GetValue("PermisoModuloEvaluacion")), Estado = decimal.Parse(constantes.GetValue("Activo")) } });
                        if (permisoModEval != "ok")
                            throw new Exception("error crear permiso de evaluacion (evaluador 2)");
                    }
                }
            }
            catch (Exception ex)
            {
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return (registro);
        }

        public Task<string> cierraEvaluacion(int idPrograma, string idUsuario)
        {
            string registro = "ok";
            try {                
                //Cambio a etapa a cierre de evaluación
                ViewDto<TablaProgramasDto> actualizaEtapa = new ViewDto<TablaProgramasDto>();
                TablaProgramasDto datosProgAct = new TablaProgramasDto();
                datosProgAct.IdPrograma = idPrograma;
                datosProgAct.Estado.IdParametro = decimal.Parse(constantes.GetValue("Activo"));
                datosProgAct.Etapa.IdParametro = decimal.Parse(constantes.GetValue("EtapaCierreEvalExAnte"));
                actualizaEtapa = bips.RegistrarProgramas(new ContextoDto(), datosProgAct, EnumAccionRealizar.Actualizar);
                if (actualizaEtapa.HasError())
                    throw new Exception("Error al actualizar etapa (cierre evaluacion)");
                //Registro usuario de cierre de evaluacion
                ViewDto<TablaRespuestasDto> borradoUsuarioCierreEval = new ViewDto<TablaRespuestasDto>();
                borradoUsuarioCierreEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaUsuarioCierreEval")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                if (!borradoUsuarioCierreEval.Sucess())
                    throw new Exception("error registro usuario que cerro evaluacion (borrado)");
                ViewDto<TablaRespuestasDto> registraUsuarioCierreEval = new ViewDto<TablaRespuestasDto>();
                registraUsuarioCierreEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaUsuarioCierreEval")), Respuesta = idUsuario, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                if (!registraUsuarioCierreEval.Sucess())
                    throw new Exception("error registro usuario que cerro evaluacion (registro)");
                //Registro fecha de cierre de evaluacion
                ViewDto<TablaRespuestasDto> borradoFechaCierreEval = new ViewDto<TablaRespuestasDto>();
                borradoFechaCierreEval = new ViewDto<TablaRespuestasDto>();
                borradoFechaCierreEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaFechaCierreEval")), TipoDelete = decimal.Parse(constantes.GetValue("TipoDeleteFormIndiv")) }, EnumAccionRealizar.Eliminar);
                if (!borradoFechaCierreEval.Sucess())
                    throw new Exception("error registro fecha de cierre evaluacion (borrado)");
                ViewDto<TablaRespuestasDto> registraFechaCierreEval = new ViewDto<TablaRespuestasDto>();
                registraFechaCierreEval = bips.RegistrarRespuestas(new ContextoDto(), new TablaRespuestasDto() { IdFormulario = idPrograma, IdPregunta = decimal.Parse(constantes.GetValue("PreguntaFechaCierreEval")), Respuesta = DateTime.Now, TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal")) }, EnumAccionRealizar.Insertar);
                if (!registraFechaCierreEval.Sucess())
                    throw new Exception("error registro fecha de cierre evaluacion (registro)");                               
            }
            catch(Exception ex){
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return Task.FromResult(registro);
        }

        public Task<Nullable<Decimal>> getPerfilEvaluador(string idUsuario)
        {
            Nullable<Decimal> data = null;
            try {
                //Busco perfil adicional usuario
                ViewDto<TablaParametrosUsuariosDto> perfilUsuario = new ViewDto<TablaParametrosUsuariosDto>();
                perfilUsuario = bips.BuscarParametrosUsuarios(new ContextoDto(), new TablaParametrosUsuariosFiltroDto()
                {
                    IdUsuario = idUsuario,
                    Ano = DateTime.Now.Year
                });
                //Busca perfil en tabla parametro
                if (perfilUsuario.HasElements()) {
                    ViewDto<TablaParametrosDto> buscarPerfilParametro = new ViewDto<TablaParametrosDto>();
                    buscarPerfilParametro = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto()
                    {
                        IdParametro = perfilUsuario.Dtos.FirstOrDefault().IdParametro,
                        Estado = decimal.Parse(constantes.GetValue("Activo"))
                    });
                    if (buscarPerfilParametro.HasElements())
                        data = buscarPerfilParametro.Dtos.FirstOrDefault().IdParametro;
                }
            }
            catch(Exception ex) {
                log.Error(ex.Message, ex);
            }
            return Task.FromResult(data);
        }

        public Task<Nullable<Decimal>> getPerfilUsuario(string idUsuario)
        {
            Nullable<Decimal> perfilUsuario = null;
            try {
                ViewDto<TablaUsuariosDto> usuario = new ViewDto<TablaUsuariosDto>();
                usuario = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() {
                    Id = idUsuario,
                    IdEstado = decimal.Parse(constantes.GetValue("Activo"))
                });
                if (usuario.HasElements())
                    perfilUsuario = usuario.Dtos.SingleOrDefault().IdPerfil;
            }
            catch(Exception ex) {
                log.Error(ex.Message, ex);
            }
            return Task.FromResult(perfilUsuario);
        }

        public Task<string> nuevaIteracion(int idPrograma, int version, string idUsuario)
        {
            string registro = "ok";
            try {
                //Crea nueva iteracion
                ViewDto<TablaProgramasDto> creaIteracion = new ViewDto<TablaProgramasDto>();
                creaIteracion = bips.RegistrarProgramas(new ContextoDto(), new TablaProgramasDto() { IdPrograma = idPrograma, IdBips = version }, EnumAccionRealizar.EliminarUserGrupo);
                if (creaIteracion.Sucess()){
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
            }
            catch(Exception ex) {
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return Task.FromResult(registro);
        }

        public Task<List<Nullable<Decimal>>> getTipoFormulariosExAnte(int tipoFormularios)
        {
            List<Nullable<Decimal>> lista = new List<decimal?>();
            try {
                ViewDto<TablaParametrosDto> buscar = new ViewDto<TablaParametrosDto>();
                buscar = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() {
                    IdCategoria = decimal.Parse(constantes.GetValue("TiposFormularios")),
                    Valor = tipoFormularios
                });
                if (buscar.HasElements()){
                    foreach(var item in buscar.Dtos){
                        if (item.IdParametro != decimal.Parse(constantes.GetValue("TiposFormularios")))
                            lista.Add(item.IdParametro);
                    }
                }
            }
            catch(Exception ex) {
                log.Error(ex.Message, ex);
            }
            return Task.FromResult(lista);
        }

        public Task<string> getRespuestasExAnte(int idPregunta, int idFormulario, decimal? idTab = null)
        {
            string dato = null;
            try
            {
                ViewDto<TablaRespuestasDto> respuesta = new ViewDto<TablaRespuestasDto>();
                respuesta = bips.BuscarRespuestas(new ContextoDto(), new TablaRespuestasFiltroDto() { IdFormulario = idFormulario, IdPregunta = idPregunta, IdTab = idTab });
                if (respuesta.HasElements())
                    dato = respuesta.Dtos.FirstOrDefault().Respuesta.ToString();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            return Task.FromResult(dato);
        }

        public Task<TablaProgramasDto> getIndicadoresDashboard(string idUsuario, int idPrograma, string tipo)
        {
            TablaProgramasDto data = new TablaProgramasDto();
            try {
                ViewDto<TablaProgramasDto> buscaIndicadores = new ViewDto<TablaProgramasDto>();
                buscaIndicadores = bips.BuscarProgramas(new ContextoDto() { Login = tipo }, new TablaProgramasFiltroDto() { Ano = 2024, TipoFormulario = 360, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.BuscarIndicDashboard);
                if (buscaIndicadores.HasElements())
                    data = buscaIndicadores.Dtos.FirstOrDefault();
            } catch (Exception ex) {
                log.Error(ex.Message, ex);
            }
            return Task.FromResult(data);
        }

        public Task<IList<TablaProgramasDto>> getIndicTipoProgramas(string idUsuario, int idPrograma, string tipo)
        {
            IList<TablaProgramasDto> data = new List<TablaProgramasDto>();
            try
            {
                ViewDto<TablaProgramasDto> buscaIndicadores = new ViewDto<TablaProgramasDto>();
                buscaIndicadores = bips.BuscarProgramas(new ContextoDto() { Login = tipo }, new TablaProgramasFiltroDto() { Ano = 2024, TipoFormulario = 360, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.BuscarIndicDashboard);
                if (buscaIndicadores.HasElements())
                    data = buscaIndicadores.Dtos;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            return Task.FromResult(data);
        }

        public Task<IList<TablaProgramasDto>> getListadoProgramas(int ano, int tipoFormulario)
        {
            IList<TablaProgramasDto> data = new List<TablaProgramasDto>();
            try {
                ViewDto<TablaProgramasDto> buscaProgramas = new ViewDto<TablaProgramasDto>();
                buscaProgramas = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto { Ano = ano, TipoFormulario = tipoFormulario, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Buscar);
                if (buscaProgramas.HasElements())
                    data = buscaProgramas.Dtos;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            return Task.FromResult(data);
        }

        public Task<IList<TablaProgramasDto>> getProgramasXIteracion(int ano, int tipoFormulario)
        {
            IList<TablaProgramasDto> data = new List<TablaProgramasDto>();
            try
            {
                ViewDto<TablaProgramasDto> buscaProgramas = new ViewDto<TablaProgramasDto>();
                buscaProgramas = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto { Ano = ano, TipoFormulario = tipoFormulario }, EnumAccionRealizar.BuscarProgramasXIteracion);
                if (buscaProgramas.HasElements())
                    data = buscaProgramas.Dtos;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            return Task.FromResult(data);
        }
    }
}