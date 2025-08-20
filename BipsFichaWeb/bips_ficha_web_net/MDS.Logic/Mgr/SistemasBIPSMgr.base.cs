using System;
using MDS.Core.Providers;
using MDS.Logic.Dom;

namespace MDS.Logic.Mgr
{
    /// <summary>
	///	clase generica mgr
	/// </summary>
    public partial class SistemasBIPSMgr : ISistemasBIPSMgr
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
        #endregion

        #region constructores
        /// <summary>
		/// constructor por defecto del objeto
		/// </summary>
        public SistemasBIPSMgr()
        {
            iProviderLog = (IProviderLog)Activator.CreateInstance(typeof(ProviderLog));
            iProviderError = (IProviderError)Activator.CreateInstance(typeof(ProviderError));
            iTablaUsuariosDom = (ITablaUsuariosDom)Activator.CreateInstance(typeof(TablaUsuariosDom));
            iTablaRolesDom = (ITablaRolesDom)Activator.CreateInstance(typeof(TablaRolesDom));
            iTablaParametrosDom = (ITablaParametrosDom)Activator.CreateInstance(typeof(TablaParametrosDom));
            iTablaProgramasDom = (ITablaProgramasDom)Activator.CreateInstance(typeof(TablaProgramasDom));            
            iTablaProgramasUsuariosDom = (ITablaProgramasUsuariosDom)Activator.CreateInstance(typeof(TablaProgramasUsuariosDom));
            iTablaMenuFormulariosDom = (ITablaMenuFormulariosDom)Activator.CreateInstance(typeof(TablaMenuFormulariosDom));
            iTablaPreguntasFormulariosDom = (ITablaPreguntasFormulariosDom)Activator.CreateInstance(typeof(TablaPreguntasFormulariosDom));
            iTablaPreguntasGruposDom = (ITablaPreguntasGruposDom)Activator.CreateInstance(typeof(TablaPreguntasGruposDom));
            iTablaPreguntasTablasDom = (ITablaPreguntasTablasDom)Activator.CreateInstance(typeof(TablaPreguntasTablasDom));
            iTablaFuncionesDependenciasDom = (ITablaFuncionesDependenciasDom)Activator.CreateInstance(typeof(TablaFuncionesDependenciasDom));
            iTablaRespuestasDom = (ITablaRespuestasDom)Activator.CreateInstance(typeof(TablaRespuestasDom));
            iTablaLogSesionDom = (ITablaLogSesionDom)Activator.CreateInstance(typeof(TablaLogSesionDom));
            iTablaLogFormulariosDom = (ITablaLogFormulariosDom)Activator.CreateInstance(typeof(TablaLogFormulariosDom));
            iTablaGruposFormulariosDom = (ITablaGruposFormulariosDom)Activator.CreateInstance(typeof(TablaGruposFormulariosDom));
            iTablaPerfilesDom = (ITablaPerfilesDom)Activator.CreateInstance(typeof(TablaPerfilesDom));
            iTablaFormulariosGruposDom = (ITablaFormulariosGruposDom)Activator.CreateInstance(typeof(TablaFormulariosGruposDom));
            iTablaExcepcionesMenuDom = (ITablaExcepcionesMenuDom)Activator.CreateInstance(typeof(TablaExcepcionesMenuDom));
            iTablaExcepcionesPreguntasDom = (ITablaExcepcionesPreguntasDom)Activator.CreateInstance(typeof(TablaExcepcionesPreguntasDom));
            iTablaRelacionFormulariosDom = (ITablaRelacionFormulariosDom)Activator.CreateInstance(typeof(TablaRelacionFormulariosDom));
            iTablaPlantillasTraspasoDom = (ITablaPlantillasTraspasoDom)Activator.CreateInstance(typeof(TablaPlantillasTraspasoDom));
            iTablaParametrosUsuariosDom = (ITablaParametrosUsuariosDom)Activator.CreateInstance(typeof(TablaParametrosUsuariosDom));
            iTablaPreguntasRespuestasDom = (ITablaPreguntasRespuestasDom)Activator.CreateInstance(typeof(TablaPreguntasRespuestasDom));
            iTablaConsultasDom = (ITablaConsultasDom)Activator.CreateInstance(typeof(TablaConsultasDom));
            iTablaRespuestasConsultasDom = (ITablaRespuestasConsultasDom)Activator.CreateInstance(typeof(TablaRespuestasConsultasDom));
            iTablaRespuestasConsultasDom = (ITablaRespuestasConsultasDom)Activator.CreateInstance(typeof(TablaRespuestasConsultasDom));
            iTablaBeneficiariosRisDom = (ITablaBeneficiariosRisDom)Activator.CreateInstance(typeof(TablaBeneficiariosRisDom));
        }
        #endregion

        #region metodos publicos
        #endregion
    }
}
