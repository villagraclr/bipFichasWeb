using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Dao
{
    /// <summary>
    /// Interfaz de cb_consultas Dao
    /// </summary>
    public partial interface ITablaBeneficiariosRisDao
    {
        /// <summary>
		/// metodo que permite buscar los registros de CB_BENEFICIARIOS_RIS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaBenefiariosRisDto> Buscar(ContextoDto p_Contexto, TablaBenefiariosRisDto p_Filtro);

        /// <summary>
        /// metodo que permite crear un nuevo registro de CB_BENEFICIARIOS_RIS
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_CONSULTAS a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaBenefiariosRisDto> Insertar(ContextoDto p_Contexto, TablaBenefiariosRisDto p_Datos);

        /// <summary>
        /// metodo que permite eliminar un registro de CB_RESPUESTAS existente
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_RESPUESTAS a eliminar</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaBenefiariosRisDto> Eliminar(ContextoDto p_Contexto, TablaBenefiariosRisDto p_Datos);
    }
}
