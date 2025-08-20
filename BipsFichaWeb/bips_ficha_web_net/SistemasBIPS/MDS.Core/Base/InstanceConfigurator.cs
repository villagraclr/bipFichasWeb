using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using MDS.Core.Dto;

namespace MDS.Core.Base
{
    /// <summary>
    /// clase encargada de la configuracion de las instancias de arquitectura
    /// </summary>
    internal class InstanceConfigurator
    {
        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        [DebuggerStepThrough]
        internal InstanceConfigurator()
        {
        }
        #endregion

        #region metodos internos
        /// <summary>
        /// metodo que permite configurar las instancias
        /// </summary>
        /// <param name="p_InstanceConfigurations">diccionario que contiene la configuracion de las instancias</param>
        /// <param name="p_Instances">diccionario que contiene las instancias a configurar</param>
        internal void ConfigureInstances(Dictionary<String, InstanceConfigurationDto> p_InstanceConfigurations, Dictionary<string, Object> p_Instances)
        {
            foreach (InstanceConfigurationDto dtoIc in p_InstanceConfigurations.Values.Where(dto => dto.Parameters != null && dto.Parameters.Count > 0))
            {
                Object dtoInstance = p_Instances[dtoIc.Name];
                foreach (ParameterConfigurationDto parameter in dtoIc.Parameters)
                {
                    if (!p_Instances.ContainsKey(parameter.Value))
                        throw new ArgumentException(
                            string.Format("{0} - El tipo {1} a inyectar en la clase {2} no existe en el listado de objetos a inyectar. Verifique el archivo de instancias", "INSTANCE_NOT_FOUND_IN_INTERNAL_LIST", parameter.Value, dtoIc.Name),
                            parameter.Value);
                    FieldInfo objectAttribute = dtoInstance.GetType().GetField(parameter.Name);
                    if (objectAttribute == null)
                        throw new ArgumentException(
                            string.Format("{0} - El campo {1} a inyectar no existe en la clase {2}. Debe verificar los campos publicos de la clase mapeada ({2}) y verificar que éstos coincidan con la configuracion del archivo de instancias", "FIELD_DONT_EXIST_IN_CLASS", parameter.Name, dtoIc.Name),
                            parameter.Value);
                    objectAttribute.SetValue(dtoInstance, p_Instances[parameter.Value]);
                }
            }
        }
        #endregion
    }
}
