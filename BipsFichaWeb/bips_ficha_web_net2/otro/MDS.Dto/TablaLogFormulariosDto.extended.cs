using System;

namespace MDS.Dto
{
    public partial class TablaLogFormulariosDto
    {
        #region campos privados
        private Nullable<Decimal> _tipoupdate;
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
        #endregion

        #region constructores
        #endregion

        #region metodos publicos
        #endregion
    }
}
