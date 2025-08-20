using System;

namespace MDS.Dto
{
    /// <summary>
    ///	Clase representativa de los filtros aplicados a las busquedas de CB_PARAMETROS_USUARIOS
    /// </summary>
    public partial class TablaParametrosUsuariosFiltroDto
    {
        #region campos privados
        private Nullable<Decimal> _idparametro;
        private Nullable<Decimal> _idplataforma;
        private String _idusuario;
        private Nullable<Decimal> _ano;
        #endregion

        #region propiedades publicas
        /// <summary>
        /// propiedad publica que almacena la clave primaria del objeto
        /// </summary>
        /// <remarks>miembro de la clave primaria de la tabla CB_PARAMETROS</remarks>
        public Nullable<Decimal> IdParametro
        {
            get
            {
                return _idparametro;
            }
            set
            {
                _idparametro = value;
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
        public Nullable<Decimal> Ano
        {
            get
            {
                return _ano;
            }
            set
            {
                _ano = value;
            }
        }
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        public TablaParametrosUsuariosFiltroDto()
        {
        }
        #endregion
    }
}
