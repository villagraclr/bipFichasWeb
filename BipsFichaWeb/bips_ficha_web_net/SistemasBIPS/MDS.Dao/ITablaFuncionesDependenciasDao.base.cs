using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Dao
{
    /// <summary>
	/// interfaz de cb_funciones_dependencias Dao
	/// </summary>
    public partial interface ITablaFuncionesDependenciasDao
    {
        /// <summary>
		/// metodo que permite buscar los registros de CB_FUNCIONES_DEPENDENCIAS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaFuncionesDependenciasDto> Buscar(ContextoDto p_Contexto, TablaFuncionesDependenciasFiltroDto p_Filtro);
    }
}
