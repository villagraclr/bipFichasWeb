using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace MDS.Identity
{
    /// <summary>
    /// Clase representativa de tabla cb_userclaims
    /// </summary>
    public class UserClaimsTable
    {
        private OracleDatabase _database;

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        /// <param name="database"></param>
        public UserClaimsTable(OracleDatabase database)
        {
            _database = database;
        }

        /// <summary>
        /// Obtiene una instancia del objeto ClaimsIdentity
        /// </summary>
        /// <param name="userId">Id usuario</param>
        /// <returns>ClaimsIdentity</returns>
        public ClaimsIdentity FindByUserId(string userId)
        {
            var claims = new ClaimsIdentity();
            const string commandText = @"SELECT * FROM CB_USERCLAIMS WHERE USERID = :USERID";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter{ ParameterName = "USERID", Value = userId, OracleDbType = OracleDbType.Varchar2 },
            };

            var rows = _database.Query(commandText, parameters);
            foreach (var claim in rows.Select(row => new Claim(row["CLAIMTYPE"], row["CLAIMVALUE"])))
            {
                claims.AddClaim(claim);
            }

            return claims;
        }

        /// <summary>
        /// Borra todas las notificaciones de un usuario específico
        /// </summary>
        /// <param name="userId">ID usuario</param>
        /// <returns></returns>
        public int Delete(string userId)
        {
            const string commandText = @"DELETE FROM CB_USERCLAIMS WHERE USERID = :USERID";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter{ ParameterName = "USERID", Value = userId, OracleDbType = OracleDbType.Varchar2 },
            };

            return _database.Execute(commandText, parameters);
        }

        /// <summary>
        /// Inserta nueva notificación
        /// </summary>
        /// <param name="claim">Notificación</param>
        /// <param name="userId">ID usuario</param>
        /// <returns></returns>
        public int Insert(Claim claim, string userId)
        {
            const string commandText = @"INSERT INTO CB_USERCLAIMS (CLAIMVALUE, CLAIMTYPE, USERID) VALUES (:VALUE, :TYPE, :USERID)";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter{ ParameterName = "USERID", Value = userId, OracleDbType = OracleDbType.Varchar2 },
                new OracleParameter{ ParameterName = "VALUE", Value = claim.Value, OracleDbType = OracleDbType.Clob },
                new OracleParameter{ ParameterName = "TYPE", Value = claim.Type, OracleDbType = OracleDbType.Clob },
            };

            return _database.Execute(commandText, parameters);
        }

        /// <summary>
        /// Borra una notificación para un usuario
        /// </summary>
        /// <param name="user">Notificación</param>
        /// <param name="claim">ID usuario</param>
        /// <returns></returns>
        public int Delete(IdentityUser user, Claim claim)
        {
            const string commandText = @"DELETE FROM CB_USERCLAIMS WHERE USERID = :USERID AND @CLAIMVALUE = :VALUE AND CLAIMTYPE = :TYPE";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter{ ParameterName = "USERID", Value = user.Id, OracleDbType = OracleDbType.Varchar2 },
                new OracleParameter{ ParameterName = "VALUE", Value = claim.Value, OracleDbType = OracleDbType.Clob },
                new OracleParameter{ ParameterName = "TYPE", Value = claim.Type, OracleDbType = OracleDbType.Clob },
            };

            return _database.Execute(commandText, parameters);
        }
    }
}
