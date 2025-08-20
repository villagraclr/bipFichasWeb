using System;

namespace MDS.Dto
{
    public partial class TablaGruposFormulariosFiltroDto
    {
        #region campos privados
        private String _idusuario;
        private Nullable<Decimal> _idplataforma;
        private Nullable<Decimal> _idtipogrupo;
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
        #endregion
    }
}
