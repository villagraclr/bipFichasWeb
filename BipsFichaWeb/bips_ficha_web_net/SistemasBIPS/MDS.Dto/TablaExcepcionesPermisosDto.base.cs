using System;
using System.Diagnostics;

namespace MDS.Dto
{
    /// <summary>
	///	Clase representativa de la tabla CB_EXCEPCIONES_PERMISOS
	/// </summary>
    public partial class TablaExcepcionesPermisosDto : ICloneable
    {
        #region campos privados
        private Nullable<Decimal> _idpermiso;
        private String _idusuario;
        private Nullable<Decimal> _idformulario;
        private Nullable<Decimal> _estado;
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
        public Nullable<Decimal> IdFormulario
        {
            get
            {
                return _idformulario;
            }
            set
            {
                _idformulario = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> Estado
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
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        [DebuggerStepThrough]
        public TablaExcepcionesPermisosDto()
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
            TablaExcepcionesPermisosDto objDto = (TablaExcepcionesPermisosDto)this.MemberwiseClone();
            return objDto;
        }
        #endregion
    }
}
