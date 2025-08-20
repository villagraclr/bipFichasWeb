using System;
using System.Diagnostics;

namespace MDS.Dto
{
    /// <summary>
	///	Clase representativa de los filtros aplicados a las busquedas de CB_FUNCIONES_DEPENDENCIAS
	/// </summary>
    public partial class TablaFuncionesDependenciasFiltroDto
    {
        #region campos privados
        private Nullable<Decimal> _idfunciondependencia;
        private Nullable<Decimal> _idfuncion;
        private Nullable<Decimal> _tipoformulario;
        private Nullable<Decimal> _idpregunta;
        private Nullable<Decimal> _idpreguntadependiente;
        private Nullable<Decimal> _idevento;
        private Nullable<Decimal> _idestado;
        private Nullable<Decimal> _tipofuncion;
        #endregion

        #region propiedades publicas
        /// <summary>
        /// propiedad publica que almacena la clave primaria del objeto
        /// </summary>
        /// <remarks>miembro de la clave primaria de la tabla CB_FUNCIONES_DEPENDENCIAS</remarks>
        public Nullable<Decimal> IdFuncionDependencia
        {
            get
            {
                return _idfunciondependencia;
            }
            set
            {
                _idfunciondependencia = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdFuncion
        {
            get
            {
                return _idfuncion;
            }
            set
            {
                _idfuncion = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> TipoFormulario
        {
            get
            {
                return _tipoformulario;
            }
            set
            {
                _tipoformulario = value;
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
        public Nullable<Decimal> IdPreguntaDependiente
        {
            get
            {
                return _idpreguntadependiente;
            }
            set
            {
                _idpreguntadependiente = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdEvento
        {
            get
            {
                return _idevento;
            }
            set
            {
                _idevento = value;
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

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> TipoFuncion
        {
            get
            {
                return _tipofuncion;
            }
            set
            {
                _tipofuncion = value;
            }
        }
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        [DebuggerStepThrough]
        public TablaFuncionesDependenciasFiltroDto()
        {
        }
        #endregion
    }
}
