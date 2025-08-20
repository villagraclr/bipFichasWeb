using MDS.Core.Dto;
using MDS.Dto;
using System.Collections.Generic;

namespace MDS.Logic.Mgr
{
    /// <summary>
	/// interfaz de preguntas formularios mgr
	/// </summary>
    public partial interface ITablaPreguntasFormulariosMgr
    {
        /// <summary>
        /// metodo que permite buscar los elementos de tipo cb_preguntas_formularios existentes
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>	
        ViewDto<PreguntasFormulariosDto> BuscarPreguntasFormularios(ContextoDto p_Contexto, TablaPreguntasFormulariosFiltroDto p_Filtro, IList<TablaExcepcionesPreguntasDto> excepPregLectura);

        /// <summary>
        /// metodo que permite buscar los elementos de tipo cb_preguntas existentes
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>	
        ViewDto<TablaPreguntasFormulariosDto> BuscarPreguntas(ContextoDto p_Contexto, TablaPreguntasFormulariosFiltroDto p_Filtro);
    }
}
