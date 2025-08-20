using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDS.Identity
{
    /// <summary>
    /// Clase implementativa de ASP.NET Identity role store interfaces
    /// </summary>
    /// <typeparam name="TRole"></typeparam>
    public class RoleStore<TRole> : IQueryableRoleStore<TRole> where TRole : IdentityRole
    {
        private RoleTable roleTable;
        public OracleDatabase Database { get; private set; }

        /// <summary>
        /// Retorna todos los roles definidos
        /// </summary>
        public IQueryable<TRole> Roles
        {
            get
            {
                var x = roleTable.GetRoles() as List<TRole>;
                return x != null ? x.AsQueryable() : null;
            }
        }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public RoleStore()
        {
            new RoleStore<TRole>(new OracleDatabase());
        }

        /// <summary>
        /// Constructor que recibe db oracle como argumento
        /// </summary>
        /// <param name="database"></param>
        public RoleStore(OracleDatabase database)
        {
            Database = database;
            roleTable = new RoleTable(database);
        }

        /// <summary>
        /// Registra nuevo rol
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task CreateAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            roleTable.Insert(role);

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Borra rol
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task DeleteAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            roleTable.Delete(role.Id);

            return Task.FromResult<Object>(null);
        }

        /// <summary>
        /// Busca rol por ID
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public Task<TRole> FindByIdAsync(string roleId)
        {
            var result = roleTable.GetRoleById(roleId) as TRole;

            return Task.FromResult(result);
        }

        /// <summary>
        /// Busca rol por nombre
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public Task<TRole> FindByNameAsync(string roleName)
        {
            var result = roleTable.GetRoleByName(roleName) as TRole;

            return Task.FromResult(result);
        }

        /// <summary>
        /// Actualiza rol
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task UpdateAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            roleTable.Update(role);

            return Task.FromResult<Object>(null);
        }

        public void Dispose()
        {
            if (Database == null) return;

            Database.Dispose();
            Database = null;
        }
    }
}
