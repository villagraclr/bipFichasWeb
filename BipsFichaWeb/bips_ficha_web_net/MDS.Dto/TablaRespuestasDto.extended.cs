using System;

namespace MDS.Dto
{
    public partial class TablaRespuestasDto
    {
        #region campos privados
        private Nullable<Decimal> _tipoinsert;
        private Nullable<Decimal> _tipodelete;
        #endregion

        #region propiedades publicas
        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> TipoInsert
        {
            get
            {
                return _tipoinsert;
            }
            set
            {
                _tipoinsert = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> TipoDelete
        {
            get
            {
                return _tipodelete;
            }
            set
            {
                _tipodelete = value;
            }
        }
        #endregion

        #region constructores
        #endregion

        #region metodos publicos
        #endregion
    }
}
