using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Dao
{
    public partial interface ITablaPerfilesDao
    {
        /// <summary>
		/// metodo que permite buscar los registros de CB_PERFILES existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaPerfilesDto> BuscarPermisosPerfiles(ContextoDto p_Contexto, TablaPerfilesFiltroDto p_Filtro);

        /// <summary>
		/// metodo que permite buscar los registros de CB_EXCEPCIONES_PERMISOS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaExcepcionesPermisosDto> BuscarExcepcionesPermisos(ContextoDto p_Contexto, TablaExcepcionesPermisosFiltroDto p_Filtro);
    }
}
