using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Logic.Mgr
{
    public partial interface ITablaGruposFormulariosMgr
    {
        /// <summary>
        /// metodo que permite buscar los elementos de tipo cb_grupos_formularios existentes
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>	
        ViewDto<TablaUsuariosDto> BuscarUsuariosGrupos(ContextoDto p_Contexto, TablaUsuariosFiltroDto p_Filtro);

        /// <summary>
        /// metodo que permite buscar los elementos de tipo cb_grupos_usuarios existentes
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>	
        ViewDto<TablaGruposFormulariosDto> BuscarGruposUsuarios(ContextoDto p_Contexto, TablaGruposFormulariosFiltroDto p_Filtro);
    }
}
