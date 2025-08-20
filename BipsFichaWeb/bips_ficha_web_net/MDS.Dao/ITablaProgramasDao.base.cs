using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Dao
{
    /// <summary>
	/// interfaz de cb_programas Dao
	/// </summary>
    public partial interface ITablaProgramasDao
    {        
        /// <summary>
		/// metodo que permite buscar los registros de cb_programas existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		ViewDto<TablaProgramasDto> Buscar(ContextoDto p_Contexto, TablaProgramasFiltroDto p_Filtro);

        /// <summary>
        /// Metodo que permite buscar los registros de años en cb_programas existentes
        /// </summary>
        /// <param name="p_Contexto">Información del contexto</param>
        /// <param name="p_Filtro">Información de filtrado para realizar la busqueda</param>
        /// <returns>Objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaProgramasDto> BuscarAnos(ContextoDto p_Contexto, TablaProgramasFiltroDto p_Filtro);

        /// <summary>
        /// Metodo que permite buscar los registros de años en cb_programas existentes
        /// </summary>
        /// <param name="p_Contexto">Información del contexto</param>
        /// <param name="p_Filtro">Información de filtrado para realizar la busqueda</param>
        /// <returns>Objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaProgramasDto> BuscarAnosGores(ContextoDto p_Contexto, TablaProgramasFiltroDto p_Filtro);

        /// <summary>
        /// metodo que permite crear un nuevo registro de CB_FORMULARIOS
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_FORMULARIOS a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaProgramasDto> Insertar(ContextoDto p_Contexto, TablaProgramasDto p_Datos);

        /// <summary>
		/// metodo que permite eliminar un registro de CB_PROGRAMAS
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Datos">formulario a eliminar</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		ViewDto<TablaProgramasDto> Eliminar(ContextoDto p_Contexto, TablaProgramasDto p_Datos);

        /// <summary>
		/// metodo que permite actualizar un registro de CB_PROGRAMAS existente
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Datos">grupo a actualizar</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		ViewDto<TablaProgramasDto> Actualizar(ContextoDto p_Contexto, TablaProgramasDto p_Datos);
    }
}
