using Microsoft.AspNet.Identity;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Linq;

namespace MDS.Identity
{
    /// <summary>
    /// Clase representativa de tabla cb_userlogins
    /// </summary>
    public class UserLoginsTable
    {
        private OracleDatabase _database;

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        /// <param name="database"></param>
        public UserLoginsTable(OracleDatabase database)
        {
            _database = database;
        }

        /// <summary>
        /// Borra logeo de usuario
        /// </summary>
        /// <param name="user">Objeto usuario</param>
        /// <param name="login">Objeto login</param>
        /// <returns></returns>
        public int Delete(IdentityUser user, UserLoginInfo login)
        {
            const string commandText = @"DELETE FROM CB_USERLOGINS WHERE USERID = :USERID AND LOGINPROVIDER = :LOGINPROVIDER AND PROVIDERKEY = :PROVIDERKEY";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter{ ParameterName = "USERID", Value = user.Id, OracleDbType = OracleDbType.Varchar2 },
                new OracleParameter{ ParameterName = "LOGINPROVIDER", Value = login.LoginProvider, OracleDbType = OracleDbType.Varchar2 },
                new OracleParameter{ ParameterName = "PROVIDERKEY", Value = login.ProviderKey, OracleDbType = OracleDbType.Varchar2 },
            };

            return _database.Execute(commandText, parameters);
        }

        /// <summary>
        /// Borra logeo de un usuario específico
        /// </summary>
        /// <param name="userId">Id usuario</param>
        /// <returns></returns>
        public int Delete(string userId)
        {
            const string commandText = @"DELETE FROM CB_USERLOGINS WHERE USERID = :USERID";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter{ ParameterName = "USERID", Value = userId, OracleDbType = OracleDbType.Varchar2 },
            };

            return _database.Execute(commandText, parameters);
        }

        /// <summary>
        /// Inserta nuevo registro de logeo
        /// </summary>
        /// <param name="user">Objeto usuario</param>
        /// <param name="login">Objeto login</param>
        /// <returns></returns>
        public int Insert(IdentityUser user, UserLoginInfo login)
        {
            const string commandText = @"INSERT INTO CB_USERLOGINS (LOGINPROVIDER, PROVIDERKEY, USERID) VALUES (:LOGINPROVIDER, :PROVIDERKEY, :USERID)";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter{ ParameterName = "USERID", Value = user.Id, OracleDbType = OracleDbType.Varchar2 },
                new OracleParameter{ ParameterName = "LOGINPROVIDER", Value = login.LoginProvider, OracleDbType = OracleDbType.Varchar2 },
                new OracleParameter{ ParameterName = "PROVIDERKEY", Value = login.ProviderKey, OracleDbType = OracleDbType.Varchar2 },
            };

            return _database.Execute(commandText, parameters);
        }

        /// <summary>
        /// Obtiene ID Usuario mediante un objeto userlogin
        /// </summary>
        /// <param name="login">Objeto usuario</param>
        /// <returns></returns>
        public string FindUserIdByLogin(UserLoginInfo login)
        {
            const string commandText = @"SELECT USERID FROM CB_USERLOGINS WHERE LOGINPROVIDER = :LOGINPROVIDER AND PROVIDERKEY = :PROVIDERKEY";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter{ ParameterName = "LOGINPROVIDER", Value = login.LoginProvider, OracleDbType = OracleDbType.Varchar2 },
                new OracleParameter{ ParameterName = "PROVIDERKEY", Value = login.ProviderKey, OracleDbType = OracleDbType.Varchar2 },
            };

            return _database.GetStrValue(commandText, parameters);
        }

        /// <summary>
        /// Obtiene lista de los usuarios logeados
        /// </summary>
        /// <param name="userId">Id usuario</param>
        /// <returns></returns>
        public List<UserLoginInfo> FindByUserId(string userId)
        {
            const string commandText = @"SELECT * FROM CB_USERLOGINS WHERE USERID = :USERID";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter{ ParameterName = "USERID", Value = userId, OracleDbType = OracleDbType.Varchar2 },
            };

            var rows = _database.Query(commandText, parameters);

            return rows.Select(row => new UserLoginInfo(row["LOGINPROVIDER"], row["PROVIDERKEY"])).ToList();
        }
    }
}
