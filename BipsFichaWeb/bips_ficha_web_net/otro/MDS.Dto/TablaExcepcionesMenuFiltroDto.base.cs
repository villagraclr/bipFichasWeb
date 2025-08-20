using System;
using System.Diagnostics;

namespace MDS.Dto
{
    /// <summary>
	///	Clase representativa de los filtros aplicados a las busquedas de CB_EXCEPCIONES_MENU
	/// </summary>
    public partial class TablaExcepcionesMenuFiltroDto
    {
        #region campos privados        
        private Nullable<Decimal> _idmenu;
        private Nullable<Decimal> _idtipomenu;
        private Nullable<Decimal> _idmenupadre;
        private Nullable<Decimal> _idexcepcionplantilla;
        private Nullable<Decimal> _tipoformulario;
        private Nullable<Decimal> _estado;
        #endregion

        #region propiedades publicas       
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
        public Nullable<Decimal> IdTipoMenu
        {
            get
            {
                return _idtipomenu;
            }
            set
            {
                _idtipomenu = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdMenuPadre
        {
            get
            {
                return _idmenupadre;
            }
            set
            {
                _idmenupadre = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdExcepcionPlantilla
        {
            get
            {
                return _idexcepcionplantilla;
            }
            set
            {
                _idexcepcionplantilla = value;
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
        public TablaExcepcionesMenuFiltroDto()
        {
        }
        #endregion
    }
}
