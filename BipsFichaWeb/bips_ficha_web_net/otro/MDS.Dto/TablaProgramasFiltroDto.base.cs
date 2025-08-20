using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MDS.Dto
{
    /// <summary>
	///	Clase representativa de los filtros aplicados a las busquedas de B_PROGRAMAS
	/// </summary>
    public partial class TablaProgramasFiltroDto
    {
        #region campos privados
        private Nullable<Decimal> _idprograma;
        private Nullable<Decimal> _idbips;
        private Nullable<Decimal> _ano;
        private Nullable<Decimal> _tipoformulario;
        private String _nombre;
        private Nullable<Decimal> _idministerio;
        private Nullable<Decimal> _idservicio;
        private Nullable<Decimal> _etapa;
        private Nullable<Decimal> _estado;
        #endregion

        #region propiedades publicas
        /// <summary>
        /// propiedad publica que almacena la clave primaria del objeto
        /// </summary>
        /// <remarks>miembro de la clave primaria de la tabla B_PROGRAMA</remarks>
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
        public TablaProgramasFiltroDto()
        {
            
        }
        #endregion
    }
}