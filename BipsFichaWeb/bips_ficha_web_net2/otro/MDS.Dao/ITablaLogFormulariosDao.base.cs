using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Dao
{
    /// <summary>
    /// Interfaz de cb_log_formularios Dao
    /// </summary>
    public partial interface ITablaLogFormulariosDao
    {
        /// <summary>
		/// metodo que permite buscar los registros de CB_LOG_FORMULARIOS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaLogFormulariosDto> Buscar(ContextoDto p_Contexto, TablaLogFormulariosFiltroDto p_Filtro);

        /// <summary>
        /// metodo que permite crear un nuevo registro de CB_LOG_FORMULARIOS
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_LOG_FORMULARIOS a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaLogFormulariosDto> Insertar(ContextoDto p_Contexto, TablaLogFormulariosDto p_Datos);

        /// <summary>
        /// metodo que permite actualizar un registro de CB_LOG_FORMULARIOS existente
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_LOG_FORMULARIOS a actualizar</param>
        /// <returns>objeto contenedor de la información generada por la accion ejecutada</returns>
        ViewDto<TablaLogFormulariosDto> Actualizar(ContextoDto p_Contexto, TablaLogFormulariosDto p_Datos);
    }
}
