using MDS.Core.Providers;
using MDS.Dto;
using System;
using System.Collections.Generic;

namespace SistemasBIPS.Models
{
    /// <summary>
    /// Contiene objetos necesarios para armar formulario
    /// </summary>
    public class FormulariosViewModels
    {
        public IList<TablaMenuFormulariosDto> menuPadres { get; set; }
        public IList<TablaMenuFormulariosDto> menuHijos { get; set; }
        public IList<PreguntasFormulariosDto> preguntas { get; set; }
        public IList<TablaRespuestasDto> respuestas { get; set; }
        public IList<PreguntasFunciones> funciones { get; set; }
        public IProviderConstante constantes { get; set; }
        public int tab { get; set; }
        public string tabForm { get; set; }
        public CabeceraFormulario cabecera { get; set; }
        public AccesoFormulario acceso { get; set; }
        public IList<TablaExcepcionesMenuDto> excepcionesMenu { get; set; }
        public IList<TablaExcepcionesPreguntasDto> excepcionesPreguntas { get; set; }
        public IList<Nullable<Decimal>> programasEvaluar { get; set; }
        public IList<PreguntasObservaciones> observaciones { get; set; }
        public String textoEnviarEvaluar { get; set; }
        public Boolean enviaObservaciones { get; set; }
        public IList<TablaParametrosDto> menuSinRevision { get; set; }
        public Boolean enviaEvaluar { get; set; }
        public String textoEvaluar { get; set; }
        public IList<TablaParametrosDto> preguntasEvaluacion { get; set; }
        public int idProgramaBota { get; set; }
        public int tipoPrograma { get; set; }
        public IList<TablaExcepcionesPlantillasFormDto> plantillaBase { get; set; }
        public Boolean permisoAbreCampos { get; set; }
        public int rolUsuario { get; set; }
        public IList<TablaParametrosDto> coordinadores { get; set; }
        public TablaUsuariosDto datosUsuario { get; set; }
        public IList<TablaParametrosDto> informesEvaluacion { get; set; }
        public int idBips { get; set; }
        public Dictionary<int, int> versiones { get; set; }
        public IList<TablaParametrosDto> contrapartes { get; set; }
        public Boolean permisoVerInformes { get; set; }
        public IList<TablaParametrosDto> validaciones { get; set; }
        public int tipoFormulario { get; set; }
        public IList<TablaParametrosDto> validacionesPerfilGore { get; set; }
        public IList<TablaConsultasDto> comentarios { get; set; }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public FormulariosViewModels()
        {
            this.menuPadres = new List<TablaMenuFormulariosDto>();
            this.menuHijos = new List<TablaMenuFormulariosDto>();
            this.preguntas = new List<PreguntasFormulariosDto>();
            this.respuestas = new List<TablaRespuestasDto>();
            this.funciones = new List<PreguntasFunciones>();
            this.constantes = (IProviderConstante)Activator.CreateInstance(typeof(ProviderConstante));
            this.excepcionesMenu = new List<TablaExcepcionesMenuDto>();
            this.excepcionesPreguntas = new List<TablaExcepcionesPreguntasDto>();
            this.programasEvaluar = new List<Nullable<Decimal>>();
            this.observaciones = new List<PreguntasObservaciones>();
            this.menuSinRevision = new List<TablaParametrosDto>();
            this.preguntasEvaluacion = new List<TablaParametrosDto>();
            this.plantillaBase = new List<TablaExcepcionesPlantillasFormDto>();
            this.coordinadores = new List<TablaParametrosDto>();
            this.datosUsuario = new TablaUsuariosDto();
            this.informesEvaluacion = new List<TablaParametrosDto>();
            this.versiones = new Dictionary<int, int>();
            this.contrapartes = new List<TablaParametrosDto>();
            this.validaciones = new List<TablaParametrosDto>();
            this.validacionesPerfilGore = new List<TablaParametrosDto>();
            this.comentarios = new List<TablaConsultasDto>();
        }
    }

    /// <summary>
    /// Objeto pregruntas formulario
    /// </summary>
    public class PreguntasObservaciones
    {
        public Nullable<Decimal> IdPregunta { get; set; }
        public Nullable<Decimal> IdTab { get; set; }
        public String Respuesta { get; set; }
    }

    /// <summary>
    /// Objeto pregruntas formulario
    /// </summary>
    public class PreguntasFormularios
    {
        public Nullable<Decimal> id { get; set; }
        public Nullable<Decimal> idPregunta { get; set; }
        public String pregunta { get; set; }
        public String respuesta { get; set; }
        public TablaParametrosDto tipoFormulario { get; set; }
        public TablaParametrosDto tipoPregunta { get; set; }
        public IList<TablaParametrosDto> valores { get; set; }
        public TablaParametrosDto funcion { get; set; }
        public IList<TablaParametrosDto> valor_funcion { get; set; }
        public Nullable<Decimal> menu { get; set; }
        public Nullable<Decimal> menuGrupo { get; set; }
        public Nullable<Decimal> orden { get; set; }
        public Nullable<Decimal> fila { get; set; }
        public Nullable<Decimal> columna { get; set; }
        public Nullable<Decimal> IdTabla { get; set; }
        public Nullable<Decimal> idTab { get; set; }
        public IList<PreguntasFormularios> preguntasGrupos { get; set; }
        public IList<PreguntasFormularios> preguntasTablas { get; set; }
        public Boolean soloLectura { get; set; }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public PreguntasFormularios()
        {
            this.tipoFormulario = new TablaParametrosDto();
            this.tipoPregunta = new TablaParametrosDto();
            this.valores = new List<TablaParametrosDto>();
            this.funcion = new TablaParametrosDto();
            this.valor_funcion = new List<TablaParametrosDto>();
            this.preguntasGrupos = new List<PreguntasFormularios>();
            this.preguntasTablas = new List<PreguntasFormularios>();
        }
    }

    /// <summary>
    /// Objeto para vista parcial de preguntas formularios (HTML)
    /// </summary>
    public class PreguntasFormulariosPartialView
    {
        public string id { get; set; }
        public string idPregunta { get; set; }
        public int indicePregunta { get; set; }
        public string pregunta { get; set; }
        public object respuesta { get; set; }
        public string tipoPregunta { get; set; }
        public string valorPregunta { get; set; }
        public string valor2Pregunta { get; set; }
        public string categoriaPregunta { get; set; }
        public string idTabla { get; set; }
        public string fila { get; set; }
        public string columna { get; set; }
        public string idTab { get; set; }
        public IList<TablaParametrosDto> valores { get; set; }
        public IList<TablaParametrosDto> valorFuncion { get; set; }
        public TablaParametrosDto funcion { get; set; }
        public int anoFormulario { get; set; }
        public IProviderConstante constantes { get; set; }
        public bool soloLectura { get; set; }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public PreguntasFormulariosPartialView()
        {
            this.valores = new List<TablaParametrosDto>();
            this.valorFuncion = new List<TablaParametrosDto>();
            this.funcion = new TablaParametrosDto();
            this.constantes = (IProviderConstante)Activator.CreateInstance(typeof(ProviderConstante));
        }
    }

    /// <summary>
    /// Objeto para vista parcial de asignaciones formularios (HTML)
    /// </summary>
    public class AsignacionesFormulariosPartialView
    {
        public string id { get; set; }
        public string idPregunta { get; set; }
        public int indicePregunta { get; set; }
        public string pregunta { get; set; }
        public string tipoPregunta { get; set; }
        public string valorPregunta { get; set; }
        public string valor2Pregunta { get; set; }
        public string categoriaPregunta { get; set; }
        public string idTab { get; set; }
        public int anoFormulario { get; set; }
        public IProviderConstante constantes { get; set; }
        public bool soloLectura { get; set; }
        public IList<TablaRespuestasDto> respuestas { get; set; }
        public IList<TablaExcepcionesPreguntasDto> excepcionesPreguntas { get; set; }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public AsignacionesFormulariosPartialView()
        {
            this.constantes = (IProviderConstante)Activator.CreateInstance(typeof(ProviderConstante));
            this.respuestas = new List<TablaRespuestasDto>();
            this.excepcionesPreguntas = new List<TablaExcepcionesPreguntasDto>();
        }
    }

    /// <summary>
    /// Objeto para vista parcial de evaluaciones formularios (HTML)
    /// </summary>
    public class EvaluacionesFormulariosPartialView
    {
        public IList<TablaParametrosDto> valor_funcion { get; set; }
        public string idPregunta { get; set; }
        public string id { get; set; }
        public IList<TablaParametrosDto> valores { get; set; }
        public IList<PreguntasFormulariosDto> preguntasGrupos { get; set; }
        public IProviderConstante constantes { get; set; }
        public CabeceraFormulario cabecera { get; set; }
        public IList<TablaRespuestasDto> respuestas { get; set; }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public EvaluacionesFormulariosPartialView()
        {
            this.valor_funcion = new List<TablaParametrosDto>();
            this.valores = new List<TablaParametrosDto>();
            this.preguntasGrupos = new List<PreguntasFormulariosDto>();
            this.constantes = (IProviderConstante)Activator.CreateInstance(typeof(ProviderConstante));
            this.cabecera = new CabeceraFormulario();
            this.respuestas = new List<TablaRespuestasDto>();
        }
    }

    /// <summary>
    /// Objeto de funciones de preguntas
    /// </summary>
    public class PreguntasFunciones
    {
        public string id { get; set; }
        public string idFuncion { get; set; }
        public string idEvento { get; set; }
        public string categoriaEvento { get; set; }
        public string valorEvento { get; set; }
        public string valor2Evento { get; set; }
        public string idPregunta { get; set; }
        public string tipoPregunta { get; set; }
        public string valorPregunta { get; set; }
        public string categoriaPregunta { get; set; }
        public string idPreguntaDependiente { get; set; }
        public string tipoPreguntaDependiente { get; set; }
        public string valorPreguntaDependiente { get; set; }
        public string categoriaPreguntaDependiente { get; set; }
        public IList<TablaParametrosDto> datos { get; set; }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public PreguntasFunciones()
        {
            this.datos = new List<TablaParametrosDto>();
        }
    }

    /// <summary>
    /// Objeto cabecera formulario
    /// </summary>
    public class CabeceraFormulario
    {
        public string nombre { get; set; }
        public string tipo { get; set; }
        public int ano { get; set; }
        public string modo { get; set; }
        public int idTipo { get; set; }
        public string version { get; set; }
        public string fechaEnvio { get; set; }
        public string fechaModificacion { get; set; }
        public string usuarioModificacion { get; set; }
        public string idEtapa { get; set; }
        public string mail { get; set; }
    }

    /// <summary>
    /// Objeto de validación de accesos
    /// </summary>
    public class AccesoFormulario
    {
        public string nombreUsuario { get; set; }
        public int tipoAcceso { get; set; }
        public string mensaje { get; set; }
    }

    public class NuevoFormulariosViewModels
    {
        public IList<MinisterioServicios> listaMinistServ { get; set; }
        public IList<TablaParametrosDto> tipos { get; set; }
        public IList<int> listaAnos { get; set; }
        public DataNuevoFormulario dataFormulario { get; set; }
        public List<TablaPerfilesDto> listaPermisosPerfiles { get; set; }
        public List<TablaGruposFormulariosDto> listaGrupos { get; set; }
        public IList<int> listaAnosDestino { get; set; }
        public List<TablaPlantillasTraspasoDto> listaPlantillasTraspaso { get; set; }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public NuevoFormulariosViewModels()
        {
            this.listaMinistServ = new List<MinisterioServicios>();
            this.tipos = new List<TablaParametrosDto>();
            this.listaPermisosPerfiles = new List<TablaPerfilesDto>();
            this.listaGrupos = new List<TablaGruposFormulariosDto>();
            this.listaPlantillasTraspaso = new List<TablaPlantillasTraspasoDto>();
        }
    }

    public class DataNuevoFormulario
    {
        public int ano { get; set; }
        public string nombre { get; set; }
        public int ministerio { get; set; }
        public int servicio { get; set; }
        public int tipo { get; set; }
    }

    /// <summary>
    /// Contiene objetos necesarios para armar formulario (version js)
    /// </summary>
    public class NewFormulariosViewModels
    {
        public IList<Menu> menu { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public NewFormulariosViewModels()
        {
            this.menu = new List<Menu>();
        }
    }

    public class Menu
    {
        public TablaMenuFormulariosDto menuPadre { get; set; }
        public IList<PreguntasMenu> menuHijo { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public Menu()
        {
            this.menuPadre = new TablaMenuFormulariosDto();
            this.menuHijo = new List<PreguntasMenu>();
        }
    }

    public class PreguntasMenu
    {
        public TablaMenuFormulariosDto menuHijo { get; set; }
        public IList<PreguntasFormulariosDto> preguntas { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public PreguntasMenu()
        {
            this.preguntas = new List<PreguntasFormulariosDto>();
        }
    }

    public class Respuestas
    {
        public string idPregunta { get; set; }
        public string idTab { get; set; }
        public string respuesta { get; set; }
    }
}
