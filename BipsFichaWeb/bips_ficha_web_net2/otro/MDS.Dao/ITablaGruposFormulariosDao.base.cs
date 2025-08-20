using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Dao
{
    /// <summary>
    /// Interfaz de cb_grupos_formularios Dao
    /// </summary>
    public partial interface ITablaGruposFormulariosDao
    {
        /// <summary>
		/// metodo que permite buscar los registros de CB_GRUPOS_FORMULARIOS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaGruposFormulariosDto> Buscar(ContextoDto p_Contexto, TablaGruposFormulariosFiltroDto p_Filtro);

        /// <summary>
        /// metodo que permite crear un nuevo registro de CB_GRUPOS_FORMULARIOS
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_GRUPOS_FORMULARIOS a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaGruposFormulariosDto> Insertar(ContextoDto p_Contexto, TablaGruposFormulariosDto p_Datos);

        /// <summary>
		/// metodo que permite actualizar un registro de CB_GRUPOS_FORMULARIOS existente
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Datos">grupo a actualizar</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		ViewDto<TablaGruposFormulariosDto> Actualizar(ContextoDto p_Contexto, TablaGruposFormulariosDto p_Datos);

        /// <summary>
		/// metodo que permite eliminar un registro de CB_GRUPOS_FORMULARIOS
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Datos">grupo a eliminar</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		ViewDto<TablaGruposFormulariosDto> Eliminar(ContextoDto p_Contexto, TablaGruposFormulariosDto p_Datos);
    }
}
