using System;
using System.Diagnostics;

namespace MDS.Dto
{
    /// <summary>
	///	Clase representativa de los filtros aplicados a las busquedas de CB_RESPUESTAS
	/// </summary>
    public partial class TablaRespuestasFiltroDto
    {
        #region campos privados    
        private Nullable<Decimal> _idpregunta;
        private Nullable<Decimal> _idformulario;
        private Nullable<Decimal> _idtab;
        #endregion

        #region propiedades publicas        
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
        public Nullable<Decimal> IdTab
        {
            get
            {
                return _idtab;
            }
            set
            {
                _idtab = value;
            }
        }       
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        [DebuggerStepThrough]
        public TablaRespuestasFiltroDto()
        {
        }
        #endregion
    }
}
