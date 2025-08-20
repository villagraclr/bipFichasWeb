using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using MDS.Core.Enum;

namespace MDS.Core.Dto
{
    /// <summary>
    ///	Clase representativa de errores generados en el sistema
    /// </summary>
    [DataContract]
    public class ErrorDto
    {
        #region campos privados
        private String _codigo;
        private String _mensaje;
        private EnumSeveridad _severidad;
        private String _detalle;
        #endregion

        #region propiedades publicas
        /// <summary>
        /// propiedad publica que contiene el codigo (unico) del mensaje
        /// </summary>
        [DataMember]
        public String Codigo
        {
            get
            {
                return _codigo;
            }
            set
            {
                _codigo = value;
            }
        }

        /// <summary>
        /// propiedad publica que contiene el mensaje descriptivo del error
        /// </summary>
        [DataMember]
        public String Mensaje
        {
            get
            {
                return _mensaje;
            }
            set
            {
                _mensaje = value;
            }
        }

        /// <summary>
        /// propiedad publica que contiene la severidad del error
        /// </summary>
        [DataMember]
        public EnumSeveridad Severidad
        {
            get
            {
                return _severidad;
            }
            set
            {
                _severidad = value;
            }
        }

        /// <summary>
        /// propiedad publica que contiene el detalle opcional del error
        /// </summary>
        [DataMember]
        public String Detalle
        {
            get
            {
                return _detalle;
            }
            set
            {
                _detalle = value;
            }
        }
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        [DebuggerStepThrough]
        public ErrorDto()
        {
        }
        #endregion
    }
}
