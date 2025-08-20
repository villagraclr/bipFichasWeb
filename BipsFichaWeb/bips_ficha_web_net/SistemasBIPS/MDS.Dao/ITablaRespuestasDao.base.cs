using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Dao
{
    /// <summary>
	/// interfaz de cb_respuestas Dao
	/// </summary>
    public partial interface ITablaRespuestasDao
    {
        /// <summary>
		/// metodo que permite buscar los registros de CB_RESPUESTAS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaRespuestasDto> Buscar(ContextoDto p_Contexto, TablaRespuestasFiltroDto p_Filtro);

        /// <summary>
        /// metodo que permite crear un nuevo registro de CB_RESPUESTAS
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_RESPUESTAS a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaRespuestasDto> Insertar(ContextoDto p_Contexto, TablaRespuestasDto p_Datos);        

        /// <summary>
        /// metodo que permite eliminar un registro de CB_RESPUESTAS existente
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_RESPUESTAS a eliminar</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaRespuestasDto> Eliminar(ContextoDto p_Contexto, TablaRespuestasDto p_Datos);
    }
}
