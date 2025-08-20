using System;
using System.Collections.Generic;

namespace MDS.Dto
{
    /// <summary>
    /// Clase representativa de la tabla CB_FUNCIONES_DEPENDENCIAS
    /// </summary>
    public partial class TablaFuncionesDependenciasDto : ICloneable
    {
        #region campos privados
        private Nullable<Decimal> _idfunciondependencia;
        private Nullable<Decimal> _idfuncion;
        private Nullable<Decimal> _tipoformulario;
        private Nullable<Decimal> _idevento;
        private String _evento;
        private Nullable<Decimal> _idcategoriaevento;
        private Nullable<Decimal> _valorevento;
        private Nullable<Decimal> _valor2evento;
        private Nullable<Decimal> _idpregunta;
        private String _pregunta;
        private Nullable<Decimal> _tipopregunta;
        private Nullable<Decimal> _valorpregunta;
        private Nullable<Decimal> _categoriapregunta;
        private Nullable<Decimal> _idpreguntadependiente;
        private String _preguntadependiente;
        private Nullable<Decimal> _tipopreguntadependiente;
        private Nullable<Decimal> _valorpreguntadependiente;
        private Nullable<Decimal> _categoriapreguntadependiente;
        private Nullable<Decimal> _valorfuncion;        
        private Nullable<Decimal> _idestado;
        private String _estado;
        private Nullable<Decimal> _idmenu;
        private Nullable<Decimal> _tipofuncion;
        private List<TablaParametrosDto> _datos { get; set; }
        #endregion

        #region propiedades publicas
        /// <summary>
        /// propiedad publica que almacena la clave primaria del objeto
        /// </summary>
        /// <remarks>miembro de la clave primaria de la tabla CB_FUNCIONES_DEPENDENCIAS</remarks>
        public Nullable<Decimal> IdFuncionDependencia
        {
            get
            {
                return _idfunciondependencia;
            }
            set
            {
                _idfunciondependencia = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdFuncion
        {
            get
            {
                return _idfuncion;
            }
            set
            {
                _idfuncion = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> TipoFormulario
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
        public Nullable<Decimal> IdEvento
        {
            get
            {
                return _idevento;
            }
            set
            {
                _idevento = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String Evento
        {
            get
            {
                return _evento;
            }
            set
            {
                _evento = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdCategoriaEvento
        {
            get
            {
                return _idcategoriaevento;
            }
            set
            {
                _idcategoriaevento = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> ValorEvento
        {
            get
            {
                return _valorevento;
            }
            set
            {
                _valorevento = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> Valor2Evento
        {
            get
            {
                return _valor2evento;
            }
            set
            {
                _valor2evento = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdPregunta
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
        public String Pregunta
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
        public Nullable<Decimal> TipoPregunta
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
        public Nullable<Decimal> ValorPregunta
        {
            get
            {
                return _valorpregunta;
            }
            set
            {
                _valorpregunta = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> CategoriaPregunta
        {
            get
            {
                return _categoriapregunta;
            }
            set
            {
                _categoriapregunta = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdPreguntaDependiente
        {
            get
            {
                return _idpreguntadependiente;
            }
            set
            {
                _idpreguntadependiente = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String PreguntaDependiente
        {
            get
            {
                return _preguntadependiente;
            }
            set
            {
                _preguntadependiente = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> TipoPreguntaDependiente
        {
            get
            {
                return _tipopreguntadependiente;
            }
            set
            {
                _tipopreguntadependiente = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> ValorPreguntaDependiente
        {
            get
            {
                return _valorpreguntadependiente;
            }
            set
            {
                _valorpreguntadependiente = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> CategoriaPreguntaDependiente
        {
            get
            {
                return _categoriapreguntadependiente;
            }
            set
            {
                _categoriapreguntadependiente = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> ValorFuncion
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
        public Nullable<Decimal> IdEstado
        {
            get
            {
                return _idestado;
            }
            set
            {
                _idestado = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String Estado
        {
            get
            {
                return _estado;
            }
            set
            {
                _estado = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdMenu
        {
            get
            {
                return _idmenu;
            }
            set
            {
                _idmenu = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> TipoFuncion
        {
            get
            {
                return _tipofuncion;
            }
            set
            {
                _tipofuncion = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public List<TablaParametrosDto> Datos
        {
            get
            {
                return _datos;
            }
            set
            {
                _datos = value;
            }
        }
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        public TablaFuncionesDependenciasDto()
        {
            this._datos = new List<TablaParametrosDto>();
        }
        #endregion

        #region ICloneable
        /// <summary>
        /// metodo que permite crear una copia de la actual instancia en memoria
        /// </summary>
        /// <returns>una copia del objeto existente en memoria</returns>
        public Object Clone()
        {
            TablaFuncionesDependenciasDto objDto = (TablaFuncionesDependenciasDto)this.MemberwiseClone();
            return objDto;
        }
        #endregion
    }
}
