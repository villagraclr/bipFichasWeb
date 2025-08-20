using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Dao
{
    /// <summary>
	/// interfaz de cb_preguntas_grupos Dao
	/// </summary>
    public partial interface ITablaPreguntasGruposDao
    {
        /// <summary>
		/// metodo que permite buscar los registros de CB_PREGUNTAS_GRUPOS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaPreguntasGruposDto> Buscar(ContextoDto p_Contexto, TablaPreguntasGruposFiltroDto p_Filtro);
    }
}
