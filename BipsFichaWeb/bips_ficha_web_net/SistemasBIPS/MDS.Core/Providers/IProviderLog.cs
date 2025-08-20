namespace MDS.Core.Providers
{
    /// <summary>
    /// interfaz de objeto de arquitectura que permite obtener las instancia de los objetos de negocio
    /// </summary>
    public interface IProviderLog
    {
        /// <summary>
        /// metodo que permite logear la informacion de los procesos de negocio
        /// </summary>
        /// <param name="p_Msg">Informacion a logear</param>
        /// <returns>valor que indica si la excepcion debe ser relanzada o no</returns>
        void LogInfo(string p_Msg);
    }
}
