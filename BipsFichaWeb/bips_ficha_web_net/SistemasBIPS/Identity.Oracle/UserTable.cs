using Oracle.ManagedDataAccess.Client;

using System;
using System.Collections.Generic;

namespace MDS.Identity
{
    public class UserTable<TUser> where TUser : IdentityUser
    {
        private OracleDatabase _database;

        /// <summary>
        /// Constructor que toma una BD oracle como argumento
        /// </summary>
        /// <param name="database"></param>
        public UserTable(OracleDatabase database)
        {
            _database = database;
        }

        /// <summary>
        /// Obtiene nombre usuario mediante el ID usuario
        /// </summary>
        /// <param name="userId">ID usuario</param>
        /// <returns></returns>
        public string GetUserName(string userId)
        {
            const string commandText = @"SELECT NAME FROM CB_USERS WHERE ID = :USERID";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter{ ParameterName = "USERID", Value = userId, OracleDbType = OracleDbType.Varchar2 }
            };
            return _database.GetStrValue(commandText, parameters);
        }

        /// <summary>
        /// Obtiene un ID usuario mediante el nombre usuario
        /// </summary>
        /// <param name="userName">Nombre usuario</param>
        /// <returns></returns>
        public string GetUserId(string userName)
        {
            const string commandText = @"SELECT ID FROM CB_USERS WHERE USERNAME = :NAME";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter{ ParameterName = "NAME", Value = userName, OracleDbType = OracleDbType.Varchar2 }
            };
            return _database.GetStrValue(commandText, parameters);
        }

        /// <summary>
        /// Obtiene un objeto TUser median el ID usuario
        /// </summary>
        /// <param name="userId">ID usuario</param>
        /// <returns></returns>
        public TUser GetUserById(string userId)
        {
            TUser user;
            const string commandText = @"SELECT * FROM CB_USERS WHERE ID = :USERID";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter{ ParameterName = "USERID", Value = userId, OracleDbType = OracleDbType.Varchar2 }
            };

            var rows = _database.Query(commandText, parameters);
            if (rows == null || rows.Count != 1) return null;

            var row = rows[0];
            user = (TUser)Activator.CreateInstance(typeof(TUser));
            user.Id = row["ID"];
            user.UserName = row["USERNAME"];
            user.PasswordHash = string.IsNullOrEmpty(row["PASSWORDHASH"]) ? null : row["PASSWORDHASH"];
            user.SecurityStamp = string.IsNullOrEmpty(row["SECURITYSTAMP"]) ? null : row["SECURITYSTAMP"];
            user.Email = string.IsNullOrEmpty(row["EMAIL"]) ? null : row["EMAIL"];
            user.EmailConfirmed = (row["EMAILCONFIRMED"] == "1");
            user.PhoneNumber = string.IsNullOrEmpty(row["PHONENUMBER"]) ? null : row["PHONENUMBER"];
            user.PhoneNumberConfirmed = (row["PHONENUMBERCONFIRMED"] == "1");
            user.LockoutEnabled = (row["LOCKOUTENABLED"] == "1");
            user.LockoutEndDateUtc = string.IsNullOrEmpty(row["LOCKOUTENDDATEUTC"]) ? DateTime.Now : DateTime.Parse(row["LOCKOUTENDDATEUTC"]);
            user.AccessFailedCount = string.IsNullOrEmpty(row["ACCESSFAILEDCOUNT"]) ? 0 : int.Parse(row["ACCESSFAILEDCOUNT"]);
            user.TwoFactorEnabled = (row["TWOFACTORENABLED"] == "1");            
            user.Ministerio = string.IsNullOrEmpty(row["MINISTERIO"]) ? 0 : int.Parse(row["MINISTERIO"]);
            user.Servicio = string.IsNullOrEmpty(row["SERVICIO"]) ? 0 : int.Parse(row["SERVICIO"]);
            user.Estado = string.IsNullOrEmpty(row["ESTADO"]) ? 0 : int.Parse(row["ESTADO"]);
            user.Perfil = string.IsNullOrEmpty(row["PERFIL"]) ? 0 : int.Parse(row["PERFIL"]);
            user.Nombre = string.IsNullOrEmpty(row["NOMBRE"]) ? null : row["NOMBRE"];

            return user;
        }

        /// <summary>
        /// Obtiene una lista de instancias TUser mediante el nombre usuario
        /// </summary>
        /// <param name="userName">Nombre usuario</param>
        /// <returns></returns>
        public List<TUser> GetUserByName(string userName)
        {
            var users = new List<TUser>();
            const string commandText = @"SELECT * FROM CB_USERS WHERE USERNAME = :NAME";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter{ ParameterName = "NAME", Value = userName, OracleDbType = OracleDbType.Varchar2}
            };

            var rows = _database.Query(commandText, parameters);
            foreach (var row in rows)
            {
                var user = (TUser)Activator.CreateInstance(typeof(TUser));
                user.Id = row["ID"];
                user.UserName = row["USERNAME"];
                user.PasswordHash = string.IsNullOrEmpty(row["PASSWORDHASH"]) ? null : row["PASSWORDHASH"];
                user.SecurityStamp = string.IsNullOrEmpty(row["SECURITYSTAMP"]) ? null : row["SECURITYSTAMP"];
                user.Email = string.IsNullOrEmpty(row["EMAIL"]) ? null : row["EMAIL"];
                user.EmailConfirmed = (row["EMAILCONFIRMED"] == "1");
                user.PhoneNumber = string.IsNullOrEmpty(row["PHONENUMBER"]) ? null : row["PHONENUMBER"];
                user.PhoneNumberConfirmed = (row["PHONENUMBERCONFIRMED"] == "1");
                user.LockoutEnabled = (row["LOCKOUTENABLED"] == "1");
                user.LockoutEndDateUtc = string.IsNullOrEmpty(row["LOCKOUTENDDATEUTC"]) ? DateTime.Now : DateTime.Parse(row["LOCKOUTENDDATEUTC"]);
                user.AccessFailedCount = string.IsNullOrEmpty(row["ACCESSFAILEDCOUNT"]) ? 0 : int.Parse(row["ACCESSFAILEDCOUNT"]);
                user.TwoFactorEnabled = (row["TWOFACTORENABLED"] == "1");
                user.Ministerio = string.IsNullOrEmpty(row["MINISTERIO"]) ? 0 : int.Parse(row["MINISTERIO"]);
                user.Servicio = string.IsNullOrEmpty(row["SERVICIO"]) ? 0 : int.Parse(row["SERVICIO"]);
                user.Estado = string.IsNullOrEmpty(row["ESTADO"]) ? 0 : int.Parse(row["ESTADO"]);
                user.Perfil = string.IsNullOrEmpty(row["PERFIL"]) ? 0 : int.Parse(row["PERFIL"]);
                user.Nombre = string.IsNullOrEmpty(row["NOMBRE"]) ? null : row["NOMBRE"];
                user.Gore = string.IsNullOrEmpty(row["GORE"]) ? 0 : int.Parse(row["GORE"]);
                user.PerfilGore = string.IsNullOrEmpty(row["PERFIL_GORE"]) ? 0 : int.Parse(row["PERFIL_GORE"]);
                users.Add(user);
            }

            return users;
        }

        /// <summary>
        /// Obtiene usuarios mediante el email (no implementado)
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns></returns>
        public List<TUser> GetUserByEmail(string email)
        {
            var users = new List<TUser>();
            // throw new NotImplementedException();
            return users;
        }

        /// <summary>
        /// Obtiene la contraseña mediante el ID usuario
        /// </summary>
        /// <param name="userId">ID usuario</param>
        /// <returns></returns>
        public string GetPasswordHash(string userId)
        {
            const string commandText = @"SELECT PASSWORDHASH FROM CB_USERS WHERE ID = :USERID";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter{ ParameterName = "USERID", Value = userId, OracleDbType = OracleDbType.Varchar2 }
            };

            var passHash = _database.GetStrValue(commandText, parameters);
            return string.IsNullOrEmpty(passHash) ? null : passHash;
        }

        /// <summary>
        /// Setea contraseña para un usuario específico
        /// </summary>
        /// <param name="userId">ID usuario</param>
        /// <param name="passwordHash">Contraseña</param>
        /// <returns></returns>
        public int SetPasswordHash(string userId, string passwordHash)
        {
            const string commandText = @"UPDATE CB_USERS SET PASSWORDHASH = :PWHASH WHERE ID = :USERID";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter{ ParameterName = "PWHASH", Value = passwordHash, OracleDbType = OracleDbType.Clob },
                new OracleParameter{ ParameterName = "USERID", Value = userId, OracleDbType = OracleDbType.Varchar2 }
            };

            return _database.Execute(commandText, parameters);
        }

        /// <summary>
        /// Obtiene el campo security stamp
        /// </summary>
        /// <param name="userId">ID usuario</param>
        /// <returns></returns>
        public string GetSecurityStamp(string userId)
        {
            const string commandText = @"SELECT SECURITYSTAMP FROM CB_USERS WHERE ID = :USERID";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter{ ParameterName = "USERID", Value =  userId, OracleDbType = OracleDbType.Varchar2 }
            };

            return _database.GetStrValue(commandText, parameters);
        }

        /// <summary>
        /// Inserta un nuevo usuario en la tabla users
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Insert(TUser user)
        {
            const string commandText = @"INSERT INTO CB_USERS (USERNAME, ID, PASSWORDHASH, SECURITYSTAMP,EMAIL,EMAILCONFIRMED,PHONENUMBER,PHONENUMBERCONFIRMED, ACCESSFAILEDCOUNT,LOCKOUTENABLED,LOCKOUTENDDATEUTC,TWOFACTORENABLED, MINISTERIO, SERVICIO, ESTADO, PERFIL, NOMBRE, GORE, PERFIL_GORE)
                VALUES (:NAME, :USERID, :PWHASH, :SECSTAMP,:EMAIL,:EMAILCONFIRMED,:PHONENUMBER,:PHONENUMBERCONFIRMED,:ACCESSFAILEDCOUNT,:LOCKOUTENABLED,:LOCKOUTENDDATEUTC,:TWOFACTORENABLED, :MINISTERIO, :SERVICIO, :ESTADO, :PERFIL, :NOMBRE, :GORE, :PERFIL_GORE)";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter{ ParameterName = "NAME", Value = user.UserName, OracleDbType = OracleDbType.Varchar2 },
                new OracleParameter{ ParameterName = "USERID", Value = user.Id, OracleDbType = OracleDbType.Varchar2 },
                new OracleParameter{ ParameterName = "PWHASH", Value = user.PasswordHash, OracleDbType = OracleDbType.Clob },
                new OracleParameter{ ParameterName = "SECSTAMP", Value = user.SecurityStamp, OracleDbType = OracleDbType.Clob },
                new OracleParameter{ ParameterName = "EMAIL", Value = user.Email, OracleDbType = OracleDbType.Varchar2 },
                new OracleParameter{ ParameterName = "EMAILCONFIRMED", Value = user.EmailConfirmed.ToDecimal(), OracleDbType = OracleDbType.Decimal },
                new OracleParameter{ ParameterName = "PHONENUMBER", Value = user.PhoneNumber, OracleDbType = OracleDbType.Clob },
                new OracleParameter{ ParameterName = "PHONENUMBERCONFIRMED", Value = user.PhoneNumberConfirmed.ToDecimal(), OracleDbType = OracleDbType.Decimal },
                new OracleParameter{ ParameterName = "ACCESSFAILEDCOUNT", Value = user.AccessFailedCount, OracleDbType = OracleDbType.Decimal },
                new OracleParameter{ ParameterName = "LOCKOUTENABLED", Value = user.LockoutEnabled.ToDecimal(), OracleDbType = OracleDbType.Decimal },
                new OracleParameter{ ParameterName = "LOCKOUTENDDATEUTC", Value = user.LockoutEndDateUtc, OracleDbType = OracleDbType.Date },
                new OracleParameter{ ParameterName = "TWOFACTORENABLED", Value = user.TwoFactorEnabled.ToDecimal(), OracleDbType = OracleDbType.Decimal },
                new OracleParameter{ ParameterName = "MINISTERIO", Value = user.Ministerio, OracleDbType = OracleDbType.Decimal },
                new OracleParameter{ ParameterName = "SERVICIO", Value = user.Servicio, OracleDbType = OracleDbType.Decimal },
                new OracleParameter{ ParameterName = "ESTADO", Value = user.Estado, OracleDbType = OracleDbType.Decimal },
                new OracleParameter{ ParameterName = "PERFIL", Value = user.Perfil, OracleDbType = OracleDbType.Decimal },
                new OracleParameter{ ParameterName = "NOMBRE", Value = user.Nombre, OracleDbType = OracleDbType.Varchar2 },
                new OracleParameter{ ParameterName = "GORE", Value = user.Gore, OracleDbType = OracleDbType.Decimal },
                new OracleParameter{ ParameterName = "PERFIL_GORE", Value = user.PerfilGore, OracleDbType = OracleDbType.Decimal },

            };

            return _database.Execute(commandText, parameters);
        }

        /// <summary>
        /// Borra usuario de la tabla users
        /// </summary>
        /// <param name="userId">ID usuario</param>
        /// <returns></returns>
        private int Delete(string userId)
        {
            const string commandText = @"DELETE FROM CB_USERS WHERE ID = :USERID";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter {ParameterName = "USERID", Value = userId, OracleDbType = OracleDbType.Varchar2},
            };

            return _database.Execute(commandText, parameters);
        }

        /// <summary>
        /// Borra usuario de la tabla users
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Delete(TUser user)
        {
            return Delete(user.Id);
        }

        /// <summary>
        /// Actualiza usuario de la tabla users
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Update(TUser user)
        {                       
            const string commandText = @"UPDATE CB_USERS SET  
                                        NOMBRE=:NOMBRE,
                                        MINISTERIO=:MINISTERIO,
                                        SERVICIO=:SERVICIO,
                                        PERFIL=:PERFIL,
                                        SECURITYSTAMP =:SECSTAMP,
                                        EMAILCONFIRMED=:EMAILCONFIRMED,
                                        ACCESSFAILEDCOUNT=:ACCESSFAILEDCOUNT,
                                        LOCKOUTENABLED=:LOCKOUTENABLED,
                                        LOCKOUTENDDATEUTC=:LOCKOUTENDDATEUTC,
                                        PASSWORDHASH =:PWHASH
                                        WHERE ID =:USERID";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter{ ParameterName = "NOMBRE", Value = user.Nombre, OracleDbType = OracleDbType.Varchar2 },
                new OracleParameter{ ParameterName = "MINISTERIO", Value = user.Ministerio, OracleDbType = OracleDbType.Decimal },
                new OracleParameter{ ParameterName = "SERVICIO", Value = user.Servicio, OracleDbType = OracleDbType.Decimal },
                new OracleParameter{ ParameterName = "PERFIL", Value = user.Perfil, OracleDbType = OracleDbType.Decimal },
                new OracleParameter{ ParameterName = "ACCESSFAILEDCOUNT", Value = user.AccessFailedCount, OracleDbType = OracleDbType.Decimal },
                new OracleParameter{ ParameterName = "LOCKOUTENABLED", Value = user.LockoutEnabled.ToDecimal(), OracleDbType = OracleDbType.Decimal },
                new OracleParameter{ ParameterName = "LOCKOUTENDDATEUTC", Value = user.LockoutEndDateUtc, OracleDbType = OracleDbType.Date, IsNullable = true },
                new OracleParameter{ ParameterName = "EMAILCONFIRMED", Value = (!user.EmailConfirmed ? GetEmailConfirm(user.Id) : user.EmailConfirmed.ToDecimal()), OracleDbType = OracleDbType.Decimal },
                new OracleParameter{ ParameterName = "SECSTAMP", Value = (string.IsNullOrEmpty(user.SecurityStamp) ? GetSecurityStamp(user.Id) : user.SecurityStamp), OracleDbType = OracleDbType.Clob, IsNullable = true },
                new OracleParameter{ ParameterName = "PWHASH", Value = (string.IsNullOrEmpty(user.PasswordHash) ? GetPasswordHash(user.Id) : user.PasswordHash), OracleDbType = OracleDbType.Clob, IsNullable = true },
                new OracleParameter{ ParameterName = "USERID", Value = user.Id, OracleDbType = OracleDbType.Varchar2 }
            };
            return _database.Execute(commandText, parameters);
        }
       
        /// <summary>
        /// Obtiene todos los usuarios de la tabla users
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TUser> GetUsers()
        {
            var users = new List<TUser>();
            const string commandText = @"SELECT * FROM CB_USERS";

            var rows = _database.Query(commandText, null);
            foreach (var row in rows)
            {
                var user = (TUser)Activator.CreateInstance(typeof(TUser));
                user.Id = row["ID"];
                user.UserName = row["USERNAME"];
                user.PasswordHash = string.IsNullOrEmpty(row["PASSWORDHASH"]) ? null : row["PASSWORDHASH"];
                user.SecurityStamp = string.IsNullOrEmpty(row["SECURITYSTAMP"]) ? null : row["SECURITYSTAMP"];
                user.Email = string.IsNullOrEmpty(row["EMAIL"]) ? null : row["EMAIL"];
                user.EmailConfirmed = (row["EMAILCONFIRMED"] == "1");
                user.PhoneNumber = string.IsNullOrEmpty(row["PHONENUMBER"]) ? null : row["PHONENUMBER"];
                user.PhoneNumberConfirmed = (row["PHONENUMBERCONFIRMED"] == "1");
                user.LockoutEnabled = (row["LOCKOUTENABLED"] == "1");
                user.LockoutEndDateUtc = string.IsNullOrEmpty(row["LOCKOUTENDDATEUTC"]) ? DateTime.Now : DateTime.Parse(row["LOCKOUTENDDATEUTC"]);
                user.AccessFailedCount = string.IsNullOrEmpty(row["ACCESSFAILEDCOUNT"]) ? 0 : int.Parse(row["ACCESSFAILEDCOUNT"]);
                user.TwoFactorEnabled = (row["TWOFACTORENABLED"] == "1");
                users.Add(user);
            }

            return users;
        }

        /// <summary>
        /// Obtiene el perfil mediante el ID usuario
        /// </summary>
        /// <param name="userId">ID usuario</param>
        /// <returns></returns>
        public int GetPerfil(string userId)
        {
            const string commandText = @"SELECT PERFIL FROM CB_USERS WHERE ID = :USERID";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter{ ParameterName = "USERID", Value = userId, OracleDbType = OracleDbType.Varchar2 }
            };

            var perfil = _database.QueryValue(commandText, parameters);
            return int.Parse(perfil.ToString());
        }

        /// <summary>
        /// Obtiene el dato del campo emailconfirmed mediante el ID usuario
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetEmailConfirm(string userId)
        {
            const string commandText = @"SELECT EMAILCONFIRMED FROM CB_USERS WHERE ID = :USERID";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter{ ParameterName = "USERID", Value = userId, OracleDbType = OracleDbType.Varchar2 }
            };

            var emailconfirmed = _database.QueryValue(commandText, parameters);
            return (emailconfirmed == null ? 0 : int.Parse(emailconfirmed.ToString()));
        }
    }
}
