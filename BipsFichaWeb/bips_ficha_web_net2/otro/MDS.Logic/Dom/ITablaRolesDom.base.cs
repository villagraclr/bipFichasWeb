using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Dto;

namespace MDS.Logic.Dom
{
    public partial interface ITablaRolesDom
    {
        /// <summary>
		/// metodo que permite buscar los elementos de tipo cb_roles existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaRolesDto> Buscar(ContextoDto p_Contexto, TablaRolesFiltroDto p_Filtro);

        /// <summary>
		/// metodo que permite registrar elementos de tipo cb_roles en el sistema
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Roles">cb_roles a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		ViewDto<TablaRolesDto> Registrar(ContextoDto p_Contexto, TablaRolesDto p_Roles, EnumAccionRealizar p_Accion);
    }
}
