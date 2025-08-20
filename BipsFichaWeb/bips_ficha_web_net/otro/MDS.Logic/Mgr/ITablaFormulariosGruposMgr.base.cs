using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Dto;

namespace MDS.Logic.Mgr
{
    /// <summary>
	/// interfaz de formularios grupos mgr
	/// </summary>
    public partial interface ITablaFormulariosGruposMgr
    {
        /// <summary>
        /// metodo que permite buscar los elementos de tipo cb_formularios_grupos existentes
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>	
        ViewDto<TablaFormulariosGruposDto> BuscarFormulariosGrupos(ContextoDto p_Contexto, TablaFormulariosGruposFiltroDto p_Filtro);

        /// <summary>
        /// metodo que permite registrar elementos de tipo cb_formularios_grupos en el sistema
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">cb_formularios_grupos a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>        
        ViewDto<TablaFormulariosGruposDto> RegistrarFormulariosGrupos(ContextoDto p_Contexto, TablaFormulariosGruposDto p_Datos, EnumAccionRealizar p_Accion);
    }
}
