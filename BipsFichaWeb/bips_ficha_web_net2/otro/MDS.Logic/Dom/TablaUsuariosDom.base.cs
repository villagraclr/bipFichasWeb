using System;
using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Core.Providers;
using MDS.Core.Util;
using MDS.Dao;
using MDS.Dto;

namespace MDS.Logic.Dom
{
    /// <summary>
	///	clase de cb_usuarios dom
	/// </summary>
    public partial class TablaUsuariosDom : ITablaUsuariosDom
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
        public ITablaUsuariosDao iUsuariosDao;
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        public TablaUsuariosDom()
        {
            iUsuariosDao = (ITablaUsuariosDao)Activator.CreateInstance(typeof(TablaUsuariosDao));
            iProviderLog = (IProviderLog)Activator.CreateInstance(typeof(ProviderLog));
            iProviderError = (IProviderError)Activator.CreateInstance(typeof(ProviderError));
        }
        #endregion

        #region metodos publicos
        /// <summary>
		/// metodo que permite buscar los elementos de tipo cb_usuarios existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		public ViewDto<TablaUsuariosDto> Buscar(ContextoDto p_Contexto, TablaUsuariosFiltroDto p_Filtro)
        {
            ViewDto<TablaUsuariosDto> viewResponse = new ViewDto<TablaUsuariosDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Filtro);
                viewResponse = iUsuariosDao.Buscar(p_Contexto, p_Filtro);
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
		/// metodo que permite registrar elementos de tipo cb_usuarios en el sistema
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Usuarios">cb_usuarios a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		public ViewDto<TablaUsuariosDto> Registrar(ContextoDto p_Contexto, TablaUsuariosDto p_Usuarios, EnumAccionRealizar p_Accion)
        {
            ViewDto<TablaUsuariosDto> viewResponse = new ViewDto<TablaUsuariosDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Usuarios);
                switch (p_Accion)
                {
                    case EnumAccionRealizar.Ninguna:
                        viewResponse.Dtos.Add(p_Usuarios);
                        break;
                    case EnumAccionRealizar.Actualizar:
                        viewResponse = iUsuariosDao.Actualizar(p_Contexto, p_Usuarios);
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
