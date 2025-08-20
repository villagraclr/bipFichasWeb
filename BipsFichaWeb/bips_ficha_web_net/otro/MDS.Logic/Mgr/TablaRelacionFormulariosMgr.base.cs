using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Dto;
using MDS.Logic.Dom;
using System;

namespace MDS.Logic.Mgr
{
    /// <summary>
	///	Clase de relacion formularios mgr
	/// </summary>
    public partial class SistemasBIPSMgr : ITablaRelacionFormulariosMgr
    {
        #region campos publicos
        /// <summary>
        /// campo publico que contiene una instancia del objeto de negocio 
        /// </summary>
        private ITablaRelacionFormulariosDom iTablaRelacionFormulariosDom;
        #endregion

        #region metodos publicos
        /// <summary>
        /// metodo que permite buscar los elementos de tipo cb_relacion_formularios existentes
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>	
        public ViewDto<TablaRelacionFormulariosDto> BuscarRelacionFormularios(ContextoDto p_Contexto, TablaRelacionFormulariosFiltroDto p_Filtro)
        {
            ViewDto<TablaRelacionFormulariosDto> viewResponse = new ViewDto<TablaRelacionFormulariosDto>();
            try
            {
                viewResponse = iTablaRelacionFormulariosDom.Buscar(p_Contexto, p_Filtro);
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
        /// metodo que permite registrar elementos de tipo cb_relacion_formularios en el sistema
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">cb_relacion_formularios a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>        
        public ViewDto<TablaRelacionFormulariosDto> RegistrarRelacionFormularios(ContextoDto p_Contexto, TablaRelacionFormulariosDto p_Datos, EnumAccionRealizar p_Accion)
        {
            ViewDto<TablaRelacionFormulariosDto> viewResponse = new ViewDto<TablaRelacionFormulariosDto>();
            try
            {
                viewResponse = iTablaRelacionFormulariosDom.Registrar(p_Contexto, p_Datos, p_Accion);
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
