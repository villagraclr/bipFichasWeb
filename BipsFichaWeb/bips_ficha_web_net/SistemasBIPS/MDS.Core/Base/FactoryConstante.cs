using System;
using System.Collections.Generic;
using MDS.Core.Enum;

namespace MDS.Core.Base
{
    /// <summary>
    /// clase encargada de gestionar el proceso de obtencion de valores parametrizables del sistema
    /// </summary>
    internal class FactoryConstante
    {
        #region campo estaticos privados
        private static FactoryConstante _instance = null;
        #endregion

        #region campos privados
        private Dictionary<String, String> constantes = null;
        #endregion

        #region constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        private FactoryConstante()
        {
            constantes = new Dictionary<string, string>();
            ConstanteConfigurationReader configurationReader = new ConstanteConfigurationReader();
            configurationReader.ReadConfiguration(constantes);
        }
        #endregion

        #region singleton
        /// <summary>
        /// metodo que permite obtener una instancia del objeto en memoria
        /// </summary>
        /// <returns></returns>
        internal static FactoryConstante GetInstance()
        {
            if (_instance != null && ConstanteConfigurationReader.IsModify())
                _instance = null;

            if (_instance == null)
                _instance = new FactoryConstante();
            return _instance;
        }
        #endregion

        #region metodos internos
        /// <summary>
        /// metodo que permite obtener el valor parametrizable asociado a una enumeracion
        /// </summary>
        /// <param name="p_Key">clave de la constante</param>
        /// <returns>valor de constante</returns>
        internal string GetValue(string p_Key)
        {
            try
            {
                if (p_Key == null || p_Key.Trim().Length == 0)
                    throw new ArgumentNullException("null", string.Format("{0} - Debe indicar una clave valida existente en el diccionario interno", "FACTORY_GET_CONSTANTE_EMPTY_TYPE"));
                if (!constantes.ContainsKey(p_Key))
                    throw new ArgumentNullException(p_Key, string.Format("{0} - No se encuentra la clave {1} en el diccionario interno", "FACTORY_GET_CONSTANTE_WRONG_TYPE", p_Key));
                return constantes[p_Key];
            }
            catch (Exception ex)
            {
                Factory.Exceptiones.HandleException(ex);
                throw;
            }
        }

        /// <summary>
        /// metodo que permite obtener el valor parametrizable asociado a una enumeracion
        /// </summary>
        /// <param name="p_Key">clave de la constante</param>
        /// <returns>valor de constante</returns>
        internal string GetValue(EnumConstantes p_Key)
        {
            try
            {
                if (!constantes.ContainsKey(p_Key.ToString()))
                    throw new ArgumentNullException(p_Key.ToString(), string.Format("{0} - No se encuentra la clave {1} en el diccionario interno", "FACTORY_GET_CONSTANTE_WRONG_TYPE", p_Key));
                return constantes[p_Key.ToString()];
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
