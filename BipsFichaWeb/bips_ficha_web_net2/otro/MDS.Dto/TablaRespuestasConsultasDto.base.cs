using System;

namespace MDS.Dto
{
    /// <summary>
    /// Clase representativa de la tabla CB_RESPUESTAS_CONSULTAS
    /// </summary>
    public partial class TablaRespuestasConsultasDto : ICloneable
    {
        #region campos privados
        private Nullable<Decimal> _idrespuesta;
        private Nullable<Decimal> _idconsulta;
        private String _idusuario;
        private String _usuario;
        private String _respuesta;
        private Nullable<DateTime> _fecha;
        private Nullable<Decimal> _estado;
        #endregion

        #region propiedades publicas        
        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdRespuesta
        {
            get
            {
                return _idrespuesta;
            }
            set
            {
                _idrespuesta = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdConsulta
        {
            get
            {
                return _idconsulta;
            }
            set
            {
                _idconsulta = value;
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
        public String Usuario
        {
            get
            {
                return _usuario;
            }
            set
            {
                _usuario = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String Respuesta
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
        public Nullable<DateTime> Fecha
        {
            get
            {
                return _fecha;
            }
            set
            {
                _fecha = value;
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
        public TablaRespuestasConsultasDto()
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
            TablaRespuestasConsultasDto objDto = (TablaRespuestasConsultasDto)this.MemberwiseClone();
            return objDto;
        }
        #endregion
    }
}
