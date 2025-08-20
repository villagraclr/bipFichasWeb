using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Dto;
using MDS.Logic.Dom;
using System;

namespace MDS.Logic.Mgr
{
    /// <summary>
	///	Clase de parametros usuarios mgr
	/// </summary>
    public partial class SistemasBIPSMgr : ITablaParametrosUsuariosMgr
    {
        #region campos publicos
        /// <summary>
        /// campo publico que contiene una instancia del objeto de negocio 
        /// </summary>
        private ITablaParametrosUsuariosDom iTablaParametrosUsuariosDom;
        #endregion

        #region metodos publicos
        /// <summary>
        /// metodo que permite buscar los elementos de tipo cb_parametros_usuarios existentes
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>	
        public ViewDto<TablaParametrosUsuariosDto> BuscarParametrosUsuarios(ContextoDto p_Contexto, TablaParametrosUsuariosFiltroDto p_Filtro)
        {
            ViewDto<TablaParametrosUsuariosDto> viewResponse = new ViewDto<TablaParametrosUsuariosDto>();
            try
            {
                viewResponse = iTablaParametrosUsuariosDom.Buscar(p_Contexto, p_Filtro);
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
        /// metodo que permite registrar elementos de tipo cb_parametros_usuarios en el sistema
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Parametros">cb_parametros_usuarios a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>        
        public ViewDto<TablaParametrosUsuariosDto> RegistrarParametrosUsuarios(ContextoDto p_Contexto, TablaParametrosUsuariosDto p_ParametrosUsuarios, EnumAccionRealizar p_Accion)
        {
            ViewDto<TablaParametrosUsuariosDto> viewResponse = new ViewDto<TablaParametrosUsuariosDto>();
            try
            {
                viewResponse = iTablaParametrosUsuariosDom.Registrar(p_Contexto, p_ParametrosUsuarios, p_Accion);
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
