using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Dao
{
    /// <summary>
    /// Interfaz de cb_excepciones_menu Dao
    /// </summary>
    public partial interface ITablaExcepcionesMenuDao
    {
        /// <summary>
		/// metodo que permite buscar los registros de CB_EXCEPCIONES_MENU existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaExcepcionesMenuDto> Buscar(ContextoDto p_Contexto, TablaExcepcionesMenuFiltroDto p_Filtro);
    }
}
