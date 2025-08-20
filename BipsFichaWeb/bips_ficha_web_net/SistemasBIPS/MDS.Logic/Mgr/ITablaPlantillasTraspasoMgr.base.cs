using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Logic.Mgr
{
    /// <summary>
	/// interfaz de plantillas traspaso mgr
	/// </summary>
    public partial interface ITablaPlantillasTraspasoMgr
    {
        /// <summary>
        /// metodo que permite buscar los elementos de tipo cb_plantillas_traspaso existentes
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>	
        ViewDto<TablaPlantillasTraspasoDto> BuscarPlantillasTraspaso(ContextoDto p_Contexto, TablaPlantillasTraspasoFiltroDto p_Filtro);
    }
}
