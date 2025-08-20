using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Dto;

namespace MDS.Logic.Mgr
{
    /// <summary>
	/// interfaz de parametros usuarios mgr
	/// </summary>
    public partial interface ITablaParametrosUsuariosMgr
    {
        /// <summary>
        /// metodo que permite buscar los elementos de tipo cb_parametros_usuarios existentes
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>	
        ViewDto<TablaParametrosUsuariosDto> BuscarParametrosUsuarios(ContextoDto p_Contexto, TablaParametrosUsuariosFiltroDto p_Filtro);

        /// <summary>
        /// metodo que permite registrar elementos de tipo cb_parametros_usuarios en el sistema
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Parametros">cb_parametros_usuarios a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>        
        ViewDto<TablaParametrosUsuariosDto> RegistrarParametrosUsuarios(ContextoDto p_Contexto, TablaParametrosUsuariosDto p_ParametrosUsuarios, EnumAccionRealizar p_Accion);
    }
}
