using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Dto;

namespace MDS.Logic.Mgr
{
    /// <summary>
	/// interfaz de beneficiarios ris mgr
	/// </summary>
    public partial interface ITablaBeneficiariosRisMgr
    {
        /// <summary>
        /// metodo que permite buscar los elementos de tipo cb_beneficiarios_ris existentes
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>	
        ViewDto<TablaBenefiariosRisDto> BuscarBeneficiariosRis(ContextoDto p_Contexto, TablaBenefiariosRisDto p_Filtro);

        /// <summary>
        /// metodo que permite registrar elementos de tipo cb_beneficiarios_ris en el sistema
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">cb_consultas a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>        
        ViewDto<TablaBenefiariosRisDto> RegistrarBeneficiariosRis(ContextoDto p_Contexto, TablaBenefiariosRisDto p_Datos, EnumAccionRealizar p_Accion);
    }
}
