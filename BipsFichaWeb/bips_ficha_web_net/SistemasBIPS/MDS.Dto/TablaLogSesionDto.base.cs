using System;

namespace MDS.Dto
{
    /// <summary>
    /// Clase representativa de la tabla CB_LOG_SESION
    /// </summary>
    public partial class TablaLogSesionDto : ICloneable
    {
        #region campos privados
        private String _idsesion;
        private String _idusuario;
        private Nullable<Decimal> _estadosesion;
        private Nullable<DateTime> _fechasesion;
        #endregion

        #region propiedades publicas        
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
        public Nullable<Decimal> EstadoSesion
        {
            get
            {
                return _estadosesion;
            }
            set
            {
                _estadosesion = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<DateTime> FechaSesion
        {
            get
            {
                return _fechasesion;
            }
            set
            {
                _fechasesion = value;
            }
        }
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        public TablaLogSesionDto()
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
            TablaLogSesionDto objDto = (TablaLogSesionDto)this.MemberwiseClone();
            return objDto;
        }
        #endregion
    }
}
