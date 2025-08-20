using System;

namespace MDS.Dto
{
    /// <summary>
    /// Clase representativa de la tabla CB_EXCEPCIONES_PREGUNTAS
    /// </summary>
    public partial class TablaExcepcionesPreguntasDto : ICloneable
    {
        #region campos privados
        private String _idusuario;
        private Nullable<Decimal> _idperfil;
        private Nullable<Decimal> _idformulario;
        private Nullable<Decimal> _idtipoformulario;
        private Nullable<Decimal> _idpregunta;
        private Nullable<Decimal> _tipoexcepcion;
        private Nullable<Decimal> _estado;
        #endregion

        #region propiedades publicas
        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String IdUsuario
        {
            get
            {
                return _idusuario;
            }
            set
            {
                _idusuario = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdPerfil
        {
            get
            {
                return _idperfil;
            }
            set
            {
                _idperfil = value;
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
        public Nullable<Decimal> IdTipoFormulario
        {
            get
            {
                return _idtipoformulario;
            }
            set
            {
                _idtipoformulario = value;
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
        public Nullable<Decimal> TipoExcepcion
        {
            get
            {
                return _tipoexcepcion;
            }
            set
            {
                _tipoexcepcion = value;
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
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        public TablaExcepcionesPreguntasDto()
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
            TablaExcepcionesPreguntasDto objDto = (TablaExcepcionesPreguntasDto)this.MemberwiseClone();
            return objDto;
        }
        #endregion
    }
}
