using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Dao
{
    /// <summary>
    /// Interfaz de cb_consultas Dao
    /// </summary>
    public partial interface ITablaConsultasDao
    {
        /// <summary>
		/// metodo que permite buscar los registros de CB_CONSULTAS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaConsultasDto> Buscar(ContextoDto p_Contexto, TablaConsultasDto p_Filtro);

        /// <summary>
        /// metodo que permite crear un nuevo registro de CB_CONSULTAS
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_LOG_FORMULARIOS a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaConsultasDto> Insertar(ContextoDto p_Contexto, TablaConsultasDto p_Datos);

        /// <summary>
		/// metodo que permite actualizar un registro de CB_CONSULTAS existente
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Datos">grupo a actualizar</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		ViewDto<TablaConsultasDto> Actualizar(ContextoDto p_Contexto, TablaConsultasDto p_Datos);

        /// <summary>
		/// metodo que permite eliminar un registro de CB_CONSULTAS
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Datos">formulario a eliminar</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		ViewDto<TablaConsultasDto> Eliminar(ContextoDto p_Contexto, TablaConsultasDto p_Datos);
    }
}
