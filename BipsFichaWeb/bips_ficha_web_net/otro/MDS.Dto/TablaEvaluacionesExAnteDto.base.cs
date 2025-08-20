using System;
using System.Diagnostics;

namespace MDS.Dto
{
    /// <summary>
	///	Clase representativa de la pantalla de evaluaciones
	/// </summary>
    public partial class TablaEvaluacionesExAnteDto : ICloneable
    {
        #region campos privados
        private Nullable<Decimal> _idprograma;
        private Nullable<Decimal> _idbips;
        private Nullable<Decimal> _idministerio;
        private String _ministerio;
        private Nullable<Decimal> _idservicio;
        private String _servicio;
        private String _nombre;
        private Nullable<Decimal> _idtipo;
        private String _tipo;
        private String _calificacion;
        private String _idevaluador1;
        private String _idevaluador2;
        private Nullable<Decimal> _estado;
        private String _version;
        private String _idencriptado;
        private Nullable<Decimal> _etapa;
        private Nullable<Decimal> _ano;
        #endregion

        #region propiedades publicas
        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdPrograma
        {
            get
            {
                return _idprograma;
            }
            set
            {
                _idprograma = value;
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
        public Nullable<Decimal> IdTipo
        {
            get
            {
                return _idtipo;
            }
            set
            {
                _idtipo = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public String Tipo
        {
            get
            {
                return _tipo;
            }
            set
            {
                _tipo = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public String Calificacion
        {
            get
            {
                return _calificacion;
            }
            set
            {
                _calificacion = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public String IdEvaluador1
        {
            get
            {
                return _idevaluador1;
            }
            set
            {
                _idevaluador1 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public String IdEvaluador2
        {
            get
            {
                return _idevaluador2;
            }
            set
            {
                _idevaluador2 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public Nullable<Decimal> Estado
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
        public String Version
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
        public String IdEncriptado
        {
            get
            {
                return _idencriptado;
            }
            set
            {
                _idencriptado = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public Nullable<Decimal> Etapa
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
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        [DebuggerStepThrough]
        public TablaEvaluacionesExAnteDto()
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
            TablaEvaluacionesExAnteDto objDto = (TablaEvaluacionesExAnteDto)this.MemberwiseClone();
            return objDto;
        }
        #endregion
    }
}
