using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Dto;

namespace MDS.Logic.Dom
{
    /// <summary>
	/// interfaz de cb_programas dom
	/// </summary>
    public partial interface ITablaProgramasDom
    {
        /// <summary>
		/// metodo que permite buscar los elementos de tipo cb_programas existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		ViewDto<TablaProgramasDto> Buscar(ContextoDto p_Contexto, TablaProgramasFiltroDto p_Filtro, EnumAccionRealizar p_Accion = EnumAccionRealizar.Buscar);

        /// <summary>
		/// metodo que permite registrar elementos de tipo cb_programas en el sistema
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Programas">programa a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		ViewDto<TablaProgramasDto> Registrar(ContextoDto p_Contexto, TablaProgramasDto p_Programas, EnumAccionRealizar p_Accion);
    }
}
