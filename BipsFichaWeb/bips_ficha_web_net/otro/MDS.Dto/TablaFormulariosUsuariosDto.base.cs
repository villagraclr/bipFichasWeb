using System;

namespace MDS.Dto
{
    /// <summary>
    /// Clase representativa de la tabla CB_FORMULARIOS_USUARIOS
    /// </summary>
    public partial class TablaFormulariosUsuariosDto : ICloneable
    {
        #region campos privados
        private String _idusuario;
        private Nullable<Decimal> _idgrupoformulario;
        private Nullable<Decimal> _tipogrupo;
        private Nullable<Decimal> _idplataforma;
        private Nullable<Decimal> _estado;
        #endregion

        #region propiedades publicas                
        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String IdUsuario
        {
            get
            {
                return _idusuario;
            }
            set
            {
                _idusuario = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdGrupoFormulario
        {
            get
            {
                return _idgrupoformulario;
            }
            set
            {
                _idgrupoformulario = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> TipoGrupo
        {
            get
            {
                return _tipogrupo;
            }
            set
            {
                _tipogrupo = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdPlataforma
        {
            get
            {
                return _idplataforma;
            }
            set
            {
                _idplataforma = value;
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
        public TablaFormulariosUsuariosDto()
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
            TablaFormulariosUsuariosDto objDto = (TablaFormulariosUsuariosDto)this.MemberwiseClone();
            return objDto;
        }
        #endregion
    }
}
