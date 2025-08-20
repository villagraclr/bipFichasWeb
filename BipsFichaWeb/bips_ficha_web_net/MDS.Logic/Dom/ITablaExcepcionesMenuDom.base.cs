using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Logic.Dom
{
    /// <summary>
	/// interfaz de cb_excepciones_menu dom
	/// </summary>
    public partial interface ITablaExcepcionesMenuDom
    {
        /// <summary>
		/// metodo que permite buscar los elementos de tipo cb_excepciones_menu existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		ViewDto<TablaExcepcionesMenuDto> Buscar(ContextoDto p_Contexto, TablaExcepcionesMenuFiltroDto p_Filtro);
    }
}
