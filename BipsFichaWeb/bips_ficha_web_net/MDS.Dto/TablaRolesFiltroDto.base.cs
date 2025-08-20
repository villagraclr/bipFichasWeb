using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace MDS.Dto
{
    /// <summary>
    ///	Clase representativa de los filtros aplicados a las busquedas de CB_ROLES
    /// </summary>
    [DataContract]
    public partial class TablaRolesFiltroDto
    {
        #region campos privados
        private Nullable<System.Decimal> _id;
        private System.String _descripcion;
        private Nullable<System.Decimal> _estado;
        #endregion

        #region propiedades publicas
        /// <summary>
        /// propiedad publica que almacena la clave primaria del objeto
        /// </summary>
        [DataMember]
        public Nullable<System.Decimal> Id
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
        public Nullable<System.Decimal> Estado
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
        public TablaRolesFiltroDto()
        {

        }
        #endregion
    }
}
