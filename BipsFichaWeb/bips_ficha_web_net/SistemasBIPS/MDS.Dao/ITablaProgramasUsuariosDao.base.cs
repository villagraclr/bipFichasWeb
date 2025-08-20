using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Dao
{
    /// <summary>
	/// interfaz de cb_formularios_usuarios Dao
	/// </summary>
    public partial interface ITablaProgramasUsuariosDao
    {
        /// <summary>
		/// metodo que permite buscar los registros de CB_FORMULARIOS_USUARIOS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaProgramasDto> Buscar(ContextoDto p_Contexto, TablaProgramasFiltroDto p_Filtro);

        /// <summary>
        /// metodo que permite crear un nuevo registro de CB_FORMULARIOS_USUARIOS
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_FORMULARIOS_USUARIOS a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaFormulariosUsuariosDto> Insertar(ContextoDto p_Contexto, TablaFormulariosUsuariosDto p_Datos);

        /// <summary>
        /// metodo que permite eliminar un registro de CB_FORMULARIOS_USUARIOS existente
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_FORMULARIOS_USUARIOS a eliminar</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaFormulariosUsuariosDto> Eliminar(ContextoDto p_Contexto, TablaFormulariosUsuariosDto p_Datos);
    }
}
