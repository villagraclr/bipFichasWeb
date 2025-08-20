using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Dto;
using MDS.Logic.Dom;
using System;

namespace MDS.Logic.Mgr
{
    /// <summary>
	///	Clase de preguntas tablas mgr
	/// </summary>
    public partial class SistemasBIPSMgr : ITablaPreguntasRespuestasMgr
    {
        #region campos publicos
        /// <summary>
        /// campo publico que contiene una instancia del objeto de negocio 
        /// </summary>
        private ITablaPreguntasRespuestasDom iTablaPreguntasRespuestasDom;
        #endregion

        #region metodos publicos
        /// <summary>
        /// metodo que permite buscar los elementos de tipo vw_preguntas_respuestas existentes
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>	
        public ViewDto<TablaPreguntasRespuestasDto> BuscarPreguntasRespuestas(ContextoDto p_Contexto, TablaPreguntasRespuestasFiltroDto p_Filtro, EnumAccionRealizar opcion)
        {
            ViewDto<TablaPreguntasRespuestasDto> viewResponse = new ViewDto<TablaPreguntasRespuestasDto>();
            try
            {
                viewResponse = iTablaPreguntasRespuestasDom.Buscar(p_Contexto, p_Filtro, opcion);
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
        #endregion
    }
}
