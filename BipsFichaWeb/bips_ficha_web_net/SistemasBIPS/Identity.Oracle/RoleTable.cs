using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MDS.Identity
{
    /// <summary>
    /// Clase representativa de la tabla cb_roles
    /// </summary>
    public class RoleTable
    {
        private OracleDatabase _database;

        /// <summary>
        /// Constructor que recibe db oracle como argumento
        /// </summary>
        /// <param name="database"></param>
        public RoleTable(OracleDatabase database)
        {
            _database = database;
        }

        /// <summary>
        /// Borra rol
        /// </summary>
        /// <param name="roleId">Rol ID</param>
        /// <returns></returns>
        public int Delete(string roleId)
        {
            const string commandText = @"DELETE FROM CB_ROLES WHERE ID = :ID";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter {ParameterName = "ID", Value = roleId, OracleDbType = OracleDbType.Varchar2 },
            };

            return _database.Execute(commandText, parameters);
        }

        /// <summary>
        /// Inserta un nuevo rol
        /// </summary>
        /// <param name="role">Objeto Rol</param>
        /// <returns></returns>
        public int Insert(IdentityRole role)
        {
            const string commandText = @"INSERT INTO CB_ROLES (ID, NAME) VALUES (:ID, :NAME)";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter {ParameterName = "ID", Value = role.Id, OracleDbType = OracleDbType.Varchar2 },
                new OracleParameter {ParameterName = "NAME", Value = role.Name, OracleDbType = OracleDbType.Varchar2 },
            };

            return _database.Execute(commandText, parameters);
        }

        /// <summary>
        /// Obtiene nombre de rol
        /// </summary>
        /// <param name="roleId">Rol ID</param>
        /// <returns>Nombre rol</returns>
        public string GetRoleName(string roleId)
        {
            const string commandText = @"SELECT NAME FROM CB_ROLES WHERE ID = :ID";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter {ParameterName = "ID", Value = roleId, OracleDbType = OracleDbType.Varchar2 },
            };

            return _database.GetStrValue(commandText, parameters);
        }

        /// <summary>
        /// Obtiene rol id
        /// </summary>
        /// <param name="roleName">Nombre rol</param>
        /// <returns>ID rol</returns>
        public string GetRoleId(string roleName)
        {
            const string commandText = @"SELECT ID FROM CB_ROLES WHERE NAME = :NAME";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter {ParameterName = "NAME", Value = roleName, OracleDbType = OracleDbType.Varchar2 },
            };

            var result = _database.QueryValue(commandText, parameters);
            return result != null ? Convert.ToString(result) : null;
        }

        /// <summary>
        /// Obtiene objeto rol mediante el nombre del rol
        /// </summary>
        /// <param name="roleId">Rol ID</param>
        /// <returns>IdentityRole</returns>
        public IdentityRole GetRoleById(string roleId)
        {
            var roleName = GetRoleName(roleId);
            IdentityRole role = null;

            if (roleName != null)
            {
                role = new IdentityRole(roleName, roleId);
            }

            return role;
        }

        /// <summary>
        /// Obtiene objeto rol mediante el nombre del rol
        /// </summary>
        /// <param name="roleName">Nombre rol</param>
        /// <returns>IdentityRole</returns>
        public IdentityRole GetRoleByName(string roleName)
        {
            var roleId = GetRoleId(roleName);
            IdentityRole role = null;

            if (roleId != null)
            {
                role = new IdentityRole(roleName, roleId);
            }

            return role;
        }

        /// <summary>
        /// Actualiza atributos de un rol
        /// </summary>
        /// <param name="role">Objeto rol</param>
        /// <returns></returns>
        public int Update(IdentityRole role)
        {
            const string commandText = @"UPDATE CB_ROLES SET NAME = :NAME WHERE ID = :ID";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter {ParameterName = "NAME", Value = role.Name, OracleDbType = OracleDbType.Varchar2 },
                new OracleParameter {ParameterName = "ID", Value = role.Id, OracleDbType = OracleDbType.Varchar2 },
            };

            return _database.Execute(commandText, parameters);
        }

        /// <summary>
        /// Obtiene totalidad de roles
        /// </summary>
        /// <returns>IdentityRole</returns>
        public IEnumerable<IdentityRole> GetRoles()
        {
            const string commandText = @"SELECT ID, NAME FROM CB_ROLES";
            var results = _database.Query(commandText, null);

            return results.Select(result => new IdentityRole
            {
                Id = string.IsNullOrEmpty(result["ID"]) ? null : result["ID"],
                Name = string.IsNullOrEmpty(result["NAME"]) ? null : result["NAME"],
            }).ToList();
        }
    }
}
