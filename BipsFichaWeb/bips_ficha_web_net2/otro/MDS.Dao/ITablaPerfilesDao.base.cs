using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Dao
{
    /// <summary>
    /// Interfaz de cb_perfiles Dao
    /// </summary>
    public partial interface ITablaPerfilesDao
    {
        /// <summary>
		/// metodo que permite buscar los registros de CB_PERFILES existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaPerfilesDto> Buscar(ContextoDto p_Contexto, TablaPerfilesFiltroDto p_Filtro);
    }
}
