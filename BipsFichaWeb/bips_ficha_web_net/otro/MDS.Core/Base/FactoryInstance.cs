using System;
using System.Collections.Generic;
using MDS.Core.Dto;

namespace MDS.Core.Base
{
    /// <summary>
    /// clase que administra las instancia de los objetos de negocio
    /// </summary>
    internal class FactoryInstance
    {
        #region campo estaticos privados
        private static FactoryInstance _instance = null;
        #endregion

        #region campos privados
        private Dictionary<String, Object> instances = null;
        #endregion

        #region constructor
        private FactoryInstance()
        {
            try
            {
                Dictionary<String, InstanceConfigurationDto> instanceConfigurations = new Dictionary<String, InstanceConfigurationDto>();
                InstanceConfigurationReader instanceConfigurationReader = new InstanceConfigurationReader();
                InstanceConfigurator instanceConfigurator = new InstanceConfigurator();
                InstanceCreator instanceCreator = new InstanceCreator();
                instanceConfigurationReader.ReadConfiguration(instanceConfigurations);
                instanceCreator.CreateInstances(instanceConfigurations, ref instances);
                instanceConfigurator.ConfigureInstances(instanceConfigurations, instances);
            }
            catch (Exception ex)
            {
                if (_instance != null)
                    _instance = null;
                throw Factory.Exceptiones.HandleException(ex, "FACTORY_INSTANCE_INITIALIZATION_ERROR");
            }
        }
        #endregion

        #region singleton
        /// <summary>
        /// metodo que permite obtener una instancia del objeto en memoria
        /// </summary>
        /// <returns></returns>
        internal static FactoryInstance GetInstance()
        {
            if (_instance != null && InstanceConfigurationReader.IsModify())
                _instance = null;

            if (_instance == null)
                _instance = new FactoryInstance();
            return _instance;
        }
        #endregion

        #region metodos internos
        /// <summary>
        /// metodo que permite obtener la instancia de un objeto en memoria
        /// </summary>
        /// <param name="p_Type">tipo del objeto a obtener</param>
        /// <returns>instancia del objeto requerido</returns>
        internal Object GetObject(Type p_Type)
        {
            try
            {
                if (p_Type == null)
                    throw new ArgumentNullException("null", string.Format("{0} - Debe indicar un tipo de objeto valido existente en el diccionario interno", "FACTORY_GET_INSTANCE_EMPTY_TYPE"));
                string typeName = p_Type.Name;
                if (!instances.ContainsKey(typeName))
                    throw new ArgumentNullException(typeName, string.Format("{0} - No se encuentra el tipo {1} en el diccionario interno", "FACTORY_GET_INSTANCE_WRONG_TYPE", typeName));
                return instances[typeName];
            }
            catch (Exception ex)
            {
                Factory.Exceptiones.HandleException(ex);
                throw;
            }
        }
        #endregion
    }
}
