using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Dto;

namespace MDS.Logic.Dom
{
    /// <summary>
	/// interfaz de cb_log_sesion dom
	/// </summary>
    public partial interface ITablaLogSesionDom
    {
        /// <summary>
        /// metodo que permite registrar elementos de tipo cb_log_sesion en el sistema
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">cb_log_sesion a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>        
        ViewDto<TablaLogSesionDto> Registrar(ContextoDto p_Contexto, TablaLogSesionDto p_Datos, EnumAccionRealizar p_Accion);
    }
}
