using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Dto;

namespace MDS.Logic.Mgr
{
    /// <summary>
	/// interfaz de grupos formularios mgr
	/// </summary>
    public partial interface ITablaGruposFormulariosMgr
    {
        /// <summary>
        /// metodo que permite buscar los elementos de tipo cb_grupos_formularios existentes
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>	
        ViewDto<TablaGruposFormulariosDto> BuscarGruposFormularios(ContextoDto p_Contexto, TablaGruposFormulariosFiltroDto p_Filtro);

        /// <summary>
        /// metodo que permite registrar elementos de tipo cb_grupos_formularios en el sistema
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">cb_grupos_formularios a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>        
        ViewDto<TablaGruposFormulariosDto> RegistrarGruposFormularios(ContextoDto p_Contexto, TablaGruposFormulariosDto p_Datos, EnumAccionRealizar p_Accion);
    }
}
