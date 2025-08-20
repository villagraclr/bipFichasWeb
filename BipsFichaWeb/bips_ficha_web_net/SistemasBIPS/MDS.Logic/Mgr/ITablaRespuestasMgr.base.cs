using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Dto;

namespace MDS.Logic.Mgr
{
    /// <summary>
	/// interfaz de respuestas mgr
	/// </summary>
    public partial interface ITablaRespuestasMgr
    {
        /// <summary>
        /// metodo que permite buscar los elementos de tipo cb_respuestas existentes
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>	
        ViewDto<TablaRespuestasDto> BuscarRespuestas(ContextoDto p_Contexto, TablaRespuestasFiltroDto p_Filtro);

        /// <summary>
        /// metodo que permite registrar elementos de tipo cb_respuestas en el sistema
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">b_programa_datos a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>        
        ViewDto<TablaRespuestasDto> RegistrarRespuestas(ContextoDto p_Contexto, TablaRespuestasDto p_Datos, EnumAccionRealizar p_Accion);
    }
}
