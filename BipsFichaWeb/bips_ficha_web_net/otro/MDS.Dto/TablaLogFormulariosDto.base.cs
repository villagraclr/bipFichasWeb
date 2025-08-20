using System;

namespace MDS.Dto
{
    /// <summary>
    /// Clase representativa de la tabla CB_LOG_FORMULARIOS
    /// </summary>
    public partial class TablaLogFormulariosDto : ICloneable
    {
        #region campos privados
        private Nullable<Decimal> _idformulario;
        private String _idusuario;
        private String _idsesion;
        private Nullable<Decimal> _tipoacceso;
        private Nullable<DateTime> _fechaacceso;
        private Nullable<DateTime> _fechasalida;
        private Nullable<Decimal> _estadoacceso;        
        #endregion

        #region propiedades publicas        
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
        public String IdSesion
        {
            get
            {
                return _idsesion;
            }
            set
            {
                _idsesion = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> TipoAcceso
        {
            get
            {
                return _tipoacceso;
            }
            set
            {
                _tipoacceso = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<DateTime> FechaAcceso
        {
            get
            {
                return _fechaacceso;
            }
            set
            {
                _fechaacceso = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<DateTime> FechaSalida
        {
            get
            {
                return _fechasalida;
            }
            set
            {
                _fechasalida = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> EstadoAcceso
        {
            get
            {
                return _estadoacceso;
            }
            set
            {
                _estadoacceso = value;
            }
        }
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        public TablaLogFormulariosDto()
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
            TablaLogFormulariosDto objDto = (TablaLogFormulariosDto)this.MemberwiseClone();
            return objDto;
        }
        #endregion
    }
}
