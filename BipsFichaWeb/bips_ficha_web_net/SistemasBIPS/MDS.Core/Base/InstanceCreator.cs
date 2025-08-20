using System;
using System.Collections.Generic;
using System.Diagnostics;
using MDS.Core.Dto;

namespace MDS.Core.Base
{
    /// <summary>
    /// clase de arquitectura encargada de la creacion de las instancias 
    /// </summary>
    internal class InstanceCreator
    {
        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        [DebuggerStepThrough]
        internal InstanceCreator()
        {
        }
        #endregion

        #region metodos internos
        /// <summary>
        /// metodo que permite generar una nueva instancia del objeto especificado
        /// </summary>
        /// <param name="p_Assembly">nombre del assembly del objeto a generar</param>
        /// <param name="p_Type">nombre del objeto a generar</param>
        /// <returns>nueva instancia del objeto especificado</returns>
        private Object GetNewInstance(String p_Assembly, String p_Type)
        {
            Object dtoSystem = null;
            try
            {
                dtoSystem = Activator.CreateInstanceFrom(p_Assembly, p_Type).Unwrap();
            }
            catch (Exception ex)
            {
                string msgDetail = string.Format("Se ha producido un error al intentar crear una instancia de la clase {0} desde el assemby {1}", p_Type, p_Assembly);
                throw new TypeLoadException(string.Format("INSTANCE_CREATION_ERROR - {0}", msgDetail), ex);
            }
            return dtoSystem;
        }

        /// <summary>
        /// metodo que permite crear instancias en memoria de los objetos
        /// </summary>
        /// <param name="p_InstanceConfigurations">diccionario que contiene la coleccion de configuraciones a aplicar sobre los objetos</param>
        /// <param name="p_Instances">diccionario que contiene la coleccion de obejtos a crear</param>
        internal void CreateInstances(Dictionary<String, InstanceConfigurationDto> p_InstanceConfigurations, ref Dictionary<String, Object> p_Instances)
        {
            string pathBinFolder = AppDomain.CurrentDomain.SetupInformation.PrivateBinPath;
            if (pathBinFolder == null)
                pathBinFolder = AppDomain.CurrentDomain.BaseDirectory;

            p_Instances = new Dictionary<String, object>();
            foreach (InstanceConfigurationDto ic in p_InstanceConfigurations.Values)
            {
                string pathAssembly = System.IO.Path.Combine(pathBinFolder, ic.Assembly);
                p_Instances.Add(ic.Name, GetNewInstance(pathAssembly, ic.Type));
            }
        }
        #endregion
    }
}
