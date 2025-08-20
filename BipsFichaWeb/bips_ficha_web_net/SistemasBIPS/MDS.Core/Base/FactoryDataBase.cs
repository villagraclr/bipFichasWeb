using System;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Oracle.DataAccess.Client;
using System.Configuration;

namespace MDS.Core.Base
{
    /// <summary>
    /// clase encargada de gestionar el proceso de loging del sistema
    /// </summary>
    internal class FactoryDataBase
    {
        #region campo estaticos privados
        private static FactoryDataBase _instance = null;
        #endregion

        #region campos privados
        private const string categoryInfo = "Info";
        #endregion

        #region constructor
        private FactoryDataBase()
        {
        }
        #endregion

        #region singleton
        /// <summary>
        /// metodo que permite obtener una instancia del objeto en memoria
        /// </summary>
        /// <returns></returns>
        internal static FactoryDataBase GetInstance()
        {
            if (_instance == null)
                _instance = new FactoryDataBase();
            return _instance;
        }
        #endregion

        #region metodos internos
        /// <summary>
        /// metodo que permite obtener una conexion de DB según el nombre definido en el parametro nameDB
        /// </summary>
        /// <param name="p_NameDB">parametro que identifica el nombre de la DB en el archivo app.config</param>
        /// <returns>retorna una instancia de la base de datos a conectar</returns>
        internal Database GetConexion(String p_NameDB)
        {
            Database dbConn = null;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                dbConn = factory.Create(p_NameDB);
                //MDC: Se actualiza línea de creación de instancia de la BD, debido a actualización de libreria "EnterpriseLibrary.Data"
                //dbConn = DatabaseFactory.CreateDatabase(p_NameDB);
            }
            catch (Exception ex)
            {
                throw Factory.Exceptiones.HandleException(ex, "CONNECTION_DATABASE_EXCEPTION");
            }
            return dbConn;
        }

        /// <summary>
        /// metodo que permite obtener una conexion Oracle de DB según el nombre definido en el parametro nameDB
        /// </summary>
        /// <param name="p_NameDB"></param>
        /// <returns></returns>
        internal OracleConnection GetConexion2(String p_NameDB)
        {
            OracleConnection cn = null;
            try
            {
                string cadenaDB = ConfigurationManager.ConnectionStrings[p_NameDB].ConnectionString;
                cn = new OracleConnection(cadenaDB);
            }
            catch (Exception ex)
            {
                throw Factory.Exceptiones.HandleException(ex, "ERROR_CONEXION_DB");
            }
            return cn;
        }
        #endregion
    }
}
