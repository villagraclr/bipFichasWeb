using System;
using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Core.Providers;
using MDS.Core.Util;
using MDS.Dao;
using MDS.Dto;

namespace MDS.Logic.Dom
{
    public partial class TablaRolesDom : ITablaRolesDom
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
        public ITablaRolesDao iRolesDao;
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        public TablaRolesDom()
        {
            iRolesDao = (ITablaRolesDao)Activator.CreateInstance(typeof(TablaRolesDao));
            iProviderLog = (IProviderLog)Activator.CreateInstance(typeof(ProviderLog));
            iProviderError = (IProviderError)Activator.CreateInstance(typeof(ProviderError));
        }
        #endregion

        #region metodos publicos
        /// <summary>
		/// metodo que permite buscar los elementos de tipo cb_roles existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		public ViewDto<TablaRolesDto> Buscar(ContextoDto p_Contexto, TablaRolesFiltroDto p_Filtro)
        {
            ViewDto<TablaRolesDto> viewResponse = new ViewDto<TablaRolesDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Filtro);
                viewResponse = iRolesDao.Buscar(p_Contexto, p_Filtro);
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
		/// metodo que permite registrar elementos de tipo cb_roles en el sistema
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Roles">cb_roles a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		public ViewDto<TablaRolesDto> Registrar(ContextoDto p_Contexto, TablaRolesDto p_Roles, EnumAccionRealizar p_Accion)
        {
            ViewDto<TablaRolesDto> viewResponse = new ViewDto<TablaRolesDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Roles);
                switch (p_Accion)
                {
                    case EnumAccionRealizar.Ninguna:
                        viewResponse.Dtos.Add(p_Roles);
                        break;
                    case EnumAccionRealizar.Insertar:
                        viewResponse = iRolesDao.Insertar(p_Contexto, p_Roles);
                        break;
                    case EnumAccionRealizar.Actualizar:
                        viewResponse = iRolesDao.Actualizar(p_Contexto, p_Roles);
                        break;
                    case EnumAccionRealizar.Eliminar:
                        viewResponse = iRolesDao.Eliminar(p_Contexto, p_Roles);
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
