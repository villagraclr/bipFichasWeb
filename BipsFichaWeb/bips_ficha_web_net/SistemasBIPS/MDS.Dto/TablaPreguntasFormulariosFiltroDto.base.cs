using System;
using System.Diagnostics;

namespace MDS.Dto
{
    /// <summary>
	///	Clase representativa de los filtros aplicados a las busquedas de CB_PREGUNTAS_FORMULARIOS
	/// </summary>
    public partial class TablaPreguntasFormulariosFiltroDto
    {
        #region campos privados
        private Nullable<Decimal> _idpreguntaformulario;
        private Nullable<Decimal> _idtipoformulario;
        private Nullable<Decimal> _idpregunta;
        private Nullable<Decimal> _idmenu;
        private Nullable<Decimal> _orden;
        private Nullable<Decimal> _idestado;
        private Nullable<Decimal> _idtipopregunta;
        #endregion

        #region propiedades publicas
        /// <summary>
        /// propiedad publica que almacena la clave primaria del objeto
        /// </summary>
        /// <remarks>miembro de la clave primaria de la tabla CB_PREGUNTAS_FORMULARIOS</remarks>
        public Nullable<Decimal> IdPreguntaFormulario
        {
            get
            {
                return _idpreguntaformulario;
            }
            set
            {
                _idpreguntaformulario = value;
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
        public Nullable<Decimal> IdTipoPregunta
        {
            get
            {
                return _idtipopregunta;
            }
            set
            {
                _idtipopregunta = value;
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
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        [DebuggerStepThrough]
        public TablaPreguntasFormulariosFiltroDto()
        {
        }
        #endregion
    }
}
