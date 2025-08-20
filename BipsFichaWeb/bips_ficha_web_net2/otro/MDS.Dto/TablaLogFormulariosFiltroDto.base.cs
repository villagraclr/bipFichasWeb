using System;
using System.Diagnostics;

namespace MDS.Dto
{
    /// <summary>
	///	Clase representativa de los filtros aplicados a las busquedas de CB_LOG_FORMULARIOS
	/// </summary>
    public partial class TablaLogFormulariosFiltroDto
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
        [DebuggerStepThrough]
        public TablaLogFormulariosFiltroDto()
        {
        }
        #endregion
    }
}
