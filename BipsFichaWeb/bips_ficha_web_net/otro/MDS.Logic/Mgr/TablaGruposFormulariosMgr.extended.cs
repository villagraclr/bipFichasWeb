using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Dto;
using System;

namespace MDS.Logic.Mgr
{
    public partial class SistemasBIPSMgr
    {
        /// <summary>
        /// metodo que permite buscar los elementos de tipo cb_grupos_formularios existentes
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>	
        public ViewDto<TablaUsuariosDto> BuscarUsuariosGrupos(ContextoDto p_Contexto, TablaUsuariosFiltroDto p_Filtro)
        {
            ViewDto<TablaUsuariosDto> viewResponse = new ViewDto<TablaUsuariosDto>();
            try
            {
                viewResponse = iTablaGruposFormulariosDom.BuscarUsuarios(p_Contexto, p_Filtro);
                if (viewResponse.HasError())
                {
                    if (viewResponse.Error.Severidad == EnumSeveridad.Warning)
                    {
                        //TODO: Generar error con especificacion de mensaje de datos no encontrados
                        iProviderError.LoadError(ref viewResponse, EnumErrores.BLL_CONSULTA_SIN_DATOS);
                    }
                    else
                    {
                        //TODO: Generar error con especificacion de mensaje de proceso defectuoso
                        iProviderError.LoadError(ref viewResponse, EnumErrores.BLL_ERROR_GENERICO_PROCESO);
                    }
                }
            }
            catch (Exception ex)
            {
                iProviderError.LoadError(ref viewResponse, ex);
            }
            return viewResponse;
        }

        /// <summary>
        /// metodo que permite buscar los elementos de tipo cb_grupos_usuarios existentes
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>	
        public ViewDto<TablaGruposFormulariosDto> BuscarGruposUsuarios(ContextoDto p_Contexto, TablaGruposFormulariosFiltroDto p_Filtro)
        {
            ViewDto<TablaGruposFormulariosDto> viewResponse = new ViewDto<TablaGruposFormulariosDto>();
            try
            {
                viewResponse = iTablaGruposFormulariosDom.BuscarGruposUsuarios(p_Contexto, p_Filtro);
                if (viewResponse.HasError())
                {
                    if (viewResponse.Error.Severidad == EnumSeveridad.Warning)
                    {
                        //TODO: Generar error con especificacion de mensaje de datos no encontrados
                        iProviderError.LoadError(ref viewResponse, EnumErrores.BLL_CONSULTA_SIN_DATOS);
                    }
                    else
                    {
                        //TODO: Generar error con especificacion de mensaje de proceso defectuoso
                        iProviderError.LoadError(ref viewResponse, EnumErrores.BLL_ERROR_GENERICO_PROCESO);
                    }
                }
            }
            catch (Exception ex)
            {
                iProviderError.LoadError(ref viewResponse, ex);
            }
            return viewResponse;
        }
    }
}
