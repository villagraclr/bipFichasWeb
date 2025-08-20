using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Dto;

namespace MDS.Logic.Dom
{
    /// <summary>
    /// Interfaz de cb_relacion_formularios dom
    /// </summary>
    public partial interface ITablaRelacionFormulariosDom
    {
        /// <summary>
		/// metodo que permite buscar los elementos de tipo cb_relacion_formularios existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		ViewDto<TablaRelacionFormulariosDto> Buscar(ContextoDto p_Contexto, TablaRelacionFormulariosFiltroDto p_Filtro);

        /// <summary>
		/// metodo que permite registrar elementos de tipo cb_relacion_formularios en el sistema
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Datos">relacion formularios a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		ViewDto<TablaRelacionFormulariosDto> Registrar(ContextoDto p_Contexto, TablaRelacionFormulariosDto p_Datos, EnumAccionRealizar p_Accion);
    }
}
