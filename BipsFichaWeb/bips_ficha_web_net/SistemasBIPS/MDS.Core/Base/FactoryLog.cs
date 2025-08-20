using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;

namespace MDS.Core.Base
{
    /// <summary>
    /// clase encargada de gestionar el proceso de loging del sistema
    /// </summary>
    internal class FactoryLog
    {
        #region campo estaticos privados
        private static FactoryLog _instance = null;
        #endregion

        #region campos privados
        private const string categoryInfo = "Info";
        private const string categoryConfig = "Config";
        private const string categoryConfigTrace = "ConfigTrace";
        #endregion

        #region constructor
        private FactoryLog()
        {
        }
        #endregion

        #region singleton
        /// <summary>
        /// metodo que permite obtener una instancia del objeto en memoria
        /// </summary>
        /// <returns></returns>
        internal static FactoryLog GetInstance()
        {
            if (_instance == null)
                _instance = new FactoryLog();
            return _instance;
        }
        #endregion

        #region metodos internos
        /// <summary>
        /// metodo que permite logear la informacion de los procesos de negocio
        /// </summary>
        /// <param name="p_Msg">Informacion a logear</param>
        /// <returns>valor que indica si la excepcion debe ser relanzada o no</returns>
        internal void LogInfo(string p_Msg)
        {
            LogEntry entry = new LogEntry()
            {
                Message = p_Msg,
                Categories = new string[] { categoryInfo },
                TimeStamp = DateTime.Now
            };
            Logger.Write(p_Msg);
        }

        /// <summary>
        /// metodo que permite logear la informacion de los levantamiento de procesos de arquitectura
        /// </summary>
        /// <param name="p_Msg">Informacion a logear</param>
        /// <returns>valor que indica si la excepcion debe ser relanzada o no</returns>
        internal void LogConfigInfo(string p_Msg)
        {
            LogEntry entry = new LogEntry()
            {
                Message = p_Msg,
                Categories = new string[] { categoryConfig },
                TimeStamp = DateTime.Now
            };
            Logger.Write(p_Msg);
        }
        #endregion
    }
}
