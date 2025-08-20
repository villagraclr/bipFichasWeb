using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Dto;

namespace MDS.Logic.Mgr
{
    /// <summary>
	/// interfaz de respuestas consultas mgr
	/// </summary>
    public partial interface ITablaRespuestasConsultasMgr
    {
        /// <summary>
        /// metodo que permite buscar los elementos de tipo cb_respuestas_consultas existentes
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>	
        ViewDto<TablaRespuestasConsultasDto> BuscarRespuestasConsultas(ContextoDto p_Contexto, TablaRespuestasConsultasDto p_Filtro);

        /// <summary>
        /// metodo que permite registrar elementos de tipo cb_respuestas_consultas en el sistema
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">cb_respuestas_consultas a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>        
        ViewDto<TablaRespuestasConsultasDto> RegistrarRespuestasConsultas(ContextoDto p_Contexto, TablaRespuestasConsultasDto p_Datos, EnumAccionRealizar p_Accion);
    }
}
