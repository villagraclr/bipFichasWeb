using MDS.Core.Base;

namespace MDS.Core.Providers
{
    /// <summary>
    /// clase de arquitectura que permite obtener las instancia de los objetos de negocio
    /// </summary>
    public class ProviderLog : IProviderLog
    {
        #region constructor
        /// <summary>
        /// constructor por defecto del proveedor
        /// </summary>
        public ProviderLog()
        {
        }
        #endregion

        #region metodos publicos
        /// <summary>
        /// metodo que permite logear la informacion de los procesos de negocio
        /// </summary>
        /// <param name="p_Msg">Informacion a logear</param>
        /// <returns>valor que indica si la excepcion debe ser relanzada o no</returns>
        public void LogInfo(string p_Msg)
        {
            Factory.Logs.LogInfo(p_Msg);
        }
        #endregion
    }
}
