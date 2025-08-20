using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Dao
{
    /// <summary>
    /// Interfaz de cb_excepciones_preguntas Dao
    /// </summary>
    public partial interface ITablaExcepcionesPreguntasDao
    {
        /// <summary>
		/// metodo que permite buscar los registros de CB_EXCEPCIONES_PREGUNTAS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaExcepcionesPreguntasDto> Buscar(ContextoDto p_Contexto, TablaExcepcionesPreguntasFiltroDto p_Filtro);
    }
}
