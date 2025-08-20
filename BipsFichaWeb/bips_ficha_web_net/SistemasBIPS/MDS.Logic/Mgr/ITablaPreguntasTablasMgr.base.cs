using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Logic.Mgr
{
    /// <summary>
	/// interfaz de preguntas tablas mgr
	/// </summary>
    public partial interface ITablaPreguntasTablasMgr
    {
        /// <summary>
        /// metodo que permite buscar los elementos de tipo cb_preguntas_tablas existentes
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>	
        ViewDto<TablaPreguntasTablasDto> BuscarPreguntasTablas(ContextoDto p_Contexto, TablaPreguntasTablasFiltroDto p_Filtro);
    }
}
