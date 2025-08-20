using System;

namespace MDS.Core.Dto
{
    /// <summary>
    ///	Clase representativa de un parametro de instacia
    /// </summary>
    [Serializable]
    internal class ParameterConfigurationDto
    {
        #region campos privados
        private String name;
        private String value;
        #endregion

        #region propiedades internas
        /// <summary>
        /// propiedad publica que contiene el nombre (unico) del parametro
        /// </summary>
        internal String Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// propiedad publica que contiene el valor del parametro
        /// </summary>
        internal String Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        internal ParameterConfigurationDto()
        {
        }
        #endregion
    }
}
