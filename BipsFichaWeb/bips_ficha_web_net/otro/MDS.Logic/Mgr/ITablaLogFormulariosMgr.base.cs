using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Dto;

namespace MDS.Logic.Mgr
{
    /// <summary>
	/// interfaz de log formularios mgr
	/// </summary>
    public partial interface ITablaLogFormulariosMgr
    {
        /// <summary>
        /// metodo que permite buscar los elementos de tipo cb_log_formularios existentes
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>	
        ViewDto<TablaLogFormulariosDto> BuscarLogFormularios(ContextoDto p_Contexto, TablaLogFormulariosFiltroDto p_Filtro);

        /// <summary>
        /// metodo que permite registrar elementos de tipo cb_log_formularios en el sistema
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">cb_log_formularios a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>        
        ViewDto<TablaLogFormulariosDto> RegistrarLogFormularios(ContextoDto p_Contexto, TablaLogFormulariosDto p_Datos, EnumAccionRealizar p_Accion);
    }
}
