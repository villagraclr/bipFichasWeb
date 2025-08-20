using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Dto;
using MDS.Logic.Dom;
using System;

namespace MDS.Logic.Mgr
{
    /// <summary>
	///	Clase de exepciones preguntas mgr
	/// </summary>
    public partial class SistemasBIPSMgr : ITablaExcepcionesPreguntasMgr
    {
        #region campos publicos
        /// <summary>
        /// campo publico que contiene una instancia del objeto de negocio 
        /// </summary>
        private ITablaExcepcionesPreguntasDom iTablaExcepcionesPreguntasDom;
        #endregion

        #region metodos publicos
        /// <summary>
        /// metodo que permite buscar los elementos de tipo cb_excepciones_preguntas existentes
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>	
        public ViewDto<TablaExcepcionesPreguntasDto> BuscarExcepcionesPreguntas(ContextoDto p_Contexto, TablaExcepcionesPreguntasFiltroDto p_Filtro)
        {
            ViewDto<TablaExcepcionesPreguntasDto> viewResponse = new ViewDto<TablaExcepcionesPreguntasDto>();
            try
            {
                viewResponse = iTablaExcepcionesPreguntasDom.Buscar(p_Contexto, p_Filtro);
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
