using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Dto;

namespace MDS.Logic.Mgr
{
    /// <summary>
	/// interfaz de programas usuarios mgr
	/// </summary>
    public partial interface ITablaProgramasUsuariosMgr
    {
        /// <summary>
        /// metodo que permite buscar los elementos de tipo cb_formularios_plataformas existentes
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>	
        ViewDto<TablaProgramasDto> BuscarProgramasUsuarios(ContextoDto p_Contexto, TablaProgramasFiltroDto p_Filtro);

        /// <summary>
        /// metodo que permite registrar elementos de tipo cb_formularios_usuarios en el sistema
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">cb_formularios_usuarios a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>        
        ViewDto<TablaFormulariosUsuariosDto> RegistrarFormulariosUsuarios(ContextoDto p_Contexto, TablaFormulariosUsuariosDto p_Datos, EnumAccionRealizar p_Accion);
    }
}