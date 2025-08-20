using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Dto;

namespace MDS.Logic.Dom
{
    /// <summary>
	/// interfaz de vw_preguntas_respuestas dom
	/// </summary>
    public partial interface ITablaPreguntasRespuestasDom
    {
        /// <summary>
		/// metodo que permite buscar los elementos de tipo vw_preguntas_respuestas existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		ViewDto<TablaPreguntasRespuestasDto> Buscar(ContextoDto p_Contexto, TablaPreguntasRespuestasFiltroDto p_Filtro, EnumAccionRealizar opcion);
    }
}
