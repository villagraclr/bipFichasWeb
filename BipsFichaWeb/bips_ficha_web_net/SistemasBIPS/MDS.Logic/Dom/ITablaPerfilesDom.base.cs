using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Logic.Dom
{
    /// <summary>
	/// interfaz de cb_perfiles dom
	/// </summary>
    public partial interface ITablaPerfilesDom
    {
        /// <summary>
		/// metodo que permite buscar los elementos de tipo cb_perfiles existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		ViewDto<TablaPerfilesDto> Buscar(ContextoDto p_Contexto, TablaPerfilesFiltroDto p_Filtro);
    }
}
