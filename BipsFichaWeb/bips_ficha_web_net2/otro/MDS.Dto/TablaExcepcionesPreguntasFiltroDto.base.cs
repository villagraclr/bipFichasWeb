using System;
using System.Diagnostics;

namespace MDS.Dto
{
    /// <summary>
	///	Clase representativa de los filtros aplicados a las busquedas de CB_EXCEPCIONES_PREGUNTAS
	/// </summary>
    public partial class TablaExcepcionesPreguntasFiltroDto
    {
        #region campos privados
        private String _idusuario;
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
        [DebuggerStepThrough]
        public TablaExcepcionesPreguntasFiltroDto()
        {
        }
        #endregion
    }
}
