using Microsoft.AspNet.Identity;
using System;

namespace MDS.Identity
{
    /// <summary>
    /// Clase implementativa de ASP.NET Identity
    /// IUser interface
    /// </summary>
    public class IdentityUser : IUser
    {
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public IdentityUser()
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Constructor que recibe nombre usuario como argumento
        /// </summary>
        /// <param name="userName"></param>
        public IdentityUser(string userName) : this()
        {
            UserName = userName;
        }

        /// <summary>
        /// Id usuario
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Nombre usuario
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// Verdadero si email esta confirmado, por defecto es falso
        /// </summary>
        public virtual bool EmailConfirmed { get; set; }

        /// <summary>
        /// Contraseña usuario
        /// </summary>
        public virtual string PasswordHash { get; set; }

        /// <summary>
        /// Un valor aleatorio que se debe cambiar cada vez que las credenciales de los usuarios han cambiado (contraseña cambiada, login eliminado)
        /// </summary>
        public virtual string SecurityStamp { get; set; }

        /// <summary>
        /// Telefono usuario
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// Confirmación número telefono
        /// </summary>
        public virtual bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// Habilitación de dos factores para usuario
        /// </summary>
        public virtual bool TwoFactorEnabled { get; set; }

        /// <summary>
        /// Fecha de término de bloqueo usuario
        /// </summary>
        public virtual DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>
        /// Bloqueo de usuario
        /// </summary>
        public virtual bool LockoutEnabled { get; set; }

        /// <summary>
        /// Contador accesos fallidos
        /// </summary>
        public virtual int AccessFailedCount { get; set; }

        /// <summary>
        /// Ministerio
        /// </summary>
        public int Ministerio { get; set; }

        /// <summary>
        /// Servicio
        /// </summary>
        public int Servicio { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        public int Estado { get; set; }

        /// <summary>
        /// Perfil
        /// </summary>
        public int Perfil { get; set; }

        /// <summary>
        /// Nombre
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Gore
        /// </summary>
        public int Gore { get; set; }

        /// <summary>
        /// Perfil Gore
        /// </summary>
        public int PerfilGore { get; set; }
    }
}
