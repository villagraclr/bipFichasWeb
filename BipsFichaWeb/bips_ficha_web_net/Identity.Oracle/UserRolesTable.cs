using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Linq;

namespace MDS.Identity
{
    /// <summary>
    /// Clase representativa de tabla cb_userroles
    /// </summary>
    public class UserRolesTable
    {
        private OracleDatabase _database;

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        /// <param name="database"></param>
        public UserRolesTable(OracleDatabase database)
        {
            _database = database;
        }

        /// <summary>
        /// Obtiene lista de roles
        /// </summary>
        /// <param name="userId">ID usuario</param>
        /// <returns></returns>
        public List<string> FindByUserId(string userId)
        {
            const string commandText = @"SELECT NAME FROM CB_USERROLES, CB_ROLES WHERE USERID = :USERID AND CB_USERROLES.ROLEID = CB_ROLES.ID";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter{ ParameterName = "USERID", Value = userId, OracleDbType = OracleDbType.Varchar2 }
            };

            var rows = _database.Query(commandText, parameters);

            return rows.Select(row => row["NAME"]).ToList();
        }

        /// <summary>
        /// Borra rol para usuario específico
        /// </summary>
        /// <param name="userId">ID usuario</param>
        /// <returns></returns>
        public int Delete(string userId)
        {
            const string commandText = @"DELETE FROM CB_USERROLES WHERE USERID = :USERID";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter{ ParameterName = "USERID", Value = userId, OracleDbType = OracleDbType.Varchar2 }
            };

            return _database.Execute(commandText, parameters);
        }

        /// <summary>
        /// Inserta nuevo rol para un usuario específico
        /// </summary>
        /// <param name="user">Objeto usuario</param>
        /// <param name="roleId">ID rol</param>
        /// <returns></returns>
        public int Insert(IdentityUser user, string roleId)
        {
            const string commandText = @"INSERT INTO CB_USERROLES (USERID, ROLEID) VALUES (:USERID, :ROLEID)";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter{ ParameterName = "USERID", Value = user.Id, OracleDbType = OracleDbType.Varchar2},
                new OracleParameter{ ParameterName = "ROLEID", Value = roleId, OracleDbType = OracleDbType.Varchar2},
            };

            return _database.Execute(commandText, parameters);
        }
    }
}
