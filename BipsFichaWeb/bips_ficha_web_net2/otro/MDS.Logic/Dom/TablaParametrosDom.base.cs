using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Core.Providers;
using MDS.Core.Util;
using MDS.Dao;
using MDS.Dto;
using System;

namespace MDS.Logic.Dom
{
    public partial class TablaParametrosDom : ITablaParametrosDom
    {
        #region campos publicos
        /// <summary>
        /// campo publico que contiene una instancia de la clase que provee la logica de loging
        /// </summary>
        public IProviderLog iProviderLog;
        /// <summary>
        /// campo publico que contiene una instancia de la clase que provee la logica de generacion de errores
        /// </summary>
        public IProviderError iProviderError;
        /// <summary>
        /// campo publico que contiene una instancia del objeto encargado del acceso a datos
        /// </summary>
        public ITablaParametrosDao iParametrosDao;
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        public TablaParametrosDom()
        {
            iParametrosDao = (ITablaParametrosDao)Activator.CreateInstance(typeof(TablaParametrosDao));
            iProviderLog = (IProviderLog)Activator.CreateInstance(typeof(ProviderLog));
            iProviderError = (IProviderError)Activator.CreateInstance(typeof(ProviderError));
        }
        #endregion

        #region metodos publicos
        /// <summary>
		/// metodo que permite buscar los elementos de tipo cb_parametros existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		public ViewDto<TablaParametrosDto> Buscar(ContextoDto p_Contexto, TablaParametrosFiltroDto p_Filtro)
        {
            ViewDto<TablaParametrosDto> viewResponse = new ViewDto<TablaParametrosDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Filtro);
                viewResponse = iParametrosDao.Buscar(p_Contexto, p_Filtro);
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
		/// metodo que permite registrar elementos de tipo cb_parametros en el sistema
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Parametros">cb_parametros a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		public ViewDto<TablaParametrosDto> Registrar(ContextoDto p_Contexto, TablaParametrosDto p_Parametros, EnumAccionRealizar p_Accion)
        {
            ViewDto<TablaParametrosDto> viewResponse = new ViewDto<TablaParametrosDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Parametros);
                switch (p_Accion)
                {
                    case EnumAccionRealizar.Ninguna:
                        viewResponse.Dtos.Add(p_Parametros);
                        break;
                    case EnumAccionRealizar.Insertar:
                        viewResponse = iParametrosDao.Insertar(p_Contexto, p_Parametros);
                        break;
                    case EnumAccionRealizar.Actualizar:
                        viewResponse = iParametrosDao.Actualizar(p_Contexto, p_Parametros);
                        break;
                    case EnumAccionRealizar.Eliminar:
                        viewResponse = iParametrosDao.Eliminar(p_Contexto, p_Parametros);
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
        #endregion
    }
}
