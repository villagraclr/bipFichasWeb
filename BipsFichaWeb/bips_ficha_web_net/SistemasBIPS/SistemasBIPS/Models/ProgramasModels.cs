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
using System.Reflection;
using System.Threading.Tasks;

namespace SistemasBIPS.Models
{
    public class ProgramasModels
    {
        private static ISistemasBIPSSvc bips = null;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private IProviderConstante constantes = null;

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public ProgramasModels()
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
                data = bips.BuscarFormulariosUsuarios(new ContextoDto(), filtros); //bips.BuscarProgramas(new ContextoDto(), filtros).Dtos;
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
        public async Task<List<TablaProgramasDto>> getProgramasFiltro(string filtroAnos, string filtroMinisterios, string filtroFormularios, string idUsuario)
        {
            List<TablaProgramasDto> objProgramas = new List<TablaProgramasDto>();
            try
            {
                IList<TablaProgramasDto> objPrograma = new List<TablaProgramasDto>();
                if (!string.IsNullOrEmpty(filtroAnos) || !string.IsNullOrEmpty(filtroMinisterios))
                {
                    Dictionary<String, Object> anosLista = !string.IsNullOrEmpty(filtroAnos) ? JsonConvert.DeserializeObject<Dictionary<String, Object>>(filtroAnos) : new Dictionary<String, Object>();
                    Dictionary<String, Object> ministeriosLista = !string.IsNullOrEmpty(filtroMinisterios) ? JsonConvert.DeserializeObject<Dictionary<String, Object>>(filtroMinisterios) : new Dictionary<String, Object>();
                    Dictionary<String, Object> formulariosLista = !string.IsNullOrEmpty(filtroFormularios) ? JsonConvert.DeserializeObject<Dictionary<String, Object>>(filtroFormularios) : new Dictionary<String, Object>();
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
                                                //Filtro formularios, ministerios, años
                                                if (formulariosLista.Count > 0)
                                                {
                                                    JsonTextReader formulario = new JsonTextReader(new StringReader(formulariosLista.Values.First().ToString()));
                                                    while (formulario.Read())
                                                    {
                                                        if (formulario.Value != null)
                                                        {
                                                            objPrograma = await getProgramasFiltro(new TablaProgramasFiltroDto()
                                                            {
                                                                Ano = ano.Value.ToString() == "0" ? (decimal?)null : decimal.Parse(ano.Value.ToString()),
                                                                IdServicio = ministerio.Value.ToString() == "0" ? (decimal?)null : decimal.Parse(ministerio.Value.ToString()),
                                                                IdUser = idUsuario,
                                                                IdPlataforma = decimal.Parse(constantes.GetValue("PlataformaCargaBIPS")),
                                                                TipoGrupo = formulario.Value.ToString() == "0" ? (decimal?)null : (formulario.Value.ToString() == "1" ? decimal.Parse(constantes.GetValue("FormulariosPropios")) : decimal.Parse(formulario.Value.ToString())),
                                                                Estado = decimal.Parse(constantes.GetValue("Activo"))
                                                            });
                                                            if (objPrograma != null)
                                                                objProgramas.AddRange(objPrograma);
                                                            if (formulario.Value.ToString() == "0")
                                                                break;
                                                        }
                                                    }
                                                }else{ //Fitro ministerios, años
                                                    objPrograma = await getProgramasFiltro(new TablaProgramasFiltroDto()
                                                    {
                                                        Ano = ano.Value.ToString() == "0" ? (decimal?)null : decimal.Parse(ano.Value.ToString()),
                                                        IdServicio = ministerio.Value.ToString() == "0" ? (decimal?)null : decimal.Parse(ministerio.Value.ToString()),
                                                        IdUser = idUsuario,
                                                        IdPlataforma = decimal.Parse(constantes.GetValue("PlataformaCargaBIPS")),
                                                        Estado = decimal.Parse(constantes.GetValue("Activo"))
                                                    });
                                                    if (objPrograma != null)
                                                        objProgramas.AddRange(objPrograma);                                                    
                                                }
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
                                }else{ //Filtro formularios, años
                                    if (formulariosLista.Count > 0)
                                    {
                                        JsonTextReader formulario = new JsonTextReader(new StringReader(formulariosLista.Values.First().ToString()));
                                        while (formulario.Read())
                                        {
                                            if (formulario.Value != null)
                                            {
                                                objPrograma = await getProgramasFiltro(new TablaProgramasFiltroDto()
                                                {
                                                    Ano = ano.Value.ToString() == "0" ? (decimal?)null : decimal.Parse(ano.Value.ToString()),
                                                    IdUser = idUsuario,
                                                    IdPlataforma = decimal.Parse(constantes.GetValue("PlataformaCargaBIPS")),
                                                    TipoGrupo = formulario.Value.ToString() == "0" ? (decimal?)null : (formulario.Value.ToString() == "1" ? decimal.Parse(constantes.GetValue("FormulariosPropios")) : decimal.Parse(formulario.Value.ToString())),
                                                    Estado = decimal.Parse(constantes.GetValue("Activo"))
                                                });
                                                if (objPrograma != null)
                                                    objProgramas.AddRange(objPrograma);
                                                if (formulario.Value.ToString() == "0")
                                                    break;
                                            }
                                        }
                                    }else{ //Filtro años
                                        objPrograma = await getProgramasFiltro(new TablaProgramasFiltroDto()
                                        {
                                            Ano = ano.Value.ToString() == "0" ? (decimal?)null : decimal.Parse(ano.Value.ToString()),
                                            IdUser = idUsuario,
                                            IdPlataforma = decimal.Parse(constantes.GetValue("PlataformaCargaBIPS")),
                                            Estado = decimal.Parse(constantes.GetValue("Activo"))
                                        });
                                        if (objPrograma != null)
                                            objProgramas.AddRange(objPrograma);
                                    }                                    
                                }
                                if (ano.Value.ToString() == "0")
                                    break;
                            }
                        }
                    }else{ //Filtro ministerios, formularios
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
                                        if (formulariosLista.Count > 0)
                                        {
                                            JsonTextReader formulario = new JsonTextReader(new StringReader(formulariosLista.Values.First().ToString()));
                                            while (formulario.Read())
                                            {
                                                if (formulario.Value != null)
                                                {
                                                    objPrograma = await getProgramasFiltro(new TablaProgramasFiltroDto()
                                                    {
                                                        IdServicio = decimal.Parse(ministerio.Value.ToString()) == 0 ? (decimal?)null : decimal.Parse(ministerio.Value.ToString()),
                                                        IdUser = idUsuario,
                                                        IdPlataforma = decimal.Parse(constantes.GetValue("PlataformaCargaBIPS")),
                                                        TipoGrupo = formulario.Value.ToString() == "0" ? (decimal?)null : (formulario.Value.ToString() == "1" ? decimal.Parse(constantes.GetValue("FormulariosPropios")) : decimal.Parse(formulario.Value.ToString())),
                                                        Estado = decimal.Parse(constantes.GetValue("Activo"))
                                                    });
                                                    if (objPrograma != null)
                                                        objProgramas.AddRange(objPrograma);
                                                    if (formulario.Value.ToString() == "0")
                                                        break;
                                                }
                                            }
                                        }else{ //Filtro ministerios
                                            objPrograma = await getProgramasFiltro(new TablaProgramasFiltroDto()
                                            {
                                                IdServicio = decimal.Parse(ministerio.Value.ToString()) == 0 ? (decimal?)null : decimal.Parse(ministerio.Value.ToString()),
                                                IdUser = idUsuario,
                                                IdPlataforma = decimal.Parse(constantes.GetValue("PlataformaCargaBIPS"))
                                            });
                                            if (objPrograma != null)
                                                objProgramas.AddRange(objPrograma);
                                        }
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
                        }else{ //Filtro formularios
                            if (formulariosLista.Count > 0)
                            {
                                JsonTextReader formulario = new JsonTextReader(new StringReader(formulariosLista.Values.First().ToString()));
                                while (formulario.Read())
                                {
                                    if (formulario.Value != null)
                                    {
                                        objPrograma = await getProgramasFiltro(new TablaProgramasFiltroDto()
                                        {
                                            IdUser = idUsuario,
                                            IdPlataforma = decimal.Parse(constantes.GetValue("PlataformaCargaBIPS")),
                                            TipoGrupo = formulario.Value.ToString() == "0" ? (decimal?)null : (formulario.Value.ToString() == "1" ? decimal.Parse(constantes.GetValue("FormulariosPropios")) : decimal.Parse(formulario.Value.ToString())),
                                            Estado = decimal.Parse(constantes.GetValue("Activo"))
                                        });
                                        if (objPrograma != null)
                                            objProgramas.AddRange(objPrograma);
                                        if (formulario.Value.ToString() == "0")
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
                if (objProgramas.Count > 0)
                    objProgramas = objProgramas.DistinctBy(p=>p.IdPrograma).ToList();
                if (objProgramas.Count > 0)
                    objProgramas.RemoveAll(p => p.TipoGeneral == decimal.Parse(constantes.GetValue("TipoPerfilGore")) || p.TipoGeneral == decimal.Parse(constantes.GetValue("TipoProgramaGore")));
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
        public async Task<List<TablaProgramasDto>> getProgramasFiltro(string idUsuario)
        {
            List<TablaProgramasDto> objProgramas = new List<TablaProgramasDto>();
            try
            {
                IList<TablaProgramasDto> objPrograma = new List<TablaProgramasDto>();
                IList<int> anos = new List<int>();
                anos = await getAnos();
                if (anos.Count > 0)
                {
                    objPrograma = await getProgramasFiltro(new TablaProgramasFiltroDto() { Ano = anos.Max(), IdUser = idUsuario, IdPlataforma = decimal.Parse(constantes.GetValue("PlataformaCargaBIPS")), Estado = decimal.Parse(constantes.GetValue("Activo")), TipoGrupo = decimal.Parse(constantes.GetValue("FormulariosPropios")) });
                    if (objPrograma != null){
                        if (objPrograma.Count > 0)
                            objProgramas.AddRange(objPrograma);

                        if (objProgramas.Count > 0)
                            objProgramas = objProgramas.DistinctBy(p => p.IdPrograma).ToList();
                    }                                            
                }
                if (objProgramas.Count > 0)
                    objProgramas.RemoveAll(p => p.TipoGeneral == decimal.Parse(constantes.GetValue("TipoPerfilGore")) || p.TipoGeneral == decimal.Parse(constantes.GetValue("TipoProgramaGore")));
            }
            catch(Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return objProgramas;
        }

        /// <summary>
        /// Obtiene lista de ministerios y sus respectivos servicios
        /// </summary>
        /// <returns></returns>
        public Task<IList<MinisterioServicios>> getListaMinistServ()
        {
            IList<MinisterioServicios> listaMinistServ = new List<MinisterioServicios>();
            ViewDto<TablaParametrosDto> ministerios = new ViewDto<TablaParametrosDto>();
            ViewDto<TablaParametrosDto> servicios = new ViewDto<TablaParametrosDto>();
            try {
                ministerios = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("Ministerios")) });
                servicios = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("Servicios")) });
                if (ministerios.HasElements())
                {                    
                    ministerios.Dtos.Where(p => p.IdParametro != p.IdCategoria && p.Estado == decimal.Parse(constantes.GetValue("Activo"))).OrderBy(p => p.Orden).ToList().ForEach(p =>
                    {
                        listaMinistServ.Add(new MinisterioServicios() {
                            Ministerios = p,
                            Servicios = servicios.HasElements() ? servicios.Dtos.Where(i => i.Valor == p.IdParametro && i.Estado == decimal.Parse(constantes.GetValue("Activo"))).OrderBy(i => i.Orden).ToList() : new List<TablaParametrosDto>()
                        });                                         
                    });
                }
            }catch (Exception ex){
                log.Error(ex.Message, ex);                
                throw ex;
            }
            return Task.FromResult(listaMinistServ);
        }        

        /// <summary>
        /// Obtiene lista de años según programas existentes en la BD
        /// </summary>
        /// <returns></returns>
        public Task<IList<int>> getAnos()
        {
            IList<int> listaAnos = new List<int>();
            ViewDto<TablaProgramasDto> anos = new ViewDto<TablaProgramasDto>();
            try {
                anos = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto(), EnumAccionRealizar.BuscarAnos);
                if (anos.HasElements())
                {
                    anos.Dtos.ForEach(p => {
                        listaAnos.Add(int.Parse(p.Ano.ToString()));
                    });
                }
            }catch(Exception ex){
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(listaAnos);
        }

        /// <summary>
        /// Obtiene lista de formularios en uso según usuario logeado
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Task<IList<TablaLogFormulariosDto>> getFormulariosTomados(String usuario)
        {
            IList<TablaLogFormulariosDto> data = new List<TablaLogFormulariosDto>();
            try{
                ViewDto<TablaLogFormulariosDto> buscar = new ViewDto<TablaLogFormulariosDto>();
                buscar = bips.BuscarLogFormularios(new ContextoDto(), new TablaLogFormulariosFiltroDto() { IdUsuario = usuario, TipoAcceso = decimal.Parse(constantes.GetValue("AccesoFormGuardado")), EstadoAcceso = decimal.Parse(constantes.GetValue("Activo")) });
                if (buscar.HasElements())
                    data = buscar.Dtos.ToList();
            }catch (Exception ex){
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(data);
        }

        /// <summary>
        /// Obtiene lista de todos los formularios existentes en la BD según filtros
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public Task<List<TablaProgramasDto>> getTodosProgramasFiltro(TablaProgramasFiltroDto filtros)
        {
            List<TablaProgramasDto> programas = new List<TablaProgramasDto>();
            try
            {
                ViewDto<TablaProgramasDto> formularios = new ViewDto<TablaProgramasDto>();
                formularios = bips.BuscarProgramas(new ContextoDto(), filtros);
                if (formularios.HasElements())
                    programas = formularios.Dtos; //bips.BuscarProgramas(new ContextoDto(), filtros).Dtos;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(programas);
        }

        public Task<List<TablaParametrosDto>> getListaRutasFichas()
        {
            List<TablaParametrosDto> rutas = new List<TablaParametrosDto>();
            try
            {
                ViewDto<TablaParametrosDto> rutasPDF = new ViewDto<TablaParametrosDto>();
                rutasPDF = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("RutasAccesoPDF")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                if (rutasPDF.HasElements())
                    rutas = rutasPDF.Dtos.Where(p=>p.IdParametro!=p.IdCategoria).ToList();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(rutas);
        }

        public Task<List<TablaParametrosDto>> getTiposProgramas()
        {
            List<TablaParametrosDto> tipos = new List<TablaParametrosDto>();
            try {
                ViewDto<TablaParametrosDto> tiposProgramas = new ViewDto<TablaParametrosDto>();
                tiposProgramas = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("TiposFormularios")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                if (tiposProgramas.HasElements())
                    tipos = tiposProgramas.Dtos.Where(p => p.IdParametro != p.IdCategoria).ToList();
            }
            catch(Exception ex) {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(tipos);
        }

        public Task<List<TablaParametrosDto>> getAnosFichas()
        {
            List<TablaParametrosDto> anos = new List<TablaParametrosDto>();
            try
            {
                ViewDto<TablaParametrosDto> anosFichas = new ViewDto<TablaParametrosDto>();
                anosFichas = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("AnosFichas")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                if (anosFichas.HasElements())
                    anos = anosFichas.Dtos.Where(p => p.IdParametro != p.IdCategoria).ToList();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(anos);
        }

        public async Task<bool> getPermisoLibera(string idUsuario)
        {
            bool tienePermiso = false;
            try {
                FormulariosModels formulario = new FormulariosModels();
                ViewDto<TablaParametrosDto> permisosLiberaProg = new ViewDto<TablaParametrosDto>();
                permisosLiberaProg = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("PermisoLiberaProg")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                TablaUsuariosDto usuario = await formulario.getDatosUsuario(idUsuario);
                if (permisosLiberaProg.HasElements())
                    if (permisosLiberaProg.Dtos.Count(p => p.Valor == usuario.IdPerfil) > 0)
                        tienePermiso = true;
            }
            catch(Exception ex){
                log.Error(ex.Message, ex);
                throw ex;
            }
            return (tienePermiso);
        }

        public async Task<Boolean> getPermisoInformeEval(string idUsuario)
        {
            Boolean acceso = false;
            try
            {
                FormulariosModels formularios = new FormulariosModels();
                TablaUsuariosDto usuario = await formularios.getDatosUsuario(idUsuario);
                ViewDto<TablaParametrosDto> data = new ViewDto<TablaParametrosDto>();
                data = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("InformeEvaluacion")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                if (data.HasElements())
                    if (data.Dtos.Count(p => p.Valor == usuario.IdPerfil) > 0)
                        acceso = true;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return (acceso);
        }        
    }
}
