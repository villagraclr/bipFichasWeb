using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Dao
{
    public partial interface ITablaParametrosUsuariosDao
    {
        /// <summary>
		/// metodo que permite crear un nuevo registro de cb_parametros_usuarios
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Parametros">usuario a crear</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaParametrosUsuariosDto> Insertar(ContextoDto p_Contexto, TablaParametrosUsuariosDto p_Datos);

        /// <summary>
		/// metodo que permite buscar los registros de cb_parametros_usuarios existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		ViewDto<TablaParametrosUsuariosDto> Buscar(ContextoDto p_Contexto, TablaParametrosUsuariosFiltroDto p_Filtro);
    }
}
