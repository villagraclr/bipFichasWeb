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
	///	Clase de cb_plantillas_traspaso dom
	/// </summary>
    public partial class TablaPlantillasTraspasoDom : ITablaPlantillasTraspasoDom
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
        public ITablaPlantillasTraspasoDao iTablaPlantillasTraspasoDao;
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        public TablaPlantillasTraspasoDom()
        {
            iProviderLog = (IProviderLog)Activator.CreateInstance(typeof(ProviderLog));
            iProviderError = (IProviderError)Activator.CreateInstance(typeof(ProviderError));
            iTablaPlantillasTraspasoDao = (ITablaPlantillasTraspasoDao)Activator.CreateInstance(typeof(TablaPlantillasTraspasoDao));
        }
        #endregion

        #region metodos publicos
        /// <summary>
		/// metodo que permite buscar los elementos de tipo cb_plantillas_traspaso existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		public ViewDto<TablaPlantillasTraspasoDto> Buscar(ContextoDto p_Contexto, TablaPlantillasTraspasoFiltroDto p_Filtro)
        {
            ViewDto<TablaPlantillasTraspasoDto> viewResponse = new ViewDto<TablaPlantillasTraspasoDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Filtro);
                viewResponse = iTablaPlantillasTraspasoDao.Buscar(p_Contexto, p_Filtro);
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
