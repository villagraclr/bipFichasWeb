using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Dto;
using MDS.Logic.Dom;
using System;
using System.Collections.Generic;

namespace MDS.Logic.Mgr
{
    /// <summary>
	///	Clase de preguntas formularios mgr
	/// </summary>
    public partial class SistemasBIPSMgr : ITablaPreguntasFormulariosMgr
    {
        #region campos publicos
        /// <summary>
        /// campo publico que contiene una instancia del objeto de negocio 
        /// </summary>
        private ITablaPreguntasFormulariosDom iTablaPreguntasFormulariosDom;
        #endregion

        #region metodos publicos
        /// <summary>
        /// metodo que permite buscar los elementos de tipo cb_preguntas_formularios existentes
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>	
        public ViewDto<PreguntasFormulariosDto> BuscarPreguntasFormularios(ContextoDto p_Contexto, TablaPreguntasFormulariosFiltroDto p_Filtro, IList<TablaExcepcionesPreguntasDto> excepPregLectura)
        {
            ViewDto<PreguntasFormulariosDto> viewResponse = new ViewDto<PreguntasFormulariosDto>();
            try
            {
                viewResponse = iTablaPreguntasFormulariosDom.Buscar(p_Contexto, p_Filtro, excepPregLectura);
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
        /// metodo que permite buscar los elementos de tipo cb_preguntas existentes
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>	
        public ViewDto<TablaPreguntasFormulariosDto> BuscarPreguntas(ContextoDto p_Contexto, TablaPreguntasFormulariosFiltroDto p_Filtro)
        {
            ViewDto<TablaPreguntasFormulariosDto> viewResponse = new ViewDto<TablaPreguntasFormulariosDto>();
            try
            {
                viewResponse = iTablaPreguntasFormulariosDom.BuscarPreguntas(p_Contexto, p_Filtro);
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
