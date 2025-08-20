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
	///	Clase de cb_grupos_formularios dom
	/// </summary>
    public partial class TablaGruposFormulariosDom : ITablaGruposFormulariosDom
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
        public ITablaGruposFormulariosDao iTablaGruposFormulariosDao;
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        public TablaGruposFormulariosDom()
        {
            iProviderLog = (IProviderLog)Activator.CreateInstance(typeof(ProviderLog));
            iProviderError = (IProviderError)Activator.CreateInstance(typeof(ProviderError));
            iTablaGruposFormulariosDao = (ITablaGruposFormulariosDao)Activator.CreateInstance(typeof(TablaGruposFormulariosDao));
        }
        #endregion

        #region metodos publicos
        /// <summary>
		/// metodo que permite buscar los elementos de tipo cb_grupos_formularios existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		public ViewDto<TablaGruposFormulariosDto> Buscar(ContextoDto p_Contexto, TablaGruposFormulariosFiltroDto p_Filtro)
        {
            ViewDto<TablaGruposFormulariosDto> viewResponse = new ViewDto<TablaGruposFormulariosDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Filtro);
                viewResponse = iTablaGruposFormulariosDao.Buscar(p_Contexto, p_Filtro);
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
		/// metodo que permite registrar elementos de tipo cb_grupos_formularios en el sistema
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Datos">grupos formularios a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		public ViewDto<TablaGruposFormulariosDto> Registrar(ContextoDto p_Contexto, TablaGruposFormulariosDto p_Datos, EnumAccionRealizar p_Accion)
        {
            ViewDto<TablaGruposFormulariosDto> viewResponse = new ViewDto<TablaGruposFormulariosDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Datos);
                switch (p_Accion)
                {
                    case EnumAccionRealizar.Ninguna:
                        viewResponse.Dtos.Add(p_Datos);
                        break;
                    case EnumAccionRealizar.Insertar:
                        viewResponse = iTablaGruposFormulariosDao.Insertar(p_Contexto, p_Datos);
                        break;
                    case EnumAccionRealizar.Actualizar:
                        viewResponse = iTablaGruposFormulariosDao.Actualizar(p_Contexto, p_Datos);
                        break;
                    case EnumAccionRealizar.Eliminar:
                        viewResponse = iTablaGruposFormulariosDao.Eliminar(p_Contexto, p_Datos);
                        break;
                    case EnumAccionRealizar.EliminarUserGrupo:
                        viewResponse = iTablaGruposFormulariosDao.EliminarUsuarioGrupo(p_Contexto, p_Datos);
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
