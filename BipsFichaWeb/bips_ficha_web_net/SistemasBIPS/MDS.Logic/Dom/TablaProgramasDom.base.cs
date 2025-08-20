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
	///	Clase de b_programas dom
	/// </summary>
    public partial class TablaProgramasDom : ITablaProgramasDom
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
        public ITablaProgramasDao iProgramasDao;
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        public TablaProgramasDom()
        {
            iProviderLog = (IProviderLog)Activator.CreateInstance(typeof(ProviderLog));
            iProviderError = (IProviderError)Activator.CreateInstance(typeof(ProviderError));
            iProgramasDao = (ITablaProgramasDao)Activator.CreateInstance(typeof(TablaProgramasDao));            
        }
        #endregion

        #region metodos publicos
        /// <summary>
		/// metodo que permite buscar los elementos de tipo cb_programas existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		public ViewDto<TablaProgramasDto> Buscar(ContextoDto p_Contexto, TablaProgramasFiltroDto p_Filtro, EnumAccionRealizar p_Accion = EnumAccionRealizar.Buscar)
        {
            ViewDto<TablaProgramasDto> viewResponse = new ViewDto<TablaProgramasDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Filtro);
                switch (p_Accion)
                {
                    case EnumAccionRealizar.Buscar:
                        viewResponse = iProgramasDao.Buscar(p_Contexto, p_Filtro);
                        break;
                    case EnumAccionRealizar.BuscarAnos:
                        viewResponse = iProgramasDao.BuscarAnos(p_Contexto, p_Filtro);
                        break;
                    case EnumAccionRealizar.BuscarAnosGores:
                        viewResponse = iProgramasDao.BuscarAnosGores(p_Contexto, p_Filtro);
                        break;
                    case EnumAccionRealizar.BuscarExAnte:
                        viewResponse = iProgramasDao.BuscarExAnte(p_Contexto, p_Filtro);
                        break;
                    case EnumAccionRealizar.BuscarPanelExAnte:
                        viewResponse = iProgramasDao.PanelExAnte(p_Contexto, p_Filtro);
                        break;
                    case EnumAccionRealizar.BuscarPanelCargaRIS:
                        viewResponse = iProgramasDao.CargaRIS(p_Contexto, p_Filtro);
                        break;
                    case EnumAccionRealizar.BuscarIndicDashboard:
                        viewResponse = iProgramasDao.IndicadoresDashboard(p_Contexto, p_Filtro);
                        break;
                    case EnumAccionRealizar.BuscarProgramasXIteracion:
                        viewResponse = iProgramasDao.ProgramasXIteracion(p_Contexto, p_Filtro);
                        break;
                    case EnumAccionRealizar.BuscarExAntePerfil:
                        viewResponse = iProgramasDao.BuscarExAnteEvalPerfil(p_Contexto, p_Filtro);
                        break;
                    default:
                        break;
                }                
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
		/// metodo que permite registrar elementos de tipo cb_programas en el sistema
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Programas">programa a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		public ViewDto<TablaProgramasDto> Registrar(ContextoDto p_Contexto, TablaProgramasDto p_Programas, EnumAccionRealizar p_Accion)
        {
            ViewDto<TablaProgramasDto> viewResponse = new ViewDto<TablaProgramasDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Programas);
                switch (p_Accion)
                {
                    case EnumAccionRealizar.Ninguna:
                        //viewResponse.Dtos.Add(p_Programas);
                        viewResponse = iProgramasDao.InsertarOrigenDestino(p_Contexto, p_Programas);
                        break;
                    case EnumAccionRealizar.Insertar:
                        viewResponse = iProgramasDao.Insertar(p_Contexto, p_Programas);
                        break;
                    case EnumAccionRealizar.Eliminar:
                        viewResponse = iProgramasDao.Eliminar(p_Contexto, p_Programas);
                        break;
                    case EnumAccionRealizar.Actualizar:
                        viewResponse = iProgramasDao.Actualizar(p_Contexto, p_Programas);
                        break;
                    case EnumAccionRealizar.EliminarUserGrupo:
                        viewResponse = iProgramasDao.CrearIteracion(p_Contexto, p_Programas);
                        break;
                    case EnumAccionRealizar.EjecutarCalculoEficiencia:
                        viewResponse = iProgramasDao.CalculoEficiencia(p_Contexto, p_Programas);
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
