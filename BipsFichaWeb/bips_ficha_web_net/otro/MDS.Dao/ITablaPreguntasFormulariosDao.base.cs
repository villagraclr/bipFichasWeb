using MDS.Core.Dto;
using MDS.Dto;
using System.Collections.Generic;

namespace MDS.Dao
{
    /// <summary>
	/// interfaz de cb_preguntas_formularios Dao
	/// </summary>
    public partial interface ITablaPreguntasFormulariosDao
    {
        /// <summary>
		/// metodo que permite buscar los registros de CB_PREGUNTAS_FORMULARIOS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<PreguntasFormulariosDto> Buscar(ContextoDto p_Contexto, TablaPreguntasFormulariosFiltroDto p_Filtro, IList<TablaExcepcionesPreguntasDto> excepPregLectura);

        /// <summary>
		/// metodo que permite buscar los registros de CB_PREGUNTAS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaPreguntasFormulariosDto> BuscarPreguntas(ContextoDto p_Contexto, TablaPreguntasFormulariosFiltroDto p_Filtro);
    }
}
