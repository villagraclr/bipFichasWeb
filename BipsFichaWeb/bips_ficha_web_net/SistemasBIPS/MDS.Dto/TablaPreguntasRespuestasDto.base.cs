using System;

namespace MDS.Dto
{
    /// <summary>
    /// Clase representativa de la tabla VW_PREGUNTAS_RESPUESTAS
    /// </summary>
    public partial class TablaPreguntasRespuestasDto : ICloneable
    {
        #region campos privados
        private Nullable<Decimal> _idbips;
        private String _nombre;
        private String _ministerio;
        private String _servicio;
        private String _tipoformulario;
        private String _etapa;
        private Nullable<Decimal> _version;
        private String _origen;
        private String _tipooferta;
        private Nullable<Decimal> _anoscomparables;
        private String _tienecalificacionexante;
        private Nullable<Decimal> _ultimoanoevaluado;
        private String _ultimacalificacion;
        private String _unidadresponsable;
        private String _paginaweb;
        private String _encargado;
        private String _cargo;
        private String _telefono;
        private String _email;
        private String _contrapartemonitoreo;
        private String _cargocontraparte;
        private String _telefonocontraparte;
        private String _emailcontraparte;
        private String _anoinicio;
        private String _anotermino;
        private String _permanente;
        private String _objetivoestrategico;
        private String _marconormativo;
        private String _planaccion;
        private String _nombreplanaccion;
        //diagnostico
        private String _problemaprincipal;
        private String _propositoprograma;
        //evaluaciones anteriores
        private String _evaluacionesexternas;
        private String _cuantas;
        private String _institucion1;
        private String _nombreevaluacion1;
        private String _anoevaluacion1;
        private String _tipoevaluacion1;
        private String _sitioweb1;
        private String _institucion2;
        private String _nombreevaluacion2;
        private String _anoevaluacion2;
        private String _tipoevaluacion2;
        private String _sitioweb2;
        private String _institucion3;
        private String _nombreevaluacion3;
        private String _anoevaluacion3;
        private String _tipoevaluacion3;
        private String _sitioweb3;
        private String _institucion4;
        private String _nombreevaluacion4;
        private String _anoevaluacion4;
        private String _tipoevaluacion4;
        private String _sitioweb4;
        private String _institucion5;
        private String _nombreevaluacion5;
        private String _anoevaluacion5;
        private String _tipoevaluacion5;
        private String _sitioweb5;
        //poblacion potencial
        private String _descpoblacionpotencial;
        private String _pobpotencial;
        private String _fuenteinformacion;
        private String _unidad_medida;
        #endregion

        #region propiedades publicas
        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdBips
        {
            get
            {
                return _idbips;
            }
            set
            {
                _idbips = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String Nombre
        {
            get
            {
                return _nombre;
            }
            set
            {
                _nombre = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String Ministerio
        {
            get
            {
                return _ministerio;
            }
            set
            {
                _ministerio = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String Servicio
        {
            get
            {
                return _servicio;
            }
            set
            {
                _servicio = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String TipoFormulario
        {
            get
            {
                return _tipoformulario;
            }
            set
            {
                _tipoformulario = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String Etapa
        {
            get
            {
                return _etapa;
            }
            set
            {
                _etapa = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> Version
        {
            get
            {
                return _version;
            }
            set
            {
                _version = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String Origen
        {
            get
            {
                return _origen;
            }
            set
            {
                _origen = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String TipoOferta
        {
            get
            {
                return _tipooferta;
            }
            set
            {
                _tipooferta = value;
            }
        }        

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> AnosComparables
        {
            get
            {
                return _anoscomparables;
            }
            set
            {
                _anoscomparables = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String TieneCalificacionExAnte
        {
            get
            {
                return _tienecalificacionexante;
            }
            set
            {
                _tienecalificacionexante = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> UltimoAnoEvaluado
        {
            get
            {
                return _ultimoanoevaluado;
            }
            set
            {
                _ultimoanoevaluado = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String UltimaCalificacion
        {
            get
            {
                return _ultimacalificacion;
            }
            set
            {
                _ultimacalificacion = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String UnidadResponsable
        {
            get
            {
                return _unidadresponsable;
            }
            set
            {
                _unidadresponsable = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String PaginaWeb
        {
            get
            {
                return _paginaweb;
            }
            set
            {
                _paginaweb = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String Encargado
        {
            get
            {
                return _encargado;
            }
            set
            {
                _encargado = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String Cargo
        {
            get
            {
                return _cargo;
            }
            set
            {
                _cargo = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String Telefono
        {
            get
            {
                return _telefono;
            }
            set
            {
                _telefono = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String ContraparteMonitoreo
        {
            get
            {
                return _contrapartemonitoreo;
            }
            set
            {
                _contrapartemonitoreo = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String CargoContraparte
        {
            get
            {
                return _cargocontraparte;
            }
            set
            {
                _cargocontraparte = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String TelefonoContraparte
        {
            get
            {
                return _telefonocontraparte;
            }
            set
            {
                _telefonocontraparte = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String EmailContraparte
        {
            get
            {
                return _emailcontraparte;
            }
            set
            {
                _emailcontraparte = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String AnoInicio
        {
            get
            {
                return _anoinicio;
            }
            set
            {
                _anoinicio = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String AnoTermino
        {
            get
            {
                return _anotermino;
            }
            set
            {
                _anotermino = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String Permanente
        {
            get
            {
                return _permanente;
            }
            set
            {
                _permanente = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String ObjetivoEstrategico
        {
            get
            {
                return _objetivoestrategico;
            }
            set
            {
                _objetivoestrategico = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String MarcoNormativo
        {
            get
            {
                return _marconormativo;
            }
            set
            {
                _marconormativo = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String PlanAccion
        {
            get
            {
                return _planaccion;
            }
            set
            {
                _planaccion = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String NombrePlanAccion
        {
            get
            {
                return _nombreplanaccion;
            }
            set
            {
                _nombreplanaccion = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String ProblemaPrincipal
        {
            get
            {
                return _problemaprincipal;
            }
            set
            {
                _problemaprincipal = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String PropositoPrograma
        {
            get
            {
                return _propositoprograma;
            }
            set
            {
                _propositoprograma = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String EvaluacionesExternas
        {
            get
            {
                return _evaluacionesexternas;
            }
            set
            {
                _evaluacionesexternas = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String Cuantas
        {
            get
            {
                return _cuantas;
            }
            set
            {
                _cuantas = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String Institucion1
        {
            get
            {
                return _institucion1;
            }
            set
            {
                _institucion1 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String NombreEvaluacion1
        {
            get
            {
                return _nombreevaluacion1;
            }
            set
            {
                _nombreevaluacion1 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String AnoEvaluacion1
        {
            get
            {
                return _anoevaluacion1;
            }
            set
            {
                _anoevaluacion1 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String TipoEvaluacion1
        {
            get
            {
                return _tipoevaluacion1;
            }
            set
            {
                _tipoevaluacion1 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String SitioWeb1
        {
            get
            {
                return _sitioweb1;
            }
            set
            {
                _sitioweb1 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String Institucion2
        {
            get
            {
                return _institucion2;
            }
            set
            {
                _institucion2 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String NombreEvaluacion2
        {
            get
            {
                return _nombreevaluacion2;
            }
            set
            {
                _nombreevaluacion2 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String AnoEvaluacion2
        {
            get
            {
                return _anoevaluacion2;
            }
            set
            {
                _anoevaluacion2 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String TipoEvaluacion2
        {
            get
            {
                return _tipoevaluacion2;
            }
            set
            {
                _tipoevaluacion2 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String SitioWeb2
        {
            get
            {
                return _sitioweb2;
            }
            set
            {
                _sitioweb2 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String Institucion3
        {
            get
            {
                return _institucion3;
            }
            set
            {
                _institucion3 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String NombreEvaluacion3
        {
            get
            {
                return _nombreevaluacion3;
            }
            set
            {
                _nombreevaluacion3 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String AnoEvaluacion3
        {
            get
            {
                return _anoevaluacion3;
            }
            set
            {
                _anoevaluacion3 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String TipoEvaluacion3
        {
            get
            {
                return _tipoevaluacion3;
            }
            set
            {
                _tipoevaluacion3 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String SitioWeb3
        {
            get
            {
                return _sitioweb3;
            }
            set
            {
                _sitioweb3 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String Institucion4
        {
            get
            {
                return _institucion4;
            }
            set
            {
                _institucion4 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String NombreEvaluacion4
        {
            get
            {
                return _nombreevaluacion4;
            }
            set
            {
                _nombreevaluacion4 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String AnoEvaluacion4
        {
            get
            {
                return _anoevaluacion4;
            }
            set
            {
                _anoevaluacion4 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String TipoEvaluacion4
        {
            get
            {
                return _tipoevaluacion4;
            }
            set
            {
                _tipoevaluacion4 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String SitioWeb4
        {
            get
            {
                return _sitioweb4;
            }
            set
            {
                _sitioweb4 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String Institucion5
        {
            get
            {
                return _institucion5;
            }
            set
            {
                _institucion5 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String NombreEvaluacion5
        {
            get
            {
                return _nombreevaluacion5;
            }
            set
            {
                _nombreevaluacion5 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String AnoEvaluacion5
        {
            get
            {
                return _anoevaluacion5;
            }
            set
            {
                _anoevaluacion5 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String TipoEvaluacion5
        {
            get
            {
                return _tipoevaluacion5;
            }
            set
            {
                _tipoevaluacion5 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String SitioWeb5
        {
            get
            {
                return _sitioweb5;
            }
            set
            {
                _sitioweb5 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String DescPoblacionPotencial
        {
            get
            {
                return _descpoblacionpotencial;
            }
            set
            {
                _descpoblacionpotencial = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String PobPotencial
        {
            get
            {
                return _pobpotencial;
            }
            set
            {
                _pobpotencial = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String FuenteInformacion
        {
            get
            {
                return _fuenteinformacion;
            }
            set
            {
                _fuenteinformacion = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String UnidadMedida
        {
            get
            {
                return _unidad_medida;
            }
            set
            {
                _unidad_medida = value;
            }
        }
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        public TablaPreguntasRespuestasDto()
        {
        }
        #endregion

        #region ICloneable
        /// <summary>
        /// metodo que permite crear una copia de la actual instancia en memoria
        /// </summary>
        /// <returns>una copia del objeto existente en memoria</returns>
        public Object Clone()
        {
            TablaPreguntasRespuestasDto objDto = (TablaPreguntasRespuestasDto)this.MemberwiseClone();
            return objDto;
        }
        #endregion
    }
}
