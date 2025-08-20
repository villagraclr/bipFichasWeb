using System;

namespace MDS.Dto
{
    public partial class TablaUsuariosDto
    {
        #region campos privados
        private String _ministerio;
        private String _servicio;
        private String _estado;
        private String _perfil;
        private String _descripcionperfil;
        private Nullable<Decimal> _tipoupdate;
        private Nullable<Decimal> _idgrupo;
        private String _grupo;
        private String _descripciongrupo;
        private Nullable<Decimal> _idtipogrupo;
        private String _tipogrupo;
        private Nullable<Decimal> _totalgrupos;
        private Nullable<Decimal> _totalpermisos;
        #endregion

        #region propiedades publicas        
        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public Nullable<Decimal> TipoUpdate
        {
            get
            {
                return _tipoupdate;
            }
            set
            {
                _tipoupdate = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public String Ministerio
        {
            get
            {
                return _ministerio;
            }
            set
            {
                _ministerio = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public String Servicio
        {
            get
            {
                return _servicio;
            }
            set
            {
                _servicio = value;
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

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public String Perfil
        {
            get
            {
                return _perfil;
            }
            set
            {
                _perfil = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public String DescripcionPerfil
        {
            get
            {
                return _descripcionperfil;
            }
            set
            {
                _descripcionperfil = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public Nullable<Decimal> IdGrupo
        {
            get
            {
                return _idgrupo;
            }
            set
            {
                _idgrupo = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public String Grupo
        {
            get
            {
                return _grupo;
            }
            set
            {
                _grupo = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public String DescripcionGrupo
        {
            get
            {
                return _descripciongrupo;
            }
            set
            {
                _descripciongrupo = value;
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

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public Nullable<Decimal> TotalGrupos
        {
            get
            {
                return _totalgrupos;
            }
            set
            {
                _totalgrupos = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public Nullable<Decimal> TotalPermisos
        {
            get
            {
                return _totalpermisos;
            }
            set
            {
                _totalpermisos = value;
            }
        }
        #endregion

        #region constructores
        #endregion

        #region ICloneable
        #endregion
    }
}