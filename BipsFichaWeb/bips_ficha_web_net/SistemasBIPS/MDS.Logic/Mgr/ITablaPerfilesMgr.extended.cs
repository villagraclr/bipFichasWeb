using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Logic.Mgr
{
    public partial interface ITablaPerfilesMgr
    {
        /// <summary>
        /// metodo que permite buscar los elementos de tipo cb_permisos_perfiles existentes
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>	
        ViewDto<TablaPerfilesDto> BuscarPermisosPerfiles(ContextoDto p_Contexto, TablaPerfilesFiltroDto p_Filtro);

        /// <summary>
        /// metodo que permite buscar los elementos de tipo cb_excepciones_permisos existentes
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>	
        ViewDto<TablaExcepcionesPermisosDto> BuscarExcepcionesPermisos(ContextoDto p_Contexto, TablaExcepcionesPermisosFiltroDto p_Filtro);
    }
}
