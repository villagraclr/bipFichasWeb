using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Logic.Dom
{
    /// <summary>
	/// interfaz de cb_funciones_dependencias dom
	/// </summary>
    public partial interface ITablaFuncionesDependenciasDom
    {
        /// <summary>
		/// metodo que permite buscar los elementos de tipo cb_funciones_dependencias existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		ViewDto<TablaFuncionesDependenciasDto> Buscar(ContextoDto p_Contexto, TablaFuncionesDependenciasFiltroDto p_Filtro);
    }
}
