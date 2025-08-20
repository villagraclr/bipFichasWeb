using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Dao
{
    /// <summary>
    /// Interfaz de cb_respuestas_consultas Dao
    /// </summary>
    public partial interface ITablaRespuestasConsultasDao
    {
        /// <summary>
		/// metodo que permite buscar los registros de CB_RESPUESTAS_CONSULTAS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaRespuestasConsultasDto> Buscar(ContextoDto p_Contexto, TablaRespuestasConsultasDto p_Filtro);

        /// <summary>
        /// metodo que permite crear un nuevo registro de CB_RESPUESTAS_CONSULTAS
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_RESPUESTAS_CONSULTAS a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaRespuestasConsultasDto> Insertar(ContextoDto p_Contexto, TablaRespuestasConsultasDto p_Datos);
    }
}
