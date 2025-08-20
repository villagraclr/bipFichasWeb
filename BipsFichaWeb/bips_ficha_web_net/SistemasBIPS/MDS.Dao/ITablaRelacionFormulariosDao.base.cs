using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Dao
{
    /// <summary>
    /// Interfaz de cb_relacion_formularios Dao
    /// </summary>
    public partial interface ITablaRelacionFormulariosDao
    {
        /// <summary>
		/// metodo que permite buscar los registros de CB_RELACION_FORMULARIOS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaRelacionFormulariosDto> Buscar(ContextoDto p_Contexto, TablaRelacionFormulariosFiltroDto p_Filtro);

        /// <summary>
        /// metodo que permite crear un nuevo registro de CB_RELACION_FORMULARIOS
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_RELACION_FORMULARIOS a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaRelacionFormulariosDto> Insertar(ContextoDto p_Contexto, TablaRelacionFormulariosDto p_Datos);
    }
}
