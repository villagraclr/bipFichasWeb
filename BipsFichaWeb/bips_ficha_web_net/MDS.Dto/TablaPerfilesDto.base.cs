using System;

namespace MDS.Dto
{
    /// <summary>
    /// Clase representativa de la tabla CB_PERFILES
    /// </summary>
    public partial class TablaPerfilesDto : ICloneable
    {
        #region campos privados
        private Nullable<Decimal> _idperfil;
        private String _nombre;
        private String _descripcion;
        private Nullable<Decimal> _estado;
        private Nullable<Decimal> _jerarquia;
        #endregion

        #region propiedades publicas        
        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdPerfil
        {
            get
            {
                return _idperfil;
            }
            set
            {
                _idperfil = value;
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
        public String Descripcion
        {
            get
            {
                return _descripcion;
            }
            set
            {
                _descripcion = value;
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
        public Nullable<Decimal> Jerarquia
        {
            get
            {
                return _jerarquia;
            }
            set
            {
                _jerarquia = value;
            }
        }
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        public TablaPerfilesDto()
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
            TablaPerfilesDto objDto = (TablaPerfilesDto)this.MemberwiseClone();
            return objDto;
        }
        #endregion
    }
}
