using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Logic.Dom
{
    /// <summary>
	/// interfaz de cb_preguntas_grupos dom
	/// </summary>
    public partial interface ITablaPreguntasGruposDom
    {
        /// <summary>
		/// metodo que permite buscar los elementos de tipo cb_preguntas_grupos existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		ViewDto<TablaPreguntasGruposDto> Buscar(ContextoDto p_Contexto, TablaPreguntasGruposFiltroDto p_Filtro);
    }
}
