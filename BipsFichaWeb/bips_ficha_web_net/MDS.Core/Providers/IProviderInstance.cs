using System;

namespace MDS.Core.Providers
{
    /// <summary>
    /// interfaz de objeto de arquitectura que permite obtener las instancia de los objetos de negocio
    /// </summary>
    public interface IProviderInstance
    {
        /// <summary>
        /// metodo que permite obtener la instancia de un objeto en memoria
        /// </summary>
        /// <param name="p_Type">tipo del objeto a obtener</param>
        /// <returns>instancia del objeto requerido</returns>
        Object GetInstance(Type p_Type);
    }
}
