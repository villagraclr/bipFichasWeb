using System;

namespace MDS.Dto
{
    public partial class TablaUsuariosFiltroDto
    {
        #region campos privados
        private Nullable<Decimal> _idgrupo;
        #endregion

        #region propiedades publicas
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
        #endregion

        #region constructores
        #endregion

        #region metodos publicos
        #endregion
    }
}