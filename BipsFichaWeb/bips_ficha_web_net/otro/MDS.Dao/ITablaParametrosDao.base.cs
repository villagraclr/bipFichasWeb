using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Dao
{
    public partial interface ITablaParametrosDao
    {
        /// <summary>
		/// metodo que permite crear un nuevo registro de cb_parametros
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Parametros">usuario a crear</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaParametrosDto> Insertar(ContextoDto p_Contexto, TablaParametrosDto p_Datos);

        /// <summary>
		/// metodo que permite actualizar un registro de cb_parametros existente
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Parametros">b_usuariomds a actualizar</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaParametrosDto> Actualizar(ContextoDto p_Contexto, TablaParametrosDto p_Datos);

        /// <summary>
		/// metodo que permite eliminar un registro de cb_parametros existente
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Parametros">b_usuariomds a eliminar</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaParametrosDto> Eliminar(ContextoDto p_Contexto, TablaParametrosDto p_Datos);

        /// <summary>
		/// metodo que permite buscar los registros de cb_parametros existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaParametrosDto> Buscar(ContextoDto p_Contexto, TablaParametrosFiltroDto p_Filtro);
    }
}
