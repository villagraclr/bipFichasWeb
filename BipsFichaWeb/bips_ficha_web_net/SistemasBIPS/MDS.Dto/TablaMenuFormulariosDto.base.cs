using System;

namespace MDS.Dto
{
    /// <summary>
    /// Clase representativa de la tabla B_MENU_FORMULARIOS
    /// </summary>
    public partial class TablaMenuFormulariosDto : ICloneable
    {
        #region campos privados
        private Nullable<Decimal> _idmenu;
        private Nullable<Decimal> _idtipomenu;
        private String _tipomenu;
        private Nullable<Decimal> _idpadre;
        private Nullable<Decimal> _idtipoformulario;
        private String _tipoformulario;
        private Nullable<Decimal> _nivel;
        private Nullable<Decimal> _orden;
        private Nullable<Decimal> _idestado;
        private String _estado;
        #endregion

        #region propiedades publicas
        /// <summary>
        /// propiedad publica que almacena la clave primaria del objeto
        /// </summary>
        /// <remarks>miembro de la clave primaria de la tabla B_MENU_FORMULARIOS</remarks>
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
        public String TipoMenu
        {
            get
            {
                return _tipomenu;
            }
            set
            {
                _tipomenu = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdPadre
        {
            get
            {
                return _idpadre;
            }
            set
            {
                _idpadre = value;
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
        public String TipoFormulario
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
        public Nullable<Decimal> Nivel
        {
            get
            {
                return _nivel;
            }
            set
            {
                _nivel = value;
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

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String Estado
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
        public TablaMenuFormulariosDto()
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
            TablaMenuFormulariosDto objDto = (TablaMenuFormulariosDto)this.MemberwiseClone();
            return objDto;
        }
        #endregion
    }
}
