using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Dto;
using System;

namespace MDS.Logic.Mgr
{
    public partial class SistemasBIPSMgr
    {
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
                viewResponse = iTablaExcepcionesPreguntasDom.BuscarPermisosFormularios(p_Contexto, p_Filtro);
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
                viewResponse = iTablaExcepcionesPreguntasDom.BuscarPlantillasFormularios(p_Contexto, p_Filtro);
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
        /// metodo que permite registrar elementos de tipo cb_excepciones_formularios en el sistema
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">cb_excepciones_formularios a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>        
        public ViewDto<TablaExcepcionesPermisosDto> RegistrarExcepcionesFormularios(ContextoDto p_Contexto, TablaExcepcionesPermisosDto p_Datos, EnumAccionRealizar p_Accion)
        {
            ViewDto<TablaExcepcionesPermisosDto> viewResponse = new ViewDto<TablaExcepcionesPermisosDto>();
            try
            {
                viewResponse = iTablaExcepcionesPreguntasDom.Registrar(p_Contexto, p_Datos, p_Accion);
                if (viewResponse.HasError())
                {
                    //TODO: Generar error con especificacion de mensaje de proceso defectuoso
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
        /// metodo que permite registrar elementos de tipo cb_excepciones_plantillas en el sistema
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">cb_excepciones_formularios a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>        
        public ViewDto<TablaExcepcionesPlantillasDto> RegistrarExcepciones(ContextoDto p_Contexto, TablaExcepcionesPlantillasDto p_Datos, EnumAccionRealizar p_Accion)
        {
            ViewDto<TablaExcepcionesPlantillasDto> viewResponse = new ViewDto<TablaExcepcionesPlantillasDto>();
            try
            {
                viewResponse = iTablaExcepcionesPreguntasDom.RegistrarExcepciones(p_Contexto, p_Datos, p_Accion);
                if (viewResponse.HasError())
                {
                    //TODO: Generar error con especificacion de mensaje de proceso defectuoso
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
        /// metodo que permite registrar elementos de tipo cb_excepciones_plantillas_form en el sistema
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">cb_excepciones_formularios a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>        
        public ViewDto<TablaExcepcionesPlantillasFormDto> RegistrarExcepcionesForm(ContextoDto p_Contexto, TablaExcepcionesPlantillasFormDto p_Datos, EnumAccionRealizar p_Accion)
        {
            ViewDto<TablaExcepcionesPlantillasFormDto> viewResponse = new ViewDto<TablaExcepcionesPlantillasFormDto>();
            try
            {
                viewResponse = iTablaExcepcionesPreguntasDom.RegistrarExcepcionesForm(p_Contexto, p_Datos, p_Accion);
                if (viewResponse.HasError())
                {
                    //TODO: Generar error con especificacion de mensaje de proceso defectuoso
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
        public ViewDto<TablaExcepcionesPlantillasFormDto> BuscarExcepcionesPlantilla(ContextoDto p_Contexto, TablaExcepcionesPlantillasFormDto p_Filtro)
        {
            ViewDto<TablaExcepcionesPlantillasFormDto> viewResponse = new ViewDto<TablaExcepcionesPlantillasFormDto>();
            try
            {
                viewResponse = iTablaExcepcionesPreguntasDom.BuscarExcepcionesPlantillas(p_Contexto, p_Filtro);
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
