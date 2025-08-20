using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Dao
{
    /// <summary>
	/// interfaz de cb_usuarios Dao
	/// </summary>
    public partial interface ITablaUsuariosDao
    {       
        /// <summary>
		/// metodo que permite actualizar un registro de cb_usuarios existente
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Usuarios">usuario a actualizar</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaUsuariosDto> Actualizar(ContextoDto p_Contexto, TablaUsuariosDto p_Usuarios);       

        /// <summary>
		/// metodo que permite buscar los registros de cb_usuarios existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaUsuariosDto> Buscar(ContextoDto p_Contexto, TablaUsuariosFiltroDto p_Filtro);
    }
}
