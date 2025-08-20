using log4net;
using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Core.Providers;
using MDS.Dto;
using MDS.Svc;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace SistemasBIPS.Models
{
    public class GruposModels
    {
        private static ISistemasBIPSSvc bips = null;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private IProviderConstante constantes = null;

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public GruposModels()
        {
            bips = (ISistemasBIPSSvc)Activator.CreateInstance(typeof(SistemasBIPSSvc));
            constantes = (IProviderConstante)Activator.CreateInstance(typeof(ProviderConstante));
        }

        public Task<string> registraGruposFormularios(TablaGruposFormulariosDto data, EnumAccionRealizar accion)
        {
            string registro = "ok";
            try {
                ViewDto<TablaGruposFormulariosDto> reg = new ViewDto<TablaGruposFormulariosDto>();                
                if (data.IdGrupoFormulario == null && accion != EnumAccionRealizar.EliminarUserGrupo)
                    data.Estado = decimal.Parse(constantes.GetValue("Activo"));

                reg = bips.RegistrarGruposFormularios(new ContextoDto(), data, accion);
                if (reg.HasError())
                    registro = reg.Error.Detalle;
            }catch (Exception ex){
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return Task.FromResult(registro);
        }

        public async Task<string> eliminaGrupoFormularios(TablaGruposFormulariosDto data)
        {
            string registro = "ok";
            try{
                UsuariosModels usuarios = new UsuariosModels();
                List<TablaFormulariosGruposDto> formulariosGrupos = new List<TablaFormulariosGruposDto>();                
                formulariosGrupos = await usuarios.getFormulariosGrupos(new TablaFormulariosGruposFiltroDto() { IdGrupoFormulario = data.IdGrupoFormulario, Estado = decimal.Parse(constantes.GetValue("Activo")) });
                if (formulariosGrupos.Count > 0){
                    ViewDto<TablaFormulariosGruposDto> regFormGrup;
                    foreach(var item in formulariosGrupos){
                        regFormGrup = new ViewDto<TablaFormulariosGruposDto>();
                        item.Estado = decimal.Parse(constantes.GetValue("Inactivo"));
                        regFormGrup = bips.RegistrarFormulariosGrupos(new ContextoDto(), item, EnumAccionRealizar.Eliminar);
                        if (regFormGrup.HasError())
                            throw new Exception(regFormGrup.Error.Detalle);
                    }
                }
                List<TablaUsuariosDto> formulariosUsuarios = new List<TablaUsuariosDto>();
                formulariosUsuarios = await getUsuariosGrupos(new TablaUsuariosFiltroDto() { IdGrupo = data.IdGrupoFormulario, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                if (formulariosUsuarios.Count > 0){
                    ViewDto<TablaFormulariosUsuariosDto> regFormUser;
                    foreach(var item in formulariosUsuarios){
                        regFormUser = new ViewDto<TablaFormulariosUsuariosDto>();
                        regFormUser = bips.RegistrarFormulariosUsuarios(new ContextoDto(), new TablaFormulariosUsuariosDto() { IdGrupoFormulario = item.IdGrupo, IdUsuario = item.Id, Estado = decimal.Parse(constantes.GetValue("Inactivo")) }, EnumAccionRealizar.EliminarUserGrupo);
                        if (regFormUser.HasError())
                            throw new Exception(regFormUser.Error.Detalle);
                    }
                }
                ViewDto<TablaGruposFormulariosDto> gruposFormularios = new ViewDto<TablaGruposFormulariosDto>();
                gruposFormularios = bips.RegistrarGruposFormularios(new ContextoDto(), new TablaGruposFormulariosDto() { IdGrupoFormulario = data.IdGrupoFormulario, Estado = decimal.Parse(constantes.GetValue("Inactivo")) }, EnumAccionRealizar.Eliminar);
                if (gruposFormularios.HasError())
                    throw new Exception(gruposFormularios.Error.Detalle);
            }
            catch (Exception ex){
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return (registro);
        }
        
        public Task<List<TablaUsuariosDto>> getUsuariosGrupos(TablaUsuariosFiltroDto filtros)
        {
            List<TablaUsuariosDto> data = new List<TablaUsuariosDto>();
            try
            {
                ViewDto<TablaUsuariosDto> buscarUserGrupo = new ViewDto<TablaUsuariosDto>();
                buscarUserGrupo = bips.BuscarUsuariosGrupos(new ContextoDto(), filtros);
                if (buscarUserGrupo.HasElements())
                    data = buscarUserGrupo.Dtos;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(data);
        }

        public Task<string> registraUsuariosGrupos(List<TablaFormulariosUsuariosDto> data)
        {
            string registro = "ok";
            try
            {
                if (data.Count > 0){
                    ViewDto<TablaFormulariosUsuariosDto> reg;
                    foreach (var item in data){
                        if (!String.IsNullOrEmpty(item.IdUsuario)){
                            reg = new ViewDto<TablaFormulariosUsuariosDto>();
                            item.Estado = decimal.Parse(constantes.GetValue("Activo"));
                            item.IdPlataforma = decimal.Parse(constantes.GetValue("PlataformaCargaBIPS"));                            
                            reg = bips.RegistrarFormulariosUsuarios(new ContextoDto(), item, EnumAccionRealizar.Insertar);
                            if (reg.HasError())
                                registro = reg.Error.Detalle;
                        }else{
                            registro = "idUsuario nulo";
                        }
                    }                                        
                }else{
                    registro = "cero registros enviados";
                }
            }
            catch (Exception ex)
            {
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return Task.FromResult(registro);
        }

        public async Task<List<TablaUsuariosDto>> getUsuariosGruposAsignados(TablaUsuariosFiltroDto filtros)
        {
            List<TablaUsuariosDto> data = new List<TablaUsuariosDto>();
            try
            {
                UsuariosModels usuarios = new UsuariosModels();
                data = await usuarios.getUsuariosFiltro(filtros);
                if (data.Count > 0){
                    List<TablaUsuariosDto> dataUsersGrupo = new List<TablaUsuariosDto>();
                    dataUsersGrupo = await getUsuariosGrupos(filtros);
                    if (dataUsersGrupo.Count > 0){
                        foreach (var item in dataUsersGrupo)
                            data.RemoveAll(p => p.Id == item.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return data;

        }

        public Task<string> registraFormulariosGrupos(TablaFormulariosGruposDto data)
        {
            string registro = "ok";
            try {
                ViewDto<TablaFormulariosGruposDto> reg = new ViewDto<TablaFormulariosGruposDto>();
                reg = bips.RegistrarFormulariosGrupos(new ContextoDto(), data, EnumAccionRealizar.Eliminar);
                if (reg.HasError())
                    registro = reg.Error.Detalle;
            } catch (Exception ex){
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return Task.FromResult(registro);
        }

        public async Task<string> eliminaFormGruposMasivo(TablaProgramasFiltroDto data, int idGrupo)
        {
            string registro = "ok";
            try{                
                ProgramasModels programas = new ProgramasModels();
                List<TablaProgramasDto> objProgramas = new List<TablaProgramasDto>();
                objProgramas = await programas.getTodosProgramasFiltro(data);
                if (objProgramas.Count > 0){
                    UsuariosModels usuarios = new UsuariosModels();
                    List<TablaFormulariosGruposDto> objFormulariosGrupos = new List<TablaFormulariosGruposDto>();
                    objFormulariosGrupos = await usuarios.getFormulariosGrupos(new TablaFormulariosGruposFiltroDto() { IdGrupoFormulario = idGrupo, Estado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (objFormulariosGrupos.Count > 0){
                        string borrado = string.Empty;
                        foreach(var item in objFormulariosGrupos){
                            if (objProgramas.Exists(p=>p.IdPrograma == item.IdFormulario)){                                
                                borrado = await registraFormulariosGrupos(new TablaFormulariosGruposDto() { Estado = decimal.Parse(constantes.GetValue("Inactivo")), IdGrupoFormulario = idGrupo, IdFormulario = item.IdFormulario });
                                if (borrado != "ok"){
                                    registro = borrado;
                                    break;
                                }                                    
                            }
                        }
                    }
                }
            }
            catch (Exception ex){
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return registro;
        }

        public Task<string> registraFormulariosGrupos(List<TablaFormulariosGruposDto> data)
        {
            string registro = "ok";
            try{
                if (data.Count > 0){
                    ViewDto<TablaFormulariosGruposDto> reg;
                    foreach (var item in data){
                        if (item.IdFormulario != 0 && item.IdFormulario != null){
                            reg = new ViewDto<TablaFormulariosGruposDto>();
                            item.Estado = decimal.Parse(constantes.GetValue("Activo"));
                            reg = bips.RegistrarFormulariosGrupos(new ContextoDto(), item, EnumAccionRealizar.Insertar);
                            if (reg.HasError())
                                registro = reg.Error.Detalle;
                        }
                    }
                }
                else{
                    registro = "cero registros enviados";
                }
            }catch (Exception ex){
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return Task.FromResult(registro);
        }

        public async Task<string> registraFormulariosGrupos(TablaProgramasFiltroDto data, int idGrupo)
        {
            string registro = "ok";
            try{
                ProgramasModels programas = new ProgramasModels();
                List<TablaProgramasDto> objProgramas = new List<TablaProgramasDto>();
                data.Estado = decimal.Parse(constantes.GetValue("Activo"));
                objProgramas = await programas.getTodosProgramasFiltro(data);
                if (objProgramas.Count > 0){
                    List<TablaFormulariosGruposDto> listaProg = new List<TablaFormulariosGruposDto>();
                    foreach(var item in objProgramas){
                        listaProg.Add(new TablaFormulariosGruposDto() { IdFormulario = item.IdPrograma, IdGrupoFormulario = idGrupo, Estado = data.Estado });
                    }
                    registro = await registraFormulariosGrupos(listaProg);
                }else{
                    registro = "cero registros enviados";
                }
            }catch (Exception ex){
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return registro;
        }

        public async Task<List<TablaProgramasDto>> getFormulariosGrupos(TablaProgramasFiltroDto filtros, int idGrupo)
        {
            List<TablaProgramasDto> data = new List<TablaProgramasDto>();
            try
            {
                ProgramasModels programas = new ProgramasModels();
                data = await programas.getTodosProgramasFiltro(filtros);
                if (data.Count > 0){
                    UsuariosModels usuarios = new UsuariosModels();
                    List<TablaFormulariosGruposDto> objFormulariosGrupos = new List<TablaFormulariosGruposDto>();
                    objFormulariosGrupos = await usuarios.getFormulariosGrupos(new TablaFormulariosGruposFiltroDto() { IdGrupoFormulario = idGrupo, Estado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (objFormulariosGrupos.Count > 0){
                        foreach(var item in objFormulariosGrupos){
                            data.RemoveAll(p=>p.IdPrograma == item.IdFormulario);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return data;
        }
    }
}
