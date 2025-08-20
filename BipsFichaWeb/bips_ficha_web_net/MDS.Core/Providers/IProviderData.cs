using Microsoft.Practices.EnterpriseLibrary.Data;
using Oracle.DataAccess.Client;
using System;

namespace MDS.Core.Providers
{
    /// <summary>
    /// interfaz de arquitectura que permite administrar las peticiones de base de datos
    /// </summary>
    public interface IProviderData
    {
        /// <summary>
        /// metodo que permite obtener una conexion de DB según el nombre definido en el parametro nameDB
        /// </summary>
        /// <param name="p_NameDB">parametro que identifica el nombre de la DB en el archivo app.config</param>
        /// <returns>retorna una instancia de la base de datos a conectar</returns>
        Database GetConexion(String p_NameDB);

        OracleConnection GetConexion2(String p_NameDB);
    }
}
