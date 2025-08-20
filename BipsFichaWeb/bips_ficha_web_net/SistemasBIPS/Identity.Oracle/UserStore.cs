using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MDS.Identity
{
    public class UserStore<TUser> : IUserLoginStore<TUser>,
        IUserClaimStore<TUser>,
        IUserRoleStore<TUser>,
        IUserPasswordStore<TUser>,
        IUserSecurityStampStore<TUser>,
        IQueryableUserStore<TUser>,
        IUserEmailStore<TUser>,
        IUserPhoneNumberStore<TUser>,
        IUserTwoFactorStore<TUser, string>,
        IUserLockoutStore<TUser, string>,
        IUserStore<TUser>
        where TUser : IdentityUser
    {
        private UserTable<TUser> userTable;
        private RoleTable roleTable;
        private UserRolesTable userRolesTable;
        private UserClaimsTable userClaimsTable;
        private UserLoginsTable userLoginsTable;
        public OracleDatabase Database { get; private set; }

        /// <summary>
        /// Obtiene todos los usuarios creados
        /// </summary>
        public IQueryable<TUser> Users
        {
            get
            {
                // If you have some performance issues, then you can implement the IQueryable.
                var x = userTable.GetUsers() as List<TUser>;
                return x != null ? x.AsQueryable() : null;
            }
        }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public UserStore()
        {
            new UserStore<TUser>(new OracleDatabase());
        }

        /// <summary>
        /// Constructor que toma una BD oracle como argumento
        /// </summary>
        /// <param name="database"></param>
        public UserStore(OracleDatabase database)
        {
            Database = database;
            userTable = new UserTable<TUser>(database);
            roleTable = new RoleTable(database);
            userRolesTable = new UserRolesTable(database);
            userClaimsTable = new UserClaimsTable(database);
            userLoginsTable = new UserLoginsTable(database);
        }

        /// <summary>
        /// Inserta nuevo TUser en la tabla users
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task CreateAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            userTable.Insert(user);
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Obtiene una instancia TUser basado en una consulta por ID usuario
        /// </summary>
        /// <param name="userId">ID usuario</param>
        /// <returns></returns>
        public Task<TUser> FindByIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("Null or empty argument: userId");
            }
            var result = userTable.GetUserById(userId);
            return Task.FromResult(result);
        }

        /// <summary>
        /// Obtiene una instancia TUser basado en una consulta por el nombre usuario
        /// </summary>
        /// <param name="userName">Nombre usuario</param>
        /// <returns></returns>
        public Task<TUser> FindByNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Null or empty argument: userName");
            }
            var result = userTable.GetUserByName(userName);
            if (result != null && result.Count == 1)
            {
                return Task.FromResult(result[0]);
            }
            return Task.FromResult<TUser>(null);
        }

        /// <summary>
        /// Actualiza tabla user con los valores de la instancia TUser
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task UpdateAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            userTable.Update(user);
            return Task.FromResult<object>(null);
        }

        public void Dispose()
        {
            if (Database == null) return;

            Database.Dispose();
            Database = null;
        }        

        /// <summary>
        /// Inserta una notificación a la tabla UserClaims para un usuario específico
        /// </summary>
        /// <param name="user"></param>
        /// <param name="claim"></param>
        /// <returns></returns>
        public Task AddClaimAsync(TUser user, Claim claim)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (claim == null)
            {
                throw new ArgumentNullException("user");
            }
            userClaimsTable.Insert(claim, user.Id);
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Obtiene todas las notificaciones de un usuario
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<IList<Claim>> GetClaimsAsync(TUser user)
        {
            var identity = userClaimsTable.FindByUserId(user.Id);
            return Task.FromResult<IList<Claim>>(identity.Claims.ToList());
        }

        /// <summary>
        /// Borra una notificación para un usuario específico
        /// </summary>
        /// <param name="user"></param>
        /// <param name="claim"></param>
        /// <returns></returns>
        public Task RemoveClaimAsync(TUser user, Claim claim)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }
            userClaimsTable.Delete(user, claim);
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Inserta un registro de logeo en la tabla UserLogins para un usuario específico
        /// </summary>
        /// <param name="user"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        public Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (login == null)
            {
                throw new ArgumentNullException("login");
            }
            userLoginsTable.Insert(user, login);
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Obtiene una instancia TUser
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public Task<TUser> FindAsync(UserLoginInfo login)
        {
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }
            var userId = userLoginsTable.FindUserIdByLogin(login);
            if (userId == null) return Task.FromResult<TUser>(null);

            var user = userTable.GetUserById(userId);
            return Task.FromResult(user);
        }

        /// <summary>
        /// Obtiene una lista de información de logeo
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            var logins = userLoginsTable.FindByUserId(user.Id);
            return Task.FromResult<IList<UserLoginInfo>>(logins);
        }

        /// <summary>
        /// Borra un logeo de la tabla UserLogins
        /// </summary>
        /// <param name="user"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        public Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (login == null)
            {
                throw new ArgumentNullException("login");
            }
            userLoginsTable.Delete(user, login);
            return Task.FromResult<Object>(null);
        }

        /// <summary>
        /// Inserta un registro en la tabla UserRoles
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public Task AddToRoleAsync(TUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Argument cannot be null or empty: roleName.");
            }
            var roleId = roleTable.GetRoleId(roleName);

            if (!string.IsNullOrEmpty(roleId))
            {
                userRolesTable.Insert(user, roleId);
            }
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Obtiene los roles para un usuario específico
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<IList<string>> GetRolesAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var roles = userRolesTable.FindByUserId(user.Id);
            {
                if (roles != null)
                {
                    return Task.FromResult<IList<string>>(roles);
                }
            }
            return Task.FromResult<IList<string>>(null);
        }

        /// <summary>
        /// Verifica si un usuario pertenece a un rol específico
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task<bool> IsInRoleAsync(TUser user, string role)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrEmpty(role))
            {
                throw new ArgumentNullException("role");
            }

            var roles = userRolesTable.FindByUserId(user.Id);
            {
                if (roles != null && roles.Contains(role))
                {
                    return Task.FromResult(true);
                }
            }
            return Task.FromResult(false);
        }

        /// <summary>
        /// Borra un usuario a partir de un rol (no implementado)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task RemoveFromRoleAsync(TUser user, string role)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Borra un usuario
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task DeleteAsync(TUser user)
        {
            if (user != null)
            {
                userTable.Delete(user);
            }
            return Task.FromResult<Object>(null);
        }

        /// <summary>
        /// Obtiene la contraseña para un usuario específico
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<string> GetPasswordHashAsync(TUser user)
        {
            var passwordHash = userTable.GetPasswordHash(user.Id);
            return Task.FromResult(passwordHash);
        }

        /// <summary>
        /// Verifica la contraseña de un usuario
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> HasPasswordAsync(TUser user)
        {
            var hasPassword = !string.IsNullOrEmpty(userTable.GetPasswordHash(user.Id));
            return Task.FromResult(Boolean.Parse(hasPassword.ToString()));
        }

        /// <summary>
        /// Setea la contraseña para un usuario específico
        /// </summary>
        /// <param name="user"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult<Object>(null);
        }

        /// <summary>
        /// Setea campo security stamp
        /// </summary>
        /// <param name="user"></param>
        /// <param name="stamp"></param>
        /// <returns></returns>
        public Task SetSecurityStampAsync(TUser user, string stamp)
        {
            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        /// <summary>
        /// Obtiene campo security stamp
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<string> GetSecurityStampAsync(TUser user)
        {
            return Task.FromResult(user.SecurityStamp);
        }

        /// <summary>
        /// Setea el email para un usuario específico
        /// </summary>
        /// <param name="user"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task SetEmailAsync(TUser user, string email)
        {
            user.Email = email;
            userTable.Update(user);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Obtiene email para un usuario específico
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<string> GetEmailAsync(TUser user)
        {
            return Task.FromResult(user.Email);
        }

        /// <summary>
        /// Obtiene si email fue confirmado
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        /// <summary>
        /// Setea confirmación de email
        /// </summary>
        /// <param name="user"></param>
        /// <param name="confirmed"></param>
        /// <returns></returns>
        public Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            user.EmailConfirmed = confirmed;
            user.LockoutEnabled = false;
            userTable.Update(user);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Obtiene usuario mediante email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<TUser> FindByEmailAsync(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("email");
            }
            var result = userTable.GetUserByEmail(email).FirstOrDefault();
            return Task.FromResult(result);
        }

        /// <summary>
        /// Setea número de telefono para un usuario específico
        /// </summary>
        /// <param name="user"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public Task SetPhoneNumberAsync(TUser user, string phoneNumber)
        {
            user.PhoneNumber = phoneNumber;
            userTable.Update(user);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Obtiene número de telefono para un usuario específico
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<string> GetPhoneNumberAsync(TUser user)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        /// <summary>
        /// Obtiene confirmación del número de telefono para un usuario específico
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> GetPhoneNumberConfirmedAsync(TUser user)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        /// <summary>
        /// Setea confirmación del número de telefono para un usuario específico
        /// </summary>
        /// <param name="user"></param>
        /// <param name="confirmed"></param>
        /// <returns></returns>
        public Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
        {
            user.PhoneNumberConfirmed = confirmed;
            userTable.Update(user);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Setea autentificación two factor para un usuario específico
        /// </summary>
        /// <param name="user"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        {
            user.TwoFactorEnabled = enabled;
            userTable.Update(user);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Obtiene si autentificación two factor esta habilitada para un usuario específico
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> GetTwoFactorEnabledAsync(TUser user)
        {
            return Task.FromResult(user.TwoFactorEnabled);
        }

        /// <summary>
        /// Obtiene campo lockoutenddate
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        {
            return
                Task.FromResult(user.LockoutEndDateUtc.HasValue
                    ? new DateTimeOffset(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc))
                    : new DateTimeOffset());
        }

        /// <summary>
        /// Setea campo lockoutenddate
        /// </summary>
        /// <param name="user"></param>
        /// <param name="lockoutEnd"></param>
        /// <returns></returns>
        public Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        {
            user.LockoutEndDateUtc = lockoutEnd.LocalDateTime;
            userTable.Update(user);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Incrementa el campo acceso fallido para un usuario específico
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<int> IncrementAccessFailedCountAsync(TUser user)
        {
            user.AccessFailedCount++;
            userTable.Update(user);
            return Task.FromResult(user.AccessFailedCount);
        }

        /// <summary>
        /// Resetea accesos fallidos
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task ResetAccessFailedCountAsync(TUser user)
        {
            user.AccessFailedCount = 0;
            userTable.Update(user);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Obtiene accesos fallidos
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<int> GetAccessFailedCountAsync(TUser user)
        {
            return Task.FromResult(user.AccessFailedCount);
        }

        /// <summary>
        /// Obtiene si campo lockout esta activado para un usuario específico
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> GetLockoutEnabledAsync(TUser user)
        {
            return Task.FromResult(user.LockoutEnabled);
        }

        /// <summary>
        /// Setea campo lockout activo para un usuario específico
        /// </summary>
        /// <param name="user"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public Task SetLockoutEnabledAsync(TUser user, bool enabled)
        {
            user.LockoutEnabled = enabled;
            userTable.Update(user);
            return Task.FromResult(0);
        }
    }
}
