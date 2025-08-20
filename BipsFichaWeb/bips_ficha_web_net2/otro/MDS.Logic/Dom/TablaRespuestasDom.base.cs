using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Core.Providers;
using MDS.Core.Util;
using MDS.Dao;
using MDS.Dto;
using System;

namespace MDS.Logic.Dom
{
    /// <summary>
	///	Clase de cb_respuestas dom
	/// </summary>
    public partial class TablaRespuestasDom : ITablaRespuestasDom
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
        public ITablaRespuestasDao iRespuestasDao;
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        public TablaRespuestasDom()
        {
            iProviderLog = (IProviderLog)Activator.CreateInstance(typeof(ProviderLog));
            iProviderError = (IProviderError)Activator.CreateInstance(typeof(ProviderError));
            iRespuestasDao = (ITablaRespuestasDao)Activator.CreateInstance(typeof(TablaRespuestasDao));
        }
        #endregion

        #region metodos publicos
        /// <summary>
		/// metodo que permite buscar los elementos de tipo cb_respuestas existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		public ViewDto<TablaRespuestasDto> Buscar(ContextoDto p_Contexto, TablaRespuestasFiltroDto p_Filtro)
        {
            ViewDto<TablaRespuestasDto> viewResponse = new ViewDto<TablaRespuestasDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Filtro);
                viewResponse = iRespuestasDao.Buscar(p_Contexto, p_Filtro);
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
        /// metodo que permite registrar elementos de tipo cb_respuestas en el sistema
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">cb_respuestas a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>        
        public ViewDto<TablaRespuestasDto> Registrar(ContextoDto p_Contexto, TablaRespuestasDto p_Datos, EnumAccionRealizar p_Accion)
        {
            ViewDto<TablaRespuestasDto> viewResponse = new ViewDto<TablaRespuestasDto>();

            try
            {
                AssertNull.NotNullOrEmpty(p_Datos);
                switch (p_Accion)
                {
                    case EnumAccionRealizar.Ninguna:
                        viewResponse.Dtos.Add(p_Datos);
                        break;
                    case EnumAccionRealizar.Insertar:
                        viewResponse = iRespuestasDao.Insertar(p_Contexto, p_Datos);
                        break;
                    case EnumAccionRealizar.Eliminar:
                        viewResponse = iRespuestasDao.Eliminar(p_Contexto, p_Datos);
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
