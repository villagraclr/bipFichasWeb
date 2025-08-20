using System;
using System.Collections.Generic;

namespace MDS.Core.Dto
{
    /// <summary>
    ///	Clase representativa de la configuracion de instacia
    /// </summary>
    [Serializable]
    internal class InstanceConfigurationDto
    {
        #region campos privados
        private String name;
        private String type;
        private String assembly;
        private String value;
        private List<ParameterConfigurationDto> parameters = new List<ParameterConfigurationDto>();
        #endregion

        #region propiedades internas
        /// <summary>
        /// propiedad publica que contiene el nombre (unico) de la instancia
        /// </summary>
        internal String Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// propiedad publica que contiene el tipo de objeto de la instancia
        /// </summary>
        internal String Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// propiedad publica que contiene el assembly de origen del objeto de la instancia
        /// </summary>
        internal String Assembly
        {
            get { return assembly; }
            set { assembly = value; }
        }

        /// <summary>
        /// propiedad publica que contiene el valor de la instancia
        /// </summary>
        internal String Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        /// <summary>
        /// propiedad publica que contiene los parametros asociados a la instancia
        /// </summary>
        internal List<ParameterConfigurationDto> Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        internal InstanceConfigurationDto()
        {
        }
        #endregion
    }
}
