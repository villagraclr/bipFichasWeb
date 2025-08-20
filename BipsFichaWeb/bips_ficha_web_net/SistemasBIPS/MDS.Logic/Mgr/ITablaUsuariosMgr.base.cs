using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Dto;

namespace MDS.Logic.Mgr
{
    public partial interface ITablaUsuariosMgr
    {
        /// <summary>
        /// metodo que permite buscar los elementos de tipo b_usuarios existentes
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>	
        ViewDto<TablaUsuariosDto> BuscarUsuarios(ContextoDto p_Contexto, TablaUsuariosFiltroDto p_Filtro);

        /// <summary>
        /// metodo que permite registrar elementos de tipo b_usuarios en el sistema
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Usuarios">b_usuarios a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>        
        ViewDto<TablaUsuariosDto> RegistrarUsuarios(ContextoDto p_Contexto, TablaUsuariosDto p_Usuarios, EnumAccionRealizar p_Accion);
    }
}
