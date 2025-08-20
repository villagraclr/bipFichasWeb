using System;
using System.Diagnostics;

namespace MDS.Dto
{
    /// <summary>
	///	Clase representativa de los filtros aplicados a las busquedas de CB_PREGUNTAS_TABLAS
	/// </summary>
    public partial class TablaPreguntasTablasFiltroDto
    {
        #region campos privados
        private Nullable<Decimal> _idpreguntatabla;
        private Nullable<Decimal> _idtabla;
        private Nullable<Decimal> _idpregunta;        
        private Nullable<Decimal> _fila;
        private Nullable<Decimal> _columna;
        private Nullable<Decimal> _idestado;
        #endregion

        #region propiedades publicas
        /// <summary>
        /// propiedad publica que almacena la clave primaria del objeto
        /// </summary>
        /// <remarks>miembro de la clave primaria de la tabla CB_PREGUNTAS_TABLAS</remarks>
        public Nullable<Decimal> IdPreguntaTabla
        {
            get
            {
                return _idpreguntatabla;
            }
            set
            {
                _idpreguntatabla = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdTabla
        {
            get
            {
                return _idtabla;
            }
            set
            {
                _idtabla = value;
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
        public Nullable<Decimal> Fila
        {
            get
            {
                return _fila;
            }
            set
            {
                _fila = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> Columna
        {
            get
            {
                return _columna;
            }
            set
            {
                _columna = value;
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
        public TablaPreguntasTablasFiltroDto()
        {
        }
        #endregion
    }
}
