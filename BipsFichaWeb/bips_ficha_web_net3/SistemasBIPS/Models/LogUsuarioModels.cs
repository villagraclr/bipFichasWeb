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
using System.Text;
using System.Threading.Tasks;

namespace SistemasBIPS.Models
{
    public class LogUsuarioModels
    {
        #region Constantes
        private static ISistemasBIPSSvc bips = null;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private IProviderConstante constantes = null;
        #endregion

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public LogUsuarioModels()
        {
            bips = (ISistemasBIPSSvc)Activator.CreateInstance(typeof(SistemasBIPSSvc));
            constantes = (IProviderConstante)Activator.CreateInstance(typeof(ProviderConstante));
        }

        /// <summary>
        /// Registra log de sesion de usuarios
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task<Boolean> registraLogSesion(TablaLogSesionDto data)
        {
            Boolean estado = true;
            try {
                ViewDto<TablaLogSesionDto> guardado = new ViewDto<TablaLogSesionDto>();
                guardado = bips.RegistrarLogSesion(new ContextoDto(), data, EnumAccionRealizar.Insertar);
                if (!guardado.Sucess())
                    throw new Exception("error guardado log sesion");
            }
            catch (Exception ex){
                estado = false;
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(estado);
        }

        /// <summary>
        /// Registra log de formularios de usuarios
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task<Boolean> registraLogFormularios(TablaLogFormulariosDto data)
        {
            Boolean estado = true;
            try{
                ViewDto<TablaLogFormulariosDto> guardado = new ViewDto<TablaLogFormulariosDto>();
                guardado = bips.RegistrarLogFormularios(new ContextoDto(), data, EnumAccionRealizar.Insertar);
                if (!guardado.Sucess())
                    throw new Exception("error guardado log formularios");
            }
            catch (Exception ex){
                estado = false;
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(estado);
        }

        /// <summary>
        /// Actualiza log de formularios de usuarios
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task<Boolean> actualizaLogFormularios(TablaLogFormulariosDto data)
        {
            Boolean estado = true;
            try{
                ViewDto<TablaLogFormulariosDto> update = new ViewDto<TablaLogFormulariosDto>();
                update = bips.RegistrarLogFormularios(new ContextoDto(), data, EnumAccionRealizar.Actualizar);
                if (!update.Sucess())
                    throw new Exception("error update log formularios");
            }catch(Exception ex){
                estado = false;
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(estado);
        }

        /// <summary>
        /// Buscar formularios tomados por un usuario
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public Task<IList<TablaLogFormulariosDto>> buscaLogFormularios(TablaLogFormulariosFiltroDto filtros)
        {
            IList<TablaLogFormulariosDto> listaLogFormularios = new List<TablaLogFormulariosDto>();
            try {
                ViewDto<TablaLogFormulariosDto> buscar = new ViewDto<TablaLogFormulariosDto>();
                buscar = bips.BuscarLogFormularios(new ContextoDto(), filtros);
                if (buscar.HasElements())
                    listaLogFormularios = buscar.Dtos;
            }
            catch(Exception ex){
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(listaLogFormularios);
        }
    }
}
