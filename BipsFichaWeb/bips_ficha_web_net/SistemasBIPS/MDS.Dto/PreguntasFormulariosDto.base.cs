using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDS.Dto
{
    /// <summary>
    /// Clase representativa de vista VW_PREGUNTAS_FORMULARIOS
    /// </summary>
    public partial class PreguntasFormulariosDto : ICloneable
    {
        #region campos privados
        public Nullable<Decimal> _id { get; set; }
        public Nullable<Decimal> _idpregunta { get; set; }
        public String _pregunta { get; set; }
        public String _respuesta { get; set; }
        public TablaParametrosDto _tipoformulario { get; set; }
        public TablaParametrosDto _tipopregunta { get; set; }
        public IList<TablaParametrosDto> _valores { get; set; }
        public TablaParametrosDto _funcion { get; set; }
        public IList<TablaParametrosDto> _valorfuncion { get; set; }
        public Nullable<Decimal> _menu { get; set; }
        public Nullable<Decimal> _menugrupo { get; set; }
        public Nullable<Decimal> _orden { get; set; }
        public Nullable<Decimal> _fila { get; set; }
        public Nullable<Decimal> _columna { get; set; }
        public Nullable<Decimal> _idtabla { get; set; }
        public Nullable<Decimal> _idtab { get; set; }
        public IList<PreguntasFormulariosDto> _preguntasgrupos { get; set; }
        public IList<PreguntasFormulariosDto> _preguntastablas { get; set; }
        public Boolean _sololectura { get; set; }
        #endregion

        #region propiedades publicas
        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> idPregunta
        {
            get
            {
                return _idpregunta;
            }
            set
            {
                _idpregunta = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String pregunta
        {
            get
            {
                return _pregunta;
            }
            set
            {
                _pregunta = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String respuesta
        {
            get
            {
                return _respuesta;
            }
            set
            {
                _respuesta = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public TablaParametrosDto tipoFormulario
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
        public TablaParametrosDto tipoPregunta
        {
            get
            {
                return _tipopregunta;
            }
            set
            {
                _tipopregunta = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public IList<TablaParametrosDto> valores
        {
            get
            {
                return _valores;
            }
            set
            {
                _valores = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public TablaParametrosDto funcion
        {
            get
            {
                return _funcion;
            }
            set
            {
                _funcion = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public IList<TablaParametrosDto> valor_funcion
        {
            get
            {
                return _valorfuncion;
            }
            set
            {
                _valorfuncion = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> menu
        {
            get
            {
                return _menu;
            }
            set
            {
                _menu = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> menuGrupo
        {
            get
            {
                return _menugrupo;
            }
            set
            {
                _menugrupo = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> orden
        {
            get
            {
                return _orden;
            }
            set
            {
                _orden = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> fila
        {
            get
            {
                return _fila;
            }
            set
            {
                _fila = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> columna
        {
            get
            {
                return _columna;
            }
            set
            {
                _columna = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdTabla
        {
            get
            {
                return _idtabla;
            }
            set
            {
                _idtabla = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> idTab
        {
            get
            {
                return _idtab;
            }
            set
            {
                _idtab = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public IList<PreguntasFormulariosDto> preguntasGrupos
        {
            get
            {
                return _preguntasgrupos;
            }
            set
            {
                _preguntasgrupos = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public IList<PreguntasFormulariosDto> preguntasTablas
        {
            get
            {
                return _preguntastablas;
            }
            set
            {
                _preguntastablas = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Boolean soloLectura
        {
            get
            {
                return _sololectura;
            }
            set
            {
                _sololectura = value;
            }
        }
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        public PreguntasFormulariosDto()
        {
            this.tipoFormulario = new TablaParametrosDto();
            this.tipoPregunta = new TablaParametrosDto();
            this.valores = new List<TablaParametrosDto>();
            this.funcion = new TablaParametrosDto();
            this.valor_funcion = new List<TablaParametrosDto>();
            this.preguntasGrupos = new List<PreguntasFormulariosDto>();
            this.preguntasTablas = new List<PreguntasFormulariosDto>();
        }
        #endregion

        #region ICloneable
        /// <summary>
        /// metodo que permite crear una copia de la actual instancia en memoria
        /// </summary>
        /// <returns>una copia del objeto existente en memoria</returns>
        public Object Clone()
        {
            PreguntasFormulariosDto objDto = (PreguntasFormulariosDto)this.MemberwiseClone();
            return objDto;
        }
        #endregion
    }
}
