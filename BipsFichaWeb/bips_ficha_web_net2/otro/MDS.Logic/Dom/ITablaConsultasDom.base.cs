using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Dto;

namespace MDS.Logic.Dom
{
    /// <summary>
	/// interfaz de cb_consultas dom
	/// </summary>
    public partial interface ITablaConsultasDom
    {
        /// <summary>
		/// metodo que permite buscar los elementos de tipo cb_consultas existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		ViewDto<TablaConsultasDto> Buscar(ContextoDto p_Contexto, TablaConsultasDto p_Filtro);

        /// <summary>
        /// metodo que permite registrar elementos de tipo cb_consultas en el sistema
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">cb_log_formularios a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>        
        ViewDto<TablaConsultasDto> Registrar(ContextoDto p_Contexto, TablaConsultasDto p_Datos, EnumAccionRealizar p_Accion);
    }
}
