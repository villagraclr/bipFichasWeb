namespace MDS.Logic.Mgr
{
    /// <summary>
	/// interfaz generica mgr
	/// </summary>
    public partial interface ISistemasBIPSMgr : 
        ITablaParametrosMgr, 
        ITablaProgramasMgr,         
        ITablaProgramasUsuariosMgr, 
        ITablaMenuFormulariosMgr, 
        ITablaPreguntasFormulariosMgr, 
        ITablaPreguntasGruposMgr, 
        ITablaPreguntasTablasMgr, 
        ITablaFuncionesDependenciasMgr, 
        ITablaRespuestasMgr, 
        ITablaLogSesionMgr, 
        ITablaLogFormulariosMgr, 
        ITablaUsuariosMgr,
        ITablaGruposFormulariosMgr,
        ITablaPerfilesMgr,
        ITablaFormulariosGruposMgr,
        ITablaExcepcionesMenuMgr,
        ITablaExcepcionesPreguntasMgr,
        ITablaRelacionFormulariosMgr,
        ITablaPlantillasTraspasoMgr,
        ITablaParametrosUsuariosMgr,
        ITablaPreguntasRespuestasMgr,
        ITablaConsultasMgr,
        ITablaRespuestasConsultasMgr,
        ITablaBeneficiariosRisMgr
    {
    }
}