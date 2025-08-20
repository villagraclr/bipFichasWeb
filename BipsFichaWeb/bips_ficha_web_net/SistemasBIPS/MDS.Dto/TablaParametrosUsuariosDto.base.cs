using System;
using System.Diagnostics;

namespace MDS.Dto
{
    /// <summary>
	///	Clase representativa de la tabla CB_PARAMETROS_USUARIOS
	/// </summary>
    public partial class TablaParametrosUsuariosDto : ICloneable
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
        [DebuggerStepThrough]
        public TablaParametrosUsuariosDto()
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
            TablaParametrosUsuariosDto objDto = (TablaParametrosUsuariosDto)this.MemberwiseClone();
            return objDto;
        }
        #endregion
    }
}
