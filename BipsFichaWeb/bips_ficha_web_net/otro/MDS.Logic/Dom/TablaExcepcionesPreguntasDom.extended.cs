using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Core.Util;
using MDS.Dto;
using System;

namespace MDS.Logic.Dom
{
    public partial class TablaExcepcionesPreguntasDom
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
		/// metodo que permite buscar los elementos de tipo cb_excepciones_plantillas existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		public ViewDto<TablaProgramasDto> BuscarPermisosFormularios(ContextoDto p_Contexto, TablaProgramasFiltroDto p_Filtro)
        {
            ViewDto<TablaProgramasDto> viewResponse = new ViewDto<TablaProgramasDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Filtro);
                viewResponse = iTablaExcepcionesPreguntasDao.BuscarPermisosFormularios(p_Contexto, p_Filtro);
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
		/// metodo que permite buscar los elementos de tipo cb_excepciones_plantillas existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		public ViewDto<TablaExcepcionesPlantillasDto> BuscarPlantillasFormularios(ContextoDto p_Contexto, TablaExcepcionesPlantillasFiltroDto p_Filtro)
        {
            ViewDto<TablaExcepcionesPlantillasDto> viewResponse = new ViewDto<TablaExcepcionesPlantillasDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Filtro);
                viewResponse = iTablaExcepcionesPreguntasDao.BuscarPlantillasFormularios(p_Contexto, p_Filtro);
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
		/// metodo que permite registrar elementos de tipo cb_excepciones_formularios en el sistema
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Datos">excepciones formularios a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		public ViewDto<TablaExcepcionesPermisosDto> Registrar(ContextoDto p_Contexto, TablaExcepcionesPermisosDto p_Datos, EnumAccionRealizar p_Accion)
        {
            ViewDto<TablaExcepcionesPermisosDto> viewResponse = new ViewDto<TablaExcepcionesPermisosDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Datos);
                switch (p_Accion)
                {
                    case EnumAccionRealizar.Ninguna:
                        viewResponse.Dtos.Add(p_Datos);
                        break;
                    case EnumAccionRealizar.Insertar:
                        viewResponse = iTablaExcepcionesPreguntasDao.Insertar(p_Contexto, p_Datos);
                        break;
                    case EnumAccionRealizar.Eliminar:
                        viewResponse = iTablaExcepcionesPreguntasDao.EliminarPermisosUsuarios(p_Contexto, p_Datos);
                        break;
                    default:
                        break;
                }
                if (viewResponse.HasError())
                    iProviderError.LoadError(ref viewResponse, EnumErrores.BLL_ERROR_GENERICO_PROCESO);
            }
            catch (Exception ex)
            {
                iProviderError.LoadError(ref viewResponse, ex);
            }
            return viewResponse;
        }

        /// <summary>
		/// metodo que permite registrar elementos de tipo cb_excepciones_plantillas en el sistema
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Datos">excepciones formularios a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		public ViewDto<TablaExcepcionesPlantillasDto> RegistrarExcepciones(ContextoDto p_Contexto, TablaExcepcionesPlantillasDto p_Datos, EnumAccionRealizar p_Accion)
        {
            ViewDto<TablaExcepcionesPlantillasDto> viewResponse = new ViewDto<TablaExcepcionesPlantillasDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Datos);
                switch (p_Accion)
                {
                    case EnumAccionRealizar.Insertar:
                        viewResponse = iTablaExcepcionesPreguntasDao.InsertarPlantilla(p_Contexto, p_Datos);
                        break;
                    default:
                        break;
                }
                if (viewResponse.HasError())
                    iProviderError.LoadError(ref viewResponse, EnumErrores.BLL_ERROR_GENERICO_PROCESO);
            }
            catch (Exception ex)
            {
                iProviderError.LoadError(ref viewResponse, ex);
            }
            return viewResponse;
        }

        /// <summary>
		/// metodo que permite registrar elementos de tipo cb_excepciones_plantillas_form en el sistema
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Datos">excepciones formularios a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		public ViewDto<TablaExcepcionesPlantillasFormDto> RegistrarExcepcionesForm(ContextoDto p_Contexto, TablaExcepcionesPlantillasFormDto p_Datos, EnumAccionRealizar p_Accion)
        {
            ViewDto<TablaExcepcionesPlantillasFormDto> viewResponse = new ViewDto<TablaExcepcionesPlantillasFormDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Datos);
                switch (p_Accion)
                {
                    case EnumAccionRealizar.Insertar:
                        viewResponse = iTablaExcepcionesPreguntasDao.InsertarPlantillaForm(p_Contexto, p_Datos);
                        break;                    
                    default:
                        break;
                }
                if (viewResponse.HasError())
                    iProviderError.LoadError(ref viewResponse, EnumErrores.BLL_ERROR_GENERICO_PROCESO);
            }
            catch (Exception ex)
            {
                iProviderError.LoadError(ref viewResponse, ex);
            }
            return viewResponse;
        }

        /// <summary>
		/// metodo que permite buscar los elementos de tipo cb_excepciones_plantillas_form existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		public ViewDto<TablaExcepcionesPlantillasFormDto> BuscarExcepcionesPlantillas(ContextoDto p_Contexto, TablaExcepcionesPlantillasFormDto p_Filtro)
        {
            ViewDto<TablaExcepcionesPlantillasFormDto> viewResponse = new ViewDto<TablaExcepcionesPlantillasFormDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Filtro);
                viewResponse = iTablaExcepcionesPreguntasDao.BuscarExcepciones(p_Contexto, p_Filtro);
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
