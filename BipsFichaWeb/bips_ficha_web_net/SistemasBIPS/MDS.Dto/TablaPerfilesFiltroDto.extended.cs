using System;

namespace MDS.Dto
{
    public partial class TablaPerfilesFiltroDto
    {
        #region campos privados
        private Nullable<Decimal> _idpermiso;
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
        #endregion        
    }
}
