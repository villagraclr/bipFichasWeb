using System;

namespace MDS.Dto
{
    public partial class TablaGruposFormulariosDto
    {
        #region campos privados  
        private Nullable<Decimal> _usuarios;
        private Nullable<Decimal> _formularios;
        private String _idusuario;
        private Nullable<Decimal> _idtipogrupo;
        private String _tipogrupo;
        #endregion

        #region propiedades publicas
        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> Usuarios
        {
            get
            {
                return _usuarios;
            }
            set
            {
                _usuarios = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> Formularios
        {
            get
            {
                return _formularios;
            }
            set
            {
                _formularios = value;
            }
        }

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
        public Nullable<Decimal> IdTipoGrupo
        {
            get
            {
                return _idtipogrupo;
            }
            set
            {
                _idtipogrupo = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String TipoGrupo
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
        #endregion

        #region constructores
        #endregion

        #region metodos publicos
        #endregion
    }
}
