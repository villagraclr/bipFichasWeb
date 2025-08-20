using MDS.Core.Base;
using System;

namespace MDS.Core.Providers
{
    /// <summary>
    /// clase de arquitectura que permite obtener las instancia de los objetos de negocio
    /// </summary>
    public class ProviderInstance : IProviderInstance
    {
        #region constructor
        /// <summary>
        /// constructor por defecto del proveedor
        /// </summary>
        public ProviderInstance()
        {
        }
        #endregion

        #region metodos publicos
        /// <summary>
        /// metodo que permite obtener la instancia de un objeto en memoria
        /// </summary>
        /// <param name="p_Type">tipo del objeto a obtener</param>
        /// <returns>instancia del objeto requerido</returns>
        public object GetInstance(Type p_Type)
        {
            return Factory.Instances.GetObject(p_Type);
        }
        #endregion
    }
}
