using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Logic.Mgr
{
    /// <summary>
	/// interfaz de excepciones preguntas mgr
	/// </summary>
    public partial interface ITablaExcepcionesPreguntasMgr
    {
        /// <summary>
        /// metodo que permite buscar los elementos de tipo cb_excepciones_preguntas existentes
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>	
        ViewDto<TablaExcepcionesPreguntasDto> BuscarExcepcionesPreguntas(ContextoDto p_Contexto, TablaExcepcionesPreguntasFiltroDto p_Filtro);
    }
}
