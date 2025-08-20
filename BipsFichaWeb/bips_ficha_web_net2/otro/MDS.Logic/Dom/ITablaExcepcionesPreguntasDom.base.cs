using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Logic.Dom
{
    /// <summary>
	/// interfaz de cb_excepciones_preguntas dom
	/// </summary>
    public partial interface ITablaExcepcionesPreguntasDom
    {
        /// <summary>
		/// metodo que permite buscar los elementos de tipo cb_excepciones_preguntas existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		ViewDto<TablaExcepcionesPreguntasDto> Buscar(ContextoDto p_Contexto, TablaExcepcionesPreguntasFiltroDto p_Filtro);
    }
}
