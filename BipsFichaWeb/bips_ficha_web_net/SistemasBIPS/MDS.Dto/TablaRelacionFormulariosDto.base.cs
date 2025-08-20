using System;
using System.Diagnostics;

namespace MDS.Dto
{
    /// <summary>
	///	Clase representativa de la tabla CB_RELACION_FORMULARIOS
	/// </summary>
    public partial class TablaRelacionFormulariosDto : ICloneable
    {
        #region campos privados
        private Nullable<Decimal> _idrelacionformulario;
        private Nullable<Decimal> _idbips;
        private Nullable<Decimal> _idformulario;
        private Nullable<Decimal> _ano;
        private String _nombre;
        private Nullable<Decimal> _idministerio;
        private String _ministerio;
        private Nullable<Decimal> _idservicio;
        private String _servicio;
        private Nullable<Decimal> _idformularioant;
        private Nullable<Decimal> _anoant;
        private String _nombreant;
        private Nullable<Decimal> _idministerioant;
        private String _ministerioant;
        private Nullable<Decimal> _idservicioant;
        private String _servicioant;
        private Nullable<Decimal> _orden;
        private Nullable<DateTime> _fecharegistro;
        private String _observaciones;
        private Nullable<Decimal> _tiporelacionformulario;
        #endregion

        #region propiedades publicas        
        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdRelacionFormulario
        {
            get
            {
                return _idrelacionformulario;
            }
            set
            {
                _idrelacionformulario = value;
            }
        }

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
        public Nullable<Decimal> IdFormulario
        {
            get
            {
                return _idformulario;
            }
            set
            {
                _idformulario = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> Ano
        {
            get
            {
                return _ano;
            }
            set
            {
                _ano = value;
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
        public Nullable<Decimal> IdMinisterio
        {
            get
            {
                return _idministerio;
            }
            set
            {
                _idministerio = value;
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
        public Nullable<Decimal> IdServicio
        {
            get
            {
                return _idservicio;
            }
            set
            {
                _idservicio = value;
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
        public Nullable<Decimal> IdFormularioAnterior
        {
            get
            {
                return _idformularioant;
            }
            set
            {
                _idformularioant = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> AnoAnterior
        {
            get
            {
                return _anoant;
            }
            set
            {
                _anoant = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String NombreAnterior
        {
            get
            {
                return _nombreant;
            }
            set
            {
                _nombreant = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdMinisterioAnterior
        {
            get
            {
                return _idministerioant;
            }
            set
            {
                _idministerioant = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String MinisterioAnterior
        {
            get
            {
                return _ministerioant;
            }
            set
            {
                _ministerioant = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdServicioAnterior
        {
            get
            {
                return _idservicioant;
            }
            set
            {
                _idservicioant = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String ServicioAnterior
        {
            get
            {
                return _servicioant;
            }
            set
            {
                _servicioant = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> Orden
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
        public Nullable<DateTime> FechaRegistro
        {
            get
            {
                return _fecharegistro;
            }
            set
            {
                _fecharegistro = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String Observaciones
        {
            get
            {
                return _observaciones;
            }
            set
            {
                _observaciones = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> TipoRelacionFormulario
        {
            get
            {
                return _tiporelacionformulario;
            }
            set
            {
                _tiporelacionformulario = value;
            }
        }
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        [DebuggerStepThrough]
        public TablaRelacionFormulariosDto()
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
            TablaRelacionFormulariosDto objDto = (TablaRelacionFormulariosDto)this.MemberwiseClone();
            return objDto;
        }
        #endregion
    }
}
