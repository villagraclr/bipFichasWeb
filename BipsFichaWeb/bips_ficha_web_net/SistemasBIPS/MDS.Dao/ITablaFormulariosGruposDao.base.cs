using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Dao
{
    public partial interface ITablaFormulariosGruposDao
    {
        /// <summary>
		/// metodo que permite buscar los registros de CB_FORMULARIOS_GRUPOS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaFormulariosGruposDto> Buscar(ContextoDto p_Contexto, TablaFormulariosGruposFiltroDto p_Filtro);

        /// <summary>
		/// metodo que permite eliminar un registro de CB_FORMULARIOS_GRUPOS
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Datos">formulario a eliminar</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		ViewDto<TablaFormulariosGruposDto> Eliminar(ContextoDto p_Contexto, TablaFormulariosGruposDto p_Datos);

        /// <summary>
        /// metodo que permite crear un nuevo registro de CB_FORMULARIOS_GRUPOS
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_FORMULARIOS_GRUPOS a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaFormulariosGruposDto> Insertar(ContextoDto p_Contexto, TablaFormulariosGruposDto p_Datos);
    }
}
