using Microsoft.AspNet.Identity;
using System;

namespace MDS.Identity
{
    /// <summary>
    /// Clase implementativa de ASP.NET Identity
    /// IRole interface
    /// </summary>
    public class IdentityRole : IRole
    {
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public IdentityRole()
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Constructor que recibe nombre rol como argumento
        /// </summary>
        /// <param name="name"></param>
        public IdentityRole(string name) : this()
        {
            Name = name;
        }

        /// <summary>
        /// Constructor que recibe nombre rol e id rol como argumento
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        public IdentityRole(string name, string id)
        {
            Name = name;
            Id = id;
        }

        /// <summary>
        /// ID rol
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Nombre rol
        /// </summary>
        public string Name { get; set; }
    }
}
