using log4net;
using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Core.Providers;
using MDS.Dto;
using MDS.Svc;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SistemasBIPS.Models
{
    public class UsuariosModels
    {
        private static ISistemasBIPSSvc bips = null;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private IProviderConstante constantes = null;

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public UsuariosModels()
        {
            bips = (ISistemasBIPSSvc)Activator.CreateInstance(typeof(SistemasBIPSSvc));
            constantes = (IProviderConstante)Activator.CreateInstance(typeof(ProviderConstante));
        }

        public Task<List<TablaUsuariosDto>> getUsuariosFiltro(TablaUsuariosFiltroDto filtros)
        {
            List<TablaUsuariosDto> usuarios = new List<TablaUsuariosDto>();
            try
            {
                usuarios = bips.BuscarUsuarios(new ContextoDto(), filtros).Dtos;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(usuarios);
        }

        public Task<List<TablaPerfilesDto>> getPerfiles(TablaPerfilesFiltroDto data)
        {
            List<TablaPerfilesDto> perfiles = new List<TablaPerfilesDto>();
            try {
                ViewDto<TablaPerfilesDto> buscaPerfiles = new ViewDto<TablaPerfilesDto>();
                buscaPerfiles = bips.BuscarPerfiles(new ContextoDto(), data);
                if (buscaPerfiles.HasElements())
                    perfiles = buscaPerfiles.Dtos;
            }catch(Exception ex) {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(perfiles);
        }

        public async Task<List<TablaPerfilesDto>> getPerfiles(String idUsuario)
        {
            List<TablaPerfilesDto> perfiles = new List<TablaPerfilesDto>();
            try {                
                var usuario = await getUsuariosFiltro(new TablaUsuariosFiltroDto() { Id = idUsuario });
                if (usuario.Count > 0) {
                    var todosPerfiles = await getPerfiles(new TablaPerfilesFiltroDto() { Estado = decimal.Parse(constantes.GetValue("Activo")) });
                    var perfilUsuario = await getPerfiles(new TablaPerfilesFiltroDto() { IdPerfil = usuario.FirstOrDefault().IdPerfil, Estado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (todosPerfiles.Count > 0){
                        if (perfilUsuario.FirstOrDefault().IdPerfil != decimal.Parse(constantes.GetValue("PerfilAdmin"))){
                            foreach (var perfil in todosPerfiles.Where(p => p.Jerarquia > perfilUsuario.FirstOrDefault().Jerarquia).Select(p => p))
                                perfiles.Add(perfil);
                        }
                        else { perfiles = todosPerfiles; }
                        perfiles.OrderBy(p => p.Jerarquia);
                    }
                }                
            } catch(Exception ex){
                log.Error(ex.Message, ex);
                throw ex;
            }
            return perfiles;
        }

        public async Task<List<TablaGruposFormulariosDto>> getGruposFormularios(TablaGruposFormulariosFiltroDto data, string usuario)
        {
            List<TablaGruposFormulariosDto> gruposFormularios = new List<TablaGruposFormulariosDto>();
            try
            {
                ViewDto<TablaGruposFormulariosDto> buscaGruposFormularios = new ViewDto<TablaGruposFormulariosDto>();
                buscaGruposFormularios = bips.BuscarGruposFormularios(new ContextoDto(), data);
                if (buscaGruposFormularios.HasElements()){
                    gruposFormularios = buscaGruposFormularios.Dtos;
                    if (usuario != null) {
                        List<TablaGruposFormulariosDto> gruposUsers = new List<TablaGruposFormulariosDto>();
                        gruposUsers = await getGruposUsuariosFiltros(new TablaGruposFormulariosFiltroDto() { IdUsuario = usuario, Estado = decimal.Parse(constantes.GetValue("Activo")) });
                        if (gruposUsers.Count > 0)
                        {
                            foreach (var item in gruposUsers)
                                gruposFormularios.RemoveAll(p => p.IdGrupoFormulario == item.IdGrupoFormulario);
                        }
                    }                    
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return gruposFormularios;
        }

        public Task<List<TablaFormulariosGruposDto>> getFormulariosGrupos(TablaFormulariosGruposFiltroDto data)
        {
            List<TablaFormulariosGruposDto> formulariosGrupos = new List<TablaFormulariosGruposDto>();
            try
            {
                ViewDto<TablaFormulariosGruposDto> buscaFormulariosGrupos = new ViewDto<TablaFormulariosGruposDto>();
                buscaFormulariosGrupos = bips.BuscarFormulariosGrupos(new ContextoDto(), data);
                if (buscaFormulariosGrupos.HasElements())
                    formulariosGrupos = buscaFormulariosGrupos.Dtos;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(formulariosGrupos);
        }
     
        public async Task<DatosUsuarios> getDatosUsuarios(string id)
        {
            DatosUsuarios data = new DatosUsuarios();
            try {
                List<TablaUsuariosDto> usuario = new List<TablaUsuariosDto>();
                usuario = await getUsuariosFiltro(new TablaUsuariosFiltroDto() { Id = id });
                if (usuario.Count > 0){
                    data.Email = usuario.SingleOrDefault().Email;
                    data.UserName = usuario.SingleOrDefault().UserName;
                    data.Nombre = usuario.SingleOrDefault().Nombre;
                    data.Ministerio = usuario.SingleOrDefault().IdMinisterio != null ? int.Parse(usuario.SingleOrDefault().IdMinisterio.ToString()) : 0;
                    data.Servicio = usuario.SingleOrDefault().IdServicio != null ? int.Parse(usuario.SingleOrDefault().IdServicio.ToString()) : 0;
                    data.Perfil = usuario.SingleOrDefault().IdPerfil != null ? int.Parse(usuario.SingleOrDefault().IdPerfil.ToString()) : 0;
                    /*ViewDto<TablaProgramasDto> buscaFormUsuario = new ViewDto<TablaProgramasDto>();
                    buscaFormUsuario = bips.BuscarFormulariosUsuarios(new ContextoDto(), new TablaProgramasFiltroDto() { IdUser = id });
                    if (buscaFormUsuario.HasElements()){
                        foreach (var item in buscaFormUsuario.Dtos.GroupBy(p=>p.IdGrupoFormulario)){
                            if (buscaFormUsuario.Dtos.FirstOrDefault(p=>p.IdGrupoFormulario == item.Key).TipoGrupo == decimal.Parse(constantes.GetValue("FormulariosPropios"))){
                                data.MisFormularios.Add(int.Parse(item.Key.ToString()));
                            }else{
                                data.OtrosFormularios.Add(int.Parse(item.Key.ToString()));
                            }
                        }
                    }*/
                    data.MisFormularios.Add(1);
                }
            }
            catch(Exception ex) {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return data;
        }

        public Task<bool> registraGrupFormUsuarios(List<TablaFormulariosUsuariosDto> data)
        {
            bool registro = true;
            try {
                ViewDto<TablaFormulariosUsuariosDto> registraFormUsuarios = new ViewDto<TablaFormulariosUsuariosDto>();
                foreach(var reg in data)
                {
                    registraFormUsuarios = new ViewDto<TablaFormulariosUsuariosDto>();
                    registraFormUsuarios = bips.RegistrarFormulariosUsuarios(new ContextoDto(), reg, EnumAccionRealizar.Insertar);
                    if (registraFormUsuarios.HasError())
                        throw new Exception("Error registro formularios usuarios");
                }                
            }
            catch (Exception ex) {
                registro = false;
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(registro);
        }

        public async Task<bool> registraGrupFormUsuarios(List<TablaUsuariosDto> usuario, UsuariosViewModels data)
        {
            bool registro = true;
            try
            {
                List<TablaFormulariosUsuariosDto> dataFormUsuario = new List<TablaFormulariosUsuariosDto>();
                foreach(var item in data.dataUsuario.MisFormularios)
                {
                    dataFormUsuario.Add(new TablaFormulariosUsuariosDto()
                    {
                        IdUsuario = usuario.SingleOrDefault().Id,
                        IdGrupoFormulario = item,
                        TipoGrupo = decimal.Parse(constantes.GetValue("FormulariosPropios")),
                        IdPlataforma = decimal.Parse(constantes.GetValue("PlataformaCargaBIPS")),
                        Estado = int.Parse(constantes.GetValue("Activo"))
                    });
                }                
                foreach (var item in data.dataUsuario.OtrosFormularios)
                {
                    dataFormUsuario.Add(new TablaFormulariosUsuariosDto()
                    {
                        IdUsuario = usuario.SingleOrDefault().Id,
                        IdGrupoFormulario = item,
                        TipoGrupo = decimal.Parse(constantes.GetValue("FormulariosGenerales")),
                        IdPlataforma = decimal.Parse(constantes.GetValue("PlataformaCargaBIPS")),
                        Estado = int.Parse(constantes.GetValue("Activo"))
                    });
                }                
                registro = await registraGrupFormUsuarios(dataFormUsuario);
            }
            catch (Exception ex)
            {
                registro = false;
                log.Error(ex.Message, ex);
                throw ex;
            }
            return registro;
        }

        public Task<string> eliminaGruposUsuarios(TablaGruposFormulariosDto data)
        {
            string registro = "ok";
            try
            {
                ViewDto<TablaGruposFormulariosDto> borraGruposusuarios = new ViewDto<TablaGruposFormulariosDto>();
                data.Estado = decimal.Parse(constantes.GetValue("Inactivo"));
                borraGruposusuarios = bips.RegistrarGruposFormularios(new ContextoDto(), data, EnumAccionRealizar.EliminarUserGrupo);
                if (borraGruposusuarios.HasError())
                    throw new Exception(borraGruposusuarios.Error.Detalle);
            }
            catch (Exception ex)
            {
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return Task.FromResult(registro);
        }

        /// <summary>
        /// Crea contraseñas aleatoriamente
        /// </summary>
        /// <param name="largoContraseña"></param>
        /// <returns></returns>
        public string creaContraseña(int largoContraseña)
        {
            string _caracteres = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ23456789!@$";
            Byte[] randomBytes = new Byte[largoContraseña];
            char[] contraseña = new char[largoContraseña];
            int totalCaracteres = _caracteres.Length;
            for (int i = 0; i < largoContraseña; i++)
            {
                Random randomObj = new Random();
                randomObj.NextBytes(randomBytes);
                contraseña[i] = _caracteres[(int)randomBytes[i] % totalCaracteres];
            }
            return new string(contraseña);
        }

        public Task<List<TablaPerfilesDto>> getPerfilesPermisos(TablaPerfilesFiltroDto filtros)
        {
            List<TablaPerfilesDto> data = new List<TablaPerfilesDto>();
            try{
                ViewDto<TablaPerfilesDto> buscaPerfilesPermisos = new ViewDto<TablaPerfilesDto>();
                buscaPerfilesPermisos = bips.BuscarPermisosPerfiles(new ContextoDto(), filtros);
                if (buscaPerfilesPermisos.HasElements())
                    data = buscaPerfilesPermisos.Dtos;
            }
            catch(Exception ex){
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(data);
        }

        public async Task<List<TablaPerfilesDto>> getPerfilesPermisos(String IdUsuario)
        {            
            List<TablaPerfilesDto> data = new List<TablaPerfilesDto>();
            try
            {
                List<TablaUsuariosDto> usuario = new List<TablaUsuariosDto>();
                usuario = await getUsuariosFiltro(new TablaUsuariosFiltroDto() { Id = IdUsuario });
                if (usuario.Count > 0)
                    if (usuario.SingleOrDefault().IdPerfil != null)
                        data = await getPerfilesPermisos(new TablaPerfilesFiltroDto() { IdPerfil = usuario.SingleOrDefault().IdPerfil, Estado = decimal.Parse(constantes.GetValue("Activo")) });               
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return data;
        }

        public async Task<List<TablaParametrosUsuariosDto>> getPerfilesEspeciales(String IdUsuario)
        {
            List<TablaParametrosUsuariosDto> data = new List<TablaParametrosUsuariosDto>();
            try
            {
                data = await getPerfilesEspeciales(new TablaParametrosUsuariosFiltroDto() { IdUsuario = IdUsuario, Ano = DateTime.Now.Year });
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return data;
        }

        public Task<List<TablaParametrosUsuariosDto>> getPerfilesEspeciales(TablaParametrosUsuariosFiltroDto filtros)
        {
            List<TablaParametrosUsuariosDto> data = new List<TablaParametrosUsuariosDto>();
            try
            {
                ViewDto<TablaParametrosUsuariosDto> buscar = new ViewDto<TablaParametrosUsuariosDto>();
                buscar = bips.BuscarParametrosUsuarios(new ContextoDto(), filtros);
                if (buscar.HasElements())
                    data = buscar.Dtos;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(data);
        }

        public async Task<Boolean> getPerfilesPermisos(int IdPermiso, string IdUsuario)
        {
            Boolean acceso = false;
            try{
                List<TablaPerfilesDto> usuario = new List<TablaPerfilesDto>();
                usuario = await getPerfilesPermisos(IdUsuario);
                if (usuario.Count > 0)
                    if (usuario.Count(p => p.IdPermiso == IdPermiso) > 0)
                        acceso = true;                
            }
            catch(Exception ex){
                log.Error(ex.Message, ex);
                throw ex;
            }
            return acceso;
        }

        public async Task<Boolean> getPerfilesPermisosTotales(List<TablaExcepcionesPermisosDto> data, string IdUsuario)
        {
            Boolean acceso = false;
            try
            {
                if (data.Count > 0){
                    //Busco perfil usuario conectado
                    List<TablaUsuariosDto> usuario = new List<TablaUsuariosDto>();
                    usuario = await getUsuariosFiltro(new TablaUsuariosFiltroDto() { Id = IdUsuario, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                    decimal? perfil = (usuario.Count > 0 ? usuario.FirstOrDefault().IdPerfil : null);
                    //Busco lista de permisos especiales
                    ViewDto<TablaParametrosDto> permisosEspeciales = new ViewDto<TablaParametrosDto>();
                    permisosEspeciales = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("PermisosEspeciales")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (permisosEspeciales.HasElements()){
                        foreach (var item in data){
                            if (permisosEspeciales.Dtos.Count(p => p.Valor == item.IdPermiso) > 0)
                            {
                                if (perfil == decimal.Parse(constantes.GetValue("PerfilAdmin")))
                                {
                                    acceso = true;
                                }
                                else {
                                    acceso = false;
                                    break;
                                }
                            }
                            else
                            {
                                acceso = true;
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
            return acceso;
        }

        public Task<List<TablaGruposFormulariosDto>> getGruposUsuariosFiltros(TablaGruposFormulariosFiltroDto filtros)
        {
            List<TablaGruposFormulariosDto> data = new List<TablaGruposFormulariosDto>();
            try{
                ViewDto<TablaGruposFormulariosDto> buscaData = new ViewDto<TablaGruposFormulariosDto>();
                buscaData = bips.BuscarGruposUsuarios(new ContextoDto(), filtros);
                if (buscaData.HasElements()){
                    data = buscaData.Dtos;
                }
            }
            catch(Exception ex){
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(data);
        }

        public Task<List<TablaProgramasDto>> getPermisosFormularios(TablaProgramasFiltroDto filtros)
        {
            List<TablaProgramasDto> data = new List<TablaProgramasDto>();
            try {
                ViewDto<TablaProgramasDto> buscaData = new ViewDto<TablaProgramasDto>();
                buscaData = bips.BuscarPermisosFormularios(new ContextoDto(), filtros);
                if (buscaData.HasElements())
                    data = buscaData.Dtos;
            }catch(Exception ex){
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(data);
        }

        public async Task<List<TablaProgramasDto>> getFormulariosPermisos(string idUsuario, string idUserConectado, TablaProgramasFiltroDto filtros)
        {
            List<TablaProgramasDto> data = new List<TablaProgramasDto>();
            try {
                List<TablaUsuariosDto> usuario = new List<TablaUsuariosDto>();
                usuario = await getUsuariosFiltro(new TablaUsuariosFiltroDto() { Id = idUserConectado, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                if (usuario.Count > 0){
                    decimal? perfil = usuario.FirstOrDefault().IdPerfil;
                    ProgramasModels formularios = new ProgramasModels();
                    IList<TablaProgramasDto> dataTemp = new List<TablaProgramasDto>();
                    dataTemp = await formularios.getProgramasFiltro(filtros);
                    if (dataTemp.Count > 0){
                        data = dataTemp.ToList();
                        if (perfil != decimal.Parse(constantes.GetValue("PerfilAdmin")) && perfil != decimal.Parse(constantes.GetValue("RolAnalistaMonitoreo")))
                            data.RemoveAll(p => p.Ano != DateTime.Now.Year);
                        else
                        {
                            if (perfil == decimal.Parse(constantes.GetValue("PerfilAdmin"))){
                                if (data.Count > 0)
                                    data.RemoveAll(p => p.Ano != DateTime.Now.Year && p.Ano != DateTime.Now.Year - 1 && p.Ano != DateTime.Now.Year + 1);
                            }
                            else if (perfil == decimal.Parse(constantes.GetValue("RolAnalistaMonitoreo"))){
                                if (data.Count > 0)
                                    data.RemoveAll(p => p.Ano != DateTime.Now.Year && p.Ano != DateTime.Now.Year + 1);
                            }                    
                        }
                        if (perfil != decimal.Parse(constantes.GetValue("PerfilAdmin")) && perfil != decimal.Parse(constantes.GetValue("RolAnalistaMonitoreo")))
                            if (data.Count > 0)
                                data.RemoveAll(p => p.IdEtapa == decimal.Parse(constantes.GetValue("EtapaPublicadoBips")));

                        if (data.Count > 0)
                            data.RemoveAll(p => p.IdEtapa == decimal.Parse(constantes.GetValue("EtapaSolciEvalExAnte")));

                        if (data.Count > 0)
                            data.RemoveAll(p => p.IdEtapa == decimal.Parse(constantes.GetValue("EtapaCierreEvalExAnte")));

                        if (data.Count > 0)
                            data.RemoveAll(p => p.IdEtapa == decimal.Parse(constantes.GetValue("EtapaEvaluacion")));

                        if (data.Count > 0){
                            List<TablaProgramasDto> asignados = new List<TablaProgramasDto>();
                            asignados = await getPermisosFormularios(new TablaProgramasFiltroDto() { IdUser = idUsuario, Estado = decimal.Parse(constantes.GetValue("Activo")) });
                            if (asignados.Count > 0)
                                foreach (var item in asignados)
                                    data.RemoveAll(p => p.IdPrograma == item.IdPrograma);
                        }                        
                    }
                }
                if (data.Count > 0)
                    data = data.DistinctBy(p => p.IdPrograma).ToList();
            }
            catch (Exception ex) {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return data;
        }

        public async Task<List<TablaExcepcionesPlantillasDto>> getPlantillasFormularios(TablaExcepcionesPlantillasFiltroDto filtros, string IdUsuario)
        {
            List<TablaExcepcionesPlantillasDto> data = new List<TablaExcepcionesPlantillasDto>();
            try{
                ViewDto<TablaExcepcionesPlantillasDto> plantillas = new ViewDto<TablaExcepcionesPlantillasDto>();
                plantillas = bips.BuscarPlantillasFormularios(new ContextoDto(), filtros);
                if (plantillas.HasElements()){
                    data = plantillas.Dtos;
                    //Busco perfil usuario conectado
                    List<TablaUsuariosDto> usuario = new List<TablaUsuariosDto>();
                    usuario = await getUsuariosFiltro(new TablaUsuariosFiltroDto() { Id = IdUsuario, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                    decimal? perfil = (usuario.Count > 0 ? usuario.FirstOrDefault().IdPerfil : null);
                    //Busco lista de permisos especiales
                    ViewDto<TablaParametrosDto> permisosEspeciales = new ViewDto<TablaParametrosDto>();
                    permisosEspeciales = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("PermisosEspeciales")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (permisosEspeciales.HasElements()){
                        foreach(var item in permisosEspeciales.Dtos)
                            if (data.Count(p => p.IdExcepcionPlantilla == item.Valor) > 0)
                                if (perfil != decimal.Parse(constantes.GetValue("PerfilAdmin")))
                                    data.RemoveAll(p=> p.IdExcepcionPlantilla == item.Valor);
                    }                    
                }
            }catch(Exception ex){
                log.Error(ex.Message, ex);
                throw ex;
            }
            return data;
        }

        public Task<string> registraPermisosUsuarios(List<TablaExcepcionesPermisosDto> data)
        {
            string registro = "ok";
            try{
                if (data.Count > 0){
                    ViewDto<TablaExcepcionesPermisosDto> reg;
                    foreach (var item in data)
                    {
                        if (!String.IsNullOrEmpty(item.IdUsuario))
                        {
                            reg = new ViewDto<TablaExcepcionesPermisosDto>();
                            item.Estado = decimal.Parse(constantes.GetValue("Activo"));
                            reg = bips.RegistrarExcepcionesFormularios(new ContextoDto(), item, EnumAccionRealizar.Insertar);
                            if (reg.HasError())
                                registro = reg.Error.Detalle;
                        }else{ 
                            registro = "idUsuario nulo";
                        }
                    }
                }else{
                    registro = "cero registros enviados";
                }
            }catch (Exception ex){
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return Task.FromResult(registro);
        }

        public async Task<string> registraPermisosUsersMasivo(TablaProgramasFiltroDto data, string idUserConectado)
        {
            string registro = "ok";
            try{
                ProgramasModels programas = new ProgramasModels();
                List<TablaProgramasDto> objProgramas = new List<TablaProgramasDto>();
                var idPermiso = data.IdExcepcion;
                data.Estado = decimal.Parse(constantes.GetValue("Activo"));
                data.IdExcepcion = (decimal?)null;
                objProgramas = await getFormulariosPermisos(data.IdUser, idUserConectado, data);
                if (objProgramas.Count > 0){
                    List<TablaProgramasDto> objPermisos = new List<TablaProgramasDto>();
                    objPermisos = await getPermisosFormularios(new TablaProgramasFiltroDto() { IdUser = data.IdUser, Estado = decimal.Parse(constantes.GetValue("Activo")) });
                    List<TablaExcepcionesPermisosDto> dataInsert = new List<TablaExcepcionesPermisosDto>();
                    bool noInsert = false;
                    foreach (var item in objProgramas){
                        noInsert = false;
                        if (objPermisos.Count > 0){
                            if (objPermisos.Exists(p=>p.IdPrograma == item.IdPrograma))
                                noInsert = true;
                        }
                        if (!noInsert)
                            dataInsert.Add(new TablaExcepcionesPermisosDto() { IdFormulario = item.IdPrograma, IdUsuario = data.IdUser, IdPermiso = idPermiso });
                    }
                    registro = await registraPermisosUsuarios(dataInsert);
                }
            }
            catch(Exception ex){
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return registro;
        }

        public Task<string> eliminaPermisosUsuarios(List<TablaExcepcionesPermisosDto> data)
        {
            string registro = "ok";
            try{
                if (data.Count > 0)
                {
                    ViewDto<TablaExcepcionesPermisosDto> reg;
                    foreach (var item in data)
                    {
                        if (!String.IsNullOrEmpty(item.IdUsuario))
                        {
                            reg = new ViewDto<TablaExcepcionesPermisosDto>();
                            item.Estado = decimal.Parse(constantes.GetValue("Inactivo"));
                            reg = bips.RegistrarExcepcionesFormularios(new ContextoDto(), item, EnumAccionRealizar.Eliminar);
                            if (reg.HasError())
                                registro = reg.Error.Detalle;
                        }
                        else
                        {
                            registro = "idUsuario nulo";
                        }
                    }
                }
                else
                {
                    registro = "cero registros enviados";
                }
            }
            catch(Exception ex){
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return Task.FromResult(registro);
        }

        public async Task<string> eliminaPermisosUsersMasivo(TablaProgramasFiltroDto data)
        {
            string registro = "ok";
            try
            {
                ProgramasModels programas = new ProgramasModels();
                List<TablaProgramasDto> objProgramas = new List<TablaProgramasDto>();
                objProgramas = (await programas.getProgramasFiltro(data)).ToList();
                if (objProgramas.Count > 0)
                {
                    List<TablaProgramasDto> objPermisos = new List<TablaProgramasDto>();
                    objPermisos = await getPermisosFormularios(new TablaProgramasFiltroDto() { IdUser = data.IdUser, Estado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (objPermisos.Count > 0){
                        List<TablaExcepcionesPermisosDto> datos = new List<TablaExcepcionesPermisosDto>();
                        foreach (var item in objPermisos){
                            if (objProgramas.Exists(p => p.IdPrograma == item.IdPrograma)){
                                datos.Add(new TablaExcepcionesPermisosDto() { IdFormulario = item.IdPrograma, IdUsuario = item.IdUser });
                            }
                        }
                        registro = await eliminaPermisosUsuarios(datos);
                    }                                                            
                }
            }
            catch (Exception ex)
            {
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return registro;
        }

        public async Task<string> eliminaUsuariosEstado(string id)
        {
            string registro = "ok";
            try{
                if (!String.IsNullOrEmpty(id)){
                    List<TablaProgramasDto> permisosUsers = new List<TablaProgramasDto>();
                    permisosUsers = await getPermisosFormularios(new TablaProgramasFiltroDto() { Estado = decimal.Parse(constantes.GetValue("Activo")), IdUser = id });
                    if (permisosUsers.Count > 0){
                        ViewDto<TablaExcepcionesPermisosDto> regPermUsers;
                        foreach(var item in permisosUsers){
                            regPermUsers = new ViewDto<TablaExcepcionesPermisosDto>();
                            regPermUsers = bips.RegistrarExcepcionesFormularios(new ContextoDto(), new TablaExcepcionesPermisosDto() { IdFormulario = item.IdPrograma, IdUsuario = id, Estado = decimal.Parse(constantes.GetValue("Inactivo")) }, EnumAccionRealizar.Eliminar);
                            if (regPermUsers.HasError())
                                throw new Exception(regPermUsers.Error.Detalle);
                        }
                    }
                    List<TablaGruposFormulariosDto> gruposUsers = new List<TablaGruposFormulariosDto>();
                    gruposUsers = await getGruposUsuariosFiltros(new TablaGruposFormulariosFiltroDto() { IdUsuario = id, Estado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (gruposUsers.Count > 0){
                        ViewDto<TablaGruposFormulariosDto> regFormUser;
                        GruposModels grupos = new GruposModels();
                        foreach (var item in gruposUsers){
                            regFormUser = new ViewDto<TablaGruposFormulariosDto>();
                            regFormUser = bips.RegistrarGruposFormularios(new ContextoDto(), new TablaGruposFormulariosDto() { IdGrupoFormulario = item.IdGrupoFormulario, IdUsuario = id, Estado = decimal.Parse(constantes.GetValue("Inactivo")) }, EnumAccionRealizar.EliminarUserGrupo);
                            if (regFormUser.HasError())
                                throw new Exception(regFormUser.Error.Detalle);
                        }
                    }                                        
                    ViewDto<TablaUsuariosDto> reg = new ViewDto<TablaUsuariosDto>();
                    reg = bips.RegistrarUsuarios(new ContextoDto(), new TablaUsuariosDto() { Id = id, IdEstado = decimal.Parse(constantes.GetValue("Inactivo")), TipoUpdate = decimal.Parse(constantes.GetValue("UpdateEstadoUser")) }, EnumAccionRealizar.Actualizar);
                    if (reg.HasError())
                        registro = reg.Error.Detalle;
                }
                else{
                    registro = "idUsuario nulo";
                }

            }
            catch (Exception ex){
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return (registro);
        }

        public async Task<IList<int>> getAnosPermisos(string idUserConectado)
        {
            List<int> data = new List<int>();
            try{
                ProgramasModels listaAnos = new ProgramasModels();
                data = (List<int>)await listaAnos.getAnos();
                if (data.Count > 0) {
                    List<TablaUsuariosDto> usuario = new List<TablaUsuariosDto>();
                    usuario = await getUsuariosFiltro(new TablaUsuariosFiltroDto() { Id = idUserConectado, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                    if (usuario.Count > 0){
                        decimal? perfil = usuario.FirstOrDefault().IdPerfil;
                        if (perfil != decimal.Parse(constantes.GetValue("PerfilAdmin"))){
                            data.RemoveAll(p=>p != DateTime.Now.Year);
                        }
                    }                    
                }
            }
            catch(Exception ex){
                log.Error(ex.Message, ex);
                throw ex;
            }
            return data;
        }

        public Task<string> actualizaEstadoUser(TablaUsuariosDto data)
        {
            string registro = "ok";
            try {
                ViewDto<TablaUsuariosDto> reg = new ViewDto<TablaUsuariosDto>();
                reg = bips.RegistrarUsuarios(new ContextoDto(), data, EnumAccionRealizar.Actualizar);
                if (reg.HasError())
                    registro = reg.Error.Detalle;
            }catch(Exception ex){
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return Task.FromResult(registro);
        }
    }
}
