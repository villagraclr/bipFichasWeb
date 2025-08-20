using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Dao
{
    public partial interface ITablaRolesDao
    {
        /// <summary>
        /// metodo que permite crear un nuevo registro de cb_roles
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Roles">usuario a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaRolesDto> Insertar(ContextoDto p_Contexto, TablaRolesDto p_Roles);

        /// <summary>
		/// metodo que permite actualizar un registro de cb_roles existente
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Roles">b_usuariomds a actualizar</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		ViewDto<TablaRolesDto> Actualizar(ContextoDto p_Contexto, TablaRolesDto p_Roles);

        /// <summary>
        /// metodo que permite eliminar un registro de cb_roles existente
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Roles">b_usuariomds a eliminar</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaRolesDto> Eliminar(ContextoDto p_Contexto, TablaRolesDto p_Roles);

        /// <summary>
		/// metodo que permite buscar los registros de cb_roles existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>	
		ViewDto<TablaRolesDto> Buscar(ContextoDto p_Contexto, TablaRolesFiltroDto p_Filtro);
    }
}
