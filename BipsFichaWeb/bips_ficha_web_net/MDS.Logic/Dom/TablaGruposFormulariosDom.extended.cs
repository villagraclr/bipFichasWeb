using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Core.Util;
using MDS.Dto;
using System;

namespace MDS.Logic.Dom
{
    public partial class TablaGruposFormulariosDom
    {
        #region campos privados
        #endregion

        #region campos publicos
        #endregion

        #region constructores
        #endregion

        #region metodos privados
        #endregion

        #region metodos publicos
        /// <summary>
		/// metodo que permite buscar los elementos de tipo cb_grupos_formularios existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		public ViewDto<TablaUsuariosDto> BuscarUsuarios(ContextoDto p_Contexto, TablaUsuariosFiltroDto p_Filtro)
        {
            ViewDto<TablaUsuariosDto> viewResponse = new ViewDto<TablaUsuariosDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Filtro);
                viewResponse = iTablaGruposFormulariosDao.BuscarUsuarios(p_Contexto, p_Filtro);
                if (!viewResponse.Sucess())
                {
                    if (viewResponse.Error.Severidad == EnumSeveridad.Warning)
                        iProviderError.LoadError(ref viewResponse, EnumErrores.BLL_CONSULTA_SIN_DATOS);
                    else
                        iProviderError.LoadError(ref viewResponse, EnumErrores.BLL_ERROR_GENERICO_PROCESO);
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
                AssertNull.NotNullOrEmpty(p_Filtro);
                viewResponse = iTablaGruposFormulariosDao.BuscarGruposUsuarios(p_Contexto, p_Filtro);
                if (!viewResponse.Sucess())
                {
                    if (viewResponse.Error.Severidad == EnumSeveridad.Warning)
                        iProviderError.LoadError(ref viewResponse, EnumErrores.BLL_CONSULTA_SIN_DATOS);
                    else
                        iProviderError.LoadError(ref viewResponse, EnumErrores.BLL_ERROR_GENERICO_PROCESO);
                }
            }
            catch (Exception ex)
            {
                iProviderError.LoadError(ref viewResponse, ex);
            }
            return viewResponse;
        }
        #endregion
    }
}
