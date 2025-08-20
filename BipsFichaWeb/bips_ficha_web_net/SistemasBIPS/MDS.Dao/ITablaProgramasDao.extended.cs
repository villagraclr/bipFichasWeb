using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Dao
{
    public partial interface ITablaProgramasDao
    {
        /// <summary>
        /// metodo de trapaso de data origen-destino
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_RESPUESTAS a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaProgramasDto> InsertarOrigenDestino(ContextoDto p_Contexto, TablaProgramasDto p_Datos);

        /// <summary>
        /// Metodo de creación de nueva iteración
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_PROGRAMAS a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaProgramasDto> CrearIteracion(ContextoDto p_Contexto, TablaProgramasDto p_Datos);

        /// <summary>
        /// Metodo de busqueda de programas en evaluaciòn ex ante
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Filtro"></param>
        /// <returns>Dto con programas Ex Ante</returns>
        ViewDto<TablaProgramasDto> BuscarExAnte(ContextoDto p_Contexto, TablaProgramasFiltroDto p_Filtro);

        /// <summary>
        /// metodo de trapaso de data origen-destino
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_RESPUESTAS a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaProgramasDto> CalculoEficiencia(ContextoDto p_Contexto, TablaProgramasDto p_Datos);

        /// <summary>
        /// Metodo de busqueda de programas en panel de evaluación ex ante
        /// </summary>
        /// <param name="p_Contexto"></param>
        /// <param name="p_Filtro"></param>
        /// <returns>Dto con programas Ex Ante</returns>
        ViewDto<TablaProgramasDto> PanelExAnte(ContextoDto p_Contexto, TablaProgramasFiltroDto p_Filtro);

        /// <summary>
        /// Metodo de busqueda de programas en panel de carga de beneficiarios RIS
        /// </summary>
        /// <param name="p_Contexto"></param>
        /// <param name="p_Filtro"></param>
        /// <returns>Dto con programas carga RIS</returns>
        ViewDto<TablaProgramasDto> CargaRIS(ContextoDto p_Contexto, TablaProgramasFiltroDto p_Filtro);

        /// <summary>
        /// Metodo de carga de indicadores dashboard
        /// </summary>
        /// <param name="p_Contexto"></param>
        /// <param name="p_Filtro"></param>
        /// <returns>Dto con indicadores dashboard</returns>
        ViewDto<TablaProgramasDto> IndicadoresDashboard(ContextoDto p_Contexto, TablaProgramasFiltroDto p_Filtro);

        /// <summary>
        /// Metodo de busqueda de programas agrupados por iteración
        /// </summary>
        /// <param name="p_Contexto"></param>
        /// <param name="p_Filtro"></param>
        /// <returns>Dto con programas carga RIS</returns>
        ViewDto<TablaProgramasDto> ProgramasXIteracion(ContextoDto p_Contexto, TablaProgramasFiltroDto p_Filtro);

        /// <summary>
        /// Metodo de busqueda de programas en evaluación ex ante gore (perfil)
        /// </summary>
        /// <param name="p_Contexto"></param>
        /// <param name="p_Filtro"></param>
        /// <returns>Dto con programas Ex Ante</returns>
        ViewDto<TablaProgramasDto> BuscarExAnteEvalPerfil(ContextoDto p_Contexto, TablaProgramasFiltroDto p_Filtro);
    }
}