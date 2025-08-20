using System;

namespace MDS.Dto
{
    /// <summary>
    /// Clase representativa de la tabla CB_EXCEPCIONES_MENU
    /// </summary>
    public partial class TablaExcepcionesMenuDto : ICloneable
    {
        #region campos privados
        private Nullable<Decimal> _idmenu;
        private Nullable<Decimal> _idtipomenu;
        private Nullable<Decimal> _idmenupadre;
        private Nullable<Decimal> _idexcepcionplantilla;
        private String _nombreplantilla;
        private Nullable<Decimal> _tipoformulario;
        private Nullable<Decimal> _estado;
        private String _descripcionestado;
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
        public String NombrePlantilla
        {
            get
            {
                return _nombreplantilla;
            }
            set
            {
                _nombreplantilla = value;
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

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String DescripcionEstado
        {
            get
            {
                return _descripcionestado;
            }
            set
            {
                _descripcionestado = value;
            }
        }
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        public TablaExcepcionesMenuDto()
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
            TablaExcepcionesMenuDto objDto = (TablaExcepcionesMenuDto)this.MemberwiseClone();
            return objDto;
        }
        #endregion
    }
}
