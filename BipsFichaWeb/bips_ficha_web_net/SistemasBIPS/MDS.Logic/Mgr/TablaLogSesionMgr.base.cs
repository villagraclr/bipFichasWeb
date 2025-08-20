using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Dto;
using MDS.Logic.Dom;
using System;

namespace MDS.Logic.Mgr
{
    /// <summary>
	///	Clase de log sesion mgr
	/// </summary>
    public partial class SistemasBIPSMgr : ITablaLogSesionMgr
    {
        #region campos publicos
        /// <summary>
        /// campo publico que contiene una instancia del objeto de negocio 
        /// </summary>
        private ITablaLogSesionDom iTablaLogSesionDom;
        #endregion

        #region metodos publicos        
        /// <summary>
        /// metodo que permite registrar elementos de tipo cb_log_sesion en el sistema
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">cb_log_sesion a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>        
        public ViewDto<TablaLogSesionDto> RegistrarLogSesion(ContextoDto p_Contexto, TablaLogSesionDto p_Datos, EnumAccionRealizar p_Accion)
        {
            ViewDto<TablaLogSesionDto> viewResponse = new ViewDto<TablaLogSesionDto>();
            try
            {
                viewResponse = iTablaLogSesionDom.Registrar(p_Contexto, p_Datos, p_Accion);
                if (viewResponse.HasError())
                {
                    //TODO: Generar error con especificacion de mensaje de proceso defectuoso
                    iProviderError.LoadError(ref viewResponse, EnumErrores.BLL_ERROR_GENERICO_PROCESO);
                }
            }
            catch (Exception ex)
            {
                iProviderError.LoadError(ref viewResponse, ex);
            }
            return viewResponse;
        }
        #endregion
    }
}
