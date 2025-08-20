using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace MDS.Dto
{
    /// <summary>
	///	Clase representativa de la tabla CB_ROLES
	[DataContract]
    public partial class TablaRolesDto : ICloneable
    {
        #region campos privados
        private System.Decimal? _id;
        private System.String _descripcion;
        private System.Decimal? _estado;
        #endregion

        #region propiedades publicas
        /// <summary>
        /// propiedad publica que almacena la clave primaria del objeto
        /// </summary>
        /// <remarks>miembro de la clave primaria de la tabla CB_ROLES</remarks>
        [DataMember]
        public System.Decimal? Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        [DataMember]
        public System.String Descripcion
        {
            get
            {
                return _descripcion;
            }
            set
            {
                _descripcion = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        [DataMember]
        public System.Decimal? Estado
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
        public TablaRolesDto()
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
            TablaRolesDto objDto = (TablaRolesDto)this.MemberwiseClone();
            return objDto;
        }
        #endregion
    }
}
