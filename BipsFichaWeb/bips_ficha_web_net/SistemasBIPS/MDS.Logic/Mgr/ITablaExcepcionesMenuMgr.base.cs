using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Logic.Mgr
{
    /// <summary>
	/// interfaz de exepciones menu mgr
	/// </summary>
    public partial interface ITablaExcepcionesMenuMgr
    {
        /// <summary>
        /// metodo que permite buscar los elementos de tipo cb_excepciones_menu existentes
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>	
        ViewDto<TablaExcepcionesMenuDto> BuscarExcepcionesMenu(ContextoDto p_Contexto, TablaExcepcionesMenuFiltroDto p_Filtro);
    }
}
