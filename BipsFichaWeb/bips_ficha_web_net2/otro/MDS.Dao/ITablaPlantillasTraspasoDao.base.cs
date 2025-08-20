using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Dao
{
    /// <summary>
    /// Interfaz de cb_plantillas_traspaso Dao
    /// </summary>
    public partial interface ITablaPlantillasTraspasoDao
    {
        /// <summary>
		/// metodo que permite buscar los registros de CB_PLANTILLAS_TRASPASO existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaPlantillasTraspasoDto> Buscar(ContextoDto p_Contexto, TablaPlantillasTraspasoFiltroDto p_Filtro);
    }
}
