using System;

namespace MDS.Dto
{
    public partial class TablaPerfilesDto
    {
        #region campos privados
        private Nullable<Decimal> _idpermiso;
        private String _permiso;
        private String _descripcionpermiso;
        private Nullable<Decimal> _estadopermiso;
        #endregion

        #region propiedades publicas
        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdPermiso
        {
            get
            {
                return _idpermiso;
            }
            set
            {
                _idpermiso = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String Permiso
        {
            get
            {
                return _permiso;
            }
            set
            {
                _permiso = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String DescripcionPermiso
        {
            get
            {
                return _descripcionpermiso;
            }
            set
            {
                _descripcionpermiso = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> EstadoPermiso
        {
            get
            {
                return _estadopermiso;
            }
            set
            {
                _estadopermiso = value;
            }
        }
        #endregion

        #region constructores
        #endregion

        #region metodos publicos
        #endregion
    }
}
