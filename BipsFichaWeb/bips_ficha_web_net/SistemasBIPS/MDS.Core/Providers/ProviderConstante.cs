using MDS.Core.Base;
using MDS.Core.Enum;

namespace MDS.Core.Providers
{
    /// <summary>
    /// clase de arquitectura que permite administrar las constantes del sistema
    /// </summary>
    public class ProviderConstante : IProviderConstante
    {
        #region constructor
        /// <summary>
        /// constructor por defecto del proveedor
        /// </summary>
        public ProviderConstante()
        {
        }
        #endregion

        #region metodos publicos
        /// <summary>
        /// metodo que permite obtener el valor parametrizable asociado a una enumeracion
        /// </summary>
        /// <param name="p_Key">clave de la constante</param>
        /// <returns>valor de constante</returns>
        public string GetValue(string p_Key)
        {
            return Factory.Constantes.GetValue(p_Key);
        }

        /// <summary>
        /// metodo que permite obtener el valor parametrizable asociado a una enumeracion
        /// </summary>
        /// <param name="p_Key">clave de la constante</param>
        /// <returns>valor de constante</returns>
        public string GetValue(EnumConstantes p_Key)
        {
            return Factory.Constantes.GetValue(p_Key);
        }
        #endregion
    }
}
