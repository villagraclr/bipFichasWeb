using System;
using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Dto;
using MDS.Logic.Dom;

namespace MDS.Logic.Mgr
{
    /// <summary>
	///	Clase de parametros mgr
	/// </summary>
    public partial class SistemasBIPSMgr : ITablaParametrosMgr
    {
        #region campos publicos
        /// <summary>
        /// campo publico que contiene una instancia del objeto de negocio 
        /// </summary>
        private ITablaParametrosDom iTablaParametrosDom;
        #endregion

        #region metodos publicos
        /// <summary>
        /// metodo que permite buscar los elementos de tipo cb_parametros existentes
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>	
        public ViewDto<TablaParametrosDto> BuscarParametros(ContextoDto p_Contexto, TablaParametrosFiltroDto p_Filtro)
        {
            ViewDto<TablaParametrosDto> viewResponse = new ViewDto<TablaParametrosDto>();
            try
            {
                viewResponse = iTablaParametrosDom.Buscar(p_Contexto, p_Filtro);
                if (viewResponse.HasError())
                {
                    if (viewResponse.Error.Severidad == EnumSeveridad.Warning)
                    {
                        //TODO: Generar error con especificacion de mensaje de datos no encontrados
                        iProviderError.LoadError(ref viewResponse, EnumErrores.BLL_CONSULTA_SIN_DATOS);
                    }
                    else
                    {
                        //TODO: Generar error con especificacion de mensaje de proceso defectuoso
                        iProviderError.LoadError(ref viewResponse, EnumErrores.BLL_ERROR_GENERICO_PROCESO);
                    }                    
                }                
            }
            catch (Exception ex)
            {
                iProviderError.LoadError(ref viewResponse, ex);
            }
            return viewResponse;
        }

        /// <summary>
        /// metodo que permite registrar elementos de tipo cb_parametros en el sistema
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Parametros">cb_parametros a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>        
        public ViewDto<TablaParametrosDto> RegistrarParametros(ContextoDto p_Contexto, TablaParametrosDto p_Parametros, EnumAccionRealizar p_Accion)
        {
            ViewDto<TablaParametrosDto> viewResponse = new ViewDto<TablaParametrosDto>();
            try
            {
                viewResponse = iTablaParametrosDom.Registrar(p_Contexto, p_Parametros, p_Accion);
                if (!viewResponse.Sucess())
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
