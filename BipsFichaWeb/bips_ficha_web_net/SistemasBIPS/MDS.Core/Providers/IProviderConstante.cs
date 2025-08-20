using MDS.Core.Enum;

namespace MDS.Core.Providers
{
    /// <summary>
    /// interfaz de arquitectura que permite administrar las constantes del sistema
    /// </summary>
    public interface IProviderConstante
    {
        /// <summary>
        /// metodo que permite obtener el valor parametrizable asociado a una enumeracion
        /// </summary>
        /// <param name="p_Key">clave de la constante</param>
        /// <returns>valor de constante</returns>
        string GetValue(string p_Key);

        /// <summary>
        /// metodo que permite obtener el valor parametrizable asociado a una enumeracion
        /// </summary>
        /// <param name="p_Key">clave de la constante</param>
        /// <returns>valor de constante</returns>
        string GetValue(EnumConstantes p_Key);
    }
}
