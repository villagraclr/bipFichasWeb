using log4net;
using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Core.Providers;
using MDS.Dto;
using MDS.Svc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SistemasBIPS.Models
{
    public class FormulariosMantenedorModels
    {
        private static ISistemasBIPSSvc bips = null;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private IProviderConstante constantes = null;

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public FormulariosMantenedorModels()
        {
            bips = (ISistemasBIPSSvc)Activator.CreateInstance(typeof(SistemasBIPSSvc));
            constantes = (IProviderConstante)Activator.CreateInstance(typeof(ProviderConstante));
        }
        #endregion

        public async Task<List<TablaProgramasDto>> getFormularios(TablaProgramasFiltroDto filtros)
        {
            List<TablaProgramasDto> data = new List<TablaProgramasDto>();
            try
            {
                ProgramasModels programas = new ProgramasModels();                
                if (filtros.IdGrupoFormulario != null){
                    UsuariosModels usuarios = new UsuariosModels();
                    List<TablaFormulariosGruposDto> objFormulariosGrupos = new List<TablaFormulariosGruposDto>();
                    objFormulariosGrupos = await usuarios.getFormulariosGrupos(new TablaFormulariosGruposFiltroDto() {
                        IdGrupoFormulario = filtros.IdGrupoFormulario,                        
                        Estado = decimal.Parse(constantes.GetValue("Activo"))
                    });
                    if (objFormulariosGrupos.Count > 0){
                        //data = objFormulariosGrupos.RemoveAll(p => p.TipoFormulario != filtros.TipoFormulario);
                        objFormulariosGrupos.RemoveAll(p=>p.IdTipoFormulario != filtros.TipoFormulario);
                        if (filtros.Ano != null && objFormulariosGrupos.Count > 0)
                            objFormulariosGrupos.RemoveAll(p => p.Ano != filtros.Ano);
                        if (filtros.IdMinisterio != null && objFormulariosGrupos.Count > 0)
                            objFormulariosGrupos.RemoveAll(p => p.IdMinisterio != filtros.IdMinisterio);
                        if (filtros.IdServicio != null && objFormulariosGrupos.Count > 0)
                            objFormulariosGrupos.RemoveAll(p => p.IdServicio != filtros.IdServicio);

                        if (objFormulariosGrupos.Count > 0){
                            foreach(var f in objFormulariosGrupos){
                                data.Add(new TablaProgramasDto() {
                                    IdPrograma = f.IdFormulario,
                                    IdBips = f.IdBips,
                                    Nombre = f.Nombre,
                                    Tipo = f.TipoFormulario,
                                    Ministerio = f.Ministerio,
                                    Servicio = f.Servicio,
                                    Ano = f.Ano
                                });
                            }
                        }
                    }
                }else{
                    data = await programas.getTodosProgramasFiltro(filtros);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return data;
        }

        public Task<List<TablaGruposFormulariosDto>> getGruposFiltros(TablaGruposFormulariosFiltroDto filtros)
        {
            List<TablaGruposFormulariosDto> data = new List<TablaGruposFormulariosDto>();
            try
            {
                ViewDto<TablaGruposFormulariosDto> buscaData = new ViewDto<TablaGruposFormulariosDto>();
                buscaData = bips.BuscarGruposUsuarios(new ContextoDto(), filtros);
                if (buscaData.HasElements())
                {
                    data = buscaData.Dtos.GroupBy(p=>p.IdGrupoFormulario).Select(p=>p.First()).OrderBy(p=>p.Nombre).ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(data);
        }

        public Task<List<TablaGruposFormulariosDto>> getGruposFormularios(TablaGruposFormulariosFiltroDto filtros)
        {
            List<TablaGruposFormulariosDto> data = new List<TablaGruposFormulariosDto>();
            try
            {
                ViewDto<TablaGruposFormulariosDto> buscaData = new ViewDto<TablaGruposFormulariosDto>();
                buscaData = bips.BuscarGruposFormularios(new ContextoDto(), filtros);
                if (buscaData.HasElements())
                    data = buscaData.Dtos.GroupBy(p => p.IdGrupoFormulario).Select(p => p.First()).OrderBy(p => p.Nombre).ToList();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(data);
        }

        public Task<List<TablaPlantillasTraspasoDto>> getPlantillasTraspaso(TablaPlantillasTraspasoFiltroDto filtros)
        {
            List<TablaPlantillasTraspasoDto> data = new List<TablaPlantillasTraspasoDto>();
            try{
                ViewDto<TablaPlantillasTraspasoDto> buscaData = new ViewDto<TablaPlantillasTraspasoDto>();
                buscaData = bips.BuscarPlantillasTraspaso(new ContextoDto(), filtros);
                if (buscaData.HasElements())
                    data = buscaData.Dtos;
            }
            catch(Exception ex){
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(data);
        }

        public async Task<string> registraFormularios(TablaProgramasDto data)
        {
            string registro = "ok";
            try
            {
                ViewDto<TablaProgramasDto> reg = new ViewDto<TablaProgramasDto>();
                data.IdMinisterio.IdParametro = decimal.Parse(data.Ministerio);
                data.IdServicio.IdParametro = decimal.Parse(data.Servicio);                
                data.Estado.IdParametro = decimal.Parse(constantes.GetValue("Activo"));
                reg = bips.RegistrarProgramas(new ContextoDto(), data, EnumAccionRealizar.Insertar);
                if (reg.Sucess()){
                    if (reg.Dtos.SingleOrDefault().IdPrograma > 0){
                        ViewDto<TablaRespuestasDto> regResp = new ViewDto<TablaRespuestasDto>();
                        regResp = bips.RegistrarRespuestas(new ContextoDto(),
                            new TablaRespuestasDto() {
                                IdPregunta = decimal.Parse(constantes.GetValue("PreguntaNombre")),
                                IdFormulario = reg.Dtos.SingleOrDefault().IdPrograma,
                                Respuesta = data.Nombre,
                                TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal"))
                            }, EnumAccionRealizar.Insertar);
                        if (regResp.HasError())
                            throw new Exception("Error al registrar respuestas (nombre)");
                        //Respuesta ministerio
                        regResp = new ViewDto<TablaRespuestasDto>();
                        regResp = bips.RegistrarRespuestas(new ContextoDto(),
                            new TablaRespuestasDto()
                            {
                                IdPregunta = decimal.Parse(constantes.GetValue("PreguntaMinisterio")),
                                IdFormulario = reg.Dtos.SingleOrDefault().IdPrograma,
                                Respuesta = data.IdMinisterio.IdParametro,
                                TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal"))
                            }, EnumAccionRealizar.Insertar);
                        if (regResp.HasError())
                            throw new Exception("Error al registrar respuestas (ministerio)");
                        //Respuesta servicio
                        regResp = new ViewDto<TablaRespuestasDto>();
                        regResp = bips.RegistrarRespuestas(new ContextoDto(),
                            new TablaRespuestasDto()
                            {
                                IdPregunta = decimal.Parse(constantes.GetValue("PreguntaServicio")),
                                IdFormulario = reg.Dtos.SingleOrDefault().IdPrograma,
                                Respuesta = data.IdServicio.IdParametro,
                                TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal"))
                            }, EnumAccionRealizar.Insertar);
                        if (regResp.HasError())
                            throw new Exception("Error al registrar respuestas (servicio)");
                        //Respuesta version
                        FormulariosModels programasEvaluar = new FormulariosModels();
                        IList<Nullable<Decimal>> listaProgramasEvaluar = new List<Nullable<Decimal>>();
                        listaProgramasEvaluar = await programasEvaluar.getProgramasEvaluar();                        
                        if (listaProgramasEvaluar.Count(p=>p.Value== data.IdTipoFormulario) > 0){
                            regResp = new ViewDto<TablaRespuestasDto>();
                            regResp = bips.RegistrarRespuestas(new ContextoDto(),
                                new TablaRespuestasDto()
                                {
                                    IdPregunta = decimal.Parse(constantes.GetValue("PreguntaVersionProgramas")),
                                    IdFormulario = reg.Dtos.SingleOrDefault().IdPrograma,
                                    Respuesta = int.Parse(constantes.GetValue("VersionInicialProgramas")),
                                    TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal"))
                                }, EnumAccionRealizar.Insertar);
                            if (regResp.HasError())
                                throw new Exception("Error al registrar respuestas (version)");
                        }
                        //Registra grupo seleccionado
                        ViewDto<TablaFormulariosGruposDto> regGrupo = new ViewDto<TablaFormulariosGruposDto>();
                        regGrupo = bips.RegistrarFormulariosGrupos(new ContextoDto(), new TablaFormulariosGruposDto() {
                            IdGrupoFormulario = data.IdGrupoFormulario,
                            IdFormulario = reg.Dtos.SingleOrDefault().IdPrograma,
                            Estado = decimal.Parse(constantes.GetValue("Activo"))
                        }, EnumAccionRealizar.Insertar);
                        if (regGrupo.HasError())
                            throw new Exception("Error al registrar formulario en un grupo");
                        //Registra grupo todos por defecto
                        regGrupo = new ViewDto<TablaFormulariosGruposDto>();
                        regGrupo = bips.RegistrarFormulariosGrupos(new ContextoDto(), new TablaFormulariosGruposDto()
                        {
                            IdGrupoFormulario = decimal.Parse(constantes.GetValue("GrupoTodos")),
                            IdFormulario = reg.Dtos.SingleOrDefault().IdPrograma,
                            Estado = decimal.Parse(constantes.GetValue("Activo"))
                        }, EnumAccionRealizar.Insertar);
                        if (regGrupo.HasError())
                            throw new Exception("Error al registrar formulario en el grupo todos");
                        //Registra relacion de formulario
                        ViewDto<TablaRelacionFormulariosDto> reg2 = new ViewDto<TablaRelacionFormulariosDto>();
                        TablaRelacionFormulariosDto data2 = new TablaRelacionFormulariosDto()
                        {
                            IdFormulario = data.IdPrograma,
                            IdBips = data.IdPrograma,
                            IdFormularioAnterior = data.IdPrograma,
                            TipoRelacionFormulario = decimal.Parse(constantes.GetValue("FormCreacionNuevo"))
                        };
                        reg2 = bips.RegistrarRelacionFormularios(new ContextoDto(), data2, EnumAccionRealizar.Insertar);
                        if (reg2.HasError())
                            registro = reg2.Error.Detalle;
                    }else{
                        registro = "Error al crear formulario";
                    }
                }
                else if (reg.HasError()) {
                    registro = reg.Error.Detalle;
                }
            }
            catch (Exception ex)
            {
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return (registro);
        }

        public async Task<string> registraFormularios(List<TablaProgramasDto> data)
        {
            string registro = "ok";
            try {
                if (data.Count > 0){
                    ViewDto<TablaProgramasDto> buscaForm;
                    ViewDto<TablaProgramasDto> reg;
                    ViewDto<TablaPlantillasTraspasoDto> buscaPlantilla;
                    ViewDto<TablaProgramasDto> regResp;
                    ViewDto<TablaFormulariosGruposDto> regGrupo;
                    ViewDto<TablaRelacionFormulariosDto> regRelForm;
                    foreach (var item in data){
                        buscaPlantilla = new ViewDto<TablaPlantillasTraspasoDto>();
                        buscaPlantilla = bips.BuscarPlantillasTraspaso(new ContextoDto(), new TablaPlantillasTraspasoFiltroDto() {
                            IdPlantillaTraspaso = item.IdTipoFormulario,
                            Estado = decimal.Parse(constantes.GetValue("Activo"))
                        });
                        if (buscaPlantilla.HasElements()){
                            buscaForm = new ViewDto<TablaProgramasDto>();
                            buscaForm = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto()
                            {
                                IdPrograma = item.IdPrograma,
                                Estado = decimal.Parse(constantes.GetValue("Activo"))
                            }, EnumAccionRealizar.Buscar);
                            if (buscaForm.HasElements())
                            {
                                if (buscaForm.Dtos.SingleOrDefault().IdTipoFormulario == buscaPlantilla.Dtos.SingleOrDefault().TipoFormularioOrigen){
                                    reg = new ViewDto<TablaProgramasDto>();
                                    item.IdMinisterio.IdParametro = buscaForm.Dtos.SingleOrDefault().IdMinisterio.IdParametro;
                                    item.IdServicio.IdParametro = buscaForm.Dtos.SingleOrDefault().IdServicio.IdParametro;
                                    item.IdBips = buscaForm.Dtos.SingleOrDefault().IdBips;
                                    item.IdTipoFormulario = buscaPlantilla.Dtos.SingleOrDefault().TipoFormularioDestino;
                                    item.Nombre = buscaForm.Dtos.SingleOrDefault().Nombre;
                                    item.Estado.IdParametro = decimal.Parse(constantes.GetValue("Activo"));
                                    reg = bips.RegistrarProgramas(new ContextoDto(), item, EnumAccionRealizar.Insertar);
                                    if (reg.HasError())
                                        throw new Exception("Error al crear nuevo formulario (origen-destino)");
                                    //Respuestas origen
                                    regResp = new ViewDto<TablaProgramasDto>();
                                    regResp = bips.RegistrarProgramas(new ContextoDto(), new TablaProgramasDto() {
                                        IdTipoFormulario = buscaPlantilla.Dtos.SingleOrDefault().IdPlantillaTraspaso,
                                        IdBips = buscaForm.Dtos.SingleOrDefault().IdPrograma,
                                        IdPrograma = reg.Dtos.SingleOrDefault().IdPrograma
                                    }, EnumAccionRealizar.Ninguna);
                                    if (regResp.HasError())
                                        throw new Exception("Error al crear nuevas respuestas");
                                    //Registra grupo seleccionado
                                    if (item.IdGrupoFormulario != null){
                                        regGrupo = new ViewDto<TablaFormulariosGruposDto>();
                                        regGrupo = bips.RegistrarFormulariosGrupos(new ContextoDto(), new TablaFormulariosGruposDto()
                                        {
                                            IdGrupoFormulario = item.IdGrupoFormulario,
                                            IdFormulario = reg.Dtos.SingleOrDefault().IdPrograma,
                                            Estado = decimal.Parse(constantes.GetValue("Activo"))
                                        }, EnumAccionRealizar.Insertar);
                                        if (regGrupo.HasError())
                                            throw new Exception("Error al registrar grupo");
                                    }
                                    //Registra grupo de origen del formulario origen                                    
                                    ViewDto<TablaFormulariosGruposDto> buscaGruposOrig = new ViewDto<TablaFormulariosGruposDto>();
                                    buscaGruposOrig = bips.BuscarFormulariosGrupos(new ContextoDto(), new TablaFormulariosGruposFiltroDto()
                                    {
                                        IdFormulario = reg.Dtos.SingleOrDefault().IdPrograma,
                                        Estado = decimal.Parse(constantes.GetValue("Activo"))
                                    });
                                    if (buscaGruposOrig.HasElements()){
                                        foreach(var grupOrig in buscaGruposOrig.Dtos){
                                            if (grupOrig.IdGrupoFormulario != decimal.Parse(constantes.GetValue("GrupoTodos"))){
                                                regGrupo = new ViewDto<TablaFormulariosGruposDto>();
                                                regGrupo = bips.RegistrarFormulariosGrupos(new ContextoDto(), new TablaFormulariosGruposDto()
                                                {
                                                    IdGrupoFormulario = grupOrig.IdGrupoFormulario,
                                                    IdFormulario = reg.Dtos.SingleOrDefault().IdPrograma,
                                                    Estado = decimal.Parse(constantes.GetValue("Activo"))
                                                }, EnumAccionRealizar.Insertar);
                                                if (regGrupo.HasError())
                                                    throw new Exception("Error al registrar grupo origen");
                                            }                                            
                                        }
                                    }
                                    //Registra grupo todos por defecto
                                    regGrupo = new ViewDto<TablaFormulariosGruposDto>();
                                    regGrupo = bips.RegistrarFormulariosGrupos(new ContextoDto(), new TablaFormulariosGruposDto()
                                    {
                                        IdGrupoFormulario = decimal.Parse(constantes.GetValue("GrupoTodos")),
                                        IdFormulario = reg.Dtos.SingleOrDefault().IdPrograma,
                                        Estado = decimal.Parse(constantes.GetValue("Activo"))
                                    }, EnumAccionRealizar.Insertar);
                                    if (regGrupo.HasError())
                                        throw new Exception("Error al registrar grupo por defecto");
                                    //Registra relacion de formulario
                                    regRelForm = new ViewDto<TablaRelacionFormulariosDto>();
                                    regRelForm = bips.RegistrarRelacionFormularios(new ContextoDto(), new TablaRelacionFormulariosDto() {
                                        IdFormulario = reg.Dtos.SingleOrDefault().IdPrograma,
                                        IdBips = buscaForm.Dtos.SingleOrDefault().IdBips,
                                        IdFormularioAnterior = item.IdPrograma,
                                        TipoRelacionFormulario = decimal.Parse(constantes.GetValue("FormCreacionExistente"))
                                    }, EnumAccionRealizar.Insertar);
                                    if (regRelForm.HasError())
                                        throw new Exception("Error al crear la relacion del formulario");
                                    //Respuesta version
                                    FormulariosModels programasEvaluar = new FormulariosModels();
                                    IList<Nullable<Decimal>> listaProgramasEvaluar = new List<Nullable<Decimal>>();
                                    listaProgramasEvaluar = await programasEvaluar.getProgramasEvaluar();
                                    if (listaProgramasEvaluar.Count(p => p.Value == item.IdTipoFormulario) > 0)
                                    {
                                        ViewDto<TablaRespuestasDto> regRespVersion = new ViewDto<TablaRespuestasDto>();
                                        regRespVersion = bips.RegistrarRespuestas(new ContextoDto(),
                                            new TablaRespuestasDto()
                                            {
                                                IdPregunta = decimal.Parse(constantes.GetValue("PreguntaVersionProgramas")),
                                                IdFormulario = reg.Dtos.SingleOrDefault().IdPrograma,
                                                Respuesta = int.Parse(constantes.GetValue("VersionInicialProgramas")),
                                                TipoInsert = decimal.Parse(constantes.GetValue("TipoInsertFormNormal"))
                                            }, EnumAccionRealizar.Insertar);
                                        if (regRespVersion.HasError())
                                            throw new Exception("Error al registrar respuestas (version)");
                                    }
                                }
                                else{
                                    throw new Exception("Tipo de formulario origen no es igual al tipo de formulario destino");
                                }
                            }else{
                                throw new Exception("Formulario origen no encontrado");
                            }
                        }else{
                            throw new Exception("Plantilla de traspaso no encontrada");
                        }                
                    }
                }else{
                    registro = "Cero registros enviados";
                }
            } catch(Exception ex) {
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return (registro);
        }

        public Task<string> registraFormularios(List<TablaProgramasDto> data1, TablaProgramasDto data2)
        {
            string registro = "ok";
            try {
                if (data1.Count > 0){
                    data2.IdMinisterio.IdParametro = decimal.Parse(data2.Ministerio);
                    data2.IdServicio.IdParametro = decimal.Parse(data2.Servicio);
                    ViewDto<TablaProgramasDto> buscaForm = new ViewDto<TablaProgramasDto>();
                    ViewDto<TablaPlantillasTraspasoDto> buscaPlantilla = new ViewDto<TablaPlantillasTraspasoDto>();
                    decimal? idOrigen = data2.IdPlataforma;
                    decimal? idGrupo = data2.IdGrupoFormulario;
                    if (idOrigen > 0){                        
                        buscaForm = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto()
                        {
                            IdPrograma = data2.IdPlataforma,
                            Estado = decimal.Parse(constantes.GetValue("Activo"))
                        }, EnumAccionRealizar.Buscar);
                        if (buscaForm.HasElements()) {                            
                            buscaPlantilla = bips.BuscarPlantillasTraspaso(new ContextoDto(), new TablaPlantillasTraspasoFiltroDto()
                            {
                                IdPlantillaTraspaso = data2.IdExcepcion,
                                Estado = decimal.Parse(constantes.GetValue("Activo"))
                            });
                            data2.IdMinisterio.IdParametro = buscaForm.Dtos.SingleOrDefault().IdMinisterio.IdParametro;
                            data2.IdServicio.IdParametro = buscaForm.Dtos.SingleOrDefault().IdServicio.IdParametro;
                            data2.IdBips = buscaForm.Dtos.SingleOrDefault().IdBips;
                            data2.Nombre = buscaForm.Dtos.SingleOrDefault().Nombre;
                            if (buscaPlantilla.HasElements())
                                data2.IdTipoFormulario = buscaPlantilla.Dtos.SingleOrDefault().TipoFormularioDestino;
                        }
                        else{
                            throw new Exception("Formulario origen no encontrado");
                        }                            
                    }
                    ViewDto<TablaProgramasDto> regForm = new ViewDto<TablaProgramasDto>();                    
                    data2.Estado.IdParametro = decimal.Parse(constantes.GetValue("Activo"));
                    regForm = bips.RegistrarProgramas(new ContextoDto(), data2, EnumAccionRealizar.Insertar);
                    if (regForm.HasError())
                        throw new Exception("Error al crear nuevo formulario (reestructuracion)");
                    //Respuestas origen (si se selecciono origen)
                    if (idOrigen > 0){
                        if (buscaForm.Dtos.SingleOrDefault().IdTipoFormulario == buscaPlantilla.Dtos.SingleOrDefault().TipoFormularioOrigen) {
                            ViewDto<TablaProgramasDto> regResp = new ViewDto<TablaProgramasDto>();
                            regResp = bips.RegistrarProgramas(new ContextoDto(), new TablaProgramasDto()
                            {
                                IdTipoFormulario = buscaPlantilla.Dtos.SingleOrDefault().IdPlantillaTraspaso,
                                IdBips = buscaForm.Dtos.SingleOrDefault().IdPrograma,
                                IdPrograma = regForm.Dtos.SingleOrDefault().IdPrograma
                            }, EnumAccionRealizar.Ninguna);
                            if (regResp.HasError())
                                throw new Exception("Error al crear respuestas según origen");
                        }else{
                            throw new Exception("Tipo de formulario origen no es igual al tipo de formulario destino");
                        }                        
                    }else{
                        ViewDto<TablaRespuestasDto> regResp = new ViewDto<TablaRespuestasDto>();
                        regResp = bips.RegistrarRespuestas(new ContextoDto(),
                            new TablaRespuestasDto()
                            {
                                IdPregunta = decimal.Parse(constantes.GetValue("PreguntaNombre")),
                                IdFormulario = regForm.Dtos.SingleOrDefault().IdPrograma,
                                Respuesta = data2.Nombre
                            }, EnumAccionRealizar.Insertar);
                        if (regResp.HasError())
                            throw new Exception("Error al registrar respuestas (nombre)");
                        //Respuesta ministerio
                        regResp = new ViewDto<TablaRespuestasDto>();
                        regResp = bips.RegistrarRespuestas(new ContextoDto(),
                            new TablaRespuestasDto()
                            {
                                IdPregunta = decimal.Parse(constantes.GetValue("PreguntaMinisterio")),
                                IdFormulario = regForm.Dtos.SingleOrDefault().IdPrograma,
                                Respuesta = data2.IdMinisterio.IdParametro
                            }, EnumAccionRealizar.Insertar);
                        if (regResp.HasError())
                            throw new Exception("Error al registrar respuestas (ministerio)");
                        //Respuesta servicio
                        regResp = new ViewDto<TablaRespuestasDto>();
                        regResp = bips.RegistrarRespuestas(new ContextoDto(),
                            new TablaRespuestasDto()
                            {
                                IdPregunta = decimal.Parse(constantes.GetValue("PreguntaServicio")),
                                IdFormulario = regForm.Dtos.SingleOrDefault().IdPrograma,
                                Respuesta = data2.IdServicio.IdParametro
                            }, EnumAccionRealizar.Insertar);
                        if (regResp.HasError())
                            throw new Exception("Error al registrar respuestas (servicio)");
                    }
                    //Registra grupo seleccionado
                    ViewDto<TablaFormulariosGruposDto> regGrupo = new ViewDto<TablaFormulariosGruposDto>();
                    regGrupo = bips.RegistrarFormulariosGrupos(new ContextoDto(), new TablaFormulariosGruposDto()
                    {
                        IdGrupoFormulario = idGrupo,
                        IdFormulario = regForm.Dtos.SingleOrDefault().IdPrograma,
                        Estado = decimal.Parse(constantes.GetValue("Activo"))
                    }, EnumAccionRealizar.Insertar);
                    if (regGrupo.HasError())
                        throw new Exception("Error al registrar grupo");
                    //Registra grupo todos por defecto
                    regGrupo = new ViewDto<TablaFormulariosGruposDto>();
                    regGrupo = bips.RegistrarFormulariosGrupos(new ContextoDto(), new TablaFormulariosGruposDto()
                    {
                        IdGrupoFormulario = decimal.Parse(constantes.GetValue("GrupoTodos")),
                        IdFormulario = regForm.Dtos.SingleOrDefault().IdPrograma,
                        Estado = decimal.Parse(constantes.GetValue("Activo"))
                    }, EnumAccionRealizar.Insertar);
                    if (regGrupo.HasError())
                        throw new Exception("Error al registrar grupo por defecto");
                    //Registra relacion de formulario
                    ViewDto<TablaRelacionFormulariosDto> regRelForm;
                    ViewDto<TablaProgramasDto> buscaFormReest;
                    foreach (var item in data1){
                        buscaFormReest = new ViewDto<TablaProgramasDto>();
                        buscaFormReest = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto()
                        {
                            IdPrograma = item.IdPrograma,
                            Estado = decimal.Parse(constantes.GetValue("Activo"))
                        }, EnumAccionRealizar.Buscar);
                        if (buscaFormReest.HasElements()){
                            regRelForm = new ViewDto<TablaRelacionFormulariosDto>();
                            regRelForm = bips.RegistrarRelacionFormularios(new ContextoDto(), new TablaRelacionFormulariosDto()
                            {
                                IdFormulario = regForm.Dtos.SingleOrDefault().IdPrograma,
                                IdBips = buscaFormReest.Dtos.SingleOrDefault().IdBips,
                                IdFormularioAnterior = item.IdPrograma,
                                TipoRelacionFormulario = decimal.Parse(constantes.GetValue("FormCreacionRestrucc"))
                            }, EnumAccionRealizar.Insertar);
                            if (regRelForm.HasError())
                                throw new Exception("Error al crear la relacion del formulario (reestructuracion)");
                        }
                        else{
                            throw new Exception("Formulario de reestructuracion no encontrado");
                        }
                    }
                }
                else{
                    registro = "Cero registros enviados";
                }
            }
            catch(Exception ex) {
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return Task.FromResult(registro);
        }

        public Task<string> eliminarFormulario(TablaProgramasDto data)
        {
            string registro = "ok";
            try {
                //Cambio estado de formularios grupos
                ViewDto<TablaFormulariosGruposDto> borraFormulariosGrupos = new ViewDto<TablaFormulariosGruposDto>();
                borraFormulariosGrupos = bips.RegistrarFormulariosGrupos(new ContextoDto(), new TablaFormulariosGruposDto() {
                    IdFormulario = data.IdPrograma,
                    Estado = decimal.Parse(constantes.GetValue("Inactivo"))
                }, EnumAccionRealizar.Eliminar);
                if (borraFormulariosGrupos.HasError()){
                    throw new Exception(borraFormulariosGrupos.Error.Mensaje);
                }
                //Cambio estado del formulario                
                ViewDto<TablaProgramasDto> borrar = new ViewDto<TablaProgramasDto>();
                data.Estado.IdParametro = decimal.Parse(constantes.GetValue("Inactivo"));
                borrar = bips.RegistrarProgramas(new ContextoDto(), data, EnumAccionRealizar.Eliminar);
                if (borrar.HasError())
                    registro = borrar.Error.Mensaje;
            }
            catch(Exception ex) {
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return Task.FromResult(registro);
        }        

        public Task<List<TablaProgramasDto>> getUserGruposFormularios(TablaProgramasFiltroDto filtros, string tipo)
        {
            List<TablaProgramasDto> data = new List<TablaProgramasDto>();
            try {
                ViewDto<TablaProgramasDto> buscaData = new ViewDto<TablaProgramasDto>();
                buscaData = bips.BuscarFormulariosUsuarios(new ContextoDto(), filtros);
                if (buscaData.HasElements()){
                    if (tipo == "grupos")
                        data = buscaData.Dtos.GroupBy(p=>p.IdGrupoFormulario).Select(p=>p.First()).OrderBy(p=>p.Nombre).ToList();
                    else if (tipo == "usuarios"){
                        List<TablaProgramasDto> temp = buscaData.Dtos.GroupBy(p => p.IdUser).Select(p => p.First()).Where(p => !String.IsNullOrEmpty(p.IdUser)).OrderBy(p => p.Nombre).ToList();
                        ViewDto<TablaUsuariosDto> usuario;
                        foreach (var item in temp)
                        {
                            if (!String.IsNullOrEmpty(item.IdUser)){
                                usuario = new ViewDto<TablaUsuariosDto>();
                                usuario = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto()
                                {
                                    Id = item.IdUser,
                                    IdEstado = decimal.Parse(constantes.GetValue("Activo"))
                                });
                                if (usuario.HasElements()){
                                    data.Add(new TablaProgramasDto()
                                    {
                                        IdPrograma = item.IdPrograma,
                                        IdUser = usuario.Dtos.SingleOrDefault().UserName,
                                        Nombre = usuario.Dtos.SingleOrDefault().Nombre,
                                        Ministerio = usuario.Dtos.SingleOrDefault().Ministerio,
                                        Servicio = usuario.Dtos.SingleOrDefault().Servicio
                                    });
                                }                                    
                            }
                        }
                    }                        
                }
            }
            catch(Exception ex) {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(data);
        }

        public async Task<IList<int>> getAnosCreacionForm(string idUsuario)
        {
            IList<int> anos = new List<int>();
            try {                                
                if (!String.IsNullOrEmpty(idUsuario)){
                    UsuariosModels usuario = new UsuariosModels();
                    List<TablaUsuariosDto> buscaUsuario = new List<TablaUsuariosDto>();
                    buscaUsuario = await usuario.getUsuariosFiltro(new TablaUsuariosFiltroDto() {
                        Id = idUsuario,
                        IdEstado = decimal.Parse(constantes.GetValue("Activo"))
                    });
                    int inicio = 0, fin = 0, parametro = 0;
                    if (buscaUsuario.Count > 0){
                        var perfil = buscaUsuario.SingleOrDefault().IdPerfil;                       
                        if (perfil == decimal.Parse(constantes.GetValue("PerfilAdmin")))
                            parametro = int.Parse(constantes.GetValue("AnoCreacFormAdmin"));
                        else
                            parametro = int.Parse(constantes.GetValue("AnoCreacFormAnalistas"));

                        ViewDto<TablaParametrosDto> anosCreacion = new ViewDto<TablaParametrosDto>();
                        anosCreacion = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto()
                        {
                            IdParametro = parametro,
                            Estado = decimal.Parse(constantes.GetValue("Activo"))
                        });
                        if (anosCreacion.HasElements()){
                            inicio = (int)anosCreacion.Dtos.FirstOrDefault().Valor;
                            fin = (int)anosCreacion.Dtos.FirstOrDefault().Valor2;
                        }
                    }
                    for (int i = (DateTime.Now.Year + inicio); i <= (DateTime.Now.Year + fin); i++)
                        anos.Add(i);
                }
            }
            catch(Exception ex) {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return (anos.OrderByDescending(p=>p).ToList());
        }

        public async Task<string> registraIteracionFormularios(List<TablaProgramasDto> data, string idUsuario)
        {
            string registro = "ok";
            try
            {
                if (data.Count > 0){
                    string version = string.Empty;
                    FormulariosModels formulario = new FormulariosModels();
                    EvaluacionExAnteModels evaluacion = new EvaluacionExAnteModels();
                    foreach (var item in data){
                        registro = string.Empty;                     
                        if (item.IdPrograma != 0 && item.IdPrograma != null){
                            version = await formulario.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaVersionProgramas")), int.Parse(item.IdPrograma.ToString()));
                            registro = await evaluacion.nuevaIteracion(int.Parse(item.IdPrograma.ToString()), (String.IsNullOrEmpty(version) ? 0 : int.Parse(version)), idUsuario);
                        }
                    }
                }
                else{
                    registro = "cero registros enviados";
                }
            }
            catch (Exception ex)
            {
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return (registro);
        }
    }
}
