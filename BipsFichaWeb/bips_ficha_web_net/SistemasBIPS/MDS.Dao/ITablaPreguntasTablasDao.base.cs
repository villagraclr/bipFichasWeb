using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Dao
{
    /// <summary>
	/// interfaz de cb_preguntas_tablas Dao
	/// </summary>
    public partial interface ITablaPreguntasTablasDao
    {
        /// <summary>
		/// metodo que permite buscar los registros de CB_PREGUNTAS_TABLAS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaPreguntasTablasDto> Buscar(ContextoDto p_Contexto, TablaPreguntasTablasFiltroDto p_Filtro);
    }
}
