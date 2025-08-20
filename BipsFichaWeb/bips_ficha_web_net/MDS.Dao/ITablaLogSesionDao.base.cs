using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Dao
{
    /// <summary>
    /// Interfaz de cb_log_sesion Dao
    /// </summary>
    public partial interface ITablaLogSesionDao
    {
        /// <summary>
        /// metodo que permite crear un nuevo registro de CB_LOG_SESION
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_LOG_SESION a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaLogSesionDto> Insertar(ContextoDto p_Contexto, TablaLogSesionDto p_Datos);
    }
}
