using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDS.Dto
{
    /// <summary>
	///	Clase representativa de los filtros aplicados a las busquedas de CB_RELACION_FORMULARIOS
	/// </summary>
    public partial class TablaRelacionFormulariosFiltroDto
    {
        #region campos privados
        private Nullable<Decimal> _idrelacionformulario;
        private Nullable<Decimal> _idformulario;
        private Nullable<Decimal> _idformularioant;
        private Nullable<Decimal> _idbips;
        private Nullable<Decimal> _ano;
        private Nullable<Decimal> _anoant;
        #endregion

        #region propiedades publicas        
        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdRelacionFormulario
        {
            get
            {
                return _idrelacionformulario;
            }
            set
            {
                _idrelacionformulario = value;
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
        public Nullable<Decimal> IdFormularioAnterior
        {
            get
            {
                return _idformularioant;
            }
            set
            {
                _idformularioant = value;
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
        public Nullable<Decimal> AnoAnterior
        {
            get
            {
                return _anoant;
            }
            set
            {
                _anoant = value;
            }
        }        
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        [DebuggerStepThrough]
        public TablaRelacionFormulariosFiltroDto()
        {
        }
        #endregion
    }
}
