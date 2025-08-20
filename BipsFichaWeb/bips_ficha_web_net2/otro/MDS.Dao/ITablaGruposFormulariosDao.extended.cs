using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Dao
{
    public partial interface ITablaGruposFormulariosDao
    {
        /// <summary>
		/// metodo que permite buscar los registros de CB_GRUPOS_FORMULARIOS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaUsuariosDto> BuscarUsuarios(ContextoDto p_Contexto, TablaUsuariosFiltroDto p_Filtro);

        /// <summary>
		/// metodo que permite eliminar un registro de CB_FORMULARIOS_USUARIOS
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Datos">usuario a eliminar</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		ViewDto<TablaGruposFormulariosDto> EliminarUsuarioGrupo(ContextoDto p_Contexto, TablaGruposFormulariosDto p_Datos);

        /// <summary>
		/// metodo que permite buscar los registros de CB_GRUPOS_USUARIOS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaGruposFormulariosDto> BuscarGruposUsuarios(ContextoDto p_Contexto, TablaGruposFormulariosFiltroDto p_Filtro);
    }
}
