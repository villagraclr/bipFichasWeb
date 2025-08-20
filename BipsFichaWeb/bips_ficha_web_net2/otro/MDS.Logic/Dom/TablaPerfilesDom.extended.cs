using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Core.Util;
using MDS.Dto;
using System;

namespace MDS.Logic.Dom
{
    public partial class TablaPerfilesDom
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
		/// metodo que permite buscar los elementos de tipo cb_permisos_perfiles existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		public ViewDto<TablaPerfilesDto> BuscarPermisosPerfiles(ContextoDto p_Contexto, TablaPerfilesFiltroDto p_Filtro)
        {
            ViewDto<TablaPerfilesDto> viewResponse = new ViewDto<TablaPerfilesDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Filtro);
                viewResponse = iTablaPerfilesDao.BuscarPermisosPerfiles(p_Contexto, p_Filtro);
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
		/// metodo que permite buscar los elementos de tipo cb_excepciones_permisos existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		public ViewDto<TablaExcepcionesPermisosDto> BuscarExcepcionesPermisos(ContextoDto p_Contexto, TablaExcepcionesPermisosFiltroDto p_Filtro)
        {
            ViewDto<TablaExcepcionesPermisosDto> viewResponse = new ViewDto<TablaExcepcionesPermisosDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Filtro);
                viewResponse = iTablaPerfilesDao.BuscarExcepcionesPermisos(p_Contexto, p_Filtro);
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
