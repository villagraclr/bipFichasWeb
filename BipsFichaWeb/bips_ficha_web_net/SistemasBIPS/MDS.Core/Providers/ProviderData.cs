using MDS.Core.Base;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Oracle.DataAccess.Client;
using System;

namespace MDS.Core.Providers
{
    /// <summary>
    /// clase de arquitectura que permite administrar las peticiones de base de datos
    /// </summary>
    public class ProviderData : IProviderData
    {
        #region constructor
        /// <summary>
        /// constructor por defecto del proveedor
        /// </summary>
        public ProviderData()
        {
        }
        #endregion

        #region metodos publicos
        /// <summary>
        /// metodo que permite obtener una conexion de DB según el nombre definido en el parametro nameDB
        /// </summary>
        /// <param name="p_NameDB">parametro que identifica el nombre de la DB en el archivo app.config</param>
        /// <returns>retorna una instancia de la base de datos a conectar</returns>
        public Database GetConexion(String p_NameDB)
        {
            return Factory.DataBase.GetConexion(p_NameDB);
        }

        public OracleConnection GetConexion2(String p_NameDB)
        {
            return Factory.DataBase.GetConexion2(p_NameDB);
        }
        #endregion
    }
}
