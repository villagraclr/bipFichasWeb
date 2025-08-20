using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Dao
{
    /// <summary>
	/// interfaz de cb_menu_formularios Dao
	/// </summary>
    public partial interface ITablaMenuFormulariosDao
    {
        /// <summary>
		/// metodo que permite buscar los registros de cb_menu_formularios existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaMenuFormulariosDto> Buscar(ContextoDto p_Contexto, TablaMenuFormulariosFiltroDto p_Filtro);
    }
}
